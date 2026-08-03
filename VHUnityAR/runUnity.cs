using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;


// usage: cscs runUnity.cs 2019.2.1f1
// usage: cscs runUnity.cs 2019.2.1f1 c:\MyProject
// usage: cscs runUnity.cs help

/// <summary>
/// runUnity.cs
///
/// Purpose:
///   - Launches a Unity project with the exact specified Unity version.
///   - Uses the standalone Unity CLI when available, with a legacy Unity Hub fallback.
///   - If the requested Unity version is not installed locally, attempts to install it automatically.
///   - Ensures strict version matching to prevent accidental project upgrades or mismatches.
///
/// Usage:
///   - cscs runUnity.cs [unityVersion] [optional projectPath]
///   - Example 1: cscs runUnity.cs 2023.2.8f1
///   - Example 2: cscs runUnity.cs 2023.2.8f1 D:\Projects\MyProject
///   - Example 3: cscs runUnity.cs help
///
/// Parameters:
///   - unityVersion (required): The exact Unity editor version string (e.g., "2023.2.8f1").
///   - projectPath (optional): Path to the Unity project folder. If omitted, defaults to the current working directory.
///
/// Behavior:
///   - Detects whether the standalone `unity` CLI is available on the machine.
///   - Detects installed Unity editors by querying the standalone Unity CLI when available, otherwise Unity Hub.
///   - If the requested version is found:
///       - Launches the project with `unity open --editor-version ...` when the standalone Unity CLI is available.
///       - Otherwise launches Unity.exe directly with the given project path.
///   - If the version is not found:
///       - Attempts install with the standalone Unity CLI when available.
///       - Otherwise uses the legacy Unity Hub CLI / Unity Hub URL fallback.
///       - If the legacy path needs a changeset and none is known, launches Unity Hub and prompts manual install.
///
/// Fallbacks and Safety:
///   - Verifies Unity.exe existence before direct legacy launch.
///   - Provides clear console messages for missing versions or missing standalone Unity CLI support.
///   - Never auto-upgrades projects or selects "close" versions without explicit matching.
///
/// Platform Support:
///   - Windows (via cscs.exe)
///   - macOS/linux (via mono)
///
/// Requirements:
///   - (windows) CSCS (C# scripting runtime)
///   - (maxOS/linux) mono
///   - Unity Hub must be installed and accessible for the legacy fallback path.
///
/// Notes:
///   - The changeset dictionary is still used by the legacy Unity Hub fallback path.
///   - Unity Hub release notes: https://unity.com/unity-hub/release-notes
///   - Unity CLI documentation: https://docs.unity.com/en-us/hub/unity-cli
/// </summary>
class Script
{
    // changeset can be found in the What's New page for each Unity verison (at the bottom), eg https://unity3d.com/unity/whats-new/2019.2.12
    // or it can be found in the ProjectSettings\ProjectVersion.txt file in your project
    // or it can be searched via this cache of versions:  https://dragonbox.github.io/unities/v1/versions.json (used by u3d, https://github.com/DragonBox/u3d)
    static readonly Dictionary<string, string> m_versionAndChangeset = new Dictionary<string, string>()
    {
        { "6000.4.7f1",  "f3c3c4248748" },
        { "6000.3.15f1", "c1aa84e375f6" },
        { "6000.2.15f1", "0707b6d1e918" },
        { "6000.1.17f1", "c0b9d3899998" },
        { "6000.1.5f1",  "923722cbbcfc" },
        { "6000.0.47f1", "2ad1ed33fd3b" },
        { "6000.0.27f1", "27c554a2199c" },
        { "2023.2.20f1", "0e25a174756c" },
        { "2023.2.19f1", "95c298372b1e" },
        { "2023.2.8f1",  "a3bb09f8c8c4" },
        { "2023.2.4f1",  "3a7eb0602d92" },
        { "2023.1.5f1",  "9dce81d9e7e0" },
        { "2022.3.62f1", "4af31df58517" },
        { "2022.3.16f1", "d2c21f0ef2f1" },
        { "2022.3.13f1", "5f90a5ebde0f" },
        { "2022.3.10f1", "ff3792e53c62" },
        { "2022.3.5f1",  "9674261d40ee" },
        { "2022.2.16f1", "d535843d11e1" },
        { "2022.1.19f1", "2fd7b40534d1" },
        { "2022.1.15f1", "42973686a05c" },
        { "2022.1.13f1", "22856944e6d2" },
        { "2022.1.12f1", "916d9c03b898" },
        { "2022.1.9f1",  "07e076b6d414" },
        { "2022.1.5f1",  "feea5ec8f162" },
        { "2022.1.0f1",  "369b620af41c" },
        { "2021.3.28f1", "232e59c3f087" },
        { "2021.3.23f1", "213b516bf396" },
        { "2021.3.2f1",  "d6360bedb9a0" },
        { "2021.2.17f1", "efb8f635e7b1" },
        { "2021.2.14f1", "bcb93e5482d2" },
        { "2021.2.12f1", "48b1aa000234" },
        { "2021.2.10f1", "ee872746220e" },
        { "2021.2.1f1",  "c20c6d589440" },
        { "2021.2.0f1",  "4bf1ec4b23c9" },
        { "2021.1.22f1", "a137e5fb0427" },
        { "2021.1.21f1", "f2d5d3c59f8c" },
        { "2021.1.20f1", "be552157821d" },
        { "2021.1.19f1", "5f5eb8bbdc25" },
        { "2021.1.17f1", "03b40fe07a36" },
        { "2021.1.16f1", "5fa502fca597" },
        { "2021.1.15f1", "e767a7370072" },
        { "2021.1.11f1", "4d8c25f7477e" },
        { "2021.1.10f1", "b15f561b2cef" },
        { "2021.1.5f1",  "3737af19df53" },
        { "2021.1.4f1",  "4cd64a618c1b" },
        { "2021.1.3f1",  "4bef613afd59" },
        { "2020.3.48f1", "b805b124c6b7" },
        { "2020.3.46f1", "18bc01a066b4" },
        { "2020.3.36f1", "71f96b79b9f0" },
        { "2020.3.34f1", "9a4c9c70452b" },
        { "2020.3.14f1", "d0d1bb862f9d" },
        { "2020.3.4f1",  "0abb6314276a" },
        { "2020.3.3f1",  "76626098c1c4" },
        { "2020.3.0f1",  "c7b5465681fb" },
        { "2020.2.6f1",  "8a2143876886" },
        { "2020.2.5f1",  "e2c53f129de5" },
        { "2020.2.4f1",  "becced5a802b" },
        { "2020.2.3f1",  "8ff31bc5bf5b" },
        { "2020.2.2f1",  "068178b99f32" },
        { "2020.2.1f1",  "270dd8c3da1c" },
        { "2020.2.0f1",  "3721df5a8b28" },
        { "2020.1.17f1", "9957aee8edc2" },
        { "2020.1.15f1", "97d0ae02d19d" },
        { "2020.1.10f1", "974a9d56f159" },
        { "2020.1.9f1",  "145f5172610f" },
        { "2020.1.8f1",  "22e8c0b0c3ec" },
        { "2020.1.7f1",  "064ffcdb64ad" },
        { "2020.1.6f1",  "fc477ca6df10" },
        { "2020.1.5f1",  "e025938fdedc" },
        { "2020.1.4f1",  "fa717bb873ec" },
        { "2020.1.3f1",  "cf5c4788e1d8" },
        { "2020.1.2f1",  "7b32bc54ba47" },
        { "2020.1.1f1",  "2285c3239188" },
        { "2020.1.0f1",  "2ab9c4179772" },
        { "2019.4.21f1", "b76dac84db26" },
        { "2019.4.11f1", "2d9804dddde7" },
        { "2019.4.4f1",  "1f1dac67805b" },
        { "2019.4.2f1",  "20b4642a3455" },
        { "2019.4.1f1",  "e6c045e14e4e" },
        { "2019.4.0f1",  "0af376155913" },
        { "2019.3.10f1", "5968d7f82152" },
        { "2019.3.1f1",  "89d6087839c2" },
        { "2019.3.0f6",  "27ab2135bccf" },
        { "2019.3.0f2",  "6e9a27477296" },
        { "2019.2.21f1", "9d528d026557" },
        { "2019.2.12f1", "b1a7e1fb4fa5" },
        { "2019.2.1f1",  "ca4d5af0be6f" },
        { "2019.2.0f1",  "20c1667945cf" },
        { "2019.1.0f2",  "292b93d75a2c" },
        { "2018.4.17f1", "b830f56f42f0" },
        { "2018.4.16f1", "e6e9ca02b32a" },
        { "2018.3.11f1", "5063218e4ab8" },
        { "2018.3.0f2",  "6e9a27477296" },
        { "2018.2.5f1",  "3071d1717b71" },
        { "2018.1.6f1",  "57cc34175ccf" },
        { "2017.4.36f1", "c663def8414c" },
        { "2017.3.1f1",  "fc1d3344e6ea" },
        { "2017.1.1f1",  "5d30cf096e79" },
        { "2017.1.0f3",  "472613c02cf7" },
        {    "5.6.1f1",  "2860b30f0b54" },
        {    "5.6.0f3",  "497a0f351392" },
        {    "5.5.1f1",  "88d00a7498cd" },
        {    "5.5.0f3",  "38b4efef76f0" },
        {    "5.4.6f3",  "7c5210d1343f" },
    };


