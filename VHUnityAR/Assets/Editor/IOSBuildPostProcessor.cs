using System;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
#if UNITY_IOS
using UnityEditor.iOS.Xcode.Extensions;
#endif
using UnityEngine;


public class IOSBuildPostProcessor : IPostprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.iOS)
        {
#if UNITY_IOS
            {
                ///////////////////////////////////////////////////////////
                // modifications to Info.plist

                //string buildString = GetBuildString();

                //Debug.Log($"IOSBuildPostProcessor.OnPostprocessBuild() - {buildString} - {report.summary.outputPath}");


                string plistPath = Path.Combine(report.summary.outputPath, "Info.plist");

                Debug.Log($"IOSBuildPostProcessor - plist path: {plistPath}");

                UnityEditor.iOS.Xcode.PlistDocument plist = new();
                plist.ReadFromFile(plistPath);
                var plistRootDict = plist.root;

                //plistRootDict.SetString("CFBundleVersion", buildString);
                plistRootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false);  // TestFlight encryption setting
                //plistRootDict.SetString("NSHealthShareUsageDescription", "User Data Reporting");  // HealthKit
                //plistRootDict.SetString("NSSpeechRecognitionUsageDescription", "Used for talking with the character");  // Mobile Speech Recognizer

                //rootDict.SetString("NSPhotoLibraryAddUsageDescription", "photo use");
                //rootDict.SetString("NSPhotoLibraryUsageDescription", "photo use");

                plist.WriteToFile(plistPath);
            }

            {
                ///////////////////////////////////////////////////////////
                // modifications to Xcode project

                //string pbxprojPath = UnityEditor.iOS.Xcode.PBXProject.GetPBXProjectPath(report.summary.outputPath);
                //UnityEditor.iOS.Xcode.PBXProject pbxProject = new();
                //pbxProject.ReadFromFile(pbxprojPath);
                //string mainTarget = pbxProject.GetUnityMainTargetGuid();  // Main
                //string unityFrameworkTarget = pbxProject.GetUnityFrameworkTargetGuid();  // Unity Framework
                //string testsTarget = pbxProject.TargetGuidByName(UnityEditor.iOS.Xcode.PBXProject.GetUnityTestTargetName());  // Unity Tests

                //pbxProject.SetBuildProperty(mainTarget, "ENABLE_BITCODE", "NO");  // needed for Azure AI Speech SDK
                //pbxProject.SetBuildProperty(unityFrameworkTarget, "ENABLE_BITCODE", "NO");  // needed for Azure AI Speech SDK
                //pbxProject.SetBuildProperty(testsTarget, "ENABLE_BITCODE", "NO");  // needed for Azure AI Speech SDK
                //pbxProject.AddFrameworkToProject(unityFrameworkTarget, "Speech.framework", true);  // Mobile Speech Recognizer

                //pbxProject.AddBuildProperty(mainTarget, "OTHER_LDFLAGS", "-ld_classic");
                //pbxProject.AddBuildProperty(unityFrameworkTarget, "OTHER_LDFLAGS", "-ld_classic");


                // Share Sheet
                //string mainTargetEntitlement = GetEntitlementFileOrCreate(pbxProject, report.summary.outputPath, mainTarget, Application.productName);
                //var mainTargetCapabilityManager = new UnityEditor.iOS.Xcode.ProjectCapabilityManager(pbxprojPath, mainTargetEntitlement, null, mainTarget);
                //mainTargetCapabilityManager.AddAppGroups(new string [] { "group.com.DayBreak.group" });
                //mainTargetCapabilityManager.WriteToFile();


                //string shareSheetTarget = pbxProject.AddTarget("ShareSheet", "appex", "com.apple.product-type.app-extension");

                //Directory.CreateDirectory(report.summary.outputPath + "/ShareSheet/Base.lproj");
                //File.Copy("ShareSheet/Info.plist", report.summary.outputPath + "/ShareSheet/Info.plist");
                //File.Copy("ShareSheet/ShareSheet.entitlements", report.summary.outputPath + "/ShareSheet/ShareSheet.entitlements");
                //File.Copy("ShareSheet/ShareViewController.h", report.summary.outputPath + "/ShareSheet/ShareViewController.h");
                //File.Copy("ShareSheet/ShareViewController.m", report.summary.outputPath + "/ShareSheet/ShareViewController.m");
                //File.Copy("ShareSheet/Base.lproj/MainInterface.storyboard", report.summary.outputPath + "/ShareSheet/Base.lproj/MainInterface.storyboard");

                //string shareSheetTarget = pbxProject.AddAppExtension(unityFrameworkTarget, "ShareSheet", "edu.usc.ict.daybreak.sharesheet", "ShareSheet/Info.plist");

                //pbxProject.AddFileToBuild(shareSheetTarget, pbxProject.AddFile(report.summary.outputPath + "/ShareSheet/Info.plist", "ShareSheet/Info.plist"));
                //pbxProject.AddFileToBuild(shareSheetTarget, pbxProject.AddFile(report.summary.outputPath + "/ShareSheet/ShareSheet.entitlements", "ShareSheet/ShareSheet.entitlements"));
                //pbxProject.AddFileToBuild(shareSheetTarget, pbxProject.AddFile(report.summary.outputPath + "/ShareSheet/ShareViewController.h", "ShareSheet/ShareViewController.h"));
                //pbxProject.AddFileToBuild(shareSheetTarget, pbxProject.AddFile(report.summary.outputPath + "/ShareSheet/ShareViewController.m", "ShareSheet/ShareViewController.m"));
                //pbxProject.AddFileToBuild(shareSheetTarget, pbxProject.AddFile(report.summary.outputPath + "/ShareSheet/Base.lproj/MainInterface.storyboard", "ShareSheet/MainInterface.storyboard"));

                //pbxProject.WriteToFile(pbxprojPath);


                //string shareSheetTargetEntitlement = GetEntitlementFileOrCreate(pbxProject, report.summary.outputPath, shareSheetTarget, "ShareSheet/ShareSheet");
                //var shareSheetTargetCapabilityManager = new UnityEditor.iOS.Xcode.ProjectCapabilityManager(pbxprojPath, shareSheetTargetEntitlement, null, shareSheetTarget);
                //shareSheetTargetCapabilityManager.AddAppGroups(new string [] { "group.com.DayBreak.group" });
                //shareSheetTargetCapabilityManager.WriteToFile();


                //pbxProject.AddFrameworkToProject(shareSheetTarget, "Social.framework", false);


                //pbxProject.WriteToFile(pbxprojPath);
            }

            // refs:
            // https://support.unity.com/hc/en-us/articles/207942813-How-can-I-disable-Bitcode-support-
            // https://support.unity.com/hc/en-us/articles/209933103
            // https://www.kokosoft.pl/forums/topic/ios-unity-2019-3-0b11-pbxproject-getunitytargetname-is-deprecated/
            // https://gist.github.com/TiborUdvari/4679d636b17ddff0d83065eefa399c04
            // http://forum.unity3d.com/threads/what-is-bundle-version-and-short-bundle-version.314819/
            // http://docs.unity3d.com/ScriptReference/iOS.Xcode.PBXProject.html
            // https://developer.apple.com/library/ios/documentation/General/Reference/InfoPlistKeyReference/Articles/CoreFoundationKeys.html#//apple_ref/doc/uid/20001431-102364
