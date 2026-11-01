// AzureAsrBridge.js - debug-friendly, loader-safe
(function () {
  var recognizer = null;
  var goName = null;
  var stopping = false;

  function log() {
    // Avoid fancy syntax; keep it WebGL-safe.
    var args = Array.prototype.slice.call(arguments);
    args.unshift("[AzureAsrBridge]");
    console.log.apply(console, args);
  }

  function getSendMessage() {
    if (typeof SendMessage === "function") return SendMessage;
    if (typeof window !== "undefined" && typeof window.SendMessage === "function") return window.SendMessage;
    return null;
  }

  function _send(go, method, arg) {
    var sm = getSendMessage();
    if (sm) {
      sm(go, method, arg);
      return;
    }

    // Buffer briefly and retry (prevents rare race conditions)
    setTimeout(function () { _send(go, method, arg); }, 50);
  }

  function enumName(enumObj, value) {
    return enumObj && enumObj[value] ? enumObj[value] : value;
  }

  function start(cfg) {
    var sdk = (typeof window !== "undefined") ? window.SpeechSDK : null;
    if (!sdk) {
      console.error("[AzureAsrBridge] SpeechSDK not loaded");
      _send((cfg && cfg.goName) ? cfg.goName : "AzureAsrBridge", "OnAzureAsrError", "SpeechSDK not loaded");
      return;
    }

    if (recognizer) {
      log("already running");
      return;
    }

    stopping = false;
    goName = (cfg && cfg.goName) ? cfg.goName : "AzureAsrBridge";
    log("start()", cfg);

    var speechConfig = sdk.SpeechConfig.fromSubscription(cfg.key, cfg.region);
    speechConfig.speechRecognitionLanguage = (cfg && cfg.lang) ? cfg.lang : "en-US";

    var audioConfig = sdk.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new sdk.SpeechRecognizer(speechConfig, audioConfig);

    recognizer.sessionStarted = function (_, e) {
      log("sessionStarted", e.sessionId);
      _send(goName, "OnAzureAsrSession", "started:" + e.sessionId);
    };

    recognizer.sessionStopped = function (_, e) {
      log("sessionStopped", e.sessionId);
      _send(goName, "OnAzureAsrSession", "stopped:" + e.sessionId);

      // Clean up without calling stop again.
      if (recognizer) {
        try { recognizer.close(); } catch (ex) { }
      }
      recognizer = null;
      stopping = false;
    };

    recognizer.speechStartDetected = function (_, e) {
      log("speechStartDetected", e.sessionId);
      _send(goName, "OnAzureAsrInfo", "speechStart");
    };

    recognizer.speechEndDetected = function (_, e) {
      log("speechEndDetected", e.sessionId);
      _send(goName, "OnAzureAsrInfo", "speechEnd");
    };

    recognizer.recognizing = function (_, e) {
      if (e && e.result) {
        var t = e.result.text;
        if (t) {
          log("recognizing", t);
          _send(goName, "OnAzureAsrPartial", t);
        }
      }
    };

    recognizer.recognized = function (_, e) {
      if (!e || !e.result) return;

      var rr = e.result.reason;
      var rrStr = enumName(sdk.ResultReason, e.result.reason);
      var t = e.result.text;

      log("recognized", rrStr, "(" + rr + ")", t);

      if (rr === sdk.ResultReason.RecognizedSpeech && t) {
        _send(goName, "OnAzureAsrFinal", t);
      }
      else if (rr === sdk.ResultReason.NoMatch) {
        _send(goName, "OnAzureAsrInfo", "NoMatch");
      }
    };

    recognizer.canceled = function (_, e) {
      var reason = e ? e.reason : "";
      var reasonStr = e ? enumName(sdk.ResultReason, e.reason) : "";
      var code = e ? e.errorCode : "";
      var details = e ? e.errorDetails : "";

      log("canceled", reasonStr, "(" + reason + ")", code, details);
      _send(goName, "OnAzureAsrError", "reason=" + reason + " reasonStr=" + reasonStr + " code=" + code + " details=" + (details || ""));

      stop();
    };

    recognizer.startContinuousRecognitionAsync(
      function () { log("startContinuousRecognitionAsync: started"); },
      function (err) {
        console.error("[AzureAsrBridge] start failed:", err);
        _send(goName, "OnAzureAsrError", String(err || "start failed"));
        stop();
      }
    );
  }

  function stop() {
    if (!recognizer || stopping) return;

    stopping = true;

    var r = recognizer;
    recognizer = null;

    try {
      r.stopContinuousRecognitionAsync(
        function () {
          log("stopped");
          try { r.close(); } catch (ex) { }
          stopping = false;
        },
        function (e) {
          console.warn("[AzureAsrBridge] stop error", e);
          try { r.close(); } catch (ex) { }
          stopping = false;
        }
      );
    }
    catch (ex) {
      try { r.close(); } catch (e2) { }
      stopping = false;
    }
  }

  function ping(goNameOverride) {
    var target = goNameOverride || goName || "AzureAsrBridge";
    log("ping()");
    _send(target, "OnAzureAsrInfo", "bridge_loaded");
  }

  window.__AzureASR = { start: start, stop: stop, ping: ping };
})();
