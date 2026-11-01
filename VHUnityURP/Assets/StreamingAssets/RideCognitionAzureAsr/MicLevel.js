// mic-level.js — simple WebAudio RMS meter to Unity
(function () {
  let audioCtx, analyser, micSrc, rafId, goName = "AzureAsrBridge";

  async function startMeter(cfg) {
    stopMeter();
    goName = (cfg && cfg.goName) || "AzureAsrBridge";
    const stream = await navigator.mediaDevices.getUserMedia({ audio: true, video: false });
    audioCtx = new (window.AudioContext || window.webkitAudioContext)();
    analyser = audioCtx.createAnalyser();
    analyser.fftSize = 1024;
    const bufferLength = analyser.fftSize;
    const data = new Float32Array(bufferLength);

    micSrc = audioCtx.createMediaStreamSource(stream);
    micSrc.connect(analyser);

    function tick() {
      analyser.getFloatTimeDomainData(data);
      // RMS
      let sum = 0;
      for (let i = 0; i < data.length; i++) { const v = data[i]; sum += v * v; }
      const rms = Math.sqrt(sum / data.length); // 0..1-ish
      // send ~0..1 in string form
      if (typeof SendMessage === "function") SendMessage(goName, "OnMicLevel", rms.toFixed(4));
      rafId = requestAnimationFrame(tick);
    }
    tick();
  }

  function stopMeter() {
    if (rafId) cancelAnimationFrame(rafId);
    rafId = null;
    if (micSrc) { try { micSrc.disconnect(); } catch(e){} micSrc = null; }
    if (audioCtx) { try { audioCtx.close(); } catch(e){} audioCtx = null; }
  }

  window.__MicLevel = { startMeter, stopMeter };
})();