    static string m_unityHubExePath = "";
    static string m_unityCliPath = "";
    static bool m_canRunUnityCli = false;
    static Dictionary<string, string> m_installedEditors = new Dictionary<string, string>();


    static bool IsUnityHubInstalled { get { return !string.IsNullOrEmpty(m_unityHubExePath); } }
    static bool IsUnityCliInstalled { get { return m_canRunUnityCli; } }


    static public void Main(string[] args)
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine(" Unity Launcher Script - runUnity");
        Console.WriteLine("--------------------------------------------");

        if (args.Length == 0)
        {
            ShowUsage();
            return;
        }


        // get location of unity hub exe
        m_unityHubExePath = GetUnityHubLocation();
        m_unityCliPath = GetUnityCliLocation();
        m_canRunUnityCli = CanRunUnityCli();

        if (!IsUnityCliInstalled)
        {
            Console.WriteLine("Standalone Unity CLI not found on PATH.");
            ShowUnityCliInstallInstructions();
            Console.WriteLine("");
        }

        // process help command
        if (args[0].Trim().ToLower() == "help")
        {
            ShowUsage();
            ShowEnvironmentSummary();
            return;
        }


        // process command line args
        string version = args[0].Trim();
        string projectPath = Directory.GetCurrentDirectory(); // default to current folder

