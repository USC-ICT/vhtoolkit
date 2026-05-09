using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ride;

/// <summary>
/// Disables specific GameObjects in this hierarchy based on the active runtime platform.
///
/// This component is intended to be placed on a root GameObject (for example, a character
/// prefab). At startup, it searches all child GameObjects under this root and disables any
/// whose names exactly match the configured target names, when running on selected platforms.
///
/// This is commonly used to strip or disable platform-incompatible visuals (such as eye
/// shells, transparent overlays, or unsupported render features) on mobile or WebGL builds,
/// while keeping them enabled on desktop platforms.
/// </summary>
/// <remarks>
/// <para>
/// Target matching is performed by exact, case-sensitive GameObject name comparison and
/// applies anywhere within the hierarchy under this component. Full hierarchy paths are
/// not required, making the setup resilient to prefab layout changes.
/// </para>
/// <para>
/// Platform selection uses an explicit "Disable On Platform" model: when a platform checkbox
/// is enabled, matching GameObjects will be disabled on that platform. Unchecked platforms
/// are unaffected.
/// </para>
/// <para>
/// This logic executes once during startup, and has no per-frame runtime cost.
/// </para>
/// </remarks>
/// <example>
/// Example use case:
/// <code>
/// Target Names:
///   - EyeShellRt
///   - EyeShellLf
///
/// Disable On Platforms:
///   - Android: checked
/// </code>
/// </example>
/// <seealso cref="Application.platform"/>
public class PlatformGameObjectDisabler : MonoBehaviour
{
    [Header("Target GameObject Names")]
    [Tooltip(
        "Exact GameObject names to disable, anywhere under this GameObject's hierarchy.\n" +
        "Name matching is exact (case-sensitive) and does not require full paths.\n" +
        "Example entries: 'EyeShellRt', 'EyeShellLf'."
    )]
    [SerializeField]
    private string[] m_targetNames;

    [Header("Disable On Platforms (checked = disabled)")]
    [SerializeField] private bool m_disableOnWindows;
    [SerializeField] private bool m_disableOnMacOS;
    [SerializeField] private bool m_disableOnLinux;
    [SerializeField] private bool m_disableOnAndroid;
    [SerializeField] private bool m_disableOnIOS;
    [SerializeField] private bool m_disableOnWebGL;

    [Header("Options")]
    [Tooltip("If true, missing paths are logged as warnings.")]
    [SerializeField]
    private bool m_warnIfMissing = true;


    private void Start()
    {
        if (!TryGetComponent(out ILoadableAsset loadedAsset))
            InitializeLoadedAsset();
    }

    public void InitializeLoadedAsset()
    {
        if (!ShouldDisableOnThisPlatform())
            return;

        DisableTargets();
    }

    private bool ShouldDisableOnThisPlatform()
    {
#if UNITY_EDITOR
        // In Editor, mirror the active build target.
        switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
        {
            case UnityEditor.BuildTarget.Android: return m_disableOnAndroid;
            case UnityEditor.BuildTarget.iOS: return m_disableOnIOS;
            case UnityEditor.BuildTarget.WebGL: return m_disableOnWebGL;
            case UnityEditor.BuildTarget.StandaloneWindows:
            case UnityEditor.BuildTarget.StandaloneWindows64: return m_disableOnWindows;
            case UnityEditor.BuildTarget.StandaloneOSX: return m_disableOnMacOS;
            case UnityEditor.BuildTarget.StandaloneLinux64: return m_disableOnLinux;
            default: return false;
        }
#else
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer: return m_disableOnWindows;
            case RuntimePlatform.OSXPlayer: return m_disableOnMacOS;
            case RuntimePlatform.LinuxPlayer: return m_disableOnLinux;
            case RuntimePlatform.Android: return m_disableOnAndroid;
            case RuntimePlatform.IPhonePlayer: return m_disableOnIOS;
            case RuntimePlatform.WebGLPlayer: return m_disableOnWebGL;
            default: return false;
        }
#endif
    }

    private void DisableTargets()
    {
        if (m_targetNames == null || m_targetNames.Length == 0)
            return;

        var transforms = GetComponentsInChildren<Transform>(true);

        foreach (var targetName in m_targetNames)
        {
            if (string.IsNullOrEmpty(targetName))
                continue;

            bool found = false;

            foreach (var t in transforms)
            {
                if (t == null || t.name != targetName)
                    continue;

                found = true;

                if (t.gameObject.activeSelf)
                    t.gameObject.SetActive(false);
            }

            if (m_warnIfMissing && !found)
                Debug.LogWarning($"[{nameof(PlatformGameObjectDisabler)}] No GameObject named '{targetName}' was found under '{name}'.", this);
        }
    }
}
