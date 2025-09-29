using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
[Serializable]
public class VHSensingData
{
    public Dictionary<string, float> emotion;
    public float age;
    public string dominant_gender;
}
public enum VHEmotion
{
    Angry,
    Fear,
    Neutral,
    Sad,
    Disgust,
    Happy,
    Surprise
}

/// <summary>
/// Senesing implementation of Local DeepFace using socket based streaming
/// </summary>
public class StreamFaceAnalysis : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private bool isRunning = false;
    private Process pythonProcess;
    public int connectionRetryLimit = 10; //Number of times Unity tries to connect to the socket server after it is initialised.

    //Recieved data
    [HideInInspector]
    public VHEmotion dominantEmotion;
    [HideInInspector]
    public VHEmotion subtleEmotion;
    [HideInInspector]
    public String age;
    [HideInInspector]
    public String gender;
    [HideInInspector]
    public VHSensingData sensingDataRaw;

    //Path to the VHSensing python env and script
    string pythonExePath = Application.dataPath + "/VHSensing/VHSensingEnv/Scripts/python.exe";
    string scriptPath = Application.dataPath + "/VHSensing/socketServer.py";
    // Server address and port
    private string serverAddress = "127.0.0.1";
    private int serverPort = 55432;

    
    public delegate void OnVHSensingDataRecieved(VHSensingData dataRecieved);
    public event OnVHSensingDataRecieved VHSensingDataRecieved;
    void Start()
    {
        InitialiseVHSensingProccess();
    }
    public void InitialiseVHSensingProccess()
    {
        pythonProcess = new Process();
        pythonProcess.StartInfo.FileName = pythonExePath;
        pythonProcess.StartInfo.Arguments = scriptPath;
        pythonProcess.StartInfo.UseShellExecute = false;
        pythonProcess.StartInfo.RedirectStandardOutput = true;
        pythonProcess.StartInfo.RedirectStandardError = true;
        pythonProcess.StartInfo.CreateNoWindow = true;
        pythonProcess.OutputDataReceived += (sender, args) => UnityEngine.Debug.Log(args.Data);
        //pythonProcess.ErrorDataReceived += (sender, args) => UnityEngine.Debug.LogError(args.Data); //Uncomment to Debug Errors from python process
        pythonProcess.Start();
        pythonProcess.BeginOutputReadLine();
        pythonProcess.BeginErrorReadLine();
        StartCoroutine(TryConnectToServer());
    }

    void OnApplicationQuit()
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            pythonProcess.Kill();
            pythonProcess.WaitForExit();
        }
        DisconnectFromServer();
    }
    void Update()
    {
        if (isRunning)
        {
            if (stream != null && stream.DataAvailable)
            {
                byte[] buffer = new byte[1024];
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        HandleReceivedData(receivedData);
                    }
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError("Exception in Update: " + e.Message);
                    DisconnectFromServer();
                }
            }
        }
    }
    private IEnumerator TryConnectToServer()
    {
        int attempt = 0;
        while (attempt < connectionRetryLimit)
        {
            bool connected = false;
            try
            {
                client = new TcpClient(serverAddress, serverPort);
                stream = client.GetStream();
                isRunning = true;
                UnityEngine.Debug.Log("Connected to server");
                connected = true;
            }
            catch (Exception e)
            {
                attempt++;
                UnityEngine.Debug.LogWarning("Attempt " + attempt + ": Exception: " + e.Message);
            }

            if (connected)
            {
                break;
            }
            else if (attempt < 10)
            {
                yield return new WaitForSeconds(5); // Wait for 5 seconds before retrying
            }
        }
        if (!isRunning)
        {
            UnityEngine.Debug.LogError("Failed to connect after 10 attempts.");
        }
    }
    void HandleReceivedData(string data)
    {
        ParseEmotionData(data);
    }
    void DisconnectFromServer()
    {
        isRunning = false;
        stream?.Close();
        client?.Close();
        UnityEngine.Debug.Log("Disconnected from server");
    }
    public void ParseEmotionData(string jsonString)
    {
        if (string.IsNullOrEmpty(jsonString))
        {
            UnityEngine.Debug.LogError("JSON string is null or empty");
            return;
        }
        try
        {
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);
            var sensingDataRecieved = jsonObject.ToObject<VHSensingData>();
            if (sensingDataRecieved == null)
            {
                UnityEngine.Debug.LogError("Failed to parse JSON string");
                return;
            }
            VHSensingDataRecieved?.Invoke(sensingDataRecieved);
            sensingDataRaw = sensingDataRecieved;
            var sortedEmotions = new List<KeyValuePair<string, float>>(sensingDataRecieved.emotion);
            sortedEmotions.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            if (sortedEmotions.Count > 0)
            {

                dominantEmotion = getVHEmotion(sortedEmotions[0].Key);
            }

            if (sortedEmotions.Count > 1)
            {
                subtleEmotion = getVHEmotion(sortedEmotions[1].Key);
            }
            age =  sensingDataRecieved.age.ToString();
            gender = sensingDataRecieved.dominant_gender;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Exception occurred while parsing JSON: {ex.Message}");
        }
    }

    public VHEmotion getVHEmotion(string emotion)
    {
        switch (emotion.ToLower())
        {
            case "angry":
                return VHEmotion.Angry;
            case "fear":
                return VHEmotion.Fear;
            case "neutral":
                return VHEmotion.Neutral;
            case "sad":
                return VHEmotion.Sad;
            case "disgust":
                return VHEmotion.Disgust;
            case "happy":
                return VHEmotion.Happy;
            case "surprise":
                return VHEmotion.Surprise;
            default:
                return VHEmotion.Neutral ;
        }
    }
}
