using System;
using System.Collections.Generic;
using System.IO;
using Reallusion.Import;
using UnityEditor;
using UnityEngine;

public static class ReallusionPackageUpgradeAutomation
{
    private const string MenuPath = "Ride/Reallusion/Upgrade Shader and Runtime Packages";
    private const string PackageListRelativePath = "Library/ReallusionPackageUpgradeAutomation/packages-to-import.txt";

    /// <summary>Runs the Reallusion shader/runtime package upgrade from the Unity Editor menu.</summary>
    [MenuItem(MenuPath, priority = 600)]
    public static void RunFromMenu()
    {
        try
        {
            bool didWork = RunInteractivePackageUpdate();
            Debug.Log(didWork
                ? "ReallusionPackageUpgradeAutomation: package upgrade/import actions completed."
                : "ReallusionPackageUpgradeAutomation: installed Reallusion packages are already current.");
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            EditorUtility.DisplayDialog("Reallusion Package Upgrade Failed", ex.Message, "OK");
        }
    }

    /// <summary>
    /// Batchmode entry point that asks Reallusion which packages need to be imported and writes
    /// that package list to disk for a later Unity process.
    /// </summary>
    public static void PreparePackageImportFromCommandLine()
    {
        try
        {
            PreparePackageImport();
            ExitBatchMode(0);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            ExitBatchMode(1);
        }
    }

    /// <summary>
    /// Batchmode entry point that refreshes Reallusion package state after Unity imports the queued
    /// packages, then saves the resulting settings and asset database state.
    /// </summary>
    public static void PostPackageImportFromCommandLine()
    {
        try
        {
            Debug.Log("ReallusionPackageUpgradeAutomation: running Reallusion post-import validation.");

            RefreshPackageState();
            LogPackageState();
            RLSettings.SaveRLSettingsObject(ImporterWindow.GeneralSettings);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

            Debug.Log("ReallusionPackageUpgradeAutomation: post-import validation completed.");

            ExitBatchMode(0);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
            ExitBatchMode(1);
        }
    }