#endif
        }
    }

#if false
    internal static string getAndroidBuildNumberString()
    {
        // find Android build number in the project
        int buildNumber = PlayerSettings.Android.bundleVersionCode;
        string buildString = buildNumber.ToString ();
        Debug.Log ("XCMAPI: reading PlayerSettings.Android.bundleVersionCode: " + buildString);
 
        return buildString;
    }
#endif

#if false
    static VHAssets.VHUtils.UnityCloudBuildManifest GetBuildInfo()
    {
        // Taken from DebugInfo.
        // TODO - consolidate this code into VHUtils


        // try and get version info from the resource file generated by Unity Cloud build server.
        // ref: https://build.cloud.unity3d.com/support/guides/manifest/
        // if that doesn't exist, try and get version info from svn.
        // otherwise, fill with some default info

        VHAssets.VHUtils.UnityCloudBuildManifest unityCloudBuildManifest = null;

        string versionText = "";

        var unityCloudBuildManifestText = (TextAsset)Resources.Load("UnityCloudBuildManifest.json");
        if (unityCloudBuildManifestText != null)
        {
            try
            {
                unityCloudBuildManifest = JsonUtility.FromJson<VHAssets.VHUtils.UnityCloudBuildManifest>(unityCloudBuildManifestText.text);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        else
        {
            // either built locally (not Unity Cloud), or run from editor via svn sandbox

            // check to see if folder is a svn working copy
            versionText = VHAssets.VHUtils.GetSVNRevision(Application.dataPath + "/../../");
        }

        if (unityCloudBuildManifest == null)
        {
            unityCloudBuildManifest = new VHAssets.VHUtils.UnityCloudBuildManifest();
            unityCloudBuildManifest.scmCommitId = versionText;
            unityCloudBuildManifest.unityVersion = Application.unityVersion;
        }

        return unityCloudBuildManifest;
    }

    static string GetBuildString()
    {
        var unityCloudBuildManifest = GetBuildInfo();
        string buildString = unityCloudBuildManifest.scmCommitId;
        buildString = buildString.Replace(":", ".");
        buildString = System.Text.RegularExpressions.Regex.Replace(buildString, "[A-Za-z ]", "");
        if (string.IsNullOrEmpty(buildString))
            buildString = "0";

        return buildString;
    }
#endif

#if false
#if UNITY_IOS
    static string GetEntitlementFileOrCreate(UnityEditor.iOS.Xcode.PBXProject pbxProject, string reportSummaryOutputPath, string targetGuid, string entitlementFileNameIfNotFound)
    {
        string plistFilePath = pbxProject.GetEntitlementFilePathForTarget(targetGuid);
        string plistFileName;

        if (string.IsNullOrEmpty(plistFilePath))
        {
            // create the entitlement file
            plistFileName = $"{entitlementFileNameIfNotFound}.entitlements";
            plistFilePath = $"{reportSummaryOutputPath}/{plistFileName}";

            var plist = new UnityEditor.iOS.Xcode.PlistDocument();
            plist.Create();
            plist.WriteToFile(plistFilePath);

            // add entitlement file to project
            pbxProject.AddFile(plistFilePath, plistFileName);
            pbxProject.AddBuildProperty(targetGuid, "CODE_SIGN_ENTITLEMENTS", plistFilePath);
        }
        else
        {
            plistFileName = Path.GetFileName(plistFilePath);
        }

        return plistFileName;
    }
#endif
#endif
}
