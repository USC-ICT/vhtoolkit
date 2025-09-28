using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
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
///   - If the requested Unity version is not installed locally, attempts to open Unity Hub to install it automatically.
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
///   - Detects installed Unity editors by querying Unity Hub.
///   - If the requested version is found:
///       - Launches Unity.exe directly with the given project path.
///   - If the version is not found:
///       - If a known changeset exists, opens a UnityHub URL to install the version automatically.
///       - If changeset is unknown, launches Unity Hub and prompts manual install.
///
/// Fallbacks and Safety:
///   - Verifies Unity.exe existence before launching.
///   - Provides clear console messages for missing versions or missing Unity Hub installation.
///   - Never auto-upgrades projects or selects "close" versions without explicit matching.
///
/// Platform Support:
///   - Windows (via cscs.exe)
///   - macOS/linux (via mono)
///
/// Requirements:
///   - (windows) CSCS (C# scripting runtime)
///   - (maxOS/linux) mono
///   - Unity Hub must be installed and accessible.
///
/// Notes:
///   - Changeset dictionary should be updated periodically to include new Unity versions.
/// </summary>
class Script
{
    // changeset can be found in the What's New page for each Unity verison (at the bottom), eg https://unity3d.com/unity/whats-new/2019.2.12
    // or it can be found in the ProjectSettings\ProjectVersion.txt file in your project
    // or it can be searched via this cache of versions:  https://dragonbox.github.io/unities/v1/versions.json (used by u3d, https://github.com/DragonBox/u3d)
    static readonly Dictionary<string, string> m_versionAndChangeset = new Dictionary<string, string>()
    {
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
    static Dictionary<string, string> m_installedEditors = new Dictionary<string, string>();


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


        // process help command
        if (args[0].Trim().ToLower() == "help")
        {
            string helpOutput = RunHubCommandHelp();
            helpOutput = helpOutput.Replace("\n\n", "\n");
            Console.WriteLine(helpOutput);
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

            if (m_versionAndChangeset.ContainsKey(version))
            {
                Console.WriteLine("Opening Unity Hub to install Unity {0} automatically...", version);
                InstallUnityViaUrl(version);
            }
            else
            {
                Console.WriteLine("No changeset available for Unity version {0}.", version);
                Console.WriteLine("You must manually install this version via Unity Hub.");
                RunHub();
            }

            // exit now while user installs Unity via Hub
            return;
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
        // https://stackoverflow.com/questions/5116977/how-to-check-the-os-version-at-runtime-e-g-on-windows-or-linux-without-using/47390306#47390306
        // https://docs.microsoft.com/en-us/dotnet/api/system.platformid?view=netframework-4.8
        return (int)System.Environment.OSVersion.Platform == 4 ||
               (int)System.Environment.OSVersion.Platform == 6;
    }

    static string GetUnityHubLocation()
    {
        string unityHubExePath = "";

        if (IsOSX())
        {
            unityHubExePath = @"/Applications/Unity Hub.app/Contents/MacOS/Unity Hub";
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
        // get list of editors
        string editorsOutput = RunHubCommandEditorsInstalled();

        // parse list
        m_installedEditors = ParseEditorsOutput(editorsOutput);

        //foreach (var e in m_installedEditors)
        //    Console.WriteLine("'{0}' - '{1}'", e.Key, e.Value);
    }

    static string RunHub()
    {
        return RunHubCommand(@"");
    }

    static string RunHubCommandHelp()
    {
        return RunHubCommand(@"-- --headless help");
    }

    static string RunHubCommandEditorsInstalled()
    {
        return RunHubCommand(@"-- --headless editors --installed");
    }

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

    static string RunHubCommand(string args)
    {
        //string command = @"C:\Program Files\Unity Hub\Unity Hub.exe";
        //string args = @"-- --headless install-path --get";
        //string args = @"-- --headless editors";
        //string args = @"-- --headless help";

        string command = m_unityHubExePath;

        Console.WriteLine("{0} {1}", command, args);

        StringBuilder output = new StringBuilder();
        var process = new Process()
        { 
            StartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = command,
                Arguments = args
            },
        };
        process.OutputDataReceived += (sender, e) =>
        {
            //Console.WriteLine(":{0}", e.Data);
            output.Append(e.Data);
            output.Append("\n");
        };
        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();

        return output.ToString();
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

        Dictionary<string, string> versionAndLocation = new Dictionary<string, string>();

        Console.WriteLine(output);

        string[] lines = output.Split('\n');
        foreach (var rawLine in lines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
                continue;

            string line = rawLine.Trim();

            string version = null;
            string location = null;

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
        System.Diagnostics.Process.Start(url);
    }

    static void LaunchEditor(string version, string projectPath)
    {
        //start Unity.exe -projectPath %PROJECTPATH% %1 %2 %3 %4 %5 %6 %7

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

    static void ShowUsage()
    {
        Console.WriteLine("");
        Console.WriteLine("usage: cscs runUnity.cs 2019.2.1f1 [projectPath (optional)]");
        Console.WriteLine("");
    }
}