    /// <summary>
    /// Executes the Reallusion GUI package actions directly for interactive editor use.
    /// </summary>
    /// <returns>
    /// True when a shader or runtime package action was performed; otherwise false.
    /// </returns>
    private static bool RunInteractivePackageUpdate()
    {
        Debug.Log("ReallusionPackageUpgradeAutomation: checking installed Reallusion shader/runtime packages.");

        ImporterWindow.SetGeneralSettings(RLSettings.FindRLSettingsObject(), false);
        RefreshPackageState();
        LogPackageState();

        bool didWork = false;

        ShaderPackageUtil.DeterminedAction shaderAction = UpdateManager.determinedShaderAction.DeterminedShaderAction;
        if (RequiresPackageAction(shaderAction))
        {
            if (shaderAction == ShaderPackageUtil.DeterminedAction.Incompatible)
                throw new InvalidOperationException(UpdateManager.determinedShaderAction.ResultString);

            LogPackageToInstall("shader", UpdateManager.latestShaderPackageManifest);
            ShaderPackageUtil.GUIPerformShaderAction(shaderAction);
            didWork = true;
        }

        ShaderPackageUtil.DeterminedAction runtimeAction = UpdateManager.determinedRuntimeAction.DeterminedShaderAction;
        if (RequiresPackageAction(runtimeAction))
        {
            if (runtimeAction == ShaderPackageUtil.DeterminedAction.Incompatible)
                throw new InvalidOperationException(UpdateManager.determinedRuntimeAction.ResultString);

            LogPackageToInstall("runtime", UpdateManager.latestRuntimePackageManifest);
            ShaderPackageUtil.GUIPerformRuntimeAction(runtimeAction);
            didWork = true;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
        return didWork;
    }

    /// <summary>
    /// Prepares any required Reallusion package upgrade actions without importing packages directly.
    /// </summary>
    /// <returns>
    /// True when at least one shader or runtime package needs to be imported; otherwise false.
    /// </returns>
    private static bool PreparePackageImport()
    {
        Debug.Log("ReallusionPackageUpgradeAutomation: checking installed Reallusion shader/runtime packages.");

        ImporterWindow.SetGeneralSettings(RLSettings.FindRLSettingsObject(), false);
        RefreshPackageState();
        LogPackageState();

        List<string> packagesToImport = new List<string>();
        bool didWork = false;

        didWork |= PrepareShaderActionIfNeeded(packagesToImport);
        didWork |= PrepareRuntimeActionIfNeeded(packagesToImport);

        if (ImporterWindow.GeneralSettings != null)
            RLSettings.SaveRLSettingsObject(ImporterWindow.GeneralSettings);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

        WritePackageList(packagesToImport);

        Debug.Log(didWork
            ? "ReallusionPackageUpgradeAutomation: prepared " + packagesToImport.Count + " package import(s)."
            : "ReallusionPackageUpgradeAutomation: installed Reallusion packages are already current.");

        return didWork;
    }

    /// <summary>
    /// Checks Reallusion's shader package action and queues the matching shader unitypackage when
    /// the active render pipeline requires an update.
    /// </summary>
    /// <param name="packagesToImport">List that receives the resolved unitypackage path.</param>
    /// <returns>
    /// True when a shader package action was prepared; otherwise false.
    /// </returns>
    private static bool PrepareShaderActionIfNeeded(List<string> packagesToImport)
    {
        ShaderPackageUtil.DeterminedAction action = UpdateManager.determinedShaderAction.DeterminedShaderAction;
        if (!RequiresPackageAction(action))
            return false;

        if (action == ShaderPackageUtil.DeterminedAction.Incompatible)
            throw new InvalidOperationException(UpdateManager.determinedShaderAction.ResultString);

        if (UpdateManager.latestShaderPackageManifest == null)
            throw new InvalidOperationException("No Reallusion shader package is available for the active render pipeline.");

        LogPackageToInstall("shader", UpdateManager.latestShaderPackageManifest);
        Debug.Log("ReallusionPackageUpgradeAutomation: preparing shader action: " + action);

        if (RequiresUninstall(action))
            ShaderPackageUtil.UnInstallShaderPackage(true);

        MarkShaderInstallPending(UpdateManager.latestShaderPackageManifest);
        packagesToImport.Add(ResolveUnityAssetOrPackagePath(UpdateManager.latestShaderPackageManifest.referenceShaderPackagePath));
        return true;
    }

    /// <summary>
    /// Checks Reallusion's runtime package action and queues the matching runtime unitypackage when
    /// the installed runtime package requires an update.
    /// </summary>
    /// <param name="packagesToImport">List that receives the resolved unitypackage path.</param>
    /// <returns>
    /// True when a runtime package action was prepared; otherwise false.
    /// </returns>
    private static bool PrepareRuntimeActionIfNeeded(List<string> packagesToImport)
    {
        ShaderPackageUtil.DeterminedAction action = UpdateManager.determinedRuntimeAction.DeterminedShaderAction;
        if (!RequiresPackageAction(action))
            return false;

        if (action == ShaderPackageUtil.DeterminedAction.Incompatible)
            throw new InvalidOperationException(UpdateManager.determinedRuntimeAction.ResultString);

        if (UpdateManager.latestRuntimePackageManifest == null)
            throw new InvalidOperationException("No Reallusion runtime package is available.");

        LogPackageToInstall("runtime", UpdateManager.latestRuntimePackageManifest);
        Debug.Log("ReallusionPackageUpgradeAutomation: preparing runtime action: " + action);

        if (RequiresUninstall(action))
            ShaderPackageUtil.UnInstallRuntimePackage(true);

        MarkRuntimeInstallPending(UpdateManager.latestRuntimePackageManifest);
        packagesToImport.Add(ResolveUnityAssetOrPackagePath(UpdateManager.latestRuntimePackageManifest.referenceShaderPackagePath));
        return true;
    }

    /// <summary>
    /// Marks the Reallusion settings object as having a shader package install in progress.
    /// </summary>
    /// <param name="manifest">Manifest for the shader package selected by Reallusion.</param>
    private static void MarkShaderInstallPending(ShaderPackageUtil.ShaderPackageManifest manifest)
    {
        if (ImporterWindow.GeneralSettings == null)
            return;

        ImporterWindow.GeneralSettings.performPostInstallationShaderCheck = true;
        ImporterWindow.GeneralSettings.postInstallShowUpdateWindow = true;
        ImporterWindow.GeneralSettings.shaderUpdateInProgress = true;
        ImporterWindow.GeneralSettings.shaderToolVersion = manifest.Version;
        ImporterWindow.GeneralSettings.InstalledShaderRevision = manifest.Revision;
        ImporterWindow.GeneralSettings.pendingShaderInstall = false;
    }

    /// <summary>
    /// Marks the Reallusion settings object as having a runtime package install in progress.
    /// </summary>
    /// <param name="manifest">Manifest for the runtime package selected by Reallusion.</param>
    private static void MarkRuntimeInstallPending(ShaderPackageUtil.ShaderPackageManifest manifest)
    {
        if (ImporterWindow.GeneralSettings == null)
            return;

        ImporterWindow.GeneralSettings.performPostInstallationRuntimeCheck = true;
        ImporterWindow.GeneralSettings.postInstallShowUpdateWindow = true;
        ImporterWindow.GeneralSettings.runtimeUpdateInProgress = true;
        ImporterWindow.GeneralSettings.runtimeToolVersion = manifest.Version;
        ImporterWindow.GeneralSettings.installedRuntimeRevision = manifest.Revision;
        ImporterWindow.GeneralSettings.pendingRuntimeInstall = false;
    }

    /// <summary>
    /// Rebuilds Reallusion's package status for the active render pipeline and validates that both
    /// shader and runtime package actions were determined.
    /// </summary>
    private static void RefreshPackageState()
    {
        ShaderPackageUtil.ShaderPackageUtilInit();
        ShaderPackageUtil.DetermineShaderAction();
        ShaderPackageUtil.DetermineRuntimeAction();
        EnsureActionWasDetermined(UpdateManager.determinedShaderAction, "shader");
        EnsureActionWasDetermined(UpdateManager.determinedRuntimeAction, "runtime");
    }

    /// <summary>
    /// Determines whether a Reallusion package action represents work that must be performed.
    /// </summary>
    /// <param name="action">Reallusion package action to inspect.</param>
    /// <returns>
    /// True when the action is not None or CurrentValid; otherwise false.
    /// </returns>
    private static bool RequiresPackageAction(ShaderPackageUtil.DeterminedAction action)
    {
        return action != ShaderPackageUtil.DeterminedAction.None &&
               action != ShaderPackageUtil.DeterminedAction.CurrentValid;
    }

    /// <summary>
    /// Determines whether the selected Reallusion package action requires removing the installed
    /// package before importing the replacement.
    /// </summary>
    /// <param name="action">Reallusion package action to inspect.</param>
    /// <returns>
    /// True when the action is one of Reallusion's uninstall/reinstall paths; otherwise false.
    /// </returns>
    private static bool RequiresUninstall(ShaderPackageUtil.DeterminedAction action)
    {
        return action == ShaderPackageUtil.DeterminedAction.Error ||
               action == ShaderPackageUtil.DeterminedAction.UninstallReinstall_optional ||
               action == ShaderPackageUtil.DeterminedAction.UninstallReinstall_force;
    }

    /// <summary>
    /// Throws when Reallusion did not produce package action rules for the requested package kind.
    /// </summary>
    /// <param name="actionRules">Action rules returned by Reallusion.</param>
    /// <param name="packageName">Human-readable package kind used in the error message.</param>
    private static void EnsureActionWasDetermined(ShaderPackageUtil.ActionRules actionRules, string packageName)
    {
        if (actionRules == null)
            throw new InvalidOperationException("Reallusion " + packageName + " package action could not be determined.");
    }

    /// <summary>
    /// Logs the active render pipeline and Reallusion shader/runtime package decision state.
    /// </summary>
    private static void LogPackageState()
    {
        Debug.Log("ReallusionPackageUpgradeAutomation: active pipeline: " +
                  UpdateManager.activePipeline + " " +
                  UpdateManager.activeVersion + " (" +
                  UpdateManager.activePipelineVersion + ")");

        Debug.Log("ReallusionPackageUpgradeAutomation: shader status: " +
                  UpdateManager.installedShaderStatus + " / " +
                  UpdateManager.shaderPackageValid + " -> " +
                  UpdateManager.determinedShaderAction.DeterminedShaderAction);

        Debug.Log("ReallusionPackageUpgradeAutomation: runtime status: " +
                  UpdateManager.installedRuntimeStatus + " / " +
                  UpdateManager.runtimePackageValid + " -> " +
                  UpdateManager.determinedRuntimeAction.DeterminedShaderAction);
    }

    /// <summary>
    /// Logs and validates the unitypackage path from a Reallusion package manifest.
    /// </summary>
    /// <param name="packageKind">Human-readable package kind used in log and error messages.</param>
    /// <param name="manifest">Manifest that points to the package Unity should import.</param>
    private static void LogPackageToInstall(string packageKind, ShaderPackageUtil.ShaderPackageManifest manifest)
    {
        string packagePath = manifest.referenceShaderPackagePath;
        string fullPackagePath = ResolveUnityAssetOrPackagePath(packagePath);

        Debug.Log("ReallusionPackageUpgradeAutomation: " + packageKind + " package asset path: " + packagePath);
        Debug.Log("ReallusionPackageUpgradeAutomation: " + packageKind + " package full path: " + fullPackagePath);

        if (!File.Exists(fullPackagePath))
            throw new FileNotFoundException("Reallusion " + packageKind + " package could not be found.", fullPackagePath);
    }

    /// <summary>
    /// Writes the resolved Reallusion unitypackage paths to the package-list file consumed by the
    /// separate import script.
    /// </summary>
    /// <param name="packagesToImport">Resolved package file paths to write.</param>
    private static void WritePackageList(List<string> packagesToImport)
    {
        string packageListPath = GetPackageListPath();
        string packageListDirectory = Path.GetDirectoryName(packageListPath);
        if (!string.IsNullOrEmpty(packageListDirectory))
            Directory.CreateDirectory(packageListDirectory);

        File.WriteAllLines(packageListPath, packagesToImport.ToArray());
        Debug.Log("ReallusionPackageUpgradeAutomation: wrote package import list: " + packageListPath);
    }

    /// <summary>
    /// Gets the absolute path to the package-list file stored under the project's Library folder.
    /// </summary>
    /// <returns>Absolute path to the queued Reallusion package import list.</returns>
    private static string GetPackageListPath() => Path.GetFullPath(Path.Combine(Application.dataPath, "..", PackageListRelativePath));

    /// <summary>
    /// Resolves a Unity asset path or package-cache asset path to a filesystem path that can be
    /// passed to Unity's package importer.
    /// </summary>
    /// <param name="assetPath">Unity asset path from a Reallusion package manifest.</param>
    /// <returns>
    /// A filesystem path for Assets or Packages paths when they can be resolved; otherwise the
    /// original path.
    /// </returns>
    private static string ResolveUnityAssetOrPackagePath(string assetPath)
    {
        if (assetPath.StartsWith("Assets/", StringComparison.OrdinalIgnoreCase))
            return assetPath.UnityAssetPathToFullPath();

        if (assetPath.StartsWith("Packages/", StringComparison.OrdinalIgnoreCase))
        {
            UnityEditor.PackageManager.PackageInfo packageInfo = UnityEditor.PackageManager.PackageInfo.FindForAssetPath(assetPath);
            if (packageInfo != null && !string.IsNullOrEmpty(packageInfo.resolvedPath))
            {
                string packageAssetPath = ("Packages/" + packageInfo.name).Replace('\\', '/').TrimEnd('/');
                string normalizedAssetPath = assetPath.Replace('\\', '/');
                string relativePath = normalizedAssetPath.Length > packageAssetPath.Length
                    ? normalizedAssetPath.Substring(packageAssetPath.Length).TrimStart('/')
                    : string.Empty;
                return Path.Combine(packageInfo.resolvedPath, relativePath);
            }
        }

        return assetPath;
    }

    /// <summary>
    /// Exits the Unity Editor only when running in batchmode.
    /// </summary>
    /// <param name="exitCode">Process exit code to return from Unity batchmode.</param>
    private static void ExitBatchMode(int exitCode)
    {
        if (Application.isBatchMode)
            EditorApplication.Exit(exitCode);
    }
}
