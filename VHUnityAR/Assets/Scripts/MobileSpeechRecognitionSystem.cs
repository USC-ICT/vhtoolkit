using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ride.SpeechRecognition
{
/// <summary>
/// SpeechRecognitionSystem for Android/iOS using KKSpeechRecognizer plugin, that utilizes native mobile libraries
/// </summary>
public class MobileSpeechRecognitionSystem : SpeechRecognitionSystemUnity
{
#if false
    [SerializeField] private KKSpeech.SpeechRecognizerListener kkListener;
    [SerializeField] private bool createListenerChildIfMissing = true;
    [SerializeField] private string listenerChildName = "KKSpeechRecognizerListener";

    public override bool IsSupported => KKSpeech.SpeechRecognizer.ExistsOnDevice();
    public override bool SupportsContinuousRecognition =>
#if UNITY_IOS
        true;
#else
        false;
#endif

    public override void SystemInit()
    {
        base.SystemInit();

        if (!IsSupported)
            return;

        EnsureListener();

        if (kkListener == null)
        {
            Debug.LogWarning($"{nameof(MobileSpeechRecognitionSystem)} could not find or create a {nameof(KKSpeech.SpeechRecognizerListener)} child.");
            return;
        }

        KKSpeech.SpeechRecognizer.RequestAccess();

        kkListener.onPartialResults.AddListener(OnPartialSpeechResult);
        kkListener.onFinalResults.AddListener(OnFinalSpeechResult);
    }

    public override void SetMicrophone(string deviceName)
    {
        // Not applicable for mobile, but required by interface
    }

    public override void OnRecognizingStarted()
    {
        KKSpeech.SpeechRecognizer.StartRecording(new KKSpeech.SpeechRecognitionOptions()
        {
            shouldCollectPartialResults = true,
        });
        base.OnRecognizingStarted();
    }

    public override void OnRecognizingStopped()
    {
        KKSpeech.SpeechRecognizer.StopIfRecording();
        base.OnRecognizingStopped();
    }

    public void OnPartialSpeechResult(string result)
    {
        OnPartialSpeechRecognized(result, Confidence);
    }

    public void OnFinalSpeechResult(string result)
    {
        OnSpeechRecognized(result, Confidence);
    }

    private void EnsureListener()
    {
        if (kkListener != null)
            return;

        kkListener = GetComponentInChildren<KKSpeech.SpeechRecognizerListener>(true);
        if (kkListener != null || !createListenerChildIfMissing)
            return;

        var listenerObject = new GameObject(listenerChildName);
        listenerObject.transform.SetParent(transform, false);
        kkListener = listenerObject.AddComponent<KKSpeech.SpeechRecognizerListener>();
    }
#else
    public override bool IsSupported => false;
    public override bool SupportsContinuousRecognition => false;
#endif
}
}
