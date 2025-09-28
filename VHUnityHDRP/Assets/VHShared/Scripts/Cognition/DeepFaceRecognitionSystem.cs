using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
#if !UNITY_WEBGL
using Amazon.Rekognition.Model;
#endif
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ride.Sensing
{
    /// <summary>
    /// Senesing implementation of Local DeepFace API
    /// </summary>
    public enum LocalFaceEmotion
    {
        Angry,
        Fear,
        Neutral,
        Sad,
        Disgust,
        Happy,
        Surprise
    }
    public enum DeepfaceModels
    {
        VGG_Face = 0,
        Facenet = 1,
        Facenet512 = 2,
        OpenFace = 3,
        DeepFace = 4,
        DeepID = 5,
        ArcFace = 6,
        Dlib = 7,
        SFace = 8,
        GhostFaceNet = 9
    }

    public class DeepFaceRecognitionSystem : RideSystemMonoBehaviour, ISensingEmotionSystem, ISensingCharacteristicsSystem, IRideSystem, IIdentity
    {
        //private Process pythonProcess;
        //string pythonExePath = Application.dataPath + "/VHSensing/VHSensingEnv/Scripts/python.exe";
        //string scriptPath = Application.dataPath + "/VHSensing/VHSensingEnv/Lib/site-packages/deepface/api/src/api.py";
        //string testImgPath = Application.dataPath + "/VHSensing/test_image.jpg";
        //[HideInInspector]
        //public bool serverIsRunning = false;
        string tempImgPath;
        private bool requestInProgress = false;
        DeepFaceResponse deepfaceResponseParsedLast;
        private string[] DeepfaceModelsArray = { "VGG-Face",
                                                  "Facenet",
                                                  "Facenet512",
                                                  "OpenFace",
                                                  "DeepFace",
                                                  "DeepID",
                                                  "ArcFace",
                                                  "Dlib",
                                                  "SFace",
                                                  "GhostFaceNet"};
        private static readonly int port = 5005;
        private static readonly string serverIP = "100.64.99.7";
        private static readonly string serverUri = "http://" + serverIP + ":" + port + "/analyze"; //Local server with poert 5100
        private Queue<DeepFaceSensingServiceRequest> m_requestQueue = new Queue<DeepFaceSensingServiceRequest>();
        public DeepfaceModels modelName;
        protected override void Start()
        {
            base.Start();
            tempImgPath = Application.temporaryCachePath + "/temp_img.png";
            InitialiseVHSensingProccess();
        }
        public void AnalyzeEmotions(object input, Action<SensingResponse> onComplete)
        {

            AddRequestToQueue(new DeepFaceSensingServiceRequest()
            {
                requestURI = serverUri,
                action = "AnalyzeEmotions",
                content = (byte[])input,
                onCompleteDelegate = onComplete
            });
        }
        void AddRequestToQueue(DeepFaceSensingServiceRequest request)
        {
            m_requestQueue.Enqueue(request);
        }
        protected override void Update()
        {
            base.Update();

            if (m_requestQueue.Count > 0)
            {

                DeepFaceSensingServiceRequest request = m_requestQueue.Dequeue();
                if (!requestInProgress)
                {
                    Request(request.requestURI, request.content, request.onCompleteDelegate);
                }
            }
        }
        private SensingEmotionResponse MapDeepFaceResponse(DeepFaceResponse deepFaceResponse)
        {
            SensingEmotionResponse m_sensingEmotionResponse = new SensingEmotionResponse("empty");
            m_sensingEmotionResponse.anger = deepFaceResponse.results[0].emotion.anger;
            m_sensingEmotionResponse.contempt = 0;
            m_sensingEmotionResponse.disgust = deepFaceResponse.results[0].emotion.disgust;
            m_sensingEmotionResponse.fear = deepFaceResponse.results[0].emotion.fear;
            m_sensingEmotionResponse.happiness = deepFaceResponse.results[0].emotion.happy;
            m_sensingEmotionResponse.neutral = deepFaceResponse.results[0].emotion.neutral;
            m_sensingEmotionResponse.sadness = deepFaceResponse.results[0].emotion.sad;
            m_sensingEmotionResponse.surprise = deepFaceResponse.results[0].emotion.surprise;

            return m_sensingEmotionResponse;
        }
        public async void Request(string uri, object input, Action<SensingResponse> onComplete)
        {
            //if(serverIsRunning)
            {
                SensingResponse sensingResponse = new SensingResponse("empty");
                byte[] byteData = (byte[])input;
                string base64Image = "data:image/jpeg;base64,"+Convert.ToBase64String(byteData);
                try
                {
                    File.WriteAllBytes(tempImgPath, byteData);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError("Failed to save image: " + e.Message);
                    onComplete?.Invoke(null);  // Invoke callback with null in case of error
                    return;
                }
                string jsonPayload = $@"
                {{
                    ""img"": ""{base64Image}"",
                    ""actions"": [""age"", ""gender"", ""emotion"", ""race""],
                    ""model_name"": ""{DeepfaceModelsArray[(int)modelName]}"",
                    ""enforce_detection"" : ""False""
                }}";
                using (var client = new HttpClient())
                {
                    // Set up the request content with JSON
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    UnityEngine.Debug.Log(content);
                    try
                    {
                        requestInProgress = true;
                        HttpResponseMessage response = await client.PostAsync(serverUri, content);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        UnityEngine.Debug.Log("Server response: " + responseBody);
                        deepfaceResponseParsedLast = JsonConvert.DeserializeObject<DeepFaceResponse>(responseBody);
                        if (deepfaceResponseParsedLast != null && deepfaceResponseParsedLast.results.Count > 0)
                        {
                            sensingResponse = MapDeepFaceResponse(deepfaceResponseParsedLast);
                            onComplete?.Invoke(sensingResponse);
                        }
                        requestInProgress = false;
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogWarning("Error sending request to DeepFace local server (Server might be inactive) : " + e.Message);
                        requestInProgress = false;
                    }
                }
            }
        }
        public void AnalyzeCharacteristics(object input, Action<SensingResponse> onComplete)
        {
            if (deepfaceResponseParsedLast != null && deepfaceResponseParsedLast.results.Count > 0)
            {
                SensingCharacteristicsResponse charResponse = new SensingCharacteristicsResponse("empty");
                charResponse.gender = deepfaceResponseParsedLast.results[0].dominant_gender;
                charResponse.age = deepfaceResponseParsedLast.results[0].age;
                onComplete?.Invoke(charResponse);
            }
        }
        
        private void OnDestroy()
        {
            EndVHSensingProcess();
        }

        //Redundant functions below
        public void InitialiseVHSensingProccess()
        {
#if false
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
            serverIsRunning = true;
            UnityEngine.Debug.Log("DeeFace local Sensing Server Started on port : " + port);
            MakeDeepfaceApiRequest(testImgPath);
#endif
        }

        public void EndVHSensingProcess()
        {
#if false
            if (pythonProcess != null && !pythonProcess.HasExited)
            {
                pythonProcess.Kill();
                pythonProcess.WaitForExit();
                serverIsRunning = false;
                UnityEngine.Debug.Log("DeeFace local Sensing Server Stopped");
            }
#endif
        }
        //Making a test call on startup to load the Model
        /*public async void MakeDeepfaceApiRequest(string imgPath, int maxAttempts = 10, int delayMilliseconds = 1000)
        {

            JObject jsonPayload = new JObject
            {
                { "img_path", imgPath},
                { "actions", new JArray { "age", "gender", "emotion", "race" } },
                //{ "enforce_detection", "False" },
                { "model_name", DeepfaceModelsArray[(int)modelName] }
            };

            string jsonString = jsonPayload.ToString();
            using (var client = new HttpClient())
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    try
                    {
                        requestInProgress = true;
                        HttpResponseMessage response = await client.PostAsync(serverUri, content);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        UnityEngine.Debug.Log("Test local Deepface request successfull : " + responseBody);
                        requestInProgress = false;
                        return;
                    }
                    catch (Exception e)
                    {
                        UnityEngine.Debug.LogWarning("Attempt: " + attempt + ". Error sending test local DeepFace test request: " + e.Message);
                        requestInProgress = false;
                    }
                    await Task.Delay(delayMilliseconds);
                }
            }
            UnityEngine.Debug.LogWarning("Failed to test the Local DeepFace : Server is not responding after multiple attempts.");
        }*/

        [Serializable]
        public class DeepFaceSensingServiceRequest
        {
            public string requestURI;
            public string action;
            public byte[] content;
            public Action<SensingResponse> onCompleteDelegate;
        }
        public class DeepFaceResponse
        {
            public List<DeepfaceResult> results;
        }
        public class DeepfaceResult
        {
            public int age { get; set; }
            public string dominant_emotion { get; set; }
            public string dominant_gender { get; set; }
            public string dominant_race { get; set; }
            public DeepfaceEmotion emotion { get; set; }
            public double face_confidence { get; set; }
#if !UNITY_WEBGL
            public Gender gender { get; set; }
#endif
            public DeepfaceRace race { get; set; }
            public DeepfaceRegion region { get; set; }
        }

        [Serializable]
        public class DeepfaceEmotion
        {
            public float anger;
            public float happy;
            public float sad;
            public float surprise;
            public float disgust;
            public float fear;
            public float neutral;
        }
        public class DeepfaceRace
        {
            public double asian;
            public double black;
            public double indian;
            public double latino_hispanic;
            public double middle_eastern;
            public double white;
        }
        public class DeepfaceRegion
        {
            public int h;
            public int x;
            public int y;
            public List<int> left_eye;
            public List<int> right_eye;
        }
    }
}