        if (args.Length >= 2)
        {
            projectPath = args[1].Trim();
            if (!Directory.Exists(projectPath))
            {
                Console.WriteLine("Specified project path does not exist: {0}", projectPath);
                return;
            }
        }


        // find which editors are installed locally on the system
        GetInstalledEditors();


        // if editor isn't in the list, install
        if (!m_installedEditors.ContainsKey(version))
        {
            Console.WriteLine();
            Console.WriteLine("Unity version {0} is not installed locally.", version);

            if (!TryInstallMissingEditor(version))
                return;

            GetInstalledEditors();
            if (!m_installedEditors.ContainsKey(version))
            {
                Console.WriteLine("Unity version {0} is still not available after install attempt.", version);
                return;
            }
        }

        // launch editor with project path on current folder
        LaunchEditor(version, projectPath);
    }

    static bool IsWindows()
    {
        // https://stackoverflow.com/questions/5116977/how-to-check-the-os-version-at-runtime-e-g-on-windows-or-linux-without-using/47390306#47390306
        // https://docs.microsoft.com/en-us/dotnet/api/system.platformid?view=netframework-4.8
        return (int)System.Environment.OSVersion.Platform == 2;
    }

    static bool IsOSX()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            return true;

        // Fallback for older Mono
        if (Environment.OSVersion.Platform == PlatformID.Unix)
            return Directory.Exists("/System/Library/CoreServices");

        return false;
    }

    static bool IsLinux()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return true;

        // Fallback for older Mono
        // macOS has /System, Linux does not
        if (Environment.OSVersion.Platform == PlatformID.Unix)
            return !Directory.Exists("/System/Library/CoreServices");

        return false;
    }

    static string GetUnityHubLocation()
    {
        string unityHubExePath = "";

        if (IsOSX())
        {
            unityHubExePath = @"/Applications/Unity Hub.app/Contents/MacOS/Unity Hub";
        }
        else if (IsLinux())
        {
            unityHubExePath = @"unityhub";
        }
        else
        {
            // set a default if there's something wrong with the reg key
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            unityHubExePath = Path.Combine(programFiles, "Unity Hub", "Unity Hub.exe");

            //@rem Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Unity Technologies\Hub
            //@rem InstallLocation

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Unity Technologies\Hub", false))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("InstallLocation");
                        if (o != null)
                        {
                            unityHubExePath = o as string;  // "as" because it's REG_SZ...otherwise ToString() might be safe(r)
                            unityHubExePath = Path.Combine(unityHubExePath, "Unity Hub.exe");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetUnityHubLocation() - exception caught trying to read from reg key - {0}", ex);
            }
        }

        return unityHubExePath;
    }

    static void GetInstalledEditors()
    {
        var installedEditors = new Dictionary<string, string>();
        string editorsOutput = QueryInstalledEditors();
        installedEditors = ParseEditorsOutput(editorsOutput);

        if (installedEditors.Count == 0)
            AddDefaultInstallLocationIfPresent(installedEditors);

        m_installedEditors = installedEditors;
    }

    static string RunHubCommand(string args)
    {
        //string command = @"C:\Program Files\Unity Hub\Unity Hub.exe";
        //string args = @"-- --headless install-path --get";
        //string args = @"-- --headless editors";
        //string args = @"-- --headless help";

        return RunCommand(m_unityHubExePath, args);
    }

    static string RunHub() { return RunHubCommand(@""); }
    static string RunHubCommandHelp() { return RunHubCommand(@"-- --headless help"); }
    static string RunHubCommandEditorsInstalled() { return RunHubCommand(@"-- --headless editors --installed"); }

    static string RunHubCommandInstallUnity(string version, List<string> components)
    {
        string changesetString = "";
        if (m_versionAndChangeset.ContainsKey(version))
            changesetString = string.Format("--changeset {0}", m_versionAndChangeset[version]);

        string componentsString = "";
        foreach (var component in components)
            componentsString += string.Format("-m {0} ", component);

        return RunHubCommand(string.Format(@"-- --headless install --version {0} {1} {2}", version, changesetString, componentsString));
    }

    static string RunUnityCliCommand(string args) { return RunCommand(m_unityCliPath, args); }
    static string RunUnityCliEditorsInstalled() { return RunUnityCliCommand(@"editors -i --format tsv"); }
    static int RunUnityCliInstallUnity(string version) { return RunCommandPassthrough(m_unityCliPath, string.Format(@"install {0}", version)); }
    //static string RunUnityCliInstallUnity(string version) { return RunUnityCliCommand(string.Format(@"install {0} -c {1}", version, m_versionAndChangeset[version])); }

    static string QueryInstalledEditors()
    {
        if (IsUnityCliInstalled)
            return RunUnityCliEditorsInstalled();

        if (IsUnityHubInstalled)
            return RunHubCommandEditorsInstalled();

        return "";
    }

    static bool InstallUnityVersion(string version)
    {
        if (IsUnityCliInstalled)
            return RunUnityCliInstallUnity(version) == 0;

        if (IsUnityHubInstalled)
            return !string.IsNullOrWhiteSpace(RunHubCommandInstallUnity(version, new List<string>()));

        return false;
    }

    static string GetCliDisplayName() { return IsUnityCliInstalled ? "Standalone Unity CLI" : "Unity Hub CLI"; }

    static string RunCommand(string command, string args)
    {
        if (string.IsNullOrWhiteSpace(command))
            return "";

        Console.WriteLine("{0} {1}", command, args);

        StringBuilder output = new StringBuilder();
        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = command,
                Arguments = args
            },
        };

        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data == null)
                return;

            //Console.WriteLine(":{0}", e.Data);

            output.Append(e.Data);
            output.Append("\n");
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data == null)
                return;

            //Console.WriteLine(":{0}", e.Data);

            output.Append(e.Data);
            output.Append("\n");
        };

        try
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }
        catch (Exception ex)
        {
            output.AppendLine(ex.ToString());
        }

        return output.ToString();
    }

    static int RunCommandPassthrough(string command, string args)
    {
        if (string.IsNullOrWhiteSpace(command))
            return -1;

        Console.WriteLine("{0} {1}", command, args);

        var process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = command,
                Arguments = args
            },
        };

        try
        {
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    static Dictionary<string, string> ParseEditorsOutput(string output)
    {
        // Hub 3.13.0 and earlier
        //2018.3.14f1 , installed at C:\Program Files\Unity\Hub\Editor\2018.3.14f1\Editor\Unity.exe
        //2019.2.0a14 , installed at E:\Unity2019.2.0a14.hub\Editor\Unity.exe
        //2019.2.0a13 , installed at E:\Unity2019.2.0a13.hub\Editor\Unity.exe
        //2019.2.0a11 , installed at E:\Unity2019.2.0a11.hub\Editor\Unity.exe
        //2019.1.0f2 , installed at e:/Projects/Restricted/tss/tss/Unity/Editor/Unity.exe
        //2019.1.6f1 , installed at D:\work\psa\psa\Unity\Editor\Unity.exe
        //2019.1.10f1 , installed at D:\work\psa\psa\Unity\Editor\Unity.exe
        //2019.2.1f1 , installed at d:/work/psa/psa/Unity/Editor/Unity.exe

        // Hub 3.14.0 and later
        //2020.2.1f1  installed at C:\Program Files\Unity\Hub\Editor\2020.2.1f1\Editor\Unity.exe
        //2021.2.0f1  installed at C:\Program Files\Unity\Hub\Editor\2021.2.0f1\Editor\Unity.exe
        //2023.2.19f1 installed at C:\Program Files\Unity\Hub\Editor\2023.2.19f1\Editor\Unity.exe
        //2022.1.5f1  installed at C:\Program Files\Unity\Hub\Editor\2022.1.5f1\Editor\Unity.exe
        //2022.3.62f1 installed at C:\Program Files\Unity\Hub\Editor\2022.3.62f1\Editor\Unity.exe
        //2022.3.10f1 installed at C:\Program Files\Unity\Hub\Editor\2022.3.10f1\Editor\Unity.exe
        //2023.2.4f1  installed at C:\Program Files\Unity\Hub\Editor\2023.2.4f1\Editor\Unity.exe
        //2023.2.8f1  installed at C:\Program Files\Unity\Hub\Editor\2023.2.8f1\Editor\Unity.exe
        //6000.0.27f1 installed at C:\Program Files\Unity\Hub\Editor\6000.0.27f1\Editor\Unity.exe
        //6000.0.47f1 installed at C:\Program Files\Unity\Hub\Editor\6000.0.47f1\Editor\Unity.exe
        //6000.1.5f1  installed at C:\Program Files\Unity\Hub\Editor\6000.1.5f1\Editor\Unity.exe

        // Standalone Unity CLI
        //Version Alias   Arch    Default Platforms
        //2023.2.4f1              x86_64  false
        //2022.3.13f1             x86_64  false
        //6000.0.27f1     6.0.27f1        x86_64  false
        //6000.1.5f1      6.1.5f1 x86_64  false   Android, Android SDK & NDK Tools, iOS, Linux, Mac, OpenJDK, Web
        //6000.0.47f1     6.0.47f1        x86_64  false

        var versionAndLocation = new Dictionary<string, string>();

        Console.WriteLine(output);

        string[] lines = output.Split('\n');
        foreach (var rawLine in lines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            string line = rawLine.Trim();

            string version = null;
            string location = null;

            // CLI header row
            if (line.StartsWith("Version\t", StringComparison.OrdinalIgnoreCase) ||
                line.StartsWith("Version ", StringComparison.OrdinalIgnoreCase))
                continue;

            int idx = line.IndexOf("installed at", StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                version = line.Substring(0, idx)
                              .Replace(",", "") // Remove trailing comma if present
                              .Replace(" (Intel)", "")
                              .Replace(" (Apple silicon)", "")
                              .Trim();

                location = line.Substring(idx + "installed at".Length).Trim();
            }
            else if (TryParseUnityCliInstalledEditorsLine(line, out version))
            {
                // The standalone Unity CLI installed-editor TSV does not include an editor path.
                // For the new CLI path we only need to know whether the version is installed.
                location = version;
            }

            if (string.IsNullOrEmpty(version) || string.IsNullOrEmpty(location))
                continue;

            versionAndLocation[version] = location;
        }

        return versionAndLocation;
    }

    static void InstallUnityViaUrl(string version)
    {
        if (!m_versionAndChangeset.ContainsKey(version))
        {
            Console.WriteLine("InstallUnityViaUrl() - Version {0} doesn't match any changeset", version);
            return;
        }

        string changeset = m_versionAndChangeset[version];

        string url = string.Format("unityhub://{0}/{1}", version, changeset);

        if (IsLinux())
            System.Diagnostics.Process.Start("xdg-open", url);
        else
            System.Diagnostics.Process.Start(url);
    }

    static bool TryInstallMissingEditor(string version)
    {
        if (IsUnityCliInstalled)
        {
            if (InstallUnityVersion(version))
            {
                Console.WriteLine("Unity {0} install completed via the {1}.", version, GetCliDisplayName());
                return true;
            }
        }

        if (!m_versionAndChangeset.ContainsKey(version))
        {
            Console.WriteLine("No changeset available for Unity version {0}.", version);
            Console.WriteLine("You must manually install this version via Unity Hub.");
            ShowUnityCliInstallInstructions();
            RunHub();
            return false;
        }

        if (InstallUnityVersion(version))
        {
            Console.WriteLine("Installing Unity {0} via the {1}...", version, GetCliDisplayName());
            return false;
        }

        Console.WriteLine("Falling back to the Unity Hub deeplink installer for Unity {0}...", version);
        ShowUnityCliInstallInstructions();
        InstallUnityViaUrl(version);
        return false;
    }

    static void LaunchEditor(string version, string projectPath)
    {
        if (IsUnityCliInstalled)
        {
            LaunchEditorWithUnityCli(version, projectPath);
            return;
        }

        // Legacy Hub CLI path: launch Unity.exe directly.

        string fileName = m_installedEditors[version];

        // on MacOS, the output only contains the app folder.  Need to append the path to the executable.
        if (IsOSX())
            fileName = fileName + "/Contents/MacOS/Unity";

        string arguments = string.Format("-projectPath \"{0}\"", projectPath);

        if (!File.Exists(fileName))
        {
            Console.WriteLine("ERROR: Unity executable not found: {0}", fileName);
            Console.WriteLine("Please check your Unity Hub installation or reinstall the editor.");
            return;
        }

        Console.WriteLine("Launching Unity {0} with project: {1}", version, projectPath);
        Console.WriteLine("{0} {1}", fileName, arguments);

        Process.Start(fileName, arguments);
    }

    static void LaunchEditorWithUnityCli(string version, string projectPath)
    {
        string args = string.Format("open \"{0}\" --editor-version {1}", projectPath, version);
        Console.WriteLine("Launching Unity {0} with project: {1}", version, projectPath);
        RunCommandPassthrough(m_unityCliPath, args);
    }

    static void AddDefaultInstallLocationIfPresent(Dictionary<string, string> installedEditors)
    {
        if (IsWindows())
        {
            string programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            string editorRoot = Path.Combine(programFiles, "Unity", "Hub", "Editor");
            if (!Directory.Exists(editorRoot))
                return;

            foreach (string versionFolder in Directory.GetDirectories(editorRoot))
            {
                string version = Path.GetFileName(versionFolder);
                string candidate = Path.Combine(versionFolder, "Editor", "Unity.exe");
                if (File.Exists(candidate))
                    installedEditors[version] = candidate;
            }
        }
        else if (IsOSX())
        {
            string editorRoot = "/Applications/Unity/Hub/Editor";
            if (!Directory.Exists(editorRoot))
                return;

            foreach (string versionFolder in Directory.GetDirectories(editorRoot))
            {
                string version = Path.GetFileName(versionFolder);
                string candidate = Path.Combine(versionFolder, "Unity.app");
                if (Directory.Exists(candidate))
                    installedEditors[version] = candidate;
            }
        }
        else if (IsLinux())
        {
            string editorRoot = "/opt/unityhub/editor";
            if (!Directory.Exists(editorRoot))
                return;

            foreach (string versionFolder in Directory.GetDirectories(editorRoot))
            {
                string version = Path.GetFileName(versionFolder);
                string candidate = Path.Combine(versionFolder, "Editor", "Unity");
                if (File.Exists(candidate))
                    installedEditors[version] = candidate;
            }
        }
    }

    // Parses the standalone `unity editors -i --format tsv` rows, which only provide editor metadata.
    // In that format we only need the installed version because the new CLI launch path uses `unity open`.
    static bool TryParseUnityCliInstalledEditorsLine(string line, out string version)
    {
        version = null;

        string[] tokens = line.Split('\t');
        if (tokens.Length == 0)
            return false;

        string firstToken = NormalizeEditorToken(tokens[0]);
        if (!LooksLikeUnityVersion(firstToken))
            return false;

        version = firstToken;
        return true;
    }

    static string NormalizeEditorToken(string token)
    {
        if (token == null)
            return "";

        return token.Trim().Trim('"');
    }

    static bool LooksLikeUnityVersion(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return false;

        return Regex.IsMatch(token, @"^\d+\.\d+\.\d+[abcfp]\d+$");
    }

    static string GetUnityCliLocation() { return "unity"; }

    static bool CanRunUnityCli()
    {
        try
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = m_unityCliPath,
                    Arguments = "--version"
                },
            };

            process.Start();
            string stdout = process.StandardOutput.ReadToEnd();
            string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
                return false;

            if (string.IsNullOrWhiteSpace(stdout) && string.IsNullOrWhiteSpace(stderr))
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }

    static void ShowEnvironmentSummary()
    {
        Console.WriteLine("Unity Hub path: {0}", string.IsNullOrEmpty(m_unityHubExePath) ? "(not found)" : m_unityHubExePath);
        Console.WriteLine("Standalone Unity CLI: {0}", IsUnityCliInstalled ? m_unityCliPath + " (installed)" : "(not found on PATH)");
        Console.WriteLine("");
        ShowUnityCliInstallInstructions();
        Console.WriteLine("");
        Console.WriteLine("Commands:");
        Console.WriteLine("  cscs runUnity.cs help");
        Console.WriteLine("  cscs runUnity.cs 6000.1.5f1");
        Console.WriteLine("  cscs runUnity.cs 6000.1.5f1 D:\\Projects\\MyProject");
    }

    static void ShowUnityCliInstallInstructions()
    {
        Console.WriteLine("Standalone Unity CLI install commands:");
        Console.WriteLine("  Windows PowerShell:");
        Console.WriteLine("    $env:UNITY_CLI_CHANNEL='beta'; irm https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.ps1 | iex");
        Console.WriteLine("  macOS:");
        Console.WriteLine("    curl -fsSL https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.sh | UNITY_CLI_CHANNEL=beta bash");
        Console.WriteLine("  Linux:");
        Console.WriteLine("    curl -fsSL https://public-cdn.cloud.unity3d.com/hub/prod/cli/install.sh | UNITY_CLI_CHANNEL=beta bash");
        Console.WriteLine("  Verify after install:");
        Console.WriteLine("    unity --version");
    }

    static void ShowUsage()
    {
        Console.WriteLine("");
        Console.WriteLine("usage: cscs runUnity.cs <unityVersion> [projectPath (optional)]");
        Console.WriteLine("usage: cscs runUnity.cs help");
        Console.WriteLine("example: cscs runUnity.cs 2019.2.1f1 D:\\Projects\\MyProject");
        Console.WriteLine("");
    }
}
