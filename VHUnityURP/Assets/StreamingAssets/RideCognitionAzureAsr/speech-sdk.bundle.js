(() => {
  var e = [, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__exportStar || function(e3, t3) {
      for (var r3 in e3) "default" === r3 || Object.prototype.hasOwnProperty.call(t3, r3) || i2(t3, e3, r3);
    };
    Object.defineProperty(t2, "__esModule", { value: true });
    new (r2(2)).AgentConfig(), n(r2(80), t2);
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__exportStar || function(e3, t3) {
      for (var r3 in e3) "default" === r3 || Object.prototype.hasOwnProperty.call(t3, r3) || i2(t3, e3, r3);
    };
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AutoDetectSourceLanguagesOpenRangeOptionName = t2.ForceDictationPropertyName = t2.ServicePropertiesPropertyName = t2.CancellationErrorCodePropertyName = t2.OutputFormatPropertyName = t2.SpeechSynthesisAdapter = t2.AvatarSynthesisAdapter = void 0, n(r2(3), t2), n(r2(56), t2), n(r2(55), t2), n(r2(57), t2), n(r2(58), t2), n(r2(59), t2), n(r2(60), t2), n(r2(200), t2), n(r2(201), t2), n(r2(202), t2), n(r2(203), t2), n(r2(204), t2), n(r2(205), t2), n(r2(206), t2), n(r2(207), t2), n(r2(182), t2), n(r2(208), t2), n(r2(209), t2), n(r2(210), t2), n(r2(211), t2), n(r2(212), t2), n(r2(213), t2), n(r2(214), t2), n(r2(215), t2), n(r2(216), t2), n(r2(217), t2), n(r2(218), t2), n(r2(220), t2), n(r2(221), t2), n(r2(222), t2), n(r2(223), t2), n(r2(225), t2), n(r2(227), t2), n(r2(229), t2), n(r2(235), t2), n(r2(236), t2), n(r2(252), t2), n(r2(253), t2), n(r2(255), t2);
    var s = r2(256);
    Object.defineProperty(t2, "AvatarSynthesisAdapter", { enumerable: true, get: function() {
      return s.AvatarSynthesisAdapter;
    } });
    var o = r2(257);
    Object.defineProperty(t2, "SpeechSynthesisAdapter", { enumerable: true, get: function() {
      return o.SpeechSynthesisAdapter;
    } }), n(r2(258), t2), n(r2(259), t2), n(r2(260), t2), n(r2(261), t2), t2.OutputFormatPropertyName = "OutputFormat", t2.CancellationErrorCodePropertyName = "CancellationErrorCode", t2.ServicePropertiesPropertyName = "ServiceProperties", t2.ForceDictationPropertyName = "ForceDictation", t2.AutoDetectSourceLanguagesOpenRangeOptionName = "UND";
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CognitiveSubscriptionKeyAuthentication = void 0;
    const i2 = r2(4), n = r2(54), s = r2(55);
    t2.CognitiveSubscriptionKeyAuthentication = class {
      constructor(e3) {
        if (!e3) throw new i2.ArgumentNullError("subscriptionKey");
        this.privAuthInfo = new s.AuthInfo(n.HeaderNames.AuthKey, e3);
      }
      fetch(e3) {
        return Promise.resolve(this.privAuthInfo);
      }
      fetchOnExpiry(e3) {
        return Promise.resolve(this.privAuthInfo);
      }
    };
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__exportStar || function(e3, t3) {
      for (var r3 in e3) "default" === r3 || Object.prototype.hasOwnProperty.call(t3, r3) || i2(t3, e3, r3);
    };
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationStatus = void 0, n(r2(5), t2), n(r2(24), t2), n(r2(25), t2), n(r2(27), t2), n(r2(28), t2), n(r2(29), t2), n(r2(26), t2), n(r2(30), t2), n(r2(31), t2), n(r2(7), t2), n(r2(32), t2), n(r2(33), t2), n(r2(34), t2), n(r2(35), t2), n(r2(36), t2), n(r2(37), t2), n(r2(38), t2), n(r2(39), t2), n(r2(40), t2), n(r2(41), t2), n(r2(42), t2), n(r2(6), t2), n(r2(43), t2), n(r2(44), t2), n(r2(45), t2), n(r2(46), t2), n(r2(47), t2);
    var s = r2(48);
    Object.defineProperty(t2, "TranslationStatus", { enumerable: true, get: function() {
      return s.TranslationStatus;
    } }), n(r2(49), t2), n(r2(50), t2), n(r2(51), t2), n(r2(52), t2), n(r2(53), t2);
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AudioStreamNodeErrorEvent = t2.AudioStreamNodeDetachedEvent = t2.AudioStreamNodeAttachedEvent = t2.AudioStreamNodeAttachingEvent = t2.AudioStreamNodeEvent = t2.AudioSourceErrorEvent = t2.AudioSourceOffEvent = t2.AudioSourceReadyEvent = t2.AudioSourceInitializingEvent = t2.AudioSourceEvent = void 0;
    const i2 = r2(6);
    class n extends i2.PlatformEvent {
      constructor(e3, t3, r3 = i2.EventType.Info) {
        super(e3, r3), this.privAudioSourceId = t3;
      }
      get audioSourceId() {
        return this.privAudioSourceId;
      }
    }
    t2.AudioSourceEvent = n;
    t2.AudioSourceInitializingEvent = class extends n {
      constructor(e3) {
        super("AudioSourceInitializingEvent", e3);
      }
    };
    t2.AudioSourceReadyEvent = class extends n {
      constructor(e3) {
        super("AudioSourceReadyEvent", e3);
      }
    };
    t2.AudioSourceOffEvent = class extends n {
      constructor(e3) {
        super("AudioSourceOffEvent", e3);
      }
    };
    t2.AudioSourceErrorEvent = class extends n {
      constructor(e3, t3) {
        super("AudioSourceErrorEvent", e3, i2.EventType.Error), this.privError = t3;
      }
      get error() {
        return this.privError;
      }
    };
    class s extends n {
      constructor(e3, t3, r3) {
        super(e3, t3), this.privAudioNodeId = r3;
      }
      get audioNodeId() {
        return this.privAudioNodeId;
      }
    }
    t2.AudioStreamNodeEvent = s;
    t2.AudioStreamNodeAttachingEvent = class extends s {
      constructor(e3, t3) {
        super("AudioStreamNodeAttachingEvent", e3, t3);
      }
    };
    t2.AudioStreamNodeAttachedEvent = class extends s {
      constructor(e3, t3) {
        super("AudioStreamNodeAttachedEvent", e3, t3);
      }
    };
    t2.AudioStreamNodeDetachedEvent = class extends s {
      constructor(e3, t3) {
        super("AudioStreamNodeDetachedEvent", e3, t3);
      }
    };
    t2.AudioStreamNodeErrorEvent = class extends s {
      constructor(e3, t3, r3) {
        super("AudioStreamNodeErrorEvent", e3, t3), this.privError = r3;
      }
      get error() {
        return this.privError;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PlatformEvent = t2.EventType = void 0;
    const i2 = r2(7);
    !function(e3) {
      e3[e3.Debug = 0] = "Debug", e3[e3.Info = 1] = "Info", e3[e3.Warning = 2] = "Warning", e3[e3.Error = 3] = "Error", e3[e3.None = 4] = "None";
    }(t2.EventType || (t2.EventType = {}));
    t2.PlatformEvent = class {
      constructor(e3, t3) {
        this.privName = e3, this.privEventId = (0, i2.createNoDashGuid)(), this.privEventTime = (/* @__PURE__ */ new Date()).toISOString(), this.privEventType = t3, this.privMetadata = {};
      }
      get name() {
        return this.privName;
      }
      get eventId() {
        return this.privEventId;
      }
      get eventTime() {
        return this.privEventTime;
      }
      get eventType() {
        return this.privEventType;
      }
      get metadata() {
        return this.privMetadata;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.createNoDashGuid = t2.createGuid = void 0;
    const i2 = r2(8), n = () => (0, i2.v4)();
    t2.createGuid = n;
    t2.createNoDashGuid = () => n().replace(new RegExp("-", "g"), "").toUpperCase();
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), Object.defineProperty(t2, "NIL", { enumerable: true, get: function() {
      return a.default;
    } }), Object.defineProperty(t2, "parse", { enumerable: true, get: function() {
      return u.default;
    } }), Object.defineProperty(t2, "stringify", { enumerable: true, get: function() {
      return h.default;
    } }), Object.defineProperty(t2, "v1", { enumerable: true, get: function() {
      return i2.default;
    } }), Object.defineProperty(t2, "v3", { enumerable: true, get: function() {
      return n.default;
    } }), Object.defineProperty(t2, "v4", { enumerable: true, get: function() {
      return s.default;
    } }), Object.defineProperty(t2, "v5", { enumerable: true, get: function() {
      return o.default;
    } }), Object.defineProperty(t2, "validate", { enumerable: true, get: function() {
      return p.default;
    } }), Object.defineProperty(t2, "version", { enumerable: true, get: function() {
      return c.default;
    } });
    var i2 = d(r2(9)), n = d(r2(14)), s = d(r2(18)), o = d(r2(20)), a = d(r2(22)), c = d(r2(23)), p = d(r2(12)), h = d(r2(11)), u = d(r2(16));
    function d(e3) {
      return e3 && e3.__esModule ? e3 : { default: e3 };
    }
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2, n = (i2 = r2(10)) && i2.__esModule ? i2 : { default: i2 }, s = r2(11);
    let o, a, c = 0, p = 0;
    var h = function(e3, t3, r3) {
      let i3 = t3 && r3 || 0;
      const h2 = t3 || new Array(16);
      let u = (e3 = e3 || {}).node || o, d = void 0 !== e3.clockseq ? e3.clockseq : a;
      if (null == u || null == d) {
        const t4 = e3.random || (e3.rng || n.default)();
        null == u && (u = o = [1 | t4[0], t4[1], t4[2], t4[3], t4[4], t4[5]]), null == d && (d = a = 16383 & (t4[6] << 8 | t4[7]));
      }
      let v = void 0 !== e3.msecs ? e3.msecs : Date.now(), l = void 0 !== e3.nsecs ? e3.nsecs : p + 1;
      const g = v - c + (l - p) / 1e4;
      if (g < 0 && void 0 === e3.clockseq && (d = d + 1 & 16383), (g < 0 || v > c) && void 0 === e3.nsecs && (l = 0), l >= 1e4) throw new Error("uuid.v1(): Can't create more than 10M uuids/sec");
      c = v, p = l, a = d, v += 122192928e5;
      const m = (1e4 * (268435455 & v) + l) % 4294967296;
      h2[i3++] = m >>> 24 & 255, h2[i3++] = m >>> 16 & 255, h2[i3++] = m >>> 8 & 255, h2[i3++] = 255 & m;
      const S = v / 4294967296 * 1e4 & 268435455;
      h2[i3++] = S >>> 8 & 255, h2[i3++] = 255 & S, h2[i3++] = S >>> 24 & 15 | 16, h2[i3++] = S >>> 16 & 255, h2[i3++] = d >>> 8 | 128, h2[i3++] = 255 & d;
      for (let e4 = 0; e4 < 6; ++e4) h2[i3 + e4] = u[e4];
      return t3 || (0, s.unsafeStringify)(h2);
    };
    t2.default = h;
  }, (e2, t2) => {
    "use strict";
    let r2;
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = function() {
      if (!r2 && (r2 = "undefined" != typeof crypto && crypto.getRandomValues && crypto.getRandomValues.bind(crypto), !r2)) throw new Error("crypto.getRandomValues() not supported. See https://github.com/uuidjs/uuid#getrandomvalues-not-supported");
      return r2(i2);
    };
    const i2 = new Uint8Array(16);
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0, t2.unsafeStringify = o;
    var i2, n = (i2 = r2(12)) && i2.__esModule ? i2 : { default: i2 };
    const s = [];
    for (let e3 = 0; e3 < 256; ++e3) s.push((e3 + 256).toString(16).slice(1));
    function o(e3, t3 = 0) {
      return (s[e3[t3 + 0]] + s[e3[t3 + 1]] + s[e3[t3 + 2]] + s[e3[t3 + 3]] + "-" + s[e3[t3 + 4]] + s[e3[t3 + 5]] + "-" + s[e3[t3 + 6]] + s[e3[t3 + 7]] + "-" + s[e3[t3 + 8]] + s[e3[t3 + 9]] + "-" + s[e3[t3 + 10]] + s[e3[t3 + 11]] + s[e3[t3 + 12]] + s[e3[t3 + 13]] + s[e3[t3 + 14]] + s[e3[t3 + 15]]).toLowerCase();
    }
    var a = function(e3, t3 = 0) {
      const r3 = o(e3, t3);
      if (!(0, n.default)(r3)) throw TypeError("Stringified UUID is invalid");
      return r3;
    };
    t2.default = a;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2, n = (i2 = r2(13)) && i2.__esModule ? i2 : { default: i2 };
    var s = function(e3) {
      return "string" == typeof e3 && n.default.test(e3);
    };
    t2.default = s;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    t2.default = /^(?:[0-9a-f]{8}-[0-9a-f]{4}-[1-5][0-9a-f]{3}-[89ab][0-9a-f]{3}-[0-9a-f]{12}|00000000-0000-0000-0000-000000000000)$/i;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2 = s(r2(15)), n = s(r2(17));
    function s(e3) {
      return e3 && e3.__esModule ? e3 : { default: e3 };
    }
    var o = (0, i2.default)("v3", 48, n.default);
    t2.default = o;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.URL = t2.DNS = void 0, t2.default = function(e3, t3, r3) {
      function i3(e4, i4, o2, a2) {
        var c;
        if ("string" == typeof e4 && (e4 = function(e5) {
          e5 = unescape(encodeURIComponent(e5));
          const t4 = [];
          for (let r4 = 0; r4 < e5.length; ++r4) t4.push(e5.charCodeAt(r4));
          return t4;
        }(e4)), "string" == typeof i4 && (i4 = (0, s.default)(i4)), 16 !== (null === (c = i4) || void 0 === c ? void 0 : c.length)) throw TypeError("Namespace must be array-like (16 iterable integer values, 0-255)");
        let p = new Uint8Array(16 + e4.length);
        if (p.set(i4), p.set(e4, i4.length), p = r3(p), p[6] = 15 & p[6] | t3, p[8] = 63 & p[8] | 128, o2) {
          a2 = a2 || 0;
          for (let e5 = 0; e5 < 16; ++e5) o2[a2 + e5] = p[e5];
          return o2;
        }
        return (0, n.unsafeStringify)(p);
      }
      try {
        i3.name = e3;
      } catch (e4) {
      }
      return i3.DNS = o, i3.URL = a, i3;
    };
    var i2, n = r2(11), s = (i2 = r2(16)) && i2.__esModule ? i2 : { default: i2 };
    const o = "6ba7b810-9dad-11d1-80b4-00c04fd430c8";
    t2.DNS = o;
    const a = "6ba7b811-9dad-11d1-80b4-00c04fd430c8";
    t2.URL = a;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2, n = (i2 = r2(12)) && i2.__esModule ? i2 : { default: i2 };
    var s = function(e3) {
      if (!(0, n.default)(e3)) throw TypeError("Invalid UUID");
      let t3;
      const r3 = new Uint8Array(16);
      return r3[0] = (t3 = parseInt(e3.slice(0, 8), 16)) >>> 24, r3[1] = t3 >>> 16 & 255, r3[2] = t3 >>> 8 & 255, r3[3] = 255 & t3, r3[4] = (t3 = parseInt(e3.slice(9, 13), 16)) >>> 8, r3[5] = 255 & t3, r3[6] = (t3 = parseInt(e3.slice(14, 18), 16)) >>> 8, r3[7] = 255 & t3, r3[8] = (t3 = parseInt(e3.slice(19, 23), 16)) >>> 8, r3[9] = 255 & t3, r3[10] = (t3 = parseInt(e3.slice(24, 36), 16)) / 1099511627776 & 255, r3[11] = t3 / 4294967296 & 255, r3[12] = t3 >>> 24 & 255, r3[13] = t3 >>> 16 & 255, r3[14] = t3 >>> 8 & 255, r3[15] = 255 & t3, r3;
    };
    t2.default = s;
  }, (e2, t2) => {
    "use strict";
    function r2(e3) {
      return 14 + (e3 + 64 >>> 9 << 4) + 1;
    }
    function i2(e3, t3) {
      const r3 = (65535 & e3) + (65535 & t3);
      return (e3 >> 16) + (t3 >> 16) + (r3 >> 16) << 16 | 65535 & r3;
    }
    function n(e3, t3, r3, n2, s2, o2) {
      return i2((a2 = i2(i2(t3, e3), i2(n2, o2))) << (c2 = s2) | a2 >>> 32 - c2, r3);
      var a2, c2;
    }
    function s(e3, t3, r3, i3, s2, o2, a2) {
      return n(t3 & r3 | ~t3 & i3, e3, t3, s2, o2, a2);
    }
    function o(e3, t3, r3, i3, s2, o2, a2) {
      return n(t3 & i3 | r3 & ~i3, e3, t3, s2, o2, a2);
    }
    function a(e3, t3, r3, i3, s2, o2, a2) {
      return n(t3 ^ r3 ^ i3, e3, t3, s2, o2, a2);
    }
    function c(e3, t3, r3, i3, s2, o2, a2) {
      return n(r3 ^ (t3 | ~i3), e3, t3, s2, o2, a2);
    }
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var p = function(e3) {
      if ("string" == typeof e3) {
        const t3 = unescape(encodeURIComponent(e3));
        e3 = new Uint8Array(t3.length);
        for (let r3 = 0; r3 < t3.length; ++r3) e3[r3] = t3.charCodeAt(r3);
      }
      return function(e4) {
        const t3 = [], r3 = 32 * e4.length, i3 = "0123456789abcdef";
        for (let n2 = 0; n2 < r3; n2 += 8) {
          const r4 = e4[n2 >> 5] >>> n2 % 32 & 255, s2 = parseInt(i3.charAt(r4 >>> 4 & 15) + i3.charAt(15 & r4), 16);
          t3.push(s2);
        }
        return t3;
      }(function(e4, t3) {
        e4[t3 >> 5] |= 128 << t3 % 32, e4[r2(t3) - 1] = t3;
        let n2 = 1732584193, p2 = -271733879, h = -1732584194, u = 271733878;
        for (let t4 = 0; t4 < e4.length; t4 += 16) {
          const r3 = n2, d = p2, v = h, l = u;
          n2 = s(n2, p2, h, u, e4[t4], 7, -680876936), u = s(u, n2, p2, h, e4[t4 + 1], 12, -389564586), h = s(h, u, n2, p2, e4[t4 + 2], 17, 606105819), p2 = s(p2, h, u, n2, e4[t4 + 3], 22, -1044525330), n2 = s(n2, p2, h, u, e4[t4 + 4], 7, -176418897), u = s(u, n2, p2, h, e4[t4 + 5], 12, 1200080426), h = s(h, u, n2, p2, e4[t4 + 6], 17, -1473231341), p2 = s(p2, h, u, n2, e4[t4 + 7], 22, -45705983), n2 = s(n2, p2, h, u, e4[t4 + 8], 7, 1770035416), u = s(u, n2, p2, h, e4[t4 + 9], 12, -1958414417), h = s(h, u, n2, p2, e4[t4 + 10], 17, -42063), p2 = s(p2, h, u, n2, e4[t4 + 11], 22, -1990404162), n2 = s(n2, p2, h, u, e4[t4 + 12], 7, 1804603682), u = s(u, n2, p2, h, e4[t4 + 13], 12, -40341101), h = s(h, u, n2, p2, e4[t4 + 14], 17, -1502002290), p2 = s(p2, h, u, n2, e4[t4 + 15], 22, 1236535329), n2 = o(n2, p2, h, u, e4[t4 + 1], 5, -165796510), u = o(u, n2, p2, h, e4[t4 + 6], 9, -1069501632), h = o(h, u, n2, p2, e4[t4 + 11], 14, 643717713), p2 = o(p2, h, u, n2, e4[t4], 20, -373897302), n2 = o(n2, p2, h, u, e4[t4 + 5], 5, -701558691), u = o(u, n2, p2, h, e4[t4 + 10], 9, 38016083), h = o(h, u, n2, p2, e4[t4 + 15], 14, -660478335), p2 = o(p2, h, u, n2, e4[t4 + 4], 20, -405537848), n2 = o(n2, p2, h, u, e4[t4 + 9], 5, 568446438), u = o(u, n2, p2, h, e4[t4 + 14], 9, -1019803690), h = o(h, u, n2, p2, e4[t4 + 3], 14, -187363961), p2 = o(p2, h, u, n2, e4[t4 + 8], 20, 1163531501), n2 = o(n2, p2, h, u, e4[t4 + 13], 5, -1444681467), u = o(u, n2, p2, h, e4[t4 + 2], 9, -51403784), h = o(h, u, n2, p2, e4[t4 + 7], 14, 1735328473), p2 = o(p2, h, u, n2, e4[t4 + 12], 20, -1926607734), n2 = a(n2, p2, h, u, e4[t4 + 5], 4, -378558), u = a(u, n2, p2, h, e4[t4 + 8], 11, -2022574463), h = a(h, u, n2, p2, e4[t4 + 11], 16, 1839030562), p2 = a(p2, h, u, n2, e4[t4 + 14], 23, -35309556), n2 = a(n2, p2, h, u, e4[t4 + 1], 4, -1530992060), u = a(u, n2, p2, h, e4[t4 + 4], 11, 1272893353), h = a(h, u, n2, p2, e4[t4 + 7], 16, -155497632), p2 = a(p2, h, u, n2, e4[t4 + 10], 23, -1094730640), n2 = a(n2, p2, h, u, e4[t4 + 13], 4, 681279174), u = a(u, n2, p2, h, e4[t4], 11, -358537222), h = a(h, u, n2, p2, e4[t4 + 3], 16, -722521979), p2 = a(p2, h, u, n2, e4[t4 + 6], 23, 76029189), n2 = a(n2, p2, h, u, e4[t4 + 9], 4, -640364487), u = a(u, n2, p2, h, e4[t4 + 12], 11, -421815835), h = a(h, u, n2, p2, e4[t4 + 15], 16, 530742520), p2 = a(p2, h, u, n2, e4[t4 + 2], 23, -995338651), n2 = c(n2, p2, h, u, e4[t4], 6, -198630844), u = c(u, n2, p2, h, e4[t4 + 7], 10, 1126891415), h = c(h, u, n2, p2, e4[t4 + 14], 15, -1416354905), p2 = c(p2, h, u, n2, e4[t4 + 5], 21, -57434055), n2 = c(n2, p2, h, u, e4[t4 + 12], 6, 1700485571), u = c(u, n2, p2, h, e4[t4 + 3], 10, -1894986606), h = c(h, u, n2, p2, e4[t4 + 10], 15, -1051523), p2 = c(p2, h, u, n2, e4[t4 + 1], 21, -2054922799), n2 = c(n2, p2, h, u, e4[t4 + 8], 6, 1873313359), u = c(u, n2, p2, h, e4[t4 + 15], 10, -30611744), h = c(h, u, n2, p2, e4[t4 + 6], 15, -1560198380), p2 = c(p2, h, u, n2, e4[t4 + 13], 21, 1309151649), n2 = c(n2, p2, h, u, e4[t4 + 4], 6, -145523070), u = c(u, n2, p2, h, e4[t4 + 11], 10, -1120210379), h = c(h, u, n2, p2, e4[t4 + 2], 15, 718787259), p2 = c(p2, h, u, n2, e4[t4 + 9], 21, -343485551), n2 = i2(n2, r3), p2 = i2(p2, d), h = i2(h, v), u = i2(u, l);
        }
        return [n2, p2, h, u];
      }(function(e4) {
        if (0 === e4.length) return [];
        const t3 = 8 * e4.length, i3 = new Uint32Array(r2(t3));
        for (let r3 = 0; r3 < t3; r3 += 8) i3[r3 >> 5] |= (255 & e4[r3 / 8]) << r3 % 32;
        return i3;
      }(e3), 8 * e3.length));
    };
    t2.default = p;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2 = o(r2(19)), n = o(r2(10)), s = r2(11);
    function o(e3) {
      return e3 && e3.__esModule ? e3 : { default: e3 };
    }
    var a = function(e3, t3, r3) {
      if (i2.default.randomUUID && !t3 && !e3) return i2.default.randomUUID();
      const o2 = (e3 = e3 || {}).random || (e3.rng || n.default)();
      if (o2[6] = 15 & o2[6] | 64, o2[8] = 63 & o2[8] | 128, t3) {
        r3 = r3 || 0;
        for (let e4 = 0; e4 < 16; ++e4) t3[r3 + e4] = o2[e4];
        return t3;
      }
      return (0, s.unsafeStringify)(o2);
    };
    t2.default = a;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var r2 = { randomUUID: "undefined" != typeof crypto && crypto.randomUUID && crypto.randomUUID.bind(crypto) };
    t2.default = r2;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2 = s(r2(15)), n = s(r2(21));
    function s(e3) {
      return e3 && e3.__esModule ? e3 : { default: e3 };
    }
    var o = (0, i2.default)("v5", 80, n.default);
    t2.default = o;
  }, (e2, t2) => {
    "use strict";
    function r2(e3, t3, r3, i3) {
      switch (e3) {
        case 0:
          return t3 & r3 ^ ~t3 & i3;
        case 1:
        case 3:
          return t3 ^ r3 ^ i3;
        case 2:
          return t3 & r3 ^ t3 & i3 ^ r3 & i3;
      }
    }
    function i2(e3, t3) {
      return e3 << t3 | e3 >>> 32 - t3;
    }
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var n = function(e3) {
      const t3 = [1518500249, 1859775393, 2400959708, 3395469782], n2 = [1732584193, 4023233417, 2562383102, 271733878, 3285377520];
      if ("string" == typeof e3) {
        const t4 = unescape(encodeURIComponent(e3));
        e3 = [];
        for (let r3 = 0; r3 < t4.length; ++r3) e3.push(t4.charCodeAt(r3));
      } else Array.isArray(e3) || (e3 = Array.prototype.slice.call(e3));
      e3.push(128);
      const s = e3.length / 4 + 2, o = Math.ceil(s / 16), a = new Array(o);
      for (let t4 = 0; t4 < o; ++t4) {
        const r3 = new Uint32Array(16);
        for (let i3 = 0; i3 < 16; ++i3) r3[i3] = e3[64 * t4 + 4 * i3] << 24 | e3[64 * t4 + 4 * i3 + 1] << 16 | e3[64 * t4 + 4 * i3 + 2] << 8 | e3[64 * t4 + 4 * i3 + 3];
        a[t4] = r3;
      }
      a[o - 1][14] = 8 * (e3.length - 1) / Math.pow(2, 32), a[o - 1][14] = Math.floor(a[o - 1][14]), a[o - 1][15] = 8 * (e3.length - 1) & 4294967295;
      for (let e4 = 0; e4 < o; ++e4) {
        const s2 = new Uint32Array(80);
        for (let t4 = 0; t4 < 16; ++t4) s2[t4] = a[e4][t4];
        for (let e5 = 16; e5 < 80; ++e5) s2[e5] = i2(s2[e5 - 3] ^ s2[e5 - 8] ^ s2[e5 - 14] ^ s2[e5 - 16], 1);
        let o2 = n2[0], c = n2[1], p = n2[2], h = n2[3], u = n2[4];
        for (let e5 = 0; e5 < 80; ++e5) {
          const n3 = Math.floor(e5 / 20), a2 = i2(o2, 5) + r2(n3, c, p, h) + u + t3[n3] + s2[e5] >>> 0;
          u = h, h = p, p = i2(c, 30) >>> 0, c = o2, o2 = a2;
        }
        n2[0] = n2[0] + o2 >>> 0, n2[1] = n2[1] + c >>> 0, n2[2] = n2[2] + p >>> 0, n2[3] = n2[3] + h >>> 0, n2[4] = n2[4] + u >>> 0;
      }
      return [n2[0] >> 24 & 255, n2[0] >> 16 & 255, n2[0] >> 8 & 255, 255 & n2[0], n2[1] >> 24 & 255, n2[1] >> 16 & 255, n2[1] >> 8 & 255, 255 & n2[1], n2[2] >> 24 & 255, n2[2] >> 16 & 255, n2[2] >> 8 & 255, 255 & n2[2], n2[3] >> 24 & 255, n2[3] >> 16 & 255, n2[3] >> 8 & 255, 255 & n2[3], n2[4] >> 24 & 255, n2[4] >> 16 & 255, n2[4] >> 8 & 255, 255 & n2[4]];
    };
    t2.default = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    t2.default = "00000000-0000-0000-0000-000000000000";
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.default = void 0;
    var i2, n = (i2 = r2(12)) && i2.__esModule ? i2 : { default: i2 };
    var s = function(e3) {
      if (!(0, n.default)(e3)) throw TypeError("Invalid UUID");
      return parseInt(e3.slice(14, 15), 16);
    };
    t2.default = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionRedirectEvent = t2.ConnectionMessageSentEvent = t2.ConnectionMessageReceivedEvent = t2.ConnectionEstablishErrorEvent = t2.ConnectionErrorEvent = t2.ConnectionClosedEvent = t2.ConnectionEstablishedEvent = t2.ConnectionStartEvent = t2.ConnectionEvent = t2.ServiceEvent = void 0;
    const i2 = r2(6);
    class n extends i2.PlatformEvent {
      constructor(e3, t3, r3 = i2.EventType.Info) {
        super(e3, r3), this.privJsonResult = t3;
      }
      get jsonString() {
        return this.privJsonResult;
      }
    }
    t2.ServiceEvent = n;
    class s extends i2.PlatformEvent {
      constructor(e3, t3, r3 = i2.EventType.Info) {
        super(e3, r3), this.privConnectionId = t3;
      }
      get connectionId() {
        return this.privConnectionId;
      }
    }
    t2.ConnectionEvent = s;
    t2.ConnectionStartEvent = class extends s {
      constructor(e3, t3, r3) {
        super("ConnectionStartEvent", e3), this.privUri = t3, this.privHeaders = r3;
      }
      get uri() {
        return this.privUri;
      }
      get headers() {
        return this.privHeaders;
      }
    };
    t2.ConnectionEstablishedEvent = class extends s {
      constructor(e3) {
        super("ConnectionEstablishedEvent", e3);
      }
    };
    t2.ConnectionClosedEvent = class extends s {
      constructor(e3, t3, r3) {
        super("ConnectionClosedEvent", e3, i2.EventType.Debug), this.privReason = r3, this.privStatusCode = t3;
      }
      get reason() {
        return this.privReason;
      }
      get statusCode() {
        return this.privStatusCode;
      }
    };
    t2.ConnectionErrorEvent = class extends s {
      constructor(e3, t3, r3) {
        super("ConnectionErrorEvent", e3, i2.EventType.Debug), this.privMessage = t3, this.privType = r3;
      }
      get message() {
        return this.privMessage;
      }
      get type() {
        return this.privType;
      }
    };
    t2.ConnectionEstablishErrorEvent = class extends s {
      constructor(e3, t3, r3) {
        super("ConnectionEstablishErrorEvent", e3, i2.EventType.Error), this.privStatusCode = t3, this.privReason = r3;
      }
      get reason() {
        return this.privReason;
      }
      get statusCode() {
        return this.privStatusCode;
      }
    };
    t2.ConnectionMessageReceivedEvent = class extends s {
      constructor(e3, t3, r3) {
        super("ConnectionMessageReceivedEvent", e3), this.privNetworkReceivedTime = t3, this.privMessage = r3;
      }
      get networkReceivedTime() {
        return this.privNetworkReceivedTime;
      }
      get message() {
        return this.privMessage;
      }
    };
    t2.ConnectionMessageSentEvent = class extends s {
      constructor(e3, t3, r3) {
        super("ConnectionMessageSentEvent", e3), this.privNetworkSentTime = t3, this.privMessage = r3;
      }
      get networkSentTime() {
        return this.privNetworkSentTime;
      }
      get message() {
        return this.privMessage;
      }
    };
    t2.ConnectionRedirectEvent = class extends s {
      constructor(e3, t3, r3, n2) {
        super("ConnectionRedirectEvent", e3, i2.EventType.Info), this.privRedirectUrl = t3, this.privOriginalUrl = r3, this.privContext = n2;
      }
      get redirectUrl() {
        return this.privRedirectUrl;
      }
      get originalUrl() {
        return this.privOriginalUrl;
      }
      get context() {
        return this.privContext;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionMessage = t2.MessageType = void 0;
    const i2 = r2(26), n = r2(7);
    var s;
    !function(e3) {
      e3[e3.Text = 0] = "Text", e3[e3.Binary = 1] = "Binary";
    }(s = t2.MessageType || (t2.MessageType = {}));
    t2.ConnectionMessage = class {
      constructor(e3, t3, r3, o) {
        if (this.privBody = null, e3 === s.Text && t3 && "string" != typeof t3) throw new i2.InvalidOperationError("Payload must be a string");
        if (e3 === s.Binary && t3 && !(t3 instanceof ArrayBuffer)) throw new i2.InvalidOperationError("Payload must be ArrayBuffer");
        switch (this.privMessageType = e3, this.privBody = t3, this.privHeaders = r3 || {}, this.privId = o || (0, n.createNoDashGuid)(), this.messageType) {
          case s.Binary:
            this.privSize = null !== this.binaryBody ? this.binaryBody.byteLength : 0;
            break;
          case s.Text:
            this.privSize = this.textBody.length;
        }
      }
      get messageType() {
        return this.privMessageType;
      }
      get headers() {
        return this.privHeaders;
      }
      get body() {
        return this.privBody;
      }
      get textBody() {
        if (this.privMessageType === s.Binary) throw new i2.InvalidOperationError("Not supported for binary message");
        return this.privBody;
      }
      get binaryBody() {
        if (this.privMessageType === s.Text) throw new i2.InvalidOperationError("Not supported for text message");
        return this.privBody;
      }
      get id() {
        return this.privId;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ObjectDisposedError = t2.InvalidOperationError = t2.ArgumentNullError = void 0;
    class r2 extends Error {
      constructor(e3) {
        super(e3), this.name = "ArgumentNull", this.message = e3;
      }
    }
    t2.ArgumentNullError = r2;
    class i2 extends Error {
      constructor(e3) {
        super(e3), this.name = "InvalidOperation", this.message = e3;
      }
    }
    t2.InvalidOperationError = i2;
    class n extends Error {
      constructor(e3, t3) {
        super(t3), this.name = e3 + "ObjectDisposed", this.message = t3;
      }
    }
    t2.ObjectDisposedError = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionOpenResponse = void 0;
    t2.ConnectionOpenResponse = class {
      constructor(e3, t3) {
        this.privStatusCode = e3, this.privReason = t3;
      }
      get statusCode() {
        return this.privStatusCode;
      }
      get reason() {
        return this.privReason;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DeferralMap = void 0;
    t2.DeferralMap = class {
      constructor() {
        this.privMap = {};
      }
      add(e3, t3) {
        this.privMap[e3] = t3;
      }
      getId(e3) {
        return this.privMap[e3];
      }
      complete(e3, t3) {
        try {
          this.privMap[e3].resolve(t3);
        } catch (t4) {
          this.privMap[e3].reject(t4);
        } finally {
          this.privMap[e3] = void 0;
        }
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SendingAgentContextMessageEvent = t2.DialogEvent = void 0;
    const i2 = r2(6);
    class n extends i2.PlatformEvent {
      constructor(e3, t3 = i2.EventType.Info) {
        super(e3, t3);
      }
    }
    t2.DialogEvent = n;
    t2.SendingAgentContextMessageEvent = class extends n {
      constructor(e3) {
        super("SendingAgentContextMessageEvent"), this.privAgentConfig = e3;
      }
      get agentConfig() {
        return this.privAgentConfig;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Events = void 0;
    const i2 = r2(26), n = r2(31);
    class s {
      static setEventSource(e3) {
        if (!e3) throw new i2.ArgumentNullError("eventSource");
        s.privInstance = e3;
      }
      static get instance() {
        return s.privInstance;
      }
    }
    t2.Events = s, s.privInstance = new n.EventSource();
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.EventSource = void 0;
    const i2 = r2(26), n = r2(7);
    t2.EventSource = class {
      constructor(e3) {
        this.privEventListeners = {}, this.privIsDisposed = false, this.privConsoleListener = void 0, this.privMetadata = e3;
      }
      onEvent(e3) {
        if (this.isDisposed()) throw new i2.ObjectDisposedError("EventSource");
        if (this.metadata) for (const t3 in this.metadata) t3 && e3.metadata && (e3.metadata[t3] || (e3.metadata[t3] = this.metadata[t3]));
        for (const t3 in this.privEventListeners) t3 && this.privEventListeners[t3] && this.privEventListeners[t3](e3);
      }
      attach(e3) {
        const t3 = (0, n.createNoDashGuid)();
        return this.privEventListeners[t3] = e3, { detach: () => (delete this.privEventListeners[t3], Promise.resolve()) };
      }
      attachListener(e3) {
        return this.attach((t3) => e3.onEvent(t3));
      }
      attachConsoleListener(e3) {
        return this.privConsoleListener && this.privConsoleListener.detach(), this.privConsoleListener = this.attach((t3) => e3.onEvent(t3)), this.privConsoleListener;
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      dispose() {
        this.privEventListeners = null, this.privIsDisposed = true;
      }
      get metadata() {
        return this.privMetadata;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionState = void 0, function(e3) {
      e3[e3.None = 0] = "None", e3[e3.Connected = 1] = "Connected", e3[e3.Connecting = 2] = "Connecting", e3[e3.Disconnected = 3] = "Disconnected";
    }(t2.ConnectionState || (t2.ConnectionState = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.List = void 0;
    const i2 = r2(26);
    class n {
      constructor(e3) {
        if (this.privSubscriptionIdCounter = 0, this.privAddSubscriptions = {}, this.privRemoveSubscriptions = {}, this.privDisposedSubscriptions = {}, this.privDisposeReason = null, this.privList = [], e3) for (const t3 of e3) this.privList.push(t3);
      }
      get(e3) {
        return this.throwIfDisposed(), this.privList[e3];
      }
      first() {
        return this.get(0);
      }
      last() {
        return this.get(this.length() - 1);
      }
      add(e3) {
        this.throwIfDisposed(), this.insertAt(this.privList.length, e3);
      }
      insertAt(e3, t3) {
        this.throwIfDisposed(), 0 === e3 ? this.privList.unshift(t3) : e3 === this.privList.length ? this.privList.push(t3) : this.privList.splice(e3, 0, t3), this.triggerSubscriptions(this.privAddSubscriptions);
      }
      removeFirst() {
        return this.throwIfDisposed(), this.removeAt(0);
      }
      removeLast() {
        return this.throwIfDisposed(), this.removeAt(this.length() - 1);
      }
      removeAt(e3) {
        return this.throwIfDisposed(), this.remove(e3, 1)[0];
      }
      remove(e3, t3) {
        this.throwIfDisposed();
        const r3 = this.privList.splice(e3, t3);
        return this.triggerSubscriptions(this.privRemoveSubscriptions), r3;
      }
      clear() {
        this.throwIfDisposed(), this.remove(0, this.length());
      }
      length() {
        return this.throwIfDisposed(), this.privList.length;
      }
      onAdded(e3) {
        this.throwIfDisposed();
        const t3 = this.privSubscriptionIdCounter++;
        return this.privAddSubscriptions[t3] = e3, { detach: () => (delete this.privAddSubscriptions[t3], Promise.resolve()) };
      }
      onRemoved(e3) {
        this.throwIfDisposed();
        const t3 = this.privSubscriptionIdCounter++;
        return this.privRemoveSubscriptions[t3] = e3, { detach: () => (delete this.privRemoveSubscriptions[t3], Promise.resolve()) };
      }
      onDisposed(e3) {
        this.throwIfDisposed();
        const t3 = this.privSubscriptionIdCounter++;
        return this.privDisposedSubscriptions[t3] = e3, { detach: () => (delete this.privDisposedSubscriptions[t3], Promise.resolve()) };
      }
      join(e3) {
        return this.throwIfDisposed(), this.privList.join(e3);
      }
      toArray() {
        const e3 = Array();
        return this.privList.forEach((t3) => {
          e3.push(t3);
        }), e3;
      }
      any(e3) {
        return this.throwIfDisposed(), e3 ? this.where(e3).length() > 0 : this.length() > 0;
      }
      all(e3) {
        return this.throwIfDisposed(), this.where(e3).length() === this.length();
      }
      forEach(e3) {
        this.throwIfDisposed();
        for (let t3 = 0; t3 < this.length(); t3++) e3(this.privList[t3], t3);
      }
      select(e3) {
        this.throwIfDisposed();
        const t3 = [];
        for (let r3 = 0; r3 < this.privList.length; r3++) t3.push(e3(this.privList[r3], r3));
        return new n(t3);
      }
      where(e3) {
        this.throwIfDisposed();
        const t3 = new n();
        for (let r3 = 0; r3 < this.privList.length; r3++) e3(this.privList[r3], r3) && t3.add(this.privList[r3]);
        return t3;
      }
      orderBy(e3) {
        this.throwIfDisposed();
        const t3 = this.toArray().sort(e3);
        return new n(t3);
      }
      orderByDesc(e3) {
        return this.throwIfDisposed(), this.orderBy((t3, r3) => e3(r3, t3));
      }
      clone() {
        return this.throwIfDisposed(), new n(this.toArray());
      }
      concat(e3) {
        return this.throwIfDisposed(), new n(this.privList.concat(e3.toArray()));
      }
      concatArray(e3) {
        return this.throwIfDisposed(), new n(this.privList.concat(e3));
      }
      isDisposed() {
        return null == this.privList;
      }
      dispose(e3) {
        this.isDisposed() || (this.privDisposeReason = e3, this.privList = null, this.privAddSubscriptions = null, this.privRemoveSubscriptions = null, this.triggerSubscriptions(this.privDisposedSubscriptions));
      }
      throwIfDisposed() {
        if (this.isDisposed()) throw new i2.ObjectDisposedError("List", this.privDisposeReason);
      }
      triggerSubscriptions(e3) {
        if (e3) for (const t3 in e3) t3 && e3[t3]();
      }
    }
    t2.List = n;
  }, (e2, t2) => {
    "use strict";
    var r2;
    Object.defineProperty(t2, "__esModule", { value: true }), t2.marshalPromiseToCallbacks = t2.Sink = t2.Deferred = t2.PromiseResultEventSource = t2.PromiseResult = t2.PromiseState = void 0, function(e3) {
      e3[e3.None = 0] = "None", e3[e3.Resolved = 1] = "Resolved", e3[e3.Rejected = 2] = "Rejected";
    }(r2 = t2.PromiseState || (t2.PromiseState = {}));
    class i2 {
      constructor(e3) {
        this.throwIfError = () => {
          if (this.isError) throw this.error;
        }, e3.on((e4) => {
          this.privIsCompleted || (this.privIsCompleted = true, this.privIsError = false, this.privResult = e4);
        }, (e4) => {
          this.privIsCompleted || (this.privIsCompleted = true, this.privIsError = true, this.privError = e4);
        });
      }
      get isCompleted() {
        return this.privIsCompleted;
      }
      get isError() {
        return this.privIsError;
      }
      get error() {
        return this.privError;
      }
      get result() {
        return this.privResult;
      }
    }
    t2.PromiseResult = i2;
    class n {
      constructor() {
        this.setResult = (e3) => {
          this.privOnSetResult(e3);
        }, this.setError = (e3) => {
          this.privOnSetError(e3);
        }, this.on = (e3, t3) => {
          this.privOnSetResult = e3, this.privOnSetError = t3;
        };
      }
    }
    t2.PromiseResultEventSource = n;
    t2.Deferred = class {
      constructor() {
        this.resolve = (e3) => (this.privResolve(e3), this), this.reject = (e3) => (this.privReject(e3), this), this.privPromise = new Promise((e3, t3) => {
          this.privResolve = e3, this.privReject = t3;
        });
      }
      get promise() {
        return this.privPromise;
      }
    };
    t2.Sink = class {
      constructor() {
        this.privState = r2.None, this.privPromiseResult = null, this.privPromiseResultEvents = null, this.privSuccessHandlers = [], this.privErrorHandlers = [], this.privPromiseResultEvents = new n(), this.privPromiseResult = new i2(this.privPromiseResultEvents);
      }
      get state() {
        return this.privState;
      }
      get result() {
        return this.privPromiseResult;
      }
      resolve(e3) {
        if (this.privState !== r2.None) throw new Error("'Cannot resolve a completed promise'");
        this.privState = r2.Resolved, this.privPromiseResultEvents.setResult(e3);
        for (let t3 = 0; t3 < this.privSuccessHandlers.length; t3++) this.executeSuccessCallback(e3, this.privSuccessHandlers[t3], this.privErrorHandlers[t3]);
        this.detachHandlers();
      }
      reject(e3) {
        if (this.privState !== r2.None) throw new Error("'Cannot reject a completed promise'");
        this.privState = r2.Rejected, this.privPromiseResultEvents.setError(e3);
        for (const t3 of this.privErrorHandlers) this.executeErrorCallback(e3, t3);
        this.detachHandlers();
      }
      on(e3, t3) {
        null == e3 && (e3 = () => {
        }), this.privState === r2.None ? (this.privSuccessHandlers.push(e3), this.privErrorHandlers.push(t3)) : (this.privState === r2.Resolved ? this.executeSuccessCallback(this.privPromiseResult.result, e3, t3) : this.privState === r2.Rejected && this.executeErrorCallback(this.privPromiseResult.error, t3), this.detachHandlers());
      }
      executeSuccessCallback(e3, t3, r3) {
        try {
          t3(e3);
        } catch (e4) {
          this.executeErrorCallback(`'Unhandled callback error: ${e4}'`, r3);
        }
      }
      executeErrorCallback(e3, t3) {
        if (!t3) throw new Error(`'Unhandled error: ${e3}'`);
        try {
          t3(e3);
        } catch (t4) {
          throw new Error(`'Unhandled callback error: ${t4}. InnerError: ${e3}'`);
        }
      }
      detachHandlers() {
        this.privErrorHandlers = [], this.privSuccessHandlers = [];
      }
    }, t2.marshalPromiseToCallbacks = function(e3, t3, r3) {
      e3.then((e4) => {
        try {
          t3 && t3(e4);
        } catch (e5) {
          if (r3) try {
            if (e5 instanceof Error) {
              const t4 = e5;
              r3(t4.name + ": " + t4.message);
            } else r3(e5);
          } catch (e6) {
          }
        }
      }, (e4) => {
        if (r3) try {
          if (e4 instanceof Error) {
            const t4 = e4;
            r3(t4.name + ": " + t4.message);
          } else r3(e4);
        } catch (e5) {
        }
      });
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Queue = void 0;
    const i2 = r2(26), n = r2(42), s = r2(43);
    var o;
    !function(e3) {
      e3[e3.Dequeue = 0] = "Dequeue", e3[e3.Peek = 1] = "Peek";
    }(o || (o = {}));
    t2.Queue = class {
      constructor(e3) {
        this.privPromiseStore = new n.List(), this.privIsDrainInProgress = false, this.privIsDisposing = false, this.privDisposeReason = null, this.privList = e3 || new n.List(), this.privDetachables = [], this.privSubscribers = new n.List(), this.privDetachables.push(this.privList.onAdded(() => this.drain()));
      }
      enqueue(e3) {
        this.throwIfDispose(), this.enqueueFromPromise(new Promise((t3) => t3(e3)));
      }
      enqueueFromPromise(e3) {
        this.throwIfDispose(), e3.then((e4) => {
          this.privList.add(e4);
        }, () => {
        });
      }
      dequeue() {
        this.throwIfDispose();
        const e3 = new s.Deferred();
        return this.privSubscribers && (this.privSubscribers.add({ deferral: e3, type: o.Dequeue }), this.drain()), e3.promise;
      }
      peek() {
        this.throwIfDispose();
        const e3 = new s.Deferred();
        return this.privSubscribers && (this.privSubscribers.add({ deferral: e3, type: o.Peek }), this.drain()), e3.promise;
      }
      length() {
        return this.throwIfDispose(), this.privList.length();
      }
      isDisposed() {
        return null == this.privSubscribers;
      }
      async drainAndDispose(e3, t3) {
        if (!this.isDisposed() && !this.privIsDisposing) {
          this.privDisposeReason = t3, this.privIsDisposing = true;
          const r3 = this.privSubscribers;
          if (r3) {
            for (; r3.length() > 0; ) {
              r3.removeFirst().deferral.resolve(void 0);
            }
            this.privSubscribers === r3 && (this.privSubscribers = r3);
          }
          for (const e4 of this.privDetachables) await e4.detach();
          if (this.privPromiseStore.length() > 0 && e3) {
            const t4 = [];
            return this.privPromiseStore.toArray().forEach((e4) => {
              t4.push(e4);
            }), Promise.all(t4).finally(() => {
              this.privSubscribers = null, this.privList.forEach((t5) => {
                e3(t5);
              }), this.privList = null;
            }).then();
          }
          this.privSubscribers = null, this.privList = null;
        }
      }
      async dispose(e3) {
        await this.drainAndDispose(null, e3);
      }
      drain() {
        if (!this.privIsDrainInProgress && !this.privIsDisposing) {
          this.privIsDrainInProgress = true;
          const e3 = this.privSubscribers, t3 = this.privList;
          if (e3 && t3) {
            for (; t3.length() > 0 && e3.length() > 0 && !this.privIsDisposing; ) {
              const r3 = e3.removeFirst();
              if (r3.type === o.Peek) r3.deferral.resolve(t3.first());
              else {
                const e4 = t3.removeFirst();
                r3.deferral.resolve(e4);
              }
            }
            this.privSubscribers === e3 && (this.privSubscribers = e3), this.privList === t3 && (this.privList = t3);
          }
          this.privIsDrainInProgress = false;
        }
      }
      throwIfDispose() {
        if (this.isDisposed()) {
          if (this.privDisposeReason) throw new i2.InvalidOperationError(this.privDisposeReason);
          throw new i2.ObjectDisposedError("Queue");
        }
        if (this.privIsDisposing) throw new i2.InvalidOperationError("Queue disposing");
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RawWebsocketMessage = void 0;
    const i2 = r2(25), n = r2(26), s = r2(7);
    t2.RawWebsocketMessage = class {
      constructor(e3, t3, r3) {
        if (this.privPayload = null, !t3) throw new n.ArgumentNullError("payload");
        if (e3 === i2.MessageType.Binary && "ArrayBuffer" !== Object.getPrototypeOf(t3).constructor.name) throw new n.InvalidOperationError("Payload must be ArrayBuffer");
        if (e3 === i2.MessageType.Text && "string" != typeof t3) throw new n.InvalidOperationError("Payload must be a string");
        this.privMessageType = e3, this.privPayload = t3, this.privId = r3 || (0, s.createNoDashGuid)();
      }
      get messageType() {
        return this.privMessageType;
      }
      get payload() {
        return this.privPayload;
      }
      get textContent() {
        if (this.privMessageType === i2.MessageType.Binary) throw new n.InvalidOperationError("Not supported for binary message");
        return this.privPayload;
      }
      get binaryContent() {
        if (this.privMessageType === i2.MessageType.Text) throw new n.InvalidOperationError("Not supported for text message");
        return this.privPayload;
      }
      get id() {
        return this.privId;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RiffPcmEncoder = void 0;
    t2.RiffPcmEncoder = class {
      constructor(e3, t3) {
        this.privActualSampleRate = e3, this.privDesiredSampleRate = t3;
      }
      encode(e3) {
        const t3 = this.downSampleAudioFrame(e3, this.privActualSampleRate, this.privDesiredSampleRate);
        if (!t3) return null;
        const r2 = 2 * t3.length, i2 = new ArrayBuffer(r2), n = new DataView(i2);
        return this.floatTo16BitPCM(n, 0, t3), i2;
      }
      setString(e3, t3, r2) {
        for (let i2 = 0; i2 < r2.length; i2++) e3.setUint8(t3 + i2, r2.charCodeAt(i2));
      }
      floatTo16BitPCM(e3, t3, r2) {
        for (let i2 = 0; i2 < r2.length; i2++, t3 += 2) {
          const n = Math.max(-1, Math.min(1, r2[i2]));
          e3.setInt16(t3, n < 0 ? 32768 * n : 32767 * n, true);
        }
      }
      downSampleAudioFrame(e3, t3, r2) {
        if (!e3) return null;
        if (r2 === t3 || r2 > t3) return e3;
        const i2 = t3 / r2, n = Math.round(e3.length / i2), s = new Float32Array(n);
        let o = 0, a = 0;
        for (; a < n; ) {
          const t4 = Math.round((a + 1) * i2);
          let r3 = 0, n2 = 0;
          for (; o < t4 && o < e3.length; ) r3 += e3[o++], n2++;
          s[a++] = r3 / n2;
        }
        return s;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Stream = void 0;
    const i2 = r2(26), n = r2(7), s = r2(44);
    t2.Stream = class {
      constructor(e3) {
        this.privIsWriteEnded = false, this.privIsReadEnded = false, this.privId = e3 || (0, n.createNoDashGuid)(), this.privReaderQueue = new s.Queue();
      }
      get isClosed() {
        return this.privIsWriteEnded;
      }
      get isReadEnded() {
        return this.privIsReadEnded;
      }
      get id() {
        return this.privId;
      }
      close() {
        this.privIsWriteEnded || (this.writeStreamChunk({ buffer: null, isEnd: true, timeReceived: Date.now() }), this.privIsWriteEnded = true);
      }
      writeStreamChunk(e3) {
        if (this.throwIfClosed(), !this.privReaderQueue.isDisposed()) try {
          this.privReaderQueue.enqueue(e3);
        } catch (e4) {
        }
      }
      read() {
        if (this.privIsReadEnded) throw new i2.InvalidOperationError("Stream read has already finished");
        return this.privReaderQueue.dequeue().then(async (e3) => ((void 0 === e3 || e3.isEnd) && await this.privReaderQueue.dispose("End of stream reached"), e3));
      }
      readEnded() {
        this.privIsReadEnded || (this.privIsReadEnded = true, this.privReaderQueue = new s.Queue());
      }
      throwIfClosed() {
        if (this.privIsWriteEnded) throw new i2.InvalidOperationError("Stream closed");
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationStatus = void 0, function(e3) {
      e3[e3.Success = 0] = "Success", e3[e3.Error = 1] = "Error";
    }(t2.TranslationStatus || (t2.TranslationStatus = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ChunkedArrayBufferStream = void 0;
    const i2 = r2(4);
    class n extends i2.Stream {
      constructor(e3, t3) {
        super(t3), this.privTargetChunkSize = Math.round(e3), this.privNextBufferReadyBytes = 0;
      }
      writeStreamChunk(e3) {
        if (e3.isEnd || 0 === this.privNextBufferReadyBytes && e3.buffer.byteLength === this.privTargetChunkSize) return void super.writeStreamChunk(e3);
        let t3 = 0;
        for (; t3 < e3.buffer.byteLength; ) {
          void 0 === this.privNextBufferToWrite && (this.privNextBufferToWrite = new ArrayBuffer(this.privTargetChunkSize), this.privNextBufferStartTime = e3.timeReceived);
          const r3 = Math.min(e3.buffer.byteLength - t3, this.privTargetChunkSize - this.privNextBufferReadyBytes), i3 = new Uint8Array(this.privNextBufferToWrite), n2 = new Uint8Array(e3.buffer.slice(t3, r3 + t3));
          i3.set(n2, this.privNextBufferReadyBytes), this.privNextBufferReadyBytes += r3, t3 += r3, this.privNextBufferReadyBytes === this.privTargetChunkSize && (super.writeStreamChunk({ buffer: this.privNextBufferToWrite, isEnd: false, timeReceived: this.privNextBufferStartTime }), this.privNextBufferReadyBytes = 0, this.privNextBufferToWrite = void 0);
        }
      }
      close() {
        0 === this.privNextBufferReadyBytes || this.isClosed || super.writeStreamChunk({ buffer: this.privNextBufferToWrite.slice(0, this.privNextBufferReadyBytes), isEnd: false, timeReceived: this.privNextBufferStartTime }), super.close();
      }
    }
    t2.ChunkedArrayBufferStream = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Timeout = void 0;
    class r2 {
      static load() {
        const e3 = /* @__PURE__ */ new Map([[0, () => {
        }]]), t3 = /* @__PURE__ */ new Map(), i2 = "data:text/javascript;base64," + btoa(`!function(e){var t={};function n(r){if(t[r])return t[r].exports;var o=t[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,n),o.l=!0,o.exports}n.m=e,n.c=t,n.d=function(e,t,r){n.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:r})},n.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},n.t=function(e,t){if(1&t&&(e=n(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var r=Object.create(null);if(n.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var o in e)n.d(r,o,function(t){return e[t]}.bind(null,o));return r},n.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return n.d(t,"a",t),t},n.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},n.p="",n(n.s=14)}([function(e,t,n){"use strict";n.d(t,"a",(function(){return i})),n.d(t,"b",(function(){return u})),n.d(t,"c",(function(){return a})),n.d(t,"d",(function(){return d}));const r=new Map,o=new Map,i=e=>{const t=r.get(e);if(void 0===t)throw new Error('There is no interval scheduled with the given id "'.concat(e,'".'));clearTimeout(t),r.delete(e)},u=e=>{const t=o.get(e);if(void 0===t)throw new Error('There is no timeout scheduled with the given id "'.concat(e,'".'));clearTimeout(t),o.delete(e)},f=(e,t)=>{let n,r;if("performance"in self){const o=performance.now();n=o,r=e-Math.max(0,o-t)}else n=Date.now(),r=e;return{expected:n+r,remainingDelay:r}},c=(e,t,n,r)=>{const o="performance"in self?performance.now():Date.now();o>n?postMessage({id:null,method:"call",params:{timerId:t}}):e.set(t,setTimeout(c,n-o,e,t,n))},a=(e,t,n)=>{const{expected:o,remainingDelay:i}=f(e,n);r.set(t,setTimeout(c,i,r,t,o))},d=(e,t,n)=>{const{expected:r,remainingDelay:i}=f(e,n);o.set(t,setTimeout(c,i,o,t,r))}},function(e,t,n){"use strict";n.r(t);var r=n(2);for(var o in r)"default"!==o&&function(e){n.d(t,e,(function(){return r[e]}))}(o);var i=n(3);for(var o in i)"default"!==o&&function(e){n.d(t,e,(function(){return i[e]}))}(o);var u=n(4);for(var o in u)"default"!==o&&function(e){n.d(t,e,(function(){return u[e]}))}(o);var f=n(5);for(var o in f)"default"!==o&&function(e){n.d(t,e,(function(){return f[e]}))}(o);var c=n(6);for(var o in c)"default"!==o&&function(e){n.d(t,e,(function(){return c[e]}))}(o);var a=n(7);for(var o in a)"default"!==o&&function(e){n.d(t,e,(function(){return a[e]}))}(o);var d=n(8);for(var o in d)"default"!==o&&function(e){n.d(t,e,(function(){return d[e]}))}(o);var s=n(9);for(var o in s)"default"!==o&&function(e){n.d(t,e,(function(){return s[e]}))}(o)},function(e,t){},function(e,t){},function(e,t){},function(e,t){},function(e,t){},function(e,t){},function(e,t){},function(e,t){},function(e,t,n){"use strict";n.r(t);var r=n(11);for(var o in r)"default"!==o&&function(e){n.d(t,e,(function(){return r[e]}))}(o);var i=n(12);for(var o in i)"default"!==o&&function(e){n.d(t,e,(function(){return i[e]}))}(o);var u=n(13);for(var o in u)"default"!==o&&function(e){n.d(t,e,(function(){return u[e]}))}(o)},function(e,t){},function(e,t){},function(e,t){},function(e,t,n){"use strict";n.r(t);var r=n(0),o=n(1);for(var i in o)"default"!==i&&function(e){n.d(t,e,(function(){return o[e]}))}(i);var u=n(10);for(var i in u)"default"!==i&&function(e){n.d(t,e,(function(){return u[e]}))}(i);addEventListener("message",({data:e})=>{try{if("clear"===e.method){const{id:t,params:{timerId:n}}=e;Object(r.b)(n),postMessage({error:null,id:t})}else{if("set"!==e.method)throw new Error('The given method "'.concat(e.method,'" is not supported'));{const{params:{delay:t,now:n,timerId:o}}=e;Object(r.d)(t,o,n)}}}catch(t){postMessage({error:{message:t.message},id:e.id,result:null})}})}]);`), n = new Worker(i2);
        n.addEventListener("message", ({ data: i3 }) => {
          if (r2.isCallNotification(i3)) {
            const { params: { timerId: r3 } } = i3, n2 = e3.get(r3);
            if ("number" == typeof n2) {
              const e4 = t3.get(n2);
              if (void 0 === e4 || e4 !== r3) throw new Error("The timer is in an undefined state.");
            } else {
              if (void 0 === n2) throw new Error("The timer is in an undefined state.");
              n2(), e3.delete(r3);
            }
          } else {
            if (!r2.isClearResponse(i3)) {
              const { error: { message: e4 } } = i3;
              throw new Error(e4);
            }
            {
              const { id: r3 } = i3, n2 = t3.get(r3);
              if (void 0 === n2) throw new Error("The timer is in an undefined state.");
              t3.delete(r3), e3.delete(n2);
            }
          }
        });
        return { clearTimeout: (r3) => {
          const i3 = Math.random();
          t3.set(i3, r3), e3.set(r3, i3), n.postMessage({ id: i3, method: "clear", params: { timerId: r3 } });
        }, setTimeout: (t4, r3) => {
          const i3 = Math.random();
          return e3.set(i3, t4), n.postMessage({ id: null, method: "set", params: { delay: r3, now: performance.now(), timerId: i3 } }), i3;
        } };
      }
      static loadWorkerTimers() {
        return () => (null !== r2.workerTimers || (r2.workerTimers = r2.load()), r2.workerTimers);
      }
      static isCallNotification(e3) {
        return void 0 !== e3.method && "call" === e3.method;
      }
      static isClearResponse(e3) {
        return null === e3.error && "number" == typeof e3.id;
      }
    }
    t2.Timeout = r2, r2.workerTimers = null, r2.clearTimeout = (e3) => r2.timers().clearTimeout(e3), r2.setTimeout = (e3, t3) => r2.timers().setTimeout(e3, t3), r2.timers = r2.loadWorkerTimers();
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.OCSPCacheUpdateErrorEvent = t2.OCSPResponseRetrievedEvent = t2.OCSPCacheFetchErrorEvent = t2.OCSPVerificationFailedEvent = t2.OCSPCacheHitEvent = t2.OCSPCacheEntryNeedsRefreshEvent = t2.OCSPCacheEntryExpiredEvent = t2.OCSPWSUpgradeStartedEvent = t2.OCSPStapleReceivedEvent = t2.OCSPCacheUpdateCompleteEvent = t2.OCSPDiskCacheStoreEvent = t2.OCSPMemoryCacheStoreEvent = t2.OCSPCacheUpdateNeededEvent = t2.OCSPDiskCacheHitEvent = t2.OCSPCacheMissEvent = t2.OCSPMemoryCacheHitEvent = t2.OCSPEvent = void 0;
    const i2 = r2(6);
    class n extends i2.PlatformEvent {
      constructor(e3, t3, r3) {
        super(e3, t3), this.privSignature = r3;
      }
    }
    t2.OCSPEvent = n;
    t2.OCSPMemoryCacheHitEvent = class extends n {
      constructor(e3) {
        super("OCSPMemoryCacheHitEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPCacheMissEvent = class extends n {
      constructor(e3) {
        super("OCSPCacheMissEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPDiskCacheHitEvent = class extends n {
      constructor(e3) {
        super("OCSPDiskCacheHitEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPCacheUpdateNeededEvent = class extends n {
      constructor(e3) {
        super("OCSPCacheUpdateNeededEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPMemoryCacheStoreEvent = class extends n {
      constructor(e3) {
        super("OCSPMemoryCacheStoreEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPDiskCacheStoreEvent = class extends n {
      constructor(e3) {
        super("OCSPDiskCacheStoreEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPCacheUpdateCompleteEvent = class extends n {
      constructor(e3) {
        super("OCSPCacheUpdateCompleteEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPStapleReceivedEvent = class extends n {
      constructor() {
        super("OCSPStapleReceivedEvent", i2.EventType.Debug, "");
      }
    };
    t2.OCSPWSUpgradeStartedEvent = class extends n {
      constructor(e3) {
        super("OCSPWSUpgradeStartedEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPCacheEntryExpiredEvent = class extends n {
      constructor(e3, t3) {
        super("OCSPCacheEntryExpiredEvent", i2.EventType.Debug, e3), this.privExpireTime = t3;
      }
    };
    t2.OCSPCacheEntryNeedsRefreshEvent = class extends n {
      constructor(e3, t3, r3) {
        super("OCSPCacheEntryNeedsRefreshEvent", i2.EventType.Debug, e3), this.privExpireTime = r3, this.privStartTime = t3;
      }
    };
    t2.OCSPCacheHitEvent = class extends n {
      constructor(e3, t3, r3) {
        super("OCSPCacheHitEvent", i2.EventType.Debug, e3), this.privExpireTime = r3, this.privExpireTimeString = new Date(r3).toLocaleDateString(), this.privStartTime = t3, this.privStartTimeString = new Date(t3).toLocaleTimeString();
      }
    };
    t2.OCSPVerificationFailedEvent = class extends n {
      constructor(e3, t3) {
        super("OCSPVerificationFailedEvent", i2.EventType.Debug, e3), this.privError = t3;
      }
    };
    t2.OCSPCacheFetchErrorEvent = class extends n {
      constructor(e3, t3) {
        super("OCSPCacheFetchErrorEvent", i2.EventType.Debug, e3), this.privError = t3;
      }
    };
    t2.OCSPResponseRetrievedEvent = class extends n {
      constructor(e3) {
        super("OCSPResponseRetrievedEvent", i2.EventType.Debug, e3);
      }
    };
    t2.OCSPCacheUpdateErrorEvent = class extends n {
      constructor(e3, t3) {
        super("OCSPCacheUpdateErrorEvent", i2.EventType.Debug, e3), this.privError = t3;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.BackgroundEvent = void 0;
    const i2 = r2(4);
    class n extends i2.PlatformEvent {
      constructor(e3) {
        super("BackgroundEvent", i2.EventType.Error), this.privError = e3;
      }
      get error() {
        return this.privError;
      }
    }
    t2.BackgroundEvent = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.HeaderNames = void 0;
    class r2 {
    }
    t2.HeaderNames = r2, r2.AuthKey = "Ocp-Apim-Subscription-Key", r2.Authorization = "Authorization", r2.SpIDAuthKey = "Apim-Subscription-Id", r2.ConnectionId = "X-ConnectionId", r2.ContentType = "Content-Type", r2.CustomCommandsAppId = "X-CommandsAppId", r2.Path = "Path", r2.RequestId = "X-RequestId", r2.RequestStreamId = "X-StreamId", r2.RequestTimestamp = "X-Timestamp";
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AuthInfo = void 0;
    t2.AuthInfo = class {
      constructor(e3, t3) {
        this.privHeaderName = e3, this.privToken = t3;
      }
      get headerName() {
        return this.privHeaderName;
      }
      get token() {
        return this.privToken;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CognitiveTokenAuthentication = void 0;
    const i2 = r2(4), n = r2(55), s = r2(54);
    class o {
      constructor(e3, t3) {
        if (!e3) throw new i2.ArgumentNullError("fetchCallback");
        if (!t3) throw new i2.ArgumentNullError("fetchOnExpiryCallback");
        this.privFetchCallback = e3, this.privFetchOnExpiryCallback = t3;
      }
      fetch(e3) {
        return this.privFetchCallback(e3).then((e4) => new n.AuthInfo(s.HeaderNames.Authorization, void 0 === e4 ? void 0 : o.privTokenPrefix + e4));
      }
      fetchOnExpiry(e3) {
        return this.privFetchOnExpiryCallback(e3).then((e4) => new n.AuthInfo(s.HeaderNames.Authorization, void 0 === e4 ? void 0 : o.privTokenPrefix + e4));
      }
    }
    t2.CognitiveTokenAuthentication = o, o.privTokenPrefix = "Bearer ";
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RecognitionEndedEvent = t2.RecognitionCompletionStatus = t2.RecognitionStartedEvent = t2.ConnectingToServiceEvent = t2.ListeningStartedEvent = t2.RecognitionTriggeredEvent = t2.SpeechRecognitionEvent = void 0;
    const i2 = r2(4);
    class n extends i2.PlatformEvent {
      constructor(e3, t3, r3, n2 = i2.EventType.Info) {
        super(e3, n2), this.privRequestId = t3, this.privSessionId = r3;
      }
      get requestId() {
        return this.privRequestId;
      }
      get sessionId() {
        return this.privSessionId;
      }
    }
    t2.SpeechRecognitionEvent = n;
    t2.RecognitionTriggeredEvent = class extends n {
      constructor(e3, t3, r3, i3) {
        super("RecognitionTriggeredEvent", e3, t3), this.privAudioSourceId = r3, this.privAudioNodeId = i3;
      }
      get audioSourceId() {
        return this.privAudioSourceId;
      }
      get audioNodeId() {
        return this.privAudioNodeId;
      }
    };
    t2.ListeningStartedEvent = class extends n {
      constructor(e3, t3, r3, i3) {
        super("ListeningStartedEvent", e3, t3), this.privAudioSourceId = r3, this.privAudioNodeId = i3;
      }
      get audioSourceId() {
        return this.privAudioSourceId;
      }
      get audioNodeId() {
        return this.privAudioNodeId;
      }
    };
    t2.ConnectingToServiceEvent = class extends n {
      constructor(e3, t3, r3) {
        super("ConnectingToServiceEvent", e3, r3), this.privAuthFetchEventid = t3;
      }
      get authFetchEventid() {
        return this.privAuthFetchEventid;
      }
    };
    var s;
    t2.RecognitionStartedEvent = class extends n {
      constructor(e3, t3, r3, i3, n2) {
        super("RecognitionStartedEvent", e3, n2), this.privAudioSourceId = t3, this.privAudioNodeId = r3, this.privAuthFetchEventId = i3;
      }
      get audioSourceId() {
        return this.privAudioSourceId;
      }
      get audioNodeId() {
        return this.privAudioNodeId;
      }
      get authFetchEventId() {
        return this.privAuthFetchEventId;
      }
    }, function(e3) {
      e3[e3.Success = 0] = "Success", e3[e3.AudioSourceError = 1] = "AudioSourceError", e3[e3.AudioSourceTimeout = 2] = "AudioSourceTimeout", e3[e3.AuthTokenFetchError = 3] = "AuthTokenFetchError", e3[e3.AuthTokenFetchTimeout = 4] = "AuthTokenFetchTimeout", e3[e3.UnAuthorized = 5] = "UnAuthorized", e3[e3.ConnectTimeout = 6] = "ConnectTimeout", e3[e3.ConnectError = 7] = "ConnectError", e3[e3.ClientRecognitionActivityTimeout = 8] = "ClientRecognitionActivityTimeout", e3[e3.UnknownError = 9] = "UnknownError";
    }(s = t2.RecognitionCompletionStatus || (t2.RecognitionCompletionStatus = {}));
    t2.RecognitionEndedEvent = class extends n {
      constructor(e3, t3, r3, n2, o, a, c, p) {
        super("RecognitionEndedEvent", e3, o, c === s.Success ? i2.EventType.Info : i2.EventType.Error), this.privAudioSourceId = t3, this.privAudioNodeId = r3, this.privAuthFetchEventId = n2, this.privStatus = c, this.privError = p, this.privServiceTag = a;
      }
      get audioSourceId() {
        return this.privAudioSourceId;
      }
      get audioNodeId() {
        return this.privAudioNodeId;
      }
      get authFetchEventId() {
        return this.privAuthFetchEventId;
      }
      get serviceTag() {
        return this.privServiceTag;
      }
      get status() {
        return this.privStatus;
      }
      get error() {
        return this.privError;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ServiceRecognizerBase = void 0;
    const i2 = r2(61), n = r2(4), s = r2(80), o = r2(2), a = r2(190), c = r2(191), p = r2(111), h = r2(192), u = r2(193), d = r2(194), v = r2(195), l = r2(196), g = r2(197), m = r2(198), S = r2(199);
    class f {
      constructor(e3, t3, r3, i3, a2) {
        if (this.privConnectionConfigurationPromise = void 0, this.privConnectionPromise = void 0, this.privSetTimeout = setTimeout, this.privIsLiveAudio = false, this.privAverageBytesPerMs = 0, this.privEnableSpeakerId = false, this.privExpectContentAssessmentResponse = false, this.recognizeOverride = void 0, this.disconnectOverride = void 0, this.receiveMessageOverride = void 0, this.sendPrePayloadJSONOverride = void 0, this.postConnectImplOverride = void 0, this.configConnectionOverride = void 0, this.handleSpeechPhraseMessage = void 0, this.handleSpeechHypothesisMessage = void 0, !e3) throw new n.ArgumentNullError("authentication");
        if (!t3) throw new n.ArgumentNullError("connectionFactory");
        if (!r3) throw new n.ArgumentNullError("audioSource");
        if (!i3) throw new n.ArgumentNullError("recognizerConfig");
        this.privEnableSpeakerId = i3.isSpeakerDiarizationEnabled, this.privMustReportEndOfStream = false, this.privAuthentication = e3, this.privConnectionFactory = t3, this.privAudioSource = r3, this.privRecognizerConfig = i3, this.privIsDisposed = false, this.privRecognizer = a2, this.privRequestSession = new o.RequestSession(this.privAudioSource.id()), this.privConnectionEvents = new n.EventSource(), this.privServiceEvents = new n.EventSource(), this.privDynamicGrammar = new o.DynamicGrammarBuilder(), this.privSpeechContext = new o.SpeechContext(this.privDynamicGrammar), this.privAgentConfig = new o.AgentConfig();
        "on" === this.privRecognizerConfig.parameters.getProperty(s.PropertyId.WebWorkerLoadType, "on").toLowerCase() && "undefined" != typeof Blob && "undefined" != typeof Worker ? this.privSetTimeout = n.Timeout.setTimeout : ("undefined" != typeof window && (this.privSetTimeout = window.setTimeout.bind(window)), "undefined" != typeof globalThis && (this.privSetTimeout = globalThis.setTimeout.bind(globalThis))), this.connectionEvents.attach((e4) => {
          if ("ConnectionClosedEvent" === e4.name) {
            const t4 = e4;
            (1003 === t4.statusCode || 1007 === t4.statusCode || 1002 === t4.statusCode || 4e3 === t4.statusCode || this.privRequestSession.numConnectionAttempts > this.privRecognizerConfig.maxRetryCount) && this.cancelRecognitionLocal(s.CancellationReason.Error, 1007 === t4.statusCode ? s.CancellationErrorCode.BadRequestParameters : s.CancellationErrorCode.ConnectionFailure, `${t4.reason} websocket error code: ${t4.statusCode}`);
          }
        }), this.privEnableSpeakerId && (this.privDiarizationSessionId = (0, n.createNoDashGuid)());
      }
      setTranslationJson() {
        const e3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages, void 0);
        if (void 0 !== e3) {
          const t3 = e3.split(","), r3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationVoice, void 0), i3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationCategoryId, void 0), n2 = void 0 !== r3 ? h.NextAction.Synthesize : h.NextAction.None;
          if (this.privSpeechContext.getContext().translation = { onPassthrough: { action: n2 }, onSuccess: { action: n2 }, output: { includePassThroughResults: true, interimResults: { mode: u.Mode.Always } }, targetLanguages: t3 }, void 0 !== i3 && (this.privSpeechContext.getContext().translation.category = i3), void 0 !== r3) {
            const e4 = {};
            for (const i4 of t3) e4[i4] = r3;
            this.privSpeechContext.getContext().synthesis = { defaultVoices: e4 };
          }
          const o2 = this.privSpeechContext.getContext().phraseDetection || {};
          o2.onSuccess = { action: S.NextAction.Translate }, o2.onInterim = { action: S.NextAction.Translate }, this.privSpeechContext.getContext().phraseDetection = o2;
        }
      }
      setSpeechSegmentationTimeoutJson() {
        const e3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.Speech_SegmentationSilenceTimeoutMs, void 0), t3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.Speech_SegmentationMaximumTimeMs, void 0), r3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.Speech_SegmentationStrategy, void 0), i3 = { mode: c.SegmentationMode.Normal };
        let n2 = false;
        if (void 0 !== r3) {
          n2 = true;
          let e4 = c.SegmentationMode.Normal;
          switch (r3.toLowerCase()) {
            case "default":
              break;
            case "time":
              e4 = c.SegmentationMode.Custom;
              break;
            case "semantic":
              e4 = c.SegmentationMode.Semantic;
          }
          i3.mode = e4;
        }
        if (void 0 !== e3) {
          n2 = true;
          const t4 = parseInt(e3, 10);
          i3.mode = c.SegmentationMode.Custom, i3.segmentationSilenceTimeoutMs = t4;
        }
        if (void 0 !== t3) {
          n2 = true;
          const e4 = parseInt(t3, 10);
          i3.mode = c.SegmentationMode.Custom, i3.segmentationForcedTimeoutMs = e4;
        }
        if (n2) {
          const e4 = this.privSpeechContext.getContext().phraseDetection || {};
          switch (e4.mode = this.recognitionMode, this.recognitionMode) {
            case p.RecognitionMode.Conversation:
              e4.conversation = e4.conversation ?? { segmentation: {} }, e4.conversation.segmentation = i3;
              break;
            case p.RecognitionMode.Interactive:
              e4.interactive = e4.interactive ?? { segmentation: {} }, e4.interactive.segmentation = i3;
              break;
            case p.RecognitionMode.Dictation:
              e4.dictation = e4.dictation ?? {}, e4.dictation.segmentation = i3;
          }
          this.privSpeechContext.getContext().phraseDetection = e4;
        }
      }
      setLanguageIdJson() {
        const e3 = this.privSpeechContext.getContext().phraseDetection || {};
        if (void 0 !== this.privRecognizerConfig.autoDetectSourceLanguages) {
          const t3 = this.privRecognizerConfig.autoDetectSourceLanguages.split(",");
          let r3;
          1 === t3.length && t3[0] === o.AutoDetectSourceLanguagesOpenRangeOptionName && (t3[0] = "UND"), r3 = "Continuous" === this.privRecognizerConfig.languageIdMode ? d.LanguageIdDetectionMode.DetectContinuous : d.LanguageIdDetectionMode.DetectAtAudioStart, this.privSpeechContext.getContext().languageId = { languages: t3, mode: r3, onSuccess: { action: v.NextAction.Recognize }, onUnknown: { action: l.OnUnknownAction.None }, priority: d.LanguageIdDetectionPriority.PrioritizeLatency }, this.privSpeechContext.getContext().phraseOutput = { interimResults: { resultType: g.ResultType.Auto }, phraseResults: { resultType: m.PhraseResultOutputType.Always } };
          const i3 = this.privRecognizerConfig.sourceLanguageModels;
          void 0 !== i3 && (e3.customModels = i3, e3.onInterim = { action: S.NextAction.None }, e3.onSuccess = { action: S.NextAction.None });
        }
        this.privSpeechContext.getContext().phraseDetection = e3;
      }
      setOutputDetailLevelJson() {
        if (this.privEnableSpeakerId) {
          if ("true" === this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceResponse_RequestWordLevelTimestamps, "false").toLowerCase()) this.privSpeechContext.setWordLevelTimings();
          else {
            this.privRecognizerConfig.parameters.getProperty(o.OutputFormatPropertyName, s.OutputFormat[s.OutputFormat.Simple]).toLowerCase() === s.OutputFormat[s.OutputFormat.Detailed].toLocaleLowerCase() && this.privSpeechContext.setDetailedOutputFormat();
          }
        }
      }
      setSpeechStartEventSensitivityJson() {
        const e3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.Speech_StartEventSensitivity, void 0);
        if (void 0 !== e3) {
          let t3 = false;
          switch (e3.toLowerCase()) {
            case p.SpeechStartEventSensitivity.Low:
            case p.SpeechStartEventSensitivity.Medium:
            case p.SpeechStartEventSensitivity.High:
              t3 = true;
          }
          if (t3) {
            const t4 = this.privSpeechContext.getContext().phraseDetection || {};
            t4.voiceOnsetSensitivity = e3.toLowerCase(), this.privSpeechContext.getContext().phraseDetection = t4;
          }
        }
      }
      get isSpeakerDiarizationEnabled() {
        return this.privEnableSpeakerId;
      }
      get audioSource() {
        return this.privAudioSource;
      }
      get speechContext() {
        return this.privSpeechContext;
      }
      get dynamicGrammar() {
        return this.privDynamicGrammar;
      }
      get agentConfig() {
        return this.privAgentConfig;
      }
      set conversationTranslatorToken(e3) {
        this.privRecognizerConfig.parameters.setProperty(s.PropertyId.ConversationTranslator_Token, e3);
      }
      set authentication(e3) {
        this.privAuthentication = e3;
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      async dispose(e3) {
        if (this.privIsDisposed = true, void 0 !== this.privConnectionPromise) try {
          const t3 = await this.privConnectionPromise;
          await t3.dispose(e3);
        } catch (e4) {
          return;
        }
      }
      get connectionEvents() {
        return this.privConnectionEvents;
      }
      get serviceEvents() {
        return this.privServiceEvents;
      }
      get recognitionMode() {
        return this.privRecognizerConfig.recognitionMode;
      }
      async recognize(e3, t3, r3) {
        if (void 0 !== this.recognizeOverride) return void await this.recognizeOverride(e3, t3, r3);
        if (this.privConnectionConfigurationPromise = void 0, this.privRecognizerConfig.recognitionMode = e3, "2" === this.privRecognizerConfig.recognitionEndpointVersion) {
          const t4 = this.privSpeechContext.getContext().phraseDetection || {};
          t4.mode = e3, this.privSpeechContext.getContext().phraseDetection = t4;
        }
        this.setLanguageIdJson(), this.setTranslationJson(), void 0 !== this.privRecognizerConfig.autoDetectSourceLanguages && void 0 !== this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages, void 0) && this.setupTranslationWithLanguageId(), this.setSpeechSegmentationTimeoutJson(), this.setOutputDetailLevelJson(), this.setSpeechStartEventSensitivityJson(), this.privSuccessCallback = t3, this.privErrorCallback = r3, this.privRequestSession.startNewRecognition(), this.privRequestSession.listenForServiceTelemetry(this.privAudioSource.events);
        const n2 = this.connectImpl();
        let a2;
        try {
          const e4 = await this.audioSource.attach(this.privRequestSession.audioNodeId), t4 = await this.audioSource.format, r4 = await this.audioSource.deviceInfo;
          this.privIsLiveAudio = r4.type && r4.type === o.type.Microphones, a2 = new i2.ReplayableAudioNode(e4, t4.avgBytesPerSec), await this.privRequestSession.onAudioSourceAttachCompleted(a2, false), this.privRecognizerConfig.SpeechServiceConfig.Context.audio = { source: r4 };
        } catch (e4) {
          throw await this.privRequestSession.onStopRecognizing(), e4;
        }
        try {
          await n2;
        } catch (e4) {
          return void await this.cancelRecognitionLocal(s.CancellationReason.Error, s.CancellationErrorCode.ConnectionFailure, e4);
        }
        const c2 = new s.SessionEventArgs(this.privRequestSession.sessionId);
        this.privRecognizer.sessionStarted && this.privRecognizer.sessionStarted(this.privRecognizer, c2), this.receiveMessage();
        this.sendAudio(a2).catch(async (e4) => {
          await this.cancelRecognitionLocal(s.CancellationReason.Error, s.CancellationErrorCode.RuntimeError, e4);
        });
      }
      async stopRecognizing() {
        if (this.privRequestSession.isRecognizing) try {
          await this.audioSource.turnOff(), await this.sendFinalAudio(), await this.privRequestSession.onStopRecognizing(), await this.privRequestSession.turnCompletionPromise;
        } finally {
          await this.privRequestSession.dispose();
        }
      }
      async connect() {
        return await this.connectImpl(), Promise.resolve();
      }
      connectAsync(e3, t3) {
        this.connectImpl().then(() => {
          try {
            e3 && e3();
          } catch (e4) {
            t3 && t3(e4);
          }
        }, (e4) => {
          try {
            t3 && t3(e4);
          } catch (e5) {
          }
        });
      }
      async disconnect() {
        if (await this.cancelRecognitionLocal(s.CancellationReason.Error, s.CancellationErrorCode.NoError, "Disconnecting"), void 0 !== this.disconnectOverride && await this.disconnectOverride(), void 0 !== this.privConnectionPromise) try {
          await (await this.privConnectionPromise).dispose();
        } catch (e3) {
        }
        this.privConnectionPromise = void 0;
      }
      sendMessage(e3) {
      }
      async sendNetworkMessage(e3, t3) {
        const r3 = "string" == typeof t3 ? n.MessageType.Text : n.MessageType.Binary, i3 = "string" == typeof t3 ? "application/json" : "";
        return (await this.fetchConnection()).send(new a.SpeechConnectionMessage(r3, e3, this.privRequestSession.requestId, i3, t3));
      }
      set activityTemplate(e3) {
        this.privActivityTemplate = e3;
      }
      get activityTemplate() {
        return this.privActivityTemplate;
      }
      set expectContentAssessmentResponse(e3) {
        this.privExpectContentAssessmentResponse = e3;
      }
      async sendTelemetryData() {
        const e3 = this.privRequestSession.getTelemetry();
        if (true !== f.telemetryDataEnabled || this.privIsDisposed || null === e3) return;
        if (f.telemetryData) try {
          f.telemetryData(e3);
        } catch {
        }
        const t3 = await this.fetchConnection();
        await t3.send(new a.SpeechConnectionMessage(n.MessageType.Text, "telemetry", this.privRequestSession.requestId, "application/json", e3));
      }
      async cancelRecognitionLocal(e3, t3, r3) {
        this.privRequestSession.isRecognizing && (await this.privRequestSession.onStopRecognizing(), this.cancelRecognition(this.privRequestSession.sessionId, this.privRequestSession.requestId, e3, t3, r3));
      }
      async receiveMessage() {
        try {
          if (this.privIsDisposed) return;
          let e3 = await this.fetchConnection();
          const t3 = await e3.read();
          if (void 0 !== this.receiveMessageOverride) return this.receiveMessageOverride();
          if (!t3) return this.receiveMessage();
          this.privServiceHasSentMessage = true;
          const r3 = a.SpeechConnectionMessage.fromConnectionMessage(t3);
          if (r3.requestId.toLowerCase() === this.privRequestSession.requestId.toLowerCase()) switch (r3.path.toLowerCase()) {
            case "turn.start":
              this.privMustReportEndOfStream = true, this.privRequestSession.onServiceTurnStartResponse();
              break;
            case "speech.startdetected":
              const t4 = o.SpeechDetected.fromJSON(r3.textBody, this.privRequestSession.currentTurnAudioOffset), i3 = new s.RecognitionEventArgs(t4.Offset, this.privRequestSession.sessionId);
              this.privRecognizer.speechStartDetected && this.privRecognizer.speechStartDetected(this.privRecognizer, i3);
              break;
            case "speech.enddetected":
              let a2;
              a2 = r3.textBody.length > 0 ? r3.textBody : "{ Offset: 0 }";
              const c2 = o.SpeechDetected.fromJSON(a2, this.privRequestSession.currentTurnAudioOffset), p2 = new s.RecognitionEventArgs(c2.Offset + this.privRequestSession.currentTurnAudioOffset, this.privRequestSession.sessionId);
              this.privRecognizer.speechEndDetected && this.privRecognizer.speechEndDetected(this.privRecognizer, p2);
              break;
            case "turn.end":
              await this.sendTelemetryData(), this.privRequestSession.isSpeechEnded && this.privMustReportEndOfStream && (this.privMustReportEndOfStream = false, await this.cancelRecognitionLocal(s.CancellationReason.EndOfStream, s.CancellationErrorCode.NoError, void 0));
              const h2 = new s.SessionEventArgs(this.privRequestSession.sessionId);
              if (await this.privRequestSession.onServiceTurnEndResponse(this.privRecognizerConfig.isContinuousRecognition), !this.privRecognizerConfig.isContinuousRecognition || this.privRequestSession.isSpeechEnded || !this.privRequestSession.isRecognizing) return void (this.privRecognizer.sessionStopped && this.privRecognizer.sessionStopped(this.privRecognizer, h2));
              e3 = await this.fetchConnection(), await this.sendPrePayloadJSON(e3);
              break;
            default:
              await this.processTypeSpecificMessages(r3) || this.privServiceEvents && this.serviceEvents.onEvent(new n.ServiceEvent(r3.path.toLowerCase(), r3.textBody));
          }
          return this.receiveMessage();
        } catch (e3) {
          return null;
        }
      }
      updateSpeakerDiarizationAudioOffset() {
        const e3 = this.privRequestSession.recognitionBytesSent, t3 = 0 !== this.privAverageBytesPerMs ? e3 / this.privAverageBytesPerMs : 0;
        this.privSpeechContext.setSpeakerDiarizationAudioOffsetMs(t3);
      }
      sendSpeechContext(e3, t3) {
        this.privEnableSpeakerId && this.updateSpeakerDiarizationAudioOffset();
        const r3 = this.speechContext.toJSON();
        if (t3 && this.privRequestSession.onSpeechContext(), r3) return e3.send(new a.SpeechConnectionMessage(n.MessageType.Text, "speech.context", this.privRequestSession.requestId, "application/json", r3));
      }
      setupTranslationWithLanguageId() {
        const e3 = this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages, void 0), t3 = void 0 !== this.privRecognizerConfig.autoDetectSourceLanguages;
        if (void 0 !== e3 && t3) {
          this.privSpeechContext.getContext().phraseOutput = { interimResults: { resultType: g.ResultType.None }, phraseResults: { resultType: m.PhraseResultOutputType.None } };
          const e4 = this.privSpeechContext.getContext().translation;
          if (e4) {
            const t4 = this.privRecognizerConfig.sourceLanguageModels;
            if (void 0 !== t4 && t4.length > 0) {
              const e5 = this.privSpeechContext.getContext().phraseDetection || {};
              e5.customModels = t4, this.privSpeechContext.getContext().phraseDetection = e5;
            }
            void 0 !== this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationVoice, void 0) && (e4.onSuccess = { action: h.NextAction.Synthesize }, e4.onPassthrough = { action: h.NextAction.Synthesize });
          }
        }
      }
      noOp() {
      }
      async sendPrePayloadJSON(e3, t3 = true) {
        if (void 0 !== this.sendPrePayloadJSONOverride) return this.sendPrePayloadJSONOverride(e3);
        await this.sendSpeechContext(e3, t3), await this.sendWaveHeader(e3);
      }
      async sendWaveHeader(e3) {
        const t3 = await this.audioSource.format;
        return e3.send(new a.SpeechConnectionMessage(n.MessageType.Binary, "audio", this.privRequestSession.requestId, "audio/x-wav", t3.header));
      }
      connectImpl() {
        return void 0 !== this.privConnectionPromise ? this.privConnectionPromise.then((e3) => e3.state() === n.ConnectionState.Disconnected ? (this.privConnectionId = null, this.privConnectionPromise = void 0, this.privServiceHasSentMessage = false, this.connectImpl()) : this.privConnectionPromise, () => (this.privConnectionId = null, this.privConnectionPromise = void 0, this.privServiceHasSentMessage = false, this.connectImpl())) : (this.privConnectionPromise = this.retryableConnect(), this.privConnectionPromise.catch(() => {
        }), void 0 !== this.postConnectImplOverride ? this.postConnectImplOverride(this.privConnectionPromise) : this.privConnectionPromise);
      }
      sendSpeechServiceConfig(e3, t3, r3) {
        if (t3.onSpeechContext(), true !== f.telemetryDataEnabled) {
          const e4 = { context: { system: JSON.parse(r3).context.system } };
          r3 = JSON.stringify(e4);
        }
        if ("true" === this.privRecognizerConfig.parameters.getProperty("f0f5debc-f8c9-4892-ac4b-90a7ab359fd2", "false").toLowerCase()) {
          const e4 = JSON.parse(r3);
          e4.context.DisableReferenceChannel = "True", e4.context.MicSpec = "1_0_0", r3 = JSON.stringify(e4);
        }
        if (r3) return e3.send(new a.SpeechConnectionMessage(n.MessageType.Text, "speech.config", t3.requestId, "application/json", r3));
      }
      async fetchConnection() {
        return void 0 !== this.privConnectionConfigurationPromise ? this.privConnectionConfigurationPromise.then((e3) => e3.state() === n.ConnectionState.Disconnected ? (this.privConnectionId = null, this.privConnectionConfigurationPromise = void 0, this.privServiceHasSentMessage = false, this.fetchConnection()) : this.privConnectionConfigurationPromise, () => (this.privConnectionId = null, this.privConnectionConfigurationPromise = void 0, this.privServiceHasSentMessage = false, this.fetchConnection())) : (this.privConnectionConfigurationPromise = this.configureConnection(), await this.privConnectionConfigurationPromise);
      }
      async sendAudio(e3) {
        const t3 = await this.audioSource.format;
        this.privAverageBytesPerMs = t3.avgBytesPerSec / 1e3;
        let r3 = Date.now();
        const i3 = this.privRecognizerConfig.parameters.getProperty("SPEECH-TransmitLengthBeforThrottleMs", "5000"), s2 = t3.avgBytesPerSec / 1e3 * parseInt(i3, 10), o2 = this.privRequestSession.recogNumber, c2 = async () => {
          if (!this.privIsDisposed && !this.privRequestSession.isSpeechEnded && this.privRequestSession.isRecognizing && this.privRequestSession.recogNumber === o2) {
            const i4 = await this.fetchConnection(), p2 = await e3.read();
            if (this.privRequestSession.isSpeechEnded) return;
            let h2, u2;
            if (!p2 || p2.isEnd ? (h2 = null, u2 = 0) : (h2 = p2.buffer, this.privRequestSession.onAudioSent(h2.byteLength), u2 = s2 >= this.privRequestSession.bytesSent ? 0 : Math.max(0, r3 - Date.now())), 0 !== u2 && await this.delay(u2), null !== h2 && (r3 = Date.now() + 1e3 * h2.byteLength / (2 * t3.avgBytesPerSec)), !this.privIsDisposed && !this.privRequestSession.isSpeechEnded && this.privRequestSession.isRecognizing && this.privRequestSession.recogNumber === o2) {
              if (i4.send(new a.SpeechConnectionMessage(n.MessageType.Binary, "audio", this.privRequestSession.requestId, null, h2)).catch(() => {
                this.privRequestSession.onServiceTurnEndResponse(this.privRecognizerConfig.isContinuousRecognition).catch(() => {
                });
              }), !p2?.isEnd) return c2();
              this.privIsLiveAudio || this.privRequestSession.onSpeechEnded();
            }
          }
        };
        return c2();
      }
      async retryableConnect() {
        let e3 = false;
        this.privAuthFetchEventId = (0, n.createNoDashGuid)();
        const t3 = this.privRequestSession.sessionId;
        this.privConnectionId = void 0 !== t3 ? t3 : (0, n.createNoDashGuid)(), this.privRequestSession.onPreConnectionStart(this.privAuthFetchEventId, this.privConnectionId);
        let r3 = 0, i3 = "";
        for (; this.privRequestSession.numConnectionAttempts <= this.privRecognizerConfig.maxRetryCount; ) {
          this.privRequestSession.onRetryConnection();
          const t4 = e3 ? this.privAuthentication.fetchOnExpiry(this.privAuthFetchEventId) : this.privAuthentication.fetch(this.privAuthFetchEventId), n2 = await t4;
          await this.privRequestSession.onAuthCompleted(false);
          const s2 = await this.privConnectionFactory.create(this.privRecognizerConfig, n2, this.privConnectionId);
          this.privRequestSession.listenForServiceTelemetry(s2.events), s2.events.attach((e4) => {
            this.connectionEvents.onEvent(e4);
          });
          const o2 = await s2.open();
          if (200 === o2.statusCode) return await this.privRequestSession.onConnectionEstablishCompleted(o2.statusCode), Promise.resolve(s2);
          1006 === o2.statusCode && (e3 = true), r3 = o2.statusCode, i3 = o2.reason;
        }
        return await this.privRequestSession.onConnectionEstablishCompleted(r3, i3), Promise.reject(`Unable to contact server. StatusCode: ${r3}, ${this.privRecognizerConfig.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Endpoint)} Reason: ${i3}`);
      }
      delay(e3) {
        return new Promise((t3) => this.privSetTimeout(t3, e3));
      }
      writeBufferToConsole(e3) {
        let t3 = "Buffer Size: ";
        if (null === e3) t3 += "null";
        else {
          const r3 = new Uint8Array(e3);
          t3 += `${e3.byteLength}\r
`;
          for (let i3 = 0; i3 < e3.byteLength; i3++) t3 += r3[i3].toString(16).padStart(2, "0") + " ", (i3 + 1) % 16 == 0 && (console.info(t3), t3 = "");
        }
        console.info(t3);
      }
      async sendFinalAudio() {
        const e3 = await this.fetchConnection();
        await e3.send(new a.SpeechConnectionMessage(n.MessageType.Binary, "audio", this.privRequestSession.requestId, null, null));
      }
      async configureConnection() {
        const e3 = await this.connectImpl();
        return void 0 !== this.configConnectionOverride ? this.configConnectionOverride(e3) : (await this.sendSpeechServiceConfig(e3, this.privRequestSession, this.privRecognizerConfig.SpeechServiceConfig.serialize()), await this.sendPrePayloadJSON(e3, false), e3);
      }
    }
    t2.ServiceRecognizerBase = f, f.telemetryDataEnabled = true;
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__exportStar || function(e3, t3) {
      for (var r3 in e3) "default" === r3 || Object.prototype.hasOwnProperty.call(t3, r3) || i2(t3, e3, r3);
    };
    Object.defineProperty(t2, "__esModule", { value: true }), n(r2(62), t2), n(r2(66), t2), n(r2(67), t2), n(r2(69), t2), n(r2(70), t2), n(r2(71), t2), n(r2(72), t2), n(r2(78), t2), n(r2(79), t2), n(r2(186), t2), n(r2(189), t2);
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__setModuleDefault || (Object.create ? function(e3, t3) {
      Object.defineProperty(e3, "default", { enumerable: true, value: t3 });
    } : function(e3, t3) {
      e3.default = t3;
    }), s = this && this.__importStar || function(e3) {
      if (e3 && e3.__esModule) return e3;
      var t3 = {};
      if (null != e3) for (var r3 in e3) "default" !== r3 && Object.prototype.hasOwnProperty.call(e3, r3) && i2(t3, e3, r3);
      return n(t3, e3), t3;
    };
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConsoleLoggingListener = void 0;
    const o = s(r2(63)), a = r2(64), c = r2(65);
    t2.ConsoleLoggingListener = class {
      constructor(e3 = a.LogLevel.None) {
        this.privLogPath = void 0, this.privEnableConsoleOutput = true, this.privLogLevelFilter = e3;
      }
      set logPath(e3) {
        c.Contracts.throwIfNullOrUndefined(o.openSync, "\nFile System access not available"), this.privLogPath = e3;
      }
      set enableConsoleOutput(e3) {
        this.privEnableConsoleOutput = e3;
      }
      onEvent(e3) {
        if (e3.eventType >= this.privLogLevelFilter) {
          const t3 = this.toString(e3);
          if (this.logCallback && this.logCallback(t3), this.privLogPath && o.writeFileSync(this.privLogPath, t3 + "\n", { flag: "a+" }), this.privEnableConsoleOutput) switch (e3.eventType) {
            case a.LogLevel.Debug:
              console.debug(t3);
              break;
            case a.LogLevel.Info:
              console.info(t3);
              break;
            case a.LogLevel.Warning:
              console.warn(t3);
              break;
            case a.LogLevel.Error:
              console.error(t3);
              break;
            default:
              console.log(t3);
          }
        }
      }
      toString(e3) {
        const t3 = [`${e3.eventTime}`, `${e3.name}`], r3 = e3;
        for (const i3 in r3) if (i3 && e3.hasOwnProperty(i3) && "eventTime" !== i3 && "eventType" !== i3 && "eventId" !== i3 && "name" !== i3 && "constructor" !== i3) {
          const e4 = r3[i3];
          let n2 = "<NULL>";
          null != e4 && (n2 = "number" == typeof e4 || "string" == typeof e4 ? e4.toString() : JSON.stringify(e4)), t3.push(`${i3}: ${n2}`);
        }
        return t3.join(" | ");
      }
    };
  }, () => {
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.LogLevel = void 0;
    const i2 = r2(4);
    Object.defineProperty(t2, "LogLevel", { enumerable: true, get: function() {
      return i2.EventType;
    } });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Contracts = void 0;
    class r2 {
      static throwIfNullOrUndefined(e3, t3) {
        if (null == e3) throw new Error("throwIfNullOrUndefined:" + t3);
      }
      static throwIfNull(e3, t3) {
        if (null === e3) throw new Error("throwIfNull:" + t3);
      }
      static throwIfNullOrWhitespace(e3, t3) {
        if (r2.throwIfNullOrUndefined(e3, t3), ("" + e3).trim().length < 1) throw new Error("throwIfNullOrWhitespace:" + t3);
      }
      static throwIfNullOrTooLong(e3, t3, i2) {
        if (r2.throwIfNullOrUndefined(e3, t3), ("" + e3).length > i2) throw new Error("throwIfNullOrTooLong:" + t3 + " (more than " + i2.toString() + " characters)");
      }
      static throwIfNullOrTooShort(e3, t3, i2) {
        if (r2.throwIfNullOrUndefined(e3, t3), ("" + e3).length < i2) throw new Error("throwIfNullOrTooShort:" + t3 + " (less than " + i2.toString() + " characters)");
      }
      static throwIfDisposed(e3) {
        if (e3) throw new Error("the object is already disposed");
      }
      static throwIfArrayEmptyOrWhitespace(e3, t3) {
        if (r2.throwIfNullOrUndefined(e3, t3), 0 === e3.length) throw new Error("throwIfArrayEmptyOrWhitespace:" + t3);
        for (const i2 of e3) r2.throwIfNullOrWhitespace(i2, t3);
      }
      static throwIfFileDoesNotExist(e3, t3) {
        r2.throwIfNullOrWhitespace(e3, t3);
      }
      static throwIfNotUndefined(e3, t3) {
        if (void 0 !== e3) throw new Error("throwIfNotUndefined:" + t3);
      }
      static throwIfNumberOutOfRange(e3, t3, i2, n) {
        if (r2.throwIfNullOrUndefined(e3, t3), e3 < i2 || e3 > n) throw new Error("throwIfNumberOutOfRange:" + t3 + " (must be between " + i2.toString() + " and " + n.toString() + ")");
      }
    }
    t2.Contracts = r2;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MicAudioSource = t2.AudioWorkletSourceURLPropertyName = void 0;
    const i2 = r2(2), n = r2(4), s = r2(68);
    t2.AudioWorkletSourceURLPropertyName = "MICROPHONE-WorkletSourceUrl";
    class o {
      constructor(e3, t3, r3, i3) {
        this.privRecorder = e3, this.deviceId = t3, this.privStreams = {}, this.privOutputChunkSize = o.AUDIOFORMAT.avgBytesPerSec / 10, this.privId = r3 || (0, n.createNoDashGuid)(), this.privEvents = new n.EventSource(), this.privMediaStream = i3 || null, this.privIsClosing = false;
      }
      get format() {
        return Promise.resolve(o.AUDIOFORMAT);
      }
      turnOn() {
        if (this.privInitializeDeferral) return this.privInitializeDeferral.promise;
        this.privInitializeDeferral = new n.Deferred();
        try {
          this.createAudioContext();
        } catch (e4) {
          if (e4 instanceof Error) {
            const t4 = e4;
            this.privInitializeDeferral.reject(t4.name + ": " + t4.message);
          } else this.privInitializeDeferral.reject(e4);
          return this.privInitializeDeferral.promise;
        }
        const e3 = window.navigator;
        let t3 = e3.getUserMedia || e3.webkitGetUserMedia || e3.mozGetUserMedia || e3.msGetUserMedia;
        if (e3.mediaDevices && (t3 = (t4, r3, i3) => {
          e3.mediaDevices.getUserMedia(t4).then(r3).catch(i3);
        }), t3) {
          const e4 = () => {
            this.onEvent(new n.AudioSourceInitializingEvent(this.privId)), this.privMediaStream && this.privMediaStream.active ? (this.onEvent(new n.AudioSourceReadyEvent(this.privId)), this.privInitializeDeferral.resolve()) : t3({ audio: !this.deviceId || { deviceId: this.deviceId }, video: false }, (e5) => {
              this.privMediaStream = e5, this.onEvent(new n.AudioSourceReadyEvent(this.privId)), this.privInitializeDeferral.resolve();
            }, (e5) => {
              const t4 = `Error occurred during microphone initialization: ${e5}`;
              this.privInitializeDeferral.reject(t4), this.onEvent(new n.AudioSourceErrorEvent(this.privId, t4));
            });
          };
          "suspended" === this.privContext.state ? this.privContext.resume().then(e4).catch((e5) => {
            this.privInitializeDeferral.reject(`Failed to initialize audio context: ${e5}`);
          }) : e4();
        } else {
          const e4 = "Browser does not support getUserMedia.";
          this.privInitializeDeferral.reject(e4), this.onEvent(new n.AudioSourceErrorEvent(e4, ""));
        }
        return this.privInitializeDeferral.promise;
      }
      id() {
        return this.privId;
      }
      attach(e3) {
        return this.onEvent(new n.AudioStreamNodeAttachingEvent(this.privId, e3)), this.listen(e3).then((t3) => (this.onEvent(new n.AudioStreamNodeAttachedEvent(this.privId, e3)), { detach: async () => (t3.readEnded(), delete this.privStreams[e3], this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3)), this.turnOff()), id: () => e3, read: () => t3.read() }));
      }
      detach(e3) {
        e3 && this.privStreams[e3] && (this.privStreams[e3].close(), delete this.privStreams[e3], this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3)));
      }
      async turnOff() {
        for (const e3 in this.privStreams) if (e3) {
          const t3 = this.privStreams[e3];
          t3 && t3.close();
        }
        this.onEvent(new n.AudioSourceOffEvent(this.privId)), this.privInitializeDeferral && (await this.privInitializeDeferral, this.privInitializeDeferral = null), await this.destroyAudioContext();
      }
      get events() {
        return this.privEvents;
      }
      get deviceInfo() {
        return this.getMicrophoneLabel().then((e3) => ({ bitspersample: o.AUDIOFORMAT.bitsPerSample, channelcount: o.AUDIOFORMAT.channels, connectivity: i2.connectivity.Unknown, manufacturer: "Speech SDK", model: e3, samplerate: o.AUDIOFORMAT.samplesPerSec, type: i2.type.Microphones }));
      }
      setProperty(e3, r3) {
        if (e3 !== t2.AudioWorkletSourceURLPropertyName) throw new Error("Property '" + e3 + "' is not supported on Microphone.");
        this.privRecorder.setWorkletUrl(r3);
      }
      getMicrophoneLabel() {
        const e3 = "microphone";
        if (void 0 !== this.privMicrophoneLabel) return Promise.resolve(this.privMicrophoneLabel);
        if (void 0 === this.privMediaStream || !this.privMediaStream.active) return Promise.resolve(e3);
        this.privMicrophoneLabel = e3;
        const t3 = this.privMediaStream.getTracks()[0].getSettings().deviceId;
        if (void 0 === t3) return Promise.resolve(this.privMicrophoneLabel);
        const r3 = new n.Deferred();
        return navigator.mediaDevices.enumerateDevices().then((e4) => {
          for (const r4 of e4) if (r4.deviceId === t3) {
            this.privMicrophoneLabel = r4.label;
            break;
          }
          r3.resolve(this.privMicrophoneLabel);
        }, () => r3.resolve(this.privMicrophoneLabel)), r3.promise;
      }
      async listen(e3) {
        await this.turnOn();
        const t3 = new n.ChunkedArrayBufferStream(this.privOutputChunkSize, e3);
        this.privStreams[e3] = t3;
        try {
          this.privRecorder.record(this.privContext, this.privMediaStream, t3);
        } catch (t4) {
          throw this.onEvent(new n.AudioStreamNodeErrorEvent(this.privId, e3, t4)), t4;
        }
        return t3;
      }
      onEvent(e3) {
        this.privEvents.onEvent(e3), n.Events.instance.onEvent(e3);
      }
      createAudioContext() {
        this.privContext || (this.privContext = s.AudioStreamFormatImpl.getAudioContext(o.AUDIOFORMAT.samplesPerSec));
      }
      async destroyAudioContext() {
        if (!this.privContext) return;
        this.privRecorder.releaseMediaResources(this.privContext);
        let e3 = false;
        "close" in this.privContext && (e3 = true), e3 ? this.privIsClosing || (this.privIsClosing = true, await this.privContext.close(), this.privContext = null, this.privIsClosing = false) : null !== this.privContext && "running" === this.privContext.state && await this.privContext.suspend();
      }
    }
    t2.MicAudioSource = o, o.AUDIOFORMAT = s.AudioStreamFormat.getDefaultInputFormat();
  }, (e2, t2) => {
    "use strict";
    var r2;
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AudioStreamFormatImpl = t2.AudioStreamFormat = t2.AudioFormatTag = void 0, function(e3) {
      e3[e3.PCM = 1] = "PCM", e3[e3.MuLaw = 2] = "MuLaw", e3[e3.Siren = 3] = "Siren", e3[e3.MP3 = 4] = "MP3", e3[e3.SILKSkype = 5] = "SILKSkype", e3[e3.OGG_OPUS = 6] = "OGG_OPUS", e3[e3.WEBM_OPUS = 7] = "WEBM_OPUS", e3[e3.ALaw = 8] = "ALaw", e3[e3.FLAC = 9] = "FLAC", e3[e3.OPUS = 10] = "OPUS", e3[e3.AMR_WB = 11] = "AMR_WB", e3[e3.G722 = 12] = "G722";
    }(r2 = t2.AudioFormatTag || (t2.AudioFormatTag = {}));
    class i2 {
      static getDefaultInputFormat() {
        return n.getDefaultInputFormat();
      }
      static getWaveFormat(e3, t3, r3, i3) {
        return new n(e3, t3, r3, i3);
      }
      static getWaveFormatPCM(e3, t3, r3) {
        return new n(e3, t3, r3);
      }
    }
    t2.AudioStreamFormat = i2;
    class n extends i2 {
      constructor(e3 = 16e3, t3 = 16, i3 = 1, n2 = r2.PCM) {
        super();
        let s = true;
        switch (n2) {
          case r2.PCM:
            this.formatTag = 1;
            break;
          case r2.ALaw:
            this.formatTag = 6;
            break;
          case r2.MuLaw:
            this.formatTag = 7;
            break;
          default:
            s = false;
        }
        if (this.bitsPerSample = t3, this.samplesPerSec = e3, this.channels = i3, this.avgBytesPerSec = this.samplesPerSec * this.channels * (this.bitsPerSample / 8), this.blockAlign = this.channels * Math.max(this.bitsPerSample, 8), s) {
          this.privHeader = new ArrayBuffer(44);
          const e4 = new DataView(this.privHeader);
          this.setString(e4, 0, "RIFF"), e4.setUint32(4, 0, true), this.setString(e4, 8, "WAVEfmt "), e4.setUint32(16, 16, true), e4.setUint16(20, this.formatTag, true), e4.setUint16(22, this.channels, true), e4.setUint32(24, this.samplesPerSec, true), e4.setUint32(28, this.avgBytesPerSec, true), e4.setUint16(32, this.channels * (this.bitsPerSample / 8), true), e4.setUint16(34, this.bitsPerSample, true), this.setString(e4, 36, "data"), e4.setUint32(40, 0, true);
        }
      }
      static getDefaultInputFormat() {
        return new n();
      }
      static getAudioContext(e3) {
        const t3 = window.AudioContext || window.webkitAudioContext || false;
        if (t3) return void 0 !== e3 && navigator.mediaDevices.getSupportedConstraints().sampleRate ? new t3({ sampleRate: e3 }) : new t3();
        throw new Error("Browser does not support Web Audio API (AudioContext is not available).");
      }
      close() {
      }
      get header() {
        return this.privHeader;
      }
      setString(e3, t3, r3) {
        for (let i3 = 0; i3 < r3.length; i3++) e3.setUint8(t3 + i3, r3.charCodeAt(i3));
      }
    }
    t2.AudioStreamFormatImpl = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.FileAudioSource = void 0;
    const i2 = r2(2), n = r2(4), s = r2(68);
    t2.FileAudioSource = class {
      constructor(e3, t3, r3) {
        this.privStreams = {}, this.privHeaderEnd = 44, this.privId = r3 || (0, n.createNoDashGuid)(), this.privEvents = new n.EventSource(), this.privSource = e3, "undefined" != typeof window && "undefined" != typeof Blob && this.privSource instanceof Blob ? this.privFilename = e3.name : this.privFilename = t3 || "unknown.wav", this.privAudioFormatPromise = this.readHeader();
      }
      get format() {
        return this.privAudioFormatPromise;
      }
      turnOn() {
        if (this.privFilename.lastIndexOf(".wav") !== this.privFilename.length - 4) {
          const e3 = this.privFilename + " is not supported. Only WAVE files are allowed at the moment.";
          return this.onEvent(new n.AudioSourceErrorEvent(e3, "")), Promise.reject(e3);
        }
        this.onEvent(new n.AudioSourceInitializingEvent(this.privId)), this.onEvent(new n.AudioSourceReadyEvent(this.privId));
      }
      id() {
        return this.privId;
      }
      async attach(e3) {
        this.onEvent(new n.AudioStreamNodeAttachingEvent(this.privId, e3));
        const t3 = await this.upload(e3);
        return this.onEvent(new n.AudioStreamNodeAttachedEvent(this.privId, e3)), Promise.resolve({ detach: async () => {
          t3.readEnded(), delete this.privStreams[e3], this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3)), await this.turnOff();
        }, id: () => e3, read: () => t3.read() });
      }
      detach(e3) {
        e3 && this.privStreams[e3] && (this.privStreams[e3].close(), delete this.privStreams[e3], this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3)));
      }
      turnOff() {
        for (const e3 in this.privStreams) if (e3) {
          const t3 = this.privStreams[e3];
          t3 && !t3.isClosed && t3.close();
        }
        return this.onEvent(new n.AudioSourceOffEvent(this.privId)), Promise.resolve();
      }
      get events() {
        return this.privEvents;
      }
      get deviceInfo() {
        return this.privAudioFormatPromise.then((e3) => Promise.resolve({ bitspersample: e3.bitsPerSample, channelcount: e3.channels, connectivity: i2.connectivity.Unknown, manufacturer: "Speech SDK", model: "File", samplerate: e3.samplesPerSec, type: i2.type.File }));
      }
      readHeader() {
        const e3 = this.privSource.slice(0, 4296), t3 = new n.Deferred(), r3 = (e4) => {
          const r4 = new DataView(e4), i3 = (e5) => String.fromCharCode(r4.getUint8(e5), r4.getUint8(e5 + 1), r4.getUint8(e5 + 2), r4.getUint8(e5 + 3));
          if ("RIFF" !== i3(0)) return void t3.reject("Invalid WAV header in file, RIFF was not found");
          if ("WAVE" !== i3(8) || "fmt " !== i3(12)) return void t3.reject("Invalid WAV header in file, WAVEfmt was not found");
          const n2 = r4.getInt32(16, true), o = r4.getUint16(22, true), a = r4.getUint32(24, true), c = r4.getUint16(34, true);
          let p = 36 + Math.max(n2 - 16, 0);
          for (; "data" !== i3(p); p += 2) if (p > 4288) return void t3.reject("Invalid WAV header in file, data block was not found");
          this.privHeaderEnd = p + 8, t3.resolve(s.AudioStreamFormat.getWaveFormatPCM(a, c, o));
        };
        if ("undefined" != typeof window && "undefined" != typeof Blob && e3 instanceof Blob) {
          const t4 = new FileReader();
          t4.onload = (e4) => {
            const t5 = e4.target.result;
            r3(t5);
          }, t4.readAsArrayBuffer(e3);
        } else {
          const t4 = e3;
          r3(t4.buffer.slice(t4.byteOffset, t4.byteOffset + t4.byteLength));
        }
        return t3.promise;
      }
      async upload(e3) {
        const t3 = (t4) => {
          const r3 = `Error occurred while processing '${this.privFilename}'. ${t4}`;
          throw this.onEvent(new n.AudioStreamNodeErrorEvent(this.privId, e3, r3)), new Error(r3);
        };
        try {
          await this.turnOn();
          const r3 = await this.privAudioFormatPromise, i3 = new n.ChunkedArrayBufferStream(r3.avgBytesPerSec / 10, e3);
          this.privStreams[e3] = i3;
          const s2 = this.privSource.slice(this.privHeaderEnd), o = (e4) => {
            i3.isClosed || (i3.writeStreamChunk({ buffer: e4, isEnd: false, timeReceived: Date.now() }), i3.close());
          };
          if ("undefined" != typeof window && "undefined" != typeof Blob && s2 instanceof Blob) {
            const e4 = new FileReader();
            e4.onerror = (e5) => t3(e5.toString()), e4.onload = (e5) => {
              const t4 = e5.target.result;
              o(t4);
            }, e4.readAsArrayBuffer(s2);
          } else {
            const e4 = s2;
            o(e4.buffer.slice(e4.byteOffset, e4.byteOffset + e4.byteLength));
          }
          return i3;
        } catch (e4) {
          t3(e4);
        }
      }
      onEvent(e3) {
        this.privEvents.onEvent(e3), n.Events.instance.onEvent(e3);
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PcmRecorder = void 0;
    const i2 = r2(4);
    t2.PcmRecorder = class {
      constructor(e3) {
        this.privStopInputOnRelease = e3;
      }
      record(e3, t3, r3) {
        const n = new i2.RiffPcmEncoder(e3.sampleRate, 16e3), s = e3.createMediaStreamSource(t3), o = () => {
          const i3 = (() => {
            let t4 = 0;
            try {
              return e3.createScriptProcessor(t4, 1, 1);
            } catch (r4) {
              t4 = 2048;
              let i4 = e3.sampleRate;
              for (; t4 < 16384 && i4 >= 32e3; ) t4 <<= 1, i4 >>= 1;
              return e3.createScriptProcessor(t4, 1, 1);
            }
          })();
          i3.onaudioprocess = (e4) => {
            const t4 = e4.inputBuffer.getChannelData(0);
            if (r3 && !r3.isClosed) {
              const e5 = n.encode(t4);
              e5 && r3.writeStreamChunk({ buffer: e5, isEnd: false, timeReceived: Date.now() });
            }
          }, s.connect(i3), i3.connect(e3.destination), this.privMediaResources = { scriptProcessorNode: i3, source: s, stream: t3 };
        }, a = !!this.privSpeechProcessorScript && "ignore" === this.privSpeechProcessorScript.toLowerCase();
        if (e3.audioWorklet && !a) {
          if (!this.privSpeechProcessorScript) {
            const e4 = new Blob(["class SP extends AudioWorkletProcessor {\n                    constructor(options) {\n                      super(options);\n                    }\n                    process(inputs, outputs) {\n                      const input = inputs[0];\n                      const output = [];\n                      for (let channel = 0; channel < input.length; channel += 1) {\n                        output[channel] = input[channel];\n                      }\n                      this.port.postMessage(output[0]);\n                      return true;\n                    }\n                  }\n                  registerProcessor('speech-processor', SP);"], { type: "application/javascript; charset=utf-8" });
            this.privSpeechProcessorScript = URL.createObjectURL(e4);
          }
          e3.audioWorklet.addModule(this.privSpeechProcessorScript).then(() => {
            const i3 = new AudioWorkletNode(e3, "speech-processor");
            i3.port.onmessage = (e4) => {
              const t4 = e4.data;
              if (r3 && !r3.isClosed) {
                const e5 = n.encode(t4);
                e5 && r3.writeStreamChunk({ buffer: e5, isEnd: false, timeReceived: Date.now() });
              }
            }, s.connect(i3), i3.connect(e3.destination), this.privMediaResources = { scriptProcessorNode: i3, source: s, stream: t3 };
          }).catch(() => {
            o();
          });
        } else try {
          o();
        } catch (e4) {
          throw new Error(`Unable to start audio worklet node for PCMRecorder: ${e4}`);
        }
      }
      releaseMediaResources(e3) {
        this.privMediaResources && (this.privMediaResources.scriptProcessorNode && (this.privMediaResources.scriptProcessorNode.disconnect(e3.destination), this.privMediaResources.scriptProcessorNode = null), this.privMediaResources.source && (this.privMediaResources.source.disconnect(), this.privStopInputOnRelease && this.privMediaResources.stream.getTracks().forEach((e4) => e4.stop()), this.privMediaResources.source = null));
      }
      setWorkletUrl(e3) {
        this.privSpeechProcessorScript = e3;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.WebsocketConnection = void 0;
    const i2 = r2(4), n = r2(72);
    t2.WebsocketConnection = class {
      constructor(e3, t3, r3, s, o, a = false, c) {
        if (this.privIsDisposed = false, !e3) throw new i2.ArgumentNullError("uri");
        if (!s) throw new i2.ArgumentNullError("messageFormatter");
        this.privMessageFormatter = s;
        let p = "", h = 0;
        if (t3) {
          for (const r4 in t3) if (r4) {
            p += 0 === h && -1 === e3.indexOf("?") ? "?" : "&";
            p += encodeURIComponent(r4);
            let i3 = t3[r4];
            i3 && (i3 = encodeURIComponent(i3), p += `=${i3}`), h++;
          }
        }
        if (r3) {
          for (const t4 in r3) if (t4) {
            p += 0 === h && -1 === e3.indexOf("?") ? "?" : "&";
            p += `${t4}=${encodeURIComponent(r3[t4])}`, h++;
          }
        }
        this.privUri = e3 + p, this.privId = c || (0, i2.createNoDashGuid)(), this.privConnectionMessageAdapter = new n.WebsocketMessageAdapter(this.privUri, this.id, this.privMessageFormatter, o, r3, a);
      }
      async dispose() {
        this.privIsDisposed = true, this.privConnectionMessageAdapter && await this.privConnectionMessageAdapter.close();
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      get id() {
        return this.privId;
      }
      get uri() {
        return this.privUri;
      }
      state() {
        return this.privConnectionMessageAdapter.state;
      }
      open() {
        return this.privConnectionMessageAdapter.open();
      }
      send(e3) {
        return this.privConnectionMessageAdapter.send(e3);
      }
      read() {
        return this.privConnectionMessageAdapter.read();
      }
      get events() {
        return this.privConnectionMessageAdapter.events;
      }
    };
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__setModuleDefault || (Object.create ? function(e3, t3) {
      Object.defineProperty(e3, "default", { enumerable: true, value: t3 });
    } : function(e3, t3) {
      e3.default = t3;
    }), s = this && this.__importStar || function(e3) {
      if (e3 && e3.__esModule) return e3;
      var t3 = {};
      if (null != e3) for (var r3 in e3) "default" !== r3 && Object.prototype.hasOwnProperty.call(e3, r3) && i2(t3, e3, r3);
      return n(t3, e3), t3;
    }, o = this && this.__importDefault || function(e3) {
      return e3 && e3.__esModule ? e3 : { default: e3 };
    };
    Object.defineProperty(t2, "__esModule", { value: true }), t2.WebsocketMessageAdapter = void 0;
    const a = s(r2(73)), c = s(r2(74)), p = o(r2(75)), h = o(r2(76)), u = o(r2(77)), d = r2(54), v = r2(4);
    class l {
      constructor(e3, t3, r3, i3, n2, s2) {
        if (!e3) throw new v.ArgumentNullError("uri");
        if (!r3) throw new v.ArgumentNullError("messageFormatter");
        this.proxyInfo = i3, this.privConnectionEvents = new v.EventSource(), this.privConnectionId = t3, this.privMessageFormatter = r3, this.privConnectionState = v.ConnectionState.None, this.privUri = e3, this.privHeaders = n2, this.privEnableCompression = s2, this.privHeaders[d.HeaderNames.ConnectionId] = this.privConnectionId, this.privHeaders.connectionId = this.privConnectionId, this.privLastErrorReceived = "";
      }
      get state() {
        return this.privConnectionState;
      }
      open() {
        if (this.privConnectionState === v.ConnectionState.Disconnected) return Promise.reject(`Cannot open a connection that is in ${this.privConnectionState} state`);
        if (this.privConnectionEstablishDeferral) return this.privConnectionEstablishDeferral.promise;
        this.privConnectionEstablishDeferral = new v.Deferred(), this.privCertificateValidatedDeferral = new v.Deferred(), this.privConnectionState = v.ConnectionState.Connecting;
        try {
          if ("undefined" == typeof WebSocket || l.forceNpmWebSocket) {
            let e3 = new URL(this.privUri).protocol;
            "wss:" === e3?.toLocaleLowerCase() ? e3 = "https:" : "ws:" === e3?.toLocaleLowerCase() && (e3 = "http:");
            const t3 = { headers: this.privHeaders, perMessageDeflate: this.privEnableCompression, followRedirects: "https:" === e3.toLocaleLowerCase() };
            this.privCertificateValidatedDeferral.resolve(), t3.agent = this.getAgent(), t3.agent.protocol = e3, this.privWebsocketClient = new u.default(this.privUri, t3), this.privWebsocketClient.on("redirect", (e4) => {
              const t4 = new v.ConnectionRedirectEvent(this.privConnectionId, e4, this.privUri, `Getting redirect URL from endpoint ${this.privUri} with redirect URL '${e4}'`);
              v.Events.instance.onEvent(t4);
            });
          } else this.privCertificateValidatedDeferral.resolve(), this.privWebsocketClient = new WebSocket(this.privUri);
          this.privWebsocketClient.binaryType = "arraybuffer", this.privReceivingMessageQueue = new v.Queue(), this.privDisconnectDeferral = new v.Deferred(), this.privSendMessageQueue = new v.Queue(), this.processSendQueue().catch((e3) => {
            v.Events.instance.onEvent(new v.BackgroundEvent(e3));
          });
        } catch (e3) {
          return this.privConnectionEstablishDeferral.resolve(new v.ConnectionOpenResponse(500, e3)), this.privConnectionEstablishDeferral.promise;
        }
        return this.onEvent(new v.ConnectionStartEvent(this.privConnectionId, this.privUri)), this.privWebsocketClient.onopen = () => {
          this.privCertificateValidatedDeferral.promise.then(() => {
            this.privConnectionState = v.ConnectionState.Connected, this.onEvent(new v.ConnectionEstablishedEvent(this.privConnectionId)), this.privConnectionEstablishDeferral.resolve(new v.ConnectionOpenResponse(200, ""));
          }, (e3) => {
            this.privConnectionEstablishDeferral.reject(e3);
          });
        }, this.privWebsocketClient.onerror = (e3) => {
          this.onEvent(new v.ConnectionErrorEvent(this.privConnectionId, e3.message, e3.type)), this.privLastErrorReceived = e3.message;
        }, this.privWebsocketClient.onclose = (e3) => {
          this.privConnectionState === v.ConnectionState.Connecting ? (this.privConnectionState = v.ConnectionState.Disconnected, this.privConnectionEstablishDeferral.resolve(new v.ConnectionOpenResponse(e3.code, e3.reason + " " + this.privLastErrorReceived))) : (this.privConnectionState = v.ConnectionState.Disconnected, this.privWebsocketClient = null, this.onEvent(new v.ConnectionClosedEvent(this.privConnectionId, e3.code, e3.reason))), this.onClose(e3.code, e3.reason).catch((e4) => {
            v.Events.instance.onEvent(new v.BackgroundEvent(e4));
          });
        }, this.privWebsocketClient.onmessage = (e3) => {
          const t3 = (/* @__PURE__ */ new Date()).toISOString();
          if (this.privConnectionState === v.ConnectionState.Connected) {
            const r3 = new v.Deferred();
            if (this.privReceivingMessageQueue.enqueueFromPromise(r3.promise), e3.data instanceof ArrayBuffer) {
              const i3 = new v.RawWebsocketMessage(v.MessageType.Binary, e3.data);
              this.privMessageFormatter.toConnectionMessage(i3).then((e4) => {
                this.onEvent(new v.ConnectionMessageReceivedEvent(this.privConnectionId, t3, e4)), r3.resolve(e4);
              }, (e4) => {
                r3.reject(`Invalid binary message format. Error: ${e4}`);
              });
            } else {
              const i3 = new v.RawWebsocketMessage(v.MessageType.Text, e3.data);
              this.privMessageFormatter.toConnectionMessage(i3).then((e4) => {
                this.onEvent(new v.ConnectionMessageReceivedEvent(this.privConnectionId, t3, e4)), r3.resolve(e4);
              }, (e4) => {
                r3.reject(`Invalid text message format. Error: ${e4}`);
              });
            }
          }
        }, this.privConnectionEstablishDeferral.promise;
      }
      send(e3) {
        if (this.privConnectionState !== v.ConnectionState.Connected) return Promise.reject(`Cannot send on connection that is in ${v.ConnectionState[this.privConnectionState]} state`);
        const t3 = new v.Deferred(), r3 = new v.Deferred();
        return this.privSendMessageQueue.enqueueFromPromise(r3.promise), this.privMessageFormatter.fromConnectionMessage(e3).then((i3) => {
          r3.resolve({ Message: e3, RawWebsocketMessage: i3, sendStatusDeferral: t3 });
        }, (e4) => {
          r3.reject(`Error formatting the message. ${e4}`);
        }), t3.promise;
      }
      read() {
        return this.privConnectionState !== v.ConnectionState.Connected ? Promise.reject(`Cannot read on connection that is in ${this.privConnectionState} state`) : this.privReceivingMessageQueue.dequeue();
      }
      close(e3) {
        return this.privWebsocketClient ? (this.privConnectionState !== v.ConnectionState.Disconnected && this.privWebsocketClient.close(1e3, e3 || "Normal closure by client"), this.privDisconnectDeferral.promise) : Promise.resolve();
      }
      get events() {
        return this.privConnectionEvents;
      }
      sendRawMessage(e3) {
        try {
          return e3 ? (this.onEvent(new v.ConnectionMessageSentEvent(this.privConnectionId, (/* @__PURE__ */ new Date()).toISOString(), e3.Message)), this.isWebsocketOpen ? (this.privWebsocketClient.send(e3.RawWebsocketMessage.payload), Promise.resolve()) : Promise.reject("websocket send error: Websocket not ready " + this.privConnectionId + " " + e3.Message.id + " " + new Error().stack)) : Promise.resolve();
        } catch (e4) {
          return Promise.reject(`websocket send error: ${e4}`);
        }
      }
      async onClose(e3, t3) {
        const r3 = `Connection closed. ${e3}: ${t3}`;
        this.privConnectionState = v.ConnectionState.Disconnected, this.privDisconnectDeferral.resolve(), await this.privReceivingMessageQueue.drainAndDispose(() => {
        }, r3), await this.privSendMessageQueue.drainAndDispose((e4) => {
          e4.sendStatusDeferral.reject(r3);
        }, r3);
      }
      async processSendQueue() {
        for (; ; ) {
          const e3 = this.privSendMessageQueue.dequeue(), t3 = await e3;
          if (!t3) return;
          try {
            await this.sendRawMessage(t3), t3.sendStatusDeferral.resolve();
          } catch (e4) {
            t3.sendStatusDeferral.reject(e4);
          }
        }
      }
      onEvent(e3) {
        this.privConnectionEvents.onEvent(e3), v.Events.instance.onEvent(e3);
      }
      getAgent() {
        const e3 = new p.default.Agent(this.createConnection);
        return void 0 !== this.proxyInfo && void 0 !== this.proxyInfo.HostName && this.proxyInfo.Port > 0 && (e3.proxyInfo = this.proxyInfo), e3;
      }
      static GetProxyAgent(e3) {
        const t3 = { host: e3.HostName, port: e3.Port };
        e3.UserName ? t3.headers = { "Proxy-Authentication": "Basic " + Buffer.from(`${e3.UserName}:${void 0 === e3.Password ? "" : e3.Password}`).toString("base64") } : t3.headers = {}, t3.headers.requestOCSP = "true";
        return new h.default(t3);
      }
      createConnection(e3, t3) {
        let r3;
        if (t3 = { ...t3, requestOCSP: true, servername: t3.host }, this.proxyInfo) {
          const i3 = l.GetProxyAgent(this.proxyInfo);
          r3 = new Promise((r4, n2) => {
            i3.callback(e3, t3, (e4, t4) => {
              e4 ? n2(e4) : r4(t4);
            });
          });
        } else r3 = t3.secureEndpoint ? Promise.resolve(c.connect(t3)) : Promise.resolve(a.connect(t3));
        return r3;
      }
      get isWebsocketOpen() {
        return this.privWebsocketClient && this.privWebsocketClient.readyState === this.privWebsocketClient.OPEN;
      }
    }
    t2.WebsocketMessageAdapter = l, l.forceNpmWebSocket = false;
  }, () => {
  }, () => {
  }, () => {
  }, () => {
  }, () => {
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ReplayableAudioNode = void 0;
    t2.ReplayableAudioNode = class {
      constructor(e3, t3) {
        this.privBuffers = [], this.privReplayOffset = 0, this.privLastShrinkOffset = 0, this.privBufferStartOffset = 0, this.privBufferSerial = 0, this.privBufferedBytes = 0, this.privReplay = false, this.privLastChunkAcquiredTime = 0, this.privAudioNode = e3, this.privBytesPerSecond = t3;
      }
      id() {
        return this.privAudioNode.id();
      }
      read() {
        if (this.privReplay && 0 !== this.privBuffers.length) {
          const e3 = this.privReplayOffset - this.privBufferStartOffset;
          let t3 = Math.round(e3 * this.privBytesPerSecond * 1e-7);
          0 != t3 % 2 && t3++;
          let r3 = 0;
          for (; r3 < this.privBuffers.length && t3 >= this.privBuffers[r3].chunk.buffer.byteLength; ) t3 -= this.privBuffers[r3++].chunk.buffer.byteLength;
          if (r3 < this.privBuffers.length) {
            const e4 = this.privBuffers[r3].chunk.buffer.slice(t3);
            return this.privReplayOffset += e4.byteLength / this.privBytesPerSecond * 1e7, r3 === this.privBuffers.length - 1 && (this.privReplay = false), Promise.resolve({ buffer: e4, isEnd: false, timeReceived: this.privBuffers[r3].chunk.timeReceived });
          }
        }
        return this.privAudioNode.read().then((e3) => (e3 && e3.buffer && this.privBuffers && (this.privBuffers.push(new r2(e3, this.privBufferSerial++, this.privBufferedBytes)), this.privBufferedBytes += e3.buffer.byteLength), e3));
      }
      detach() {
        return this.privBuffers = void 0, this.privAudioNode.detach();
      }
      replay() {
        this.privBuffers && 0 !== this.privBuffers.length && (this.privReplay = true, this.privReplayOffset = this.privLastShrinkOffset);
      }
      shrinkBuffers(e3) {
        if (void 0 === this.privBuffers || 0 === this.privBuffers.length) return;
        this.privLastShrinkOffset = e3;
        const t3 = e3 - this.privBufferStartOffset;
        let r3 = Math.round(t3 * this.privBytesPerSecond * 1e-7), i2 = 0;
        for (; i2 < this.privBuffers.length && r3 >= this.privBuffers[i2].chunk.buffer.byteLength; ) r3 -= this.privBuffers[i2++].chunk.buffer.byteLength;
        this.privBufferStartOffset = Math.round(e3 - r3 / this.privBytesPerSecond * 1e7), this.privBuffers = this.privBuffers.slice(i2);
      }
      findTimeAtOffset(e3) {
        if (e3 < this.privBufferStartOffset || void 0 === this.privBuffers) return 0;
        for (const t3 of this.privBuffers) {
          const r3 = t3.byteOffset / this.privBytesPerSecond * 1e7, i2 = r3 + t3.chunk.buffer.byteLength / this.privBytesPerSecond * 1e7;
          if (e3 >= r3 && e3 <= i2) return t3.chunk.timeReceived;
        }
        return 0;
      }
    };
    class r2 {
      constructor(e3, t3, r3) {
        this.chunk = e3, this.serial = t3, this.byteOffset = r3;
      }
    }
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ProxyInfo = void 0;
    const i2 = r2(80);
    class n {
      constructor(e3, t3, r3, i3) {
        this.privProxyHostName = e3, this.privProxyPort = t3, this.privProxyUserName = r3, this.privProxyPassword = i3;
      }
      static fromParameters(e3) {
        return new n(e3.getProperty(i2.PropertyId.SpeechServiceConnection_ProxyHostName), parseInt(e3.getProperty(i2.PropertyId.SpeechServiceConnection_ProxyPort), 10), e3.getProperty(i2.PropertyId.SpeechServiceConnection_ProxyUserName), e3.getProperty(i2.PropertyId.SpeechServiceConnection_ProxyPassword));
      }
      static fromRecognizerConfig(e3) {
        return this.fromParameters(e3.parameters);
      }
      get HostName() {
        return this.privProxyHostName;
      }
      get Port() {
        return this.privProxyPort;
      }
      get UserName() {
        return this.privProxyUserName;
      }
      get Password() {
        return this.privProxyPassword;
      }
    }
    t2.ProxyInfo = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CustomCommandsConfig = t2.BotFrameworkConfig = t2.DialogServiceConfig = t2.PhraseListGrammar = t2.Connection = t2.ServiceEventArgs = t2.ConnectionEventArgs = t2.CancellationErrorCode = t2.CancellationDetails = t2.CancellationDetailsBase = t2.TranslationRecognitionCanceledEventArgs = t2.NoMatchDetails = t2.NoMatchReason = t2.Translations = t2.TranslationRecognizer = t2.SpeechRecognizer = t2.Recognizer = t2.PropertyId = t2.PropertyCollection = t2.SpeechTranslationConfigImpl = t2.SpeechTranslationConfig = t2.SpeechConfigImpl = t2.SpeechConfig = t2.ResultReason = t2.TranslationSynthesisResult = t2.TranslationRecognitionResult = t2.TranslationSynthesisEventArgs = t2.TranslationRecognitionEventArgs = t2.SpeechRecognitionCanceledEventArgs = t2.MeetingTranscriptionEventArgs = t2.ConversationTranscriptionEventArgs = t2.SpeechRecognitionEventArgs = t2.SpeechRecognitionResult = t2.RecognitionResult = t2.OutputFormat = t2.RecognitionEventArgs = t2.SessionEventArgs = t2.KeywordRecognitionModel = t2.PushAudioOutputStreamCallback = t2.PullAudioInputStreamCallback = t2.CancellationReason = t2.PushAudioOutputStream = t2.PullAudioOutputStream = t2.AudioOutputStream = t2.PushAudioInputStream = t2.PullAudioInputStream = t2.AudioInputStream = t2.AudioFormatTag = t2.AudioStreamFormat = t2.AudioConfig = void 0, t2.AvatarSynthesizer = t2.AvatarEventArgs = t2.AvatarConfig = t2.LanguageIdMode = t2.PronunciationAssessmentResult = t2.PronunciationAssessmentConfig = t2.PronunciationAssessmentGranularity = t2.PronunciationAssessmentGradingSystem = t2.MeetingTranscriptionCanceledEventArgs = t2.ConversationTranscriptionCanceledEventArgs = t2.SpeakerAudioDestination = t2.VoiceInfo = t2.SynthesisVoiceType = t2.SynthesisVoiceGender = t2.SynthesisVoicesResult = t2.SpeechSynthesisBoundaryType = t2.SpeechSynthesisVisemeEventArgs = t2.SpeechSynthesisBookmarkEventArgs = t2.SpeechSynthesisWordBoundaryEventArgs = t2.SpeechSynthesisEventArgs = t2.SpeechSynthesisResult = t2.SynthesisResult = t2.SpeechSynthesizer = t2.SpeechSynthesisOutputFormat = t2.Synthesizer = t2.User = t2.ParticipantChangedReason = t2.Participant = t2.MeetingTranscriber = t2.Meeting = t2.ConversationTranscriptionResult = t2.ConversationTranscriber = t2.ConversationTranslator = t2.ConversationTranslationResult = t2.ConversationTranslationEventArgs = t2.ConversationTranslationCanceledEventArgs = t2.ConversationParticipantsChangedEventArgs = t2.ConversationExpirationEventArgs = t2.Conversation = t2.SourceLanguageConfig = t2.AutoDetectSourceLanguageResult = t2.AutoDetectSourceLanguageConfig = t2.ConnectionMessage = t2.ConnectionMessageEventArgs = t2.BaseAudioPlayer = t2.ProfanityOption = t2.ServicePropertyChannel = t2.TurnStatusReceivedEventArgs = t2.ActivityReceivedEventArgs = t2.DialogServiceConnector = void 0, t2.LogLevel = t2.Diagnostics = t2.AvatarWebRTCConnectionResult = t2.Coordinate = t2.AvatarVideoFormat = void 0;
    var i2 = r2(81);
    Object.defineProperty(t2, "AudioConfig", { enumerable: true, get: function() {
      return i2.AudioConfig;
    } });
    var n = r2(68);
    Object.defineProperty(t2, "AudioStreamFormat", { enumerable: true, get: function() {
      return n.AudioStreamFormat;
    } }), Object.defineProperty(t2, "AudioFormatTag", { enumerable: true, get: function() {
      return n.AudioFormatTag;
    } });
    var s = r2(84);
    Object.defineProperty(t2, "AudioInputStream", { enumerable: true, get: function() {
      return s.AudioInputStream;
    } }), Object.defineProperty(t2, "PullAudioInputStream", { enumerable: true, get: function() {
      return s.PullAudioInputStream;
    } }), Object.defineProperty(t2, "PushAudioInputStream", { enumerable: true, get: function() {
      return s.PushAudioInputStream;
    } });
    var o = r2(85);
    Object.defineProperty(t2, "AudioOutputStream", { enumerable: true, get: function() {
      return o.AudioOutputStream;
    } }), Object.defineProperty(t2, "PullAudioOutputStream", { enumerable: true, get: function() {
      return o.PullAudioOutputStream;
    } }), Object.defineProperty(t2, "PushAudioOutputStream", { enumerable: true, get: function() {
      return o.PushAudioOutputStream;
    } });
    var a = r2(88);
    Object.defineProperty(t2, "CancellationReason", { enumerable: true, get: function() {
      return a.CancellationReason;
    } });
    var c = r2(89);
    Object.defineProperty(t2, "PullAudioInputStreamCallback", { enumerable: true, get: function() {
      return c.PullAudioInputStreamCallback;
    } });
    var p = r2(90);
    Object.defineProperty(t2, "PushAudioOutputStreamCallback", { enumerable: true, get: function() {
      return p.PushAudioOutputStreamCallback;
    } });
    var h = r2(91);
    Object.defineProperty(t2, "KeywordRecognitionModel", { enumerable: true, get: function() {
      return h.KeywordRecognitionModel;
    } });
    var u = r2(92);
    Object.defineProperty(t2, "SessionEventArgs", { enumerable: true, get: function() {
      return u.SessionEventArgs;
    } });
    var d = r2(93);
    Object.defineProperty(t2, "RecognitionEventArgs", { enumerable: true, get: function() {
      return d.RecognitionEventArgs;
    } });
    var v = r2(94);
    Object.defineProperty(t2, "OutputFormat", { enumerable: true, get: function() {
      return v.OutputFormat;
    } });
    var l = r2(95);
    Object.defineProperty(t2, "RecognitionResult", { enumerable: true, get: function() {
      return l.RecognitionResult;
    } });
    var g = r2(96);
    Object.defineProperty(t2, "SpeechRecognitionResult", { enumerable: true, get: function() {
      return g.SpeechRecognitionResult;
    } });
    var m = r2(97);
    Object.defineProperty(t2, "SpeechRecognitionEventArgs", { enumerable: true, get: function() {
      return m.SpeechRecognitionEventArgs;
    } }), Object.defineProperty(t2, "ConversationTranscriptionEventArgs", { enumerable: true, get: function() {
      return m.ConversationTranscriptionEventArgs;
    } }), Object.defineProperty(t2, "MeetingTranscriptionEventArgs", { enumerable: true, get: function() {
      return m.MeetingTranscriptionEventArgs;
    } });
    var S = r2(98);
    Object.defineProperty(t2, "SpeechRecognitionCanceledEventArgs", { enumerable: true, get: function() {
      return S.SpeechRecognitionCanceledEventArgs;
    } });
    var f = r2(100);
    Object.defineProperty(t2, "TranslationRecognitionEventArgs", { enumerable: true, get: function() {
      return f.TranslationRecognitionEventArgs;
    } });
    var y = r2(101);
    Object.defineProperty(t2, "TranslationSynthesisEventArgs", { enumerable: true, get: function() {
      return y.TranslationSynthesisEventArgs;
    } });
    var C = r2(102);
    Object.defineProperty(t2, "TranslationRecognitionResult", { enumerable: true, get: function() {
      return C.TranslationRecognitionResult;
    } });
    var P = r2(103);
    Object.defineProperty(t2, "TranslationSynthesisResult", { enumerable: true, get: function() {
      return P.TranslationSynthesisResult;
    } });
    var R = r2(104);
    Object.defineProperty(t2, "ResultReason", { enumerable: true, get: function() {
      return R.ResultReason;
    } });
    var I = r2(105);
    Object.defineProperty(t2, "SpeechConfig", { enumerable: true, get: function() {
      return I.SpeechConfig;
    } }), Object.defineProperty(t2, "SpeechConfigImpl", { enumerable: true, get: function() {
      return I.SpeechConfigImpl;
    } });
    var T = r2(106);
    Object.defineProperty(t2, "SpeechTranslationConfig", { enumerable: true, get: function() {
      return T.SpeechTranslationConfig;
    } }), Object.defineProperty(t2, "SpeechTranslationConfigImpl", { enumerable: true, get: function() {
      return T.SpeechTranslationConfigImpl;
    } });
    var w = r2(107);
    Object.defineProperty(t2, "PropertyCollection", { enumerable: true, get: function() {
      return w.PropertyCollection;
    } });
    var E = r2(108);
    Object.defineProperty(t2, "PropertyId", { enumerable: true, get: function() {
      return E.PropertyId;
    } });
    var A = r2(109);
    Object.defineProperty(t2, "Recognizer", { enumerable: true, get: function() {
      return A.Recognizer;
    } });
    var b = r2(110);
    Object.defineProperty(t2, "SpeechRecognizer", { enumerable: true, get: function() {
      return b.SpeechRecognizer;
    } });
    var O = r2(112);
    Object.defineProperty(t2, "TranslationRecognizer", { enumerable: true, get: function() {
      return O.TranslationRecognizer;
    } });
    var M = r2(115);
    Object.defineProperty(t2, "Translations", { enumerable: true, get: function() {
      return M.Translations;
    } });
    var D = r2(116);
    Object.defineProperty(t2, "NoMatchReason", { enumerable: true, get: function() {
      return D.NoMatchReason;
    } });
    var N = r2(117);
    Object.defineProperty(t2, "NoMatchDetails", { enumerable: true, get: function() {
      return N.NoMatchDetails;
    } });
    var k = r2(118);
    Object.defineProperty(t2, "TranslationRecognitionCanceledEventArgs", { enumerable: true, get: function() {
      return k.TranslationRecognitionCanceledEventArgs;
    } });
    var _ = r2(119);
    Object.defineProperty(t2, "CancellationDetailsBase", { enumerable: true, get: function() {
      return _.CancellationDetailsBase;
    } });
    var z = r2(120);
    Object.defineProperty(t2, "CancellationDetails", { enumerable: true, get: function() {
      return z.CancellationDetails;
    } });
    var L = r2(121);
    Object.defineProperty(t2, "CancellationErrorCode", { enumerable: true, get: function() {
      return L.CancellationErrorCode;
    } });
    var x = r2(122);
    Object.defineProperty(t2, "ConnectionEventArgs", { enumerable: true, get: function() {
      return x.ConnectionEventArgs;
    } });
    var F = r2(123);
    Object.defineProperty(t2, "ServiceEventArgs", { enumerable: true, get: function() {
      return F.ServiceEventArgs;
    } });
    var B = r2(113);
    Object.defineProperty(t2, "Connection", { enumerable: true, get: function() {
      return B.Connection;
    } });
    var j = r2(124);
    Object.defineProperty(t2, "PhraseListGrammar", { enumerable: true, get: function() {
      return j.PhraseListGrammar;
    } });
    var U = r2(125);
    Object.defineProperty(t2, "DialogServiceConfig", { enumerable: true, get: function() {
      return U.DialogServiceConfig;
    } });
    var q = r2(126);
    Object.defineProperty(t2, "BotFrameworkConfig", { enumerable: true, get: function() {
      return q.BotFrameworkConfig;
    } });
    var W = r2(127);
    Object.defineProperty(t2, "CustomCommandsConfig", { enumerable: true, get: function() {
      return W.CustomCommandsConfig;
    } });
    var H = r2(128);
    Object.defineProperty(t2, "DialogServiceConnector", { enumerable: true, get: function() {
      return H.DialogServiceConnector;
    } });
    var K = r2(132);
    Object.defineProperty(t2, "ActivityReceivedEventArgs", { enumerable: true, get: function() {
      return K.ActivityReceivedEventArgs;
    } });
    var J = r2(133);
    Object.defineProperty(t2, "TurnStatusReceivedEventArgs", { enumerable: true, get: function() {
      return J.TurnStatusReceivedEventArgs;
    } });
    var V = r2(135);
    Object.defineProperty(t2, "ServicePropertyChannel", { enumerable: true, get: function() {
      return V.ServicePropertyChannel;
    } });
    var G = r2(136);
    Object.defineProperty(t2, "ProfanityOption", { enumerable: true, get: function() {
      return G.ProfanityOption;
    } });
    var Q = r2(137);
    Object.defineProperty(t2, "BaseAudioPlayer", { enumerable: true, get: function() {
      return Q.BaseAudioPlayer;
    } });
    var $ = r2(138);
    Object.defineProperty(t2, "ConnectionMessageEventArgs", { enumerable: true, get: function() {
      return $.ConnectionMessageEventArgs;
    } });
    var X = r2(114);
    Object.defineProperty(t2, "ConnectionMessage", { enumerable: true, get: function() {
      return X.ConnectionMessage;
    } });
    var Z = r2(139);
    Object.defineProperty(t2, "AutoDetectSourceLanguageConfig", { enumerable: true, get: function() {
      return Z.AutoDetectSourceLanguageConfig;
    } });
    var Y = r2(141);
    Object.defineProperty(t2, "AutoDetectSourceLanguageResult", { enumerable: true, get: function() {
      return Y.AutoDetectSourceLanguageResult;
    } });
    var ee = r2(142);
    Object.defineProperty(t2, "SourceLanguageConfig", { enumerable: true, get: function() {
      return ee.SourceLanguageConfig;
    } });
    var te = r2(143);
    Object.defineProperty(t2, "Conversation", { enumerable: true, get: function() {
      return te.Conversation;
    } }), Object.defineProperty(t2, "ConversationExpirationEventArgs", { enumerable: true, get: function() {
      return te.ConversationExpirationEventArgs;
    } }), Object.defineProperty(t2, "ConversationParticipantsChangedEventArgs", { enumerable: true, get: function() {
      return te.ConversationParticipantsChangedEventArgs;
    } }), Object.defineProperty(t2, "ConversationTranslationCanceledEventArgs", { enumerable: true, get: function() {
      return te.ConversationTranslationCanceledEventArgs;
    } }), Object.defineProperty(t2, "ConversationTranslationEventArgs", { enumerable: true, get: function() {
      return te.ConversationTranslationEventArgs;
    } }), Object.defineProperty(t2, "ConversationTranslationResult", { enumerable: true, get: function() {
      return te.ConversationTranslationResult;
    } }), Object.defineProperty(t2, "ConversationTranslator", { enumerable: true, get: function() {
      return te.ConversationTranslator;
    } }), Object.defineProperty(t2, "ConversationTranscriber", { enumerable: true, get: function() {
      return te.ConversationTranscriber;
    } }), Object.defineProperty(t2, "ConversationTranscriptionResult", { enumerable: true, get: function() {
      return te.ConversationTranscriptionResult;
    } }), Object.defineProperty(t2, "Meeting", { enumerable: true, get: function() {
      return te.Meeting;
    } }), Object.defineProperty(t2, "MeetingTranscriber", { enumerable: true, get: function() {
      return te.MeetingTranscriber;
    } }), Object.defineProperty(t2, "Participant", { enumerable: true, get: function() {
      return te.Participant;
    } }), Object.defineProperty(t2, "ParticipantChangedReason", { enumerable: true, get: function() {
      return te.ParticipantChangedReason;
    } }), Object.defineProperty(t2, "User", { enumerable: true, get: function() {
      return te.User;
    } });
    var re = r2(161);
    Object.defineProperty(t2, "Synthesizer", { enumerable: true, get: function() {
      return re.Synthesizer;
    } });
    var ie = r2(87);
    Object.defineProperty(t2, "SpeechSynthesisOutputFormat", { enumerable: true, get: function() {
      return ie.SpeechSynthesisOutputFormat;
    } });
    var ne = r2(162);
    Object.defineProperty(t2, "SpeechSynthesizer", { enumerable: true, get: function() {
      return ne.SpeechSynthesizer;
    } });
    var se = r2(163);
    Object.defineProperty(t2, "SynthesisResult", { enumerable: true, get: function() {
      return se.SynthesisResult;
    } });
    var oe = r2(164);
    Object.defineProperty(t2, "SpeechSynthesisResult", { enumerable: true, get: function() {
      return oe.SpeechSynthesisResult;
    } });
    var ae = r2(165);
    Object.defineProperty(t2, "SpeechSynthesisEventArgs", { enumerable: true, get: function() {
      return ae.SpeechSynthesisEventArgs;
    } });
    var ce = r2(166);
    Object.defineProperty(t2, "SpeechSynthesisWordBoundaryEventArgs", { enumerable: true, get: function() {
      return ce.SpeechSynthesisWordBoundaryEventArgs;
    } });
    var pe = r2(167);
    Object.defineProperty(t2, "SpeechSynthesisBookmarkEventArgs", { enumerable: true, get: function() {
      return pe.SpeechSynthesisBookmarkEventArgs;
    } });
    var he = r2(168);
    Object.defineProperty(t2, "SpeechSynthesisVisemeEventArgs", { enumerable: true, get: function() {
      return he.SpeechSynthesisVisemeEventArgs;
    } });
    var ue = r2(169);
    Object.defineProperty(t2, "SpeechSynthesisBoundaryType", { enumerable: true, get: function() {
      return ue.SpeechSynthesisBoundaryType;
    } });
    var de = r2(170);
    Object.defineProperty(t2, "SynthesisVoicesResult", { enumerable: true, get: function() {
      return de.SynthesisVoicesResult;
    } });
    var ve = r2(171);
    Object.defineProperty(t2, "SynthesisVoiceGender", { enumerable: true, get: function() {
      return ve.SynthesisVoiceGender;
    } }), Object.defineProperty(t2, "SynthesisVoiceType", { enumerable: true, get: function() {
      return ve.SynthesisVoiceType;
    } }), Object.defineProperty(t2, "VoiceInfo", { enumerable: true, get: function() {
      return ve.VoiceInfo;
    } });
    var le = r2(172);
    Object.defineProperty(t2, "SpeakerAudioDestination", { enumerable: true, get: function() {
      return le.SpeakerAudioDestination;
    } });
    var ge = r2(173);
    Object.defineProperty(t2, "ConversationTranscriptionCanceledEventArgs", { enumerable: true, get: function() {
      return ge.ConversationTranscriptionCanceledEventArgs;
    } });
    var me = r2(174);
    Object.defineProperty(t2, "MeetingTranscriptionCanceledEventArgs", { enumerable: true, get: function() {
      return me.MeetingTranscriptionCanceledEventArgs;
    } });
    var Se = r2(175);
    Object.defineProperty(t2, "PronunciationAssessmentGradingSystem", { enumerable: true, get: function() {
      return Se.PronunciationAssessmentGradingSystem;
    } });
    var fe = r2(176);
    Object.defineProperty(t2, "PronunciationAssessmentGranularity", { enumerable: true, get: function() {
      return fe.PronunciationAssessmentGranularity;
    } });
    var ye = r2(177);
    Object.defineProperty(t2, "PronunciationAssessmentConfig", { enumerable: true, get: function() {
      return ye.PronunciationAssessmentConfig;
    } });
    var Ce = r2(178);
    Object.defineProperty(t2, "PronunciationAssessmentResult", { enumerable: true, get: function() {
      return Ce.PronunciationAssessmentResult;
    } });
    var Pe = r2(140);
    Object.defineProperty(t2, "LanguageIdMode", { enumerable: true, get: function() {
      return Pe.LanguageIdMode;
    } });
    var Re = r2(179);
    Object.defineProperty(t2, "AvatarConfig", { enumerable: true, get: function() {
      return Re.AvatarConfig;
    } });
    var Ie = r2(180);
    Object.defineProperty(t2, "AvatarEventArgs", { enumerable: true, get: function() {
      return Ie.AvatarEventArgs;
    } });
    var Te = r2(181);
    Object.defineProperty(t2, "AvatarSynthesizer", { enumerable: true, get: function() {
      return Te.AvatarSynthesizer;
    } });
    var we = r2(183);
    Object.defineProperty(t2, "AvatarVideoFormat", { enumerable: true, get: function() {
      return we.AvatarVideoFormat;
    } }), Object.defineProperty(t2, "Coordinate", { enumerable: true, get: function() {
      return we.Coordinate;
    } });
    var Ee = r2(184);
    Object.defineProperty(t2, "AvatarWebRTCConnectionResult", { enumerable: true, get: function() {
      return Ee.AvatarWebRTCConnectionResult;
    } });
    var Ae = r2(185);
    Object.defineProperty(t2, "Diagnostics", { enumerable: true, get: function() {
      return Ae.Diagnostics;
    } });
    var be = r2(64);
    Object.defineProperty(t2, "LogLevel", { enumerable: true, get: function() {
      return be.LogLevel;
    } });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AudioOutputConfigImpl = t2.AudioConfigImpl = t2.AudioConfig = void 0;
    const i2 = r2(61), n = r2(65), s = r2(80), o = r2(82), a = r2(84), c = r2(85);
    class p {
      static fromDefaultMicrophoneInput() {
        const e3 = new i2.PcmRecorder(true);
        return new h(new i2.MicAudioSource(e3));
      }
      static fromMicrophoneInput(e3) {
        const t3 = new i2.PcmRecorder(true);
        return new h(new i2.MicAudioSource(t3, e3));
      }
      static fromWavFileInput(e3, t3 = "unnamedBuffer.wav") {
        return new h(new i2.FileAudioSource(e3, t3));
      }
      static fromStreamInput(e3) {
        if (e3 instanceof s.PullAudioInputStreamCallback) return new h(new a.PullAudioInputStreamImpl(e3));
        if (e3 instanceof s.AudioInputStream) return new h(e3);
        if ("undefined" != typeof MediaStream && e3 instanceof MediaStream) {
          const t3 = new i2.PcmRecorder(false);
          return new h(new i2.MicAudioSource(t3, null, null, e3));
        }
        throw new Error("Not Supported Type");
      }
      static fromDefaultSpeakerOutput() {
        return new u(new s.SpeakerAudioDestination());
      }
      static fromSpeakerOutput(e3) {
        if (void 0 === e3) return p.fromDefaultSpeakerOutput();
        if (e3 instanceof s.SpeakerAudioDestination) return new u(e3);
        throw new Error("Not Supported Type");
      }
      static fromAudioFileOutput(e3) {
        return new u(new o.AudioFileWriter(e3));
      }
      static fromStreamOutput(e3) {
        if (e3 instanceof s.PushAudioOutputStreamCallback) return new u(new c.PushAudioOutputStreamImpl(e3));
        if (e3 instanceof s.PushAudioOutputStream) return new u(e3);
        if (e3 instanceof s.PullAudioOutputStream) return new u(e3);
        throw new Error("Not Supported Type");
      }
    }
    t2.AudioConfig = p;
    class h extends p {
      constructor(e3) {
        super(), this.privSource = e3;
      }
      get format() {
        return this.privSource.format;
      }
      close(e3, t3) {
        this.privSource.turnOff().then(() => {
          e3 && e3();
        }, (e4) => {
          t3 && t3(e4);
        });
      }
      id() {
        return this.privSource.id();
      }
      turnOn() {
        return this.privSource.turnOn();
      }
      attach(e3) {
        return this.privSource.attach(e3);
      }
      detach(e3) {
        return this.privSource.detach(e3);
      }
      turnOff() {
        return this.privSource.turnOff();
      }
      get events() {
        return this.privSource.events;
      }
      setProperty(e3, t3) {
        if (n.Contracts.throwIfNull(t3, "value"), void 0 === this.privSource.setProperty) throw new Error("This AudioConfig instance does not support setting properties.");
        this.privSource.setProperty(e3, t3);
      }
      getProperty(e3, t3) {
        if (void 0 !== this.privSource.getProperty) return this.privSource.getProperty(e3, t3);
        throw new Error("This AudioConfig instance does not support getting properties.");
      }
      get deviceInfo() {
        return this.privSource.deviceInfo;
      }
    }
    t2.AudioConfigImpl = h;
    class u extends p {
      constructor(e3) {
        super(), this.privDestination = e3;
      }
      set format(e3) {
        this.privDestination.format = e3;
      }
      write(e3) {
        this.privDestination.write(e3);
      }
      close() {
        this.privDestination.close();
      }
      id() {
        return this.privDestination.id();
      }
      setProperty() {
        throw new Error("This AudioConfig instance does not support setting properties.");
      }
      getProperty() {
        throw new Error("This AudioConfig instance does not support getting properties.");
      }
    }
    t2.AudioOutputConfigImpl = u;
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__createBinding || (Object.create ? function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), Object.defineProperty(e3, i3, { enumerable: true, get: function() {
        return t3[r3];
      } });
    } : function(e3, t3, r3, i3) {
      void 0 === i3 && (i3 = r3), e3[i3] = t3[r3];
    }), n = this && this.__setModuleDefault || (Object.create ? function(e3, t3) {
      Object.defineProperty(e3, "default", { enumerable: true, value: t3 });
    } : function(e3, t3) {
      e3.default = t3;
    }), s = this && this.__importStar || function(e3) {
      if (e3 && e3.__esModule) return e3;
      var t3 = {};
      if (null != e3) for (var r3 in e3) "default" !== r3 && Object.prototype.hasOwnProperty.call(e3, r3) && i2(t3, e3, r3);
      return n(t3, e3), t3;
    };
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AudioFileWriter = void 0;
    const o = s(r2(83)), a = r2(65);
    t2.AudioFileWriter = class {
      constructor(e3) {
        a.Contracts.throwIfNullOrUndefined(o.openSync, "\nFile System access not available, please use Push or PullAudioOutputStream"), this.privFd = o.openSync(e3, "w");
      }
      set format(e3) {
        a.Contracts.throwIfNotUndefined(this.privAudioFormat, "format is already set"), this.privAudioFormat = e3;
        let t3 = 0;
        this.privAudioFormat.hasHeader && (t3 = this.privAudioFormat.header.byteLength), void 0 !== this.privFd && (this.privWriteStream = o.createWriteStream("", { fd: this.privFd, start: t3, autoClose: false }));
      }
      write(e3) {
        a.Contracts.throwIfNullOrUndefined(this.privAudioFormat, "must set format before writing."), void 0 !== this.privWriteStream && this.privWriteStream.write(new Uint8Array(e3.slice(0)));
      }
      close() {
        void 0 !== this.privFd && (this.privWriteStream.on("finish", () => {
          this.privAudioFormat.hasHeader && (this.privAudioFormat.updateHeader(this.privWriteStream.bytesWritten), o.writeSync(this.privFd, new Int8Array(this.privAudioFormat.header), 0, this.privAudioFormat.header.byteLength, 0)), o.closeSync(this.privFd), this.privFd = void 0;
        }), this.privWriteStream.end());
      }
      id() {
        return this.privId;
      }
    };
  }, () => {
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PullAudioInputStreamImpl = t2.PullAudioInputStream = t2.PushAudioInputStreamImpl = t2.PushAudioInputStream = t2.AudioInputStream = void 0;
    const i2 = r2(2), n = r2(4), s = r2(7), o = r2(80), a = r2(68);
    class c {
      constructor() {
      }
      static createPushStream(e3) {
        return p.create(e3);
      }
      static createPullStream(e3, t3) {
        return u.create(e3, t3);
      }
    }
    t2.AudioInputStream = c;
    class p extends c {
      static create(e3) {
        return new h(e3);
      }
    }
    t2.PushAudioInputStream = p;
    class h extends p {
      constructor(e3) {
        super(), this.privFormat = void 0 === e3 ? a.AudioStreamFormatImpl.getDefaultInputFormat() : e3, this.privEvents = new n.EventSource(), this.privId = (0, s.createNoDashGuid)(), this.privStream = new n.ChunkedArrayBufferStream(this.privFormat.avgBytesPerSec / 10);
      }
      get format() {
        return Promise.resolve(this.privFormat);
      }
      write(e3) {
        this.privStream.writeStreamChunk({ buffer: e3, isEnd: false, timeReceived: Date.now() });
      }
      close() {
        this.privStream.close();
      }
      id() {
        return this.privId;
      }
      turnOn() {
        this.onEvent(new n.AudioSourceInitializingEvent(this.privId)), this.onEvent(new n.AudioSourceReadyEvent(this.privId));
      }
      async attach(e3) {
        this.onEvent(new n.AudioStreamNodeAttachingEvent(this.privId, e3)), await this.turnOn();
        const t3 = this.privStream;
        return this.onEvent(new n.AudioStreamNodeAttachedEvent(this.privId, e3)), { detach: async () => (this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3)), this.turnOff()), id: () => e3, read: () => t3.read() };
      }
      detach(e3) {
        this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3));
      }
      turnOff() {
      }
      get events() {
        return this.privEvents;
      }
      get deviceInfo() {
        return Promise.resolve({ bitspersample: this.privFormat.bitsPerSample, channelcount: this.privFormat.channels, connectivity: i2.connectivity.Unknown, manufacturer: "Speech SDK", model: "PushStream", samplerate: this.privFormat.samplesPerSec, type: i2.type.Stream });
      }
      onEvent(e3) {
        this.privEvents.onEvent(e3), n.Events.instance.onEvent(e3);
      }
      toBuffer(e3) {
        const t3 = Buffer.alloc(e3.byteLength), r3 = new Uint8Array(e3);
        for (let e4 = 0; e4 < t3.length; ++e4) t3[e4] = r3[e4];
        return t3;
      }
    }
    t2.PushAudioInputStreamImpl = h;
    class u extends c {
      constructor() {
        super();
      }
      static create(e3, t3) {
        return new d(e3, t3);
      }
    }
    t2.PullAudioInputStream = u;
    class d extends u {
      constructor(e3, t3) {
        super(), this.privFormat = void 0 === t3 ? o.AudioStreamFormat.getDefaultInputFormat() : t3, this.privEvents = new n.EventSource(), this.privId = (0, s.createNoDashGuid)(), this.privCallback = e3, this.privIsClosed = false, this.privBufferSize = this.privFormat.avgBytesPerSec / 10;
      }
      get format() {
        return Promise.resolve(this.privFormat);
      }
      close() {
        this.privIsClosed = true, this.privCallback.close();
      }
      id() {
        return this.privId;
      }
      turnOn() {
        this.onEvent(new n.AudioSourceInitializingEvent(this.privId)), this.onEvent(new n.AudioSourceReadyEvent(this.privId));
      }
      async attach(e3) {
        return this.onEvent(new n.AudioStreamNodeAttachingEvent(this.privId, e3)), await this.turnOn(), this.onEvent(new n.AudioStreamNodeAttachedEvent(this.privId, e3)), { detach: () => (this.privCallback.close(), this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3)), this.turnOff()), id: () => e3, read: () => {
          let e4, t3 = 0;
          for (; t3 < this.privBufferSize; ) {
            const r3 = new ArrayBuffer(this.privBufferSize - t3), i3 = this.privCallback.read(r3);
            if (void 0 === e4) e4 = r3;
            else {
              new Int8Array(e4).set(new Int8Array(r3), t3);
            }
            if (0 === i3) break;
            t3 += i3;
          }
          return Promise.resolve({ buffer: e4.slice(0, t3), isEnd: this.privIsClosed || 0 === t3, timeReceived: Date.now() });
        } };
      }
      detach(e3) {
        this.onEvent(new n.AudioStreamNodeDetachedEvent(this.privId, e3));
      }
      turnOff() {
      }
      get events() {
        return this.privEvents;
      }
      get deviceInfo() {
        return Promise.resolve({ bitspersample: this.privFormat.bitsPerSample, channelcount: this.privFormat.channels, connectivity: i2.connectivity.Unknown, manufacturer: "Speech SDK", model: "PullStream", samplerate: this.privFormat.samplesPerSec, type: i2.type.Stream });
      }
      onEvent(e3) {
        this.privEvents.onEvent(e3), n.Events.instance.onEvent(e3);
      }
    }
    t2.PullAudioInputStreamImpl = d;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PushAudioOutputStreamImpl = t2.PushAudioOutputStream = t2.PullAudioOutputStreamImpl = t2.PullAudioOutputStream = t2.AudioOutputStream = void 0;
    const i2 = r2(4), n = r2(65), s = r2(86);
    class o {
      constructor() {
      }
      static createPullStream() {
        return a.create();
      }
    }
    t2.AudioOutputStream = o;
    class a extends o {
      static create() {
        return new c();
      }
    }
    t2.PullAudioOutputStream = a;
    class c extends a {
      constructor() {
        super(), this.privId = (0, i2.createNoDashGuid)(), this.privStream = new i2.Stream();
      }
      set format(e3) {
        null == e3 && (this.privFormat = s.AudioOutputFormatImpl.getDefaultOutputFormat()), this.privFormat = e3;
      }
      get format() {
        return this.privFormat;
      }
      get isClosed() {
        return this.privStream.isClosed;
      }
      id() {
        return this.privId;
      }
      async read(e3) {
        const t3 = new Int8Array(e3);
        let r3 = 0;
        if (void 0 !== this.privLastChunkView) {
          if (this.privLastChunkView.length > e3.byteLength) return t3.set(this.privLastChunkView.slice(0, e3.byteLength)), this.privLastChunkView = this.privLastChunkView.slice(e3.byteLength), Promise.resolve(e3.byteLength);
          t3.set(this.privLastChunkView), r3 = this.privLastChunkView.length, this.privLastChunkView = void 0;
        }
        for (; r3 < e3.byteLength && !this.privStream.isReadEnded; ) {
          const i3 = await this.privStream.read();
          if (void 0 === i3 || i3.isEnd) this.privStream.readEnded();
          else {
            let n2;
            i3.buffer.byteLength > e3.byteLength - r3 ? (n2 = i3.buffer.slice(0, e3.byteLength - r3), this.privLastChunkView = new Int8Array(i3.buffer.slice(e3.byteLength - r3))) : n2 = i3.buffer, t3.set(new Int8Array(n2), r3), r3 += n2.byteLength;
          }
        }
        return r3;
      }
      write(e3) {
        n.Contracts.throwIfNullOrUndefined(this.privStream, "must set format before writing"), this.privStream.writeStreamChunk({ buffer: e3, isEnd: false, timeReceived: Date.now() });
      }
      close() {
        this.privStream.close();
      }
    }
    t2.PullAudioOutputStreamImpl = c;
    class p extends o {
      constructor() {
        super();
      }
      static create(e3) {
        return new h(e3);
      }
    }
    t2.PushAudioOutputStream = p;
    class h extends p {
      constructor(e3) {
        super(), this.privId = (0, i2.createNoDashGuid)(), this.privCallback = e3;
      }
      set format(e3) {
      }
      write(e3) {
        this.privCallback.write && this.privCallback.write(e3);
      }
      close() {
        this.privCallback.close && this.privCallback.close();
      }
      id() {
        return this.privId;
      }
    }
    t2.PushAudioOutputStreamImpl = h;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AudioOutputFormatImpl = void 0;
    const i2 = r2(87), n = r2(68);
    class s extends n.AudioStreamFormatImpl {
      constructor(e3, t3, r3, i3, n2, s2, o, a, c) {
        super(r3, s2, t3, e3), this.formatTag = e3, this.avgBytesPerSec = i3, this.blockAlign = n2, this.priAudioFormatString = o, this.priRequestAudioFormatString = a, this.priHasHeader = c;
      }
      static fromSpeechSynthesisOutputFormat(e3) {
        return void 0 === e3 ? s.getDefaultOutputFormat() : s.fromSpeechSynthesisOutputFormatString(s.SpeechSynthesisOutputFormatToString[e3]);
      }
      static fromSpeechSynthesisOutputFormatString(e3) {
        switch (e3) {
          case "raw-8khz-8bit-mono-mulaw":
            return new s(n.AudioFormatTag.MuLaw, 1, 8e3, 8e3, 1, 8, e3, e3, false);
          case "riff-16khz-16kbps-mono-siren":
            return new s(n.AudioFormatTag.Siren, 1, 16e3, 2e3, 40, 0, e3, "audio-16khz-16kbps-mono-siren", true);
          case "audio-16khz-16kbps-mono-siren":
            return new s(n.AudioFormatTag.Siren, 1, 16e3, 2e3, 40, 0, e3, e3, false);
          case "audio-16khz-32kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 16e3, 4096, 2, 16, e3, e3, false);
          case "audio-16khz-128kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 16e3, 16384, 2, 16, e3, e3, false);
          case "audio-16khz-64kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 16e3, 8192, 2, 16, e3, e3, false);
          case "audio-24khz-48kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 24e3, 6144, 2, 16, e3, e3, false);
          case "audio-24khz-96kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 24e3, 12288, 2, 16, e3, e3, false);
          case "audio-24khz-160kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 24e3, 20480, 2, 16, e3, e3, false);
          case "raw-16khz-16bit-mono-truesilk":
            return new s(n.AudioFormatTag.SILKSkype, 1, 16e3, 32e3, 2, 16, e3, e3, false);
          case "riff-8khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 8e3, 16e3, 2, 16, e3, "raw-8khz-16bit-mono-pcm", true);
          case "riff-24khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 24e3, 48e3, 2, 16, e3, "raw-24khz-16bit-mono-pcm", true);
          case "riff-8khz-8bit-mono-mulaw":
            return new s(n.AudioFormatTag.MuLaw, 1, 8e3, 8e3, 1, 8, e3, "raw-8khz-8bit-mono-mulaw", true);
          case "raw-16khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 16e3, 32e3, 2, 16, e3, "raw-16khz-16bit-mono-pcm", false);
          case "raw-24khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 24e3, 48e3, 2, 16, e3, "raw-24khz-16bit-mono-pcm", false);
          case "raw-8khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 8e3, 16e3, 2, 16, e3, "raw-8khz-16bit-mono-pcm", false);
          case "ogg-16khz-16bit-mono-opus":
            return new s(n.AudioFormatTag.OGG_OPUS, 1, 16e3, 8192, 2, 16, e3, e3, false);
          case "ogg-24khz-16bit-mono-opus":
            return new s(n.AudioFormatTag.OGG_OPUS, 1, 24e3, 8192, 2, 16, e3, e3, false);
          case "raw-48khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 48e3, 96e3, 2, 16, e3, "raw-48khz-16bit-mono-pcm", false);
          case "riff-48khz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 48e3, 96e3, 2, 16, e3, "raw-48khz-16bit-mono-pcm", true);
          case "audio-48khz-96kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 48e3, 12288, 2, 16, e3, e3, false);
          case "audio-48khz-192kbitrate-mono-mp3":
            return new s(n.AudioFormatTag.MP3, 1, 48e3, 24576, 2, 16, e3, e3, false);
          case "ogg-48khz-16bit-mono-opus":
            return new s(n.AudioFormatTag.OGG_OPUS, 1, 48e3, 12e3, 2, 16, e3, e3, false);
          case "webm-16khz-16bit-mono-opus":
            return new s(n.AudioFormatTag.WEBM_OPUS, 1, 16e3, 4e3, 2, 16, e3, e3, false);
          case "webm-24khz-16bit-mono-opus":
            return new s(n.AudioFormatTag.WEBM_OPUS, 1, 24e3, 6e3, 2, 16, e3, e3, false);
          case "webm-24khz-16bit-24kbps-mono-opus":
            return new s(n.AudioFormatTag.WEBM_OPUS, 1, 24e3, 3e3, 2, 16, e3, e3, false);
          case "audio-16khz-16bit-32kbps-mono-opus":
            return new s(n.AudioFormatTag.OPUS, 1, 16e3, 4e3, 2, 16, e3, e3, false);
          case "audio-24khz-16bit-48kbps-mono-opus":
            return new s(n.AudioFormatTag.OPUS, 1, 24e3, 6e3, 2, 16, e3, e3, false);
          case "audio-24khz-16bit-24kbps-mono-opus":
            return new s(n.AudioFormatTag.OPUS, 1, 24e3, 3e3, 2, 16, e3, e3, false);
          case "audio-24khz-16bit-mono-flac":
            return new s(n.AudioFormatTag.FLAC, 1, 24e3, 24e3, 2, 16, e3, e3, false);
          case "audio-48khz-16bit-mono-flac":
            return new s(n.AudioFormatTag.FLAC, 1, 48e3, 3e4, 2, 16, e3, e3, false);
          case "raw-24khz-16bit-mono-truesilk":
            return new s(n.AudioFormatTag.SILKSkype, 1, 24e3, 48e3, 2, 16, e3, e3, false);
          case "raw-8khz-8bit-mono-alaw":
            return new s(n.AudioFormatTag.ALaw, 1, 8e3, 8e3, 1, 8, e3, e3, false);
          case "riff-8khz-8bit-mono-alaw":
            return new s(n.AudioFormatTag.ALaw, 1, 8e3, 8e3, 1, 8, e3, "raw-8khz-8bit-mono-alaw", true);
          case "raw-22050hz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 22050, 44100, 2, 16, e3, e3, false);
          case "riff-22050hz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 22050, 44100, 2, 16, e3, "raw-22050hz-16bit-mono-pcm", true);
          case "raw-44100hz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 44100, 88200, 2, 16, e3, e3, false);
          case "riff-44100hz-16bit-mono-pcm":
            return new s(n.AudioFormatTag.PCM, 1, 44100, 88200, 2, 16, e3, "raw-44100hz-16bit-mono-pcm", true);
          case "amr-wb-16000h":
            return new s(n.AudioFormatTag.AMR_WB, 1, 16e3, 3052, 2, 16, e3, e3, false);
          case "g722-16khz-64kbps":
            return new s(n.AudioFormatTag.G722, 1, 16e3, 8e3, 2, 16, e3, e3, false);
          default:
            return new s(n.AudioFormatTag.PCM, 1, 16e3, 32e3, 2, 16, "riff-16khz-16bit-mono-pcm", "raw-16khz-16bit-mono-pcm", true);
        }
      }
      static getDefaultOutputFormat() {
        return s.fromSpeechSynthesisOutputFormatString("undefined" != typeof window ? "audio-24khz-48kbitrate-mono-mp3" : "riff-16khz-16bit-mono-pcm");
      }
      get hasHeader() {
        return this.priHasHeader;
      }
      get header() {
        if (this.hasHeader) return this.privHeader;
      }
      updateHeader(e3) {
        if (this.priHasHeader) {
          const t3 = new DataView(this.privHeader);
          t3.setUint32(4, e3 + this.privHeader.byteLength - 8, true), t3.setUint32(40, e3, true);
        }
      }
      get requestAudioFormatString() {
        return this.priRequestAudioFormatString;
      }
      addHeader(e3) {
        if (!this.hasHeader) return e3;
        this.updateHeader(e3.byteLength);
        const t3 = new Uint8Array(e3.byteLength + this.header.byteLength);
        return t3.set(new Uint8Array(this.header), 0), t3.set(new Uint8Array(e3), this.header.byteLength), t3.buffer;
      }
    }
    t2.AudioOutputFormatImpl = s, s.SpeechSynthesisOutputFormatToString = { [i2.SpeechSynthesisOutputFormat.Raw8Khz8BitMonoMULaw]: "raw-8khz-8bit-mono-mulaw", [i2.SpeechSynthesisOutputFormat.Riff16Khz16KbpsMonoSiren]: "riff-16khz-16kbps-mono-siren", [i2.SpeechSynthesisOutputFormat.Audio16Khz16KbpsMonoSiren]: "audio-16khz-16kbps-mono-siren", [i2.SpeechSynthesisOutputFormat.Audio16Khz32KBitRateMonoMp3]: "audio-16khz-32kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Audio16Khz128KBitRateMonoMp3]: "audio-16khz-128kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Audio16Khz64KBitRateMonoMp3]: "audio-16khz-64kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Audio24Khz48KBitRateMonoMp3]: "audio-24khz-48kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Audio24Khz96KBitRateMonoMp3]: "audio-24khz-96kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Audio24Khz160KBitRateMonoMp3]: "audio-24khz-160kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Raw16Khz16BitMonoTrueSilk]: "raw-16khz-16bit-mono-truesilk", [i2.SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm]: "riff-16khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Riff8Khz16BitMonoPcm]: "riff-8khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm]: "riff-24khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Riff8Khz8BitMonoMULaw]: "riff-8khz-8bit-mono-mulaw", [i2.SpeechSynthesisOutputFormat.Raw16Khz16BitMonoPcm]: "raw-16khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Raw24Khz16BitMonoPcm]: "raw-24khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Raw8Khz16BitMonoPcm]: "raw-8khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Ogg16Khz16BitMonoOpus]: "ogg-16khz-16bit-mono-opus", [i2.SpeechSynthesisOutputFormat.Ogg24Khz16BitMonoOpus]: "ogg-24khz-16bit-mono-opus", [i2.SpeechSynthesisOutputFormat.Raw48Khz16BitMonoPcm]: "raw-48khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Riff48Khz16BitMonoPcm]: "riff-48khz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Audio48Khz96KBitRateMonoMp3]: "audio-48khz-96kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Audio48Khz192KBitRateMonoMp3]: "audio-48khz-192kbitrate-mono-mp3", [i2.SpeechSynthesisOutputFormat.Ogg48Khz16BitMonoOpus]: "ogg-48khz-16bit-mono-opus", [i2.SpeechSynthesisOutputFormat.Webm16Khz16BitMonoOpus]: "webm-16khz-16bit-mono-opus", [i2.SpeechSynthesisOutputFormat.Webm24Khz16BitMonoOpus]: "webm-24khz-16bit-mono-opus", [i2.SpeechSynthesisOutputFormat.Webm24Khz16Bit24KbpsMonoOpus]: "webm-24khz-16bit-24kbps-mono-opus", [i2.SpeechSynthesisOutputFormat.Raw24Khz16BitMonoTrueSilk]: "raw-24khz-16bit-mono-truesilk", [i2.SpeechSynthesisOutputFormat.Raw8Khz8BitMonoALaw]: "raw-8khz-8bit-mono-alaw", [i2.SpeechSynthesisOutputFormat.Riff8Khz8BitMonoALaw]: "riff-8khz-8bit-mono-alaw", [i2.SpeechSynthesisOutputFormat.Audio16Khz16Bit32KbpsMonoOpus]: "audio-16khz-16bit-32kbps-mono-opus", [i2.SpeechSynthesisOutputFormat.Audio24Khz16Bit48KbpsMonoOpus]: "audio-24khz-16bit-48kbps-mono-opus", [i2.SpeechSynthesisOutputFormat.Audio24Khz16Bit24KbpsMonoOpus]: "audio-24khz-16bit-24kbps-mono-opus", [i2.SpeechSynthesisOutputFormat.Raw22050Hz16BitMonoPcm]: "raw-22050hz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Riff22050Hz16BitMonoPcm]: "riff-22050hz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Raw44100Hz16BitMonoPcm]: "raw-44100hz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.Riff44100Hz16BitMonoPcm]: "riff-44100hz-16bit-mono-pcm", [i2.SpeechSynthesisOutputFormat.AmrWb16000Hz]: "amr-wb-16000hz", [i2.SpeechSynthesisOutputFormat.G72216Khz64Kbps]: "g722-16khz-64kbps" };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisOutputFormat = void 0, function(e3) {
      e3[e3.Raw8Khz8BitMonoMULaw = 0] = "Raw8Khz8BitMonoMULaw", e3[e3.Riff16Khz16KbpsMonoSiren = 1] = "Riff16Khz16KbpsMonoSiren", e3[e3.Audio16Khz16KbpsMonoSiren = 2] = "Audio16Khz16KbpsMonoSiren", e3[e3.Audio16Khz32KBitRateMonoMp3 = 3] = "Audio16Khz32KBitRateMonoMp3", e3[e3.Audio16Khz128KBitRateMonoMp3 = 4] = "Audio16Khz128KBitRateMonoMp3", e3[e3.Audio16Khz64KBitRateMonoMp3 = 5] = "Audio16Khz64KBitRateMonoMp3", e3[e3.Audio24Khz48KBitRateMonoMp3 = 6] = "Audio24Khz48KBitRateMonoMp3", e3[e3.Audio24Khz96KBitRateMonoMp3 = 7] = "Audio24Khz96KBitRateMonoMp3", e3[e3.Audio24Khz160KBitRateMonoMp3 = 8] = "Audio24Khz160KBitRateMonoMp3", e3[e3.Raw16Khz16BitMonoTrueSilk = 9] = "Raw16Khz16BitMonoTrueSilk", e3[e3.Riff16Khz16BitMonoPcm = 10] = "Riff16Khz16BitMonoPcm", e3[e3.Riff8Khz16BitMonoPcm = 11] = "Riff8Khz16BitMonoPcm", e3[e3.Riff24Khz16BitMonoPcm = 12] = "Riff24Khz16BitMonoPcm", e3[e3.Riff8Khz8BitMonoMULaw = 13] = "Riff8Khz8BitMonoMULaw", e3[e3.Raw16Khz16BitMonoPcm = 14] = "Raw16Khz16BitMonoPcm", e3[e3.Raw24Khz16BitMonoPcm = 15] = "Raw24Khz16BitMonoPcm", e3[e3.Raw8Khz16BitMonoPcm = 16] = "Raw8Khz16BitMonoPcm", e3[e3.Ogg16Khz16BitMonoOpus = 17] = "Ogg16Khz16BitMonoOpus", e3[e3.Ogg24Khz16BitMonoOpus = 18] = "Ogg24Khz16BitMonoOpus", e3[e3.Raw48Khz16BitMonoPcm = 19] = "Raw48Khz16BitMonoPcm", e3[e3.Riff48Khz16BitMonoPcm = 20] = "Riff48Khz16BitMonoPcm", e3[e3.Audio48Khz96KBitRateMonoMp3 = 21] = "Audio48Khz96KBitRateMonoMp3", e3[e3.Audio48Khz192KBitRateMonoMp3 = 22] = "Audio48Khz192KBitRateMonoMp3", e3[e3.Ogg48Khz16BitMonoOpus = 23] = "Ogg48Khz16BitMonoOpus", e3[e3.Webm16Khz16BitMonoOpus = 24] = "Webm16Khz16BitMonoOpus", e3[e3.Webm24Khz16BitMonoOpus = 25] = "Webm24Khz16BitMonoOpus", e3[e3.Raw24Khz16BitMonoTrueSilk = 26] = "Raw24Khz16BitMonoTrueSilk", e3[e3.Raw8Khz8BitMonoALaw = 27] = "Raw8Khz8BitMonoALaw", e3[e3.Riff8Khz8BitMonoALaw = 28] = "Riff8Khz8BitMonoALaw", e3[e3.Webm24Khz16Bit24KbpsMonoOpus = 29] = "Webm24Khz16Bit24KbpsMonoOpus", e3[e3.Audio16Khz16Bit32KbpsMonoOpus = 30] = "Audio16Khz16Bit32KbpsMonoOpus", e3[e3.Audio24Khz16Bit48KbpsMonoOpus = 31] = "Audio24Khz16Bit48KbpsMonoOpus", e3[e3.Audio24Khz16Bit24KbpsMonoOpus = 32] = "Audio24Khz16Bit24KbpsMonoOpus", e3[e3.Raw22050Hz16BitMonoPcm = 33] = "Raw22050Hz16BitMonoPcm", e3[e3.Riff22050Hz16BitMonoPcm = 34] = "Riff22050Hz16BitMonoPcm", e3[e3.Raw44100Hz16BitMonoPcm = 35] = "Raw44100Hz16BitMonoPcm", e3[e3.Riff44100Hz16BitMonoPcm = 36] = "Riff44100Hz16BitMonoPcm", e3[e3.AmrWb16000Hz = 37] = "AmrWb16000Hz", e3[e3.G72216Khz64Kbps = 38] = "G72216Khz64Kbps";
    }(t2.SpeechSynthesisOutputFormat || (t2.SpeechSynthesisOutputFormat = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CancellationReason = void 0, function(e3) {
      e3[e3.Error = 0] = "Error", e3[e3.EndOfStream = 1] = "EndOfStream";
    }(t2.CancellationReason || (t2.CancellationReason = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PullAudioInputStreamCallback = void 0;
    t2.PullAudioInputStreamCallback = class {
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PushAudioOutputStreamCallback = void 0;
    t2.PushAudioOutputStreamCallback = class {
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.KeywordRecognitionModel = void 0;
    const i2 = r2(65);
    t2.KeywordRecognitionModel = class {
      constructor() {
        this.privDisposed = false;
      }
      static fromFile(e3) {
        throw i2.Contracts.throwIfFileDoesNotExist(e3, "fileName"), new Error("Not yet implemented.");
      }
      static fromStream(e3) {
        throw i2.Contracts.throwIfNull(e3, "file"), new Error("Not yet implemented.");
      }
      close() {
        this.privDisposed || (this.privDisposed = true);
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SessionEventArgs = void 0;
    t2.SessionEventArgs = class {
      constructor(e3) {
        this.privSessionId = e3;
      }
      get sessionId() {
        return this.privSessionId;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RecognitionEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
      constructor(e3, t3) {
        super(t3), this.privOffset = e3;
      }
      get offset() {
        return this.privOffset;
      }
    }
    t2.RecognitionEventArgs = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.OutputFormat = void 0, function(e3) {
      e3[e3.Simple = 0] = "Simple", e3[e3.Detailed = 1] = "Detailed";
    }(t2.OutputFormat || (t2.OutputFormat = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RecognitionResult = void 0;
    t2.RecognitionResult = class {
      constructor(e3, t3, r2, i2, n, s, o, a, c, p) {
        this.privResultId = e3, this.privReason = t3, this.privText = r2, this.privDuration = i2, this.privOffset = n, this.privLanguage = s, this.privLanguageDetectionConfidence = o, this.privErrorDetails = a, this.privJson = c, this.privProperties = p;
      }
      get resultId() {
        return this.privResultId;
      }
      get reason() {
        return this.privReason;
      }
      get text() {
        return this.privText;
      }
      get duration() {
        return this.privDuration;
      }
      get offset() {
        return this.privOffset;
      }
      get language() {
        return this.privLanguage;
      }
      get languageDetectionConfidence() {
        return this.privLanguageDetectionConfidence;
      }
      get errorDetails() {
        return this.privErrorDetails;
      }
      get json() {
        return this.privJson;
      }
      get properties() {
        return this.privProperties;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechRecognitionResult = void 0;
    const i2 = r2(80);
    class n extends i2.RecognitionResult {
      constructor(e3, t3, r3, i3, n2, s, o, a, c, p, h) {
        super(e3, t3, r3, i3, n2, s, o, c, p, h), this.privSpeakerId = a;
      }
      get speakerId() {
        return this.privSpeakerId;
      }
    }
    t2.SpeechRecognitionResult = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MeetingTranscriptionEventArgs = t2.ConversationTranscriptionEventArgs = t2.SpeechRecognitionEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.RecognitionEventArgs {
      constructor(e3, t3, r3) {
        super(t3, r3), this.privResult = e3;
      }
      get result() {
        return this.privResult;
      }
    }
    t2.SpeechRecognitionEventArgs = n;
    class s extends i2.RecognitionEventArgs {
      constructor(e3, t3, r3) {
        super(t3, r3), this.privResult = e3;
      }
      get result() {
        return this.privResult;
      }
    }
    t2.ConversationTranscriptionEventArgs = s;
    t2.MeetingTranscriptionEventArgs = class extends n {
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechRecognitionCanceledEventArgs = void 0;
    const i2 = r2(99);
    class n extends i2.CancellationEventArgsBase {
    }
    t2.SpeechRecognitionCanceledEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CancellationEventArgsBase = void 0;
    const i2 = r2(80);
    class n extends i2.RecognitionEventArgs {
      constructor(e3, t3, r3, i3, n2) {
        super(i3, n2), this.privReason = e3, this.privErrorDetails = t3, this.privErrorCode = r3;
      }
      get reason() {
        return this.privReason;
      }
      get errorCode() {
        return this.privErrorCode;
      }
      get errorDetails() {
        return this.privErrorDetails;
      }
    }
    t2.CancellationEventArgsBase = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationRecognitionEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.RecognitionEventArgs {
      constructor(e3, t3, r3) {
        super(t3, r3), this.privResult = e3;
      }
      get result() {
        return this.privResult;
      }
    }
    t2.TranslationRecognitionEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationSynthesisEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
      constructor(e3, t3) {
        super(t3), this.privResult = e3;
      }
      get result() {
        return this.privResult;
      }
    }
    t2.TranslationSynthesisEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationRecognitionResult = void 0;
    const i2 = r2(80);
    class n extends i2.SpeechRecognitionResult {
      constructor(e3, t3, r3, i3, n2, s, o, a, c, p, h) {
        super(t3, r3, i3, n2, s, o, a, void 0, c, p, h), this.privTranslations = e3;
      }
      static fromSpeechRecognitionResult(e3) {
        return new n(void 0, e3.resultId, e3.reason, e3.text, e3.duration, e3.offset, e3.language, e3.languageDetectionConfidence, e3.errorDetails, e3.json, e3.properties);
      }
      get translations() {
        return this.privTranslations;
      }
    }
    t2.TranslationRecognitionResult = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationSynthesisResult = void 0;
    t2.TranslationSynthesisResult = class {
      constructor(e3, t3) {
        this.privReason = e3, this.privAudio = t3;
      }
      get audio() {
        return this.privAudio;
      }
      get reason() {
        return this.privReason;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ResultReason = void 0, function(e3) {
      e3[e3.NoMatch = 0] = "NoMatch", e3[e3.Canceled = 1] = "Canceled", e3[e3.RecognizingSpeech = 2] = "RecognizingSpeech", e3[e3.RecognizedSpeech = 3] = "RecognizedSpeech", e3[e3.RecognizedKeyword = 4] = "RecognizedKeyword", e3[e3.TranslatingSpeech = 5] = "TranslatingSpeech", e3[e3.TranslatedSpeech = 6] = "TranslatedSpeech", e3[e3.SynthesizingAudio = 7] = "SynthesizingAudio", e3[e3.SynthesizingAudioCompleted = 8] = "SynthesizingAudioCompleted", e3[e3.SynthesizingAudioStarted = 9] = "SynthesizingAudioStarted", e3[e3.VoicesListRetrieved = 10] = "VoicesListRetrieved", e3[e3.TranslatingParticipantSpeech = 11] = "TranslatingParticipantSpeech", e3[e3.TranslatedParticipantSpeech = 12] = "TranslatedParticipantSpeech", e3[e3.TranslatedInstantMessage = 13] = "TranslatedInstantMessage", e3[e3.TranslatedParticipantInstantMessage = 14] = "TranslatedParticipantInstantMessage";
    }(t2.ResultReason || (t2.ResultReason = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechConfigImpl = t2.SpeechConfig = void 0;
    const i2 = r2(2), n = r2(65), s = r2(80);
    class o {
      constructor() {
      }
      static fromSubscription(e3, t3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "subscriptionKey"), n.Contracts.throwIfNullOrWhitespace(t3, "region");
        const r3 = new a();
        return r3.setProperty(s.PropertyId.SpeechServiceConnection_Region, t3), r3.setProperty(s.PropertyId.SpeechServiceConnection_Key, e3), r3;
      }
      static fromEndpoint(e3, t3) {
        n.Contracts.throwIfNull(e3, "endpoint");
        const r3 = "string" == typeof t3 && t3.trim().length > 0, i3 = "object" == typeof t3 && null !== t3 && "function" == typeof t3.getToken, o2 = "object" == typeof t3 && null !== t3 && "string" == typeof t3.key;
        if (void 0 !== t3 && !r3 && !i3 && !o2) throw new Error("Invalid 'auth' parameter: expected a non-empty API key string, a TokenCredential, or a KeyCredential.");
        let c;
        return "string" == typeof t3 ? (c = new a(), c.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3)) : "object" == typeof t3 && "function" == typeof t3.getToken ? c = new a(t3) : "object" == typeof t3 && "string" == typeof t3.key ? (c = new a(), c.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3.key)) : c = new a(), c.setProperty(s.PropertyId.SpeechServiceConnection_Endpoint, e3.href), c;
      }
      static fromHost(e3, t3) {
        n.Contracts.throwIfNull(e3, "hostName");
        const r3 = new a();
        return r3.setProperty(s.PropertyId.SpeechServiceConnection_Host, e3.protocol + "//" + e3.hostname + ("" === e3.port ? "" : ":" + e3.port)), r3.setProperty(s.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, "1"), void 0 !== t3 && r3.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3), r3;
      }
      static fromAuthorizationToken(e3, t3) {
        n.Contracts.throwIfNull(e3, "authorizationToken"), n.Contracts.throwIfNullOrWhitespace(t3, "region");
        const r3 = new a();
        return r3.setProperty(s.PropertyId.SpeechServiceConnection_Region, t3), r3.authorizationToken = e3, r3;
      }
      close() {
      }
    }
    t2.SpeechConfig = o;
    class a extends o {
      constructor(e3) {
        super(), this.privProperties = new s.PropertyCollection(), this.speechRecognitionLanguage = "en-US", this.outputFormat = s.OutputFormat.Simple, this.privTokenCredential = e3;
      }
      get properties() {
        return this.privProperties;
      }
      get endPoint() {
        return new URL(this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_Endpoint));
      }
      get subscriptionKey() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_Key);
      }
      get region() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_Region);
      }
      get authorizationToken() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      get speechRecognitionLanguage() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage);
      }
      set speechRecognitionLanguage(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage, e3);
      }
      get autoDetectSourceLanguages() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages);
      }
      set autoDetectSourceLanguages(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages, e3);
      }
      get outputFormat() {
        return s.OutputFormat[this.privProperties.getProperty(i2.OutputFormatPropertyName, void 0)];
      }
      set outputFormat(e3) {
        this.privProperties.setProperty(i2.OutputFormatPropertyName, s.OutputFormat[e3]);
      }
      get endpointId() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_EndpointId);
      }
      set endpointId(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_EndpointId, e3);
      }
      get tokenCredential() {
        return this.privTokenCredential;
      }
      setProperty(e3, t3) {
        n.Contracts.throwIfNull(t3, "value"), this.privProperties.setProperty(e3, t3);
      }
      getProperty(e3, t3) {
        return this.privProperties.getProperty(e3, t3);
      }
      setProxy(e3, t3, r3, i3) {
        this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyHostName], e3), this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyPort], t3), this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyUserName], r3), this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyPassword], i3);
      }
      setServiceProperty(e3, t3) {
        const r3 = JSON.parse(this.privProperties.getProperty(i2.ServicePropertiesPropertyName, "{}"));
        r3[e3] = t3, this.privProperties.setProperty(i2.ServicePropertiesPropertyName, JSON.stringify(r3));
      }
      setProfanity(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceResponse_ProfanityOption, s.ProfanityOption[e3]);
      }
      enableAudioLogging() {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_EnableAudioLogging, "true");
      }
      requestWordLevelTimestamps() {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceResponse_RequestWordLevelTimestamps, "true"), this.privProperties.setProperty(i2.OutputFormatPropertyName, s.OutputFormat[s.OutputFormat.Detailed]);
      }
      enableDictation() {
        this.privProperties.setProperty(i2.ForceDictationPropertyName, "true");
      }
      clone() {
        const e3 = new a(this.tokenCredential);
        return e3.privProperties = this.privProperties.clone(), e3;
      }
      get speechSynthesisLanguage() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_SynthLanguage);
      }
      set speechSynthesisLanguage(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_SynthLanguage, e3);
      }
      get speechSynthesisVoiceName() {
        return this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_SynthVoice);
      }
      set speechSynthesisVoiceName(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_SynthVoice, e3);
      }
      get speechSynthesisOutputFormat() {
        return s.SpeechSynthesisOutputFormat[this.privProperties.getProperty(s.PropertyId.SpeechServiceConnection_SynthOutputFormat, void 0)];
      }
      set speechSynthesisOutputFormat(e3) {
        this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_SynthOutputFormat, s.SpeechSynthesisOutputFormat[e3]);
      }
    }
    t2.SpeechConfigImpl = a;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechTranslationConfigImpl = t2.SpeechTranslationConfig = void 0;
    const i2 = r2(2), n = r2(65), s = r2(80);
    class o extends s.SpeechConfig {
      constructor() {
        super();
      }
      static fromSubscription(e3, t3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "subscriptionKey"), n.Contracts.throwIfNullOrWhitespace(t3, "region");
        const r3 = new a();
        return r3.properties.setProperty(s.PropertyId.SpeechServiceConnection_Key, e3), r3.properties.setProperty(s.PropertyId.SpeechServiceConnection_Region, t3), r3;
      }
      static fromAuthorizationToken(e3, t3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "authorizationToken"), n.Contracts.throwIfNullOrWhitespace(t3, "region");
        const r3 = new a();
        return r3.properties.setProperty(s.PropertyId.SpeechServiceAuthorization_Token, e3), r3.properties.setProperty(s.PropertyId.SpeechServiceConnection_Region, t3), r3;
      }
      static fromHost(e3, t3) {
        n.Contracts.throwIfNull(e3, "hostName");
        const r3 = new a();
        return r3.setProperty(s.PropertyId.SpeechServiceConnection_Host, e3.protocol + "//" + e3.hostname + ("" === e3.port ? "" : ":" + e3.port)), void 0 !== t3 && r3.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3), r3;
      }
      static fromEndpoint(e3, t3) {
        n.Contracts.throwIfNull(e3, "endpoint");
        const r3 = "string" == typeof t3 && t3.trim().length > 0, i3 = "object" == typeof t3 && null !== t3 && "function" == typeof t3.getToken, o2 = "object" == typeof t3 && null !== t3 && "string" == typeof t3.key;
        if (void 0 !== t3 && !r3 && !i3 && !o2) throw new Error("Invalid 'auth' parameter: expected a non-empty API key string, a TokenCredential, or a KeyCredential.");
        let c;
        return "string" == typeof t3 ? (c = new a(), c.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3)) : "object" == typeof t3 && "function" == typeof t3.getToken ? c = new a(t3) : "object" == typeof t3 && "string" == typeof t3.key ? (c = new a(), c.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3.key)) : c = new a(), c.setProperty(s.PropertyId.SpeechServiceConnection_Endpoint, e3.href), c;
      }
    }
    t2.SpeechTranslationConfig = o;
    class a extends o {
      constructor(e3) {
        super(), this.privSpeechProperties = new s.PropertyCollection(), this.outputFormat = s.OutputFormat.Simple, this.privTokenCredential = e3;
      }
      set authorizationToken(e3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "value"), this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      set speechRecognitionLanguage(e3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "value"), this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage, e3);
      }
      get speechRecognitionLanguage() {
        return this.privSpeechProperties.getProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_RecoLanguage]);
      }
      get subscriptionKey() {
        return this.privSpeechProperties.getProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_Key]);
      }
      get outputFormat() {
        return s.OutputFormat[this.privSpeechProperties.getProperty(i2.OutputFormatPropertyName, void 0)];
      }
      set outputFormat(e3) {
        this.privSpeechProperties.setProperty(i2.OutputFormatPropertyName, s.OutputFormat[e3]);
      }
      get endpointId() {
        return this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_EndpointId);
      }
      set endpointId(e3) {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_EndpointId, e3);
      }
      addTargetLanguage(e3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "value");
        const t3 = this.targetLanguages;
        t3.includes(e3) || (t3.push(e3), this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages, t3.join(",")));
      }
      get targetLanguages() {
        return void 0 !== this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages, void 0) ? this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages).split(",") : [];
      }
      get voiceName() {
        return this.getProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_TranslationVoice]);
      }
      set voiceName(e3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "value"), this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_TranslationVoice, e3);
      }
      get region() {
        return this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_Region);
      }
      get tokenCredential() {
        return this.privTokenCredential;
      }
      setProxy(e3, t3, r3, i3) {
        this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyHostName], e3), this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyPort], t3), this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyUserName], r3), this.setProperty(s.PropertyId[s.PropertyId.SpeechServiceConnection_ProxyPassword], i3);
      }
      getProperty(e3, t3) {
        return this.privSpeechProperties.getProperty(e3, t3);
      }
      setProperty(e3, t3) {
        this.privSpeechProperties.setProperty(e3, t3);
      }
      get properties() {
        return this.privSpeechProperties;
      }
      close() {
      }
      setServiceProperty(e3, t3) {
        const r3 = JSON.parse(this.privSpeechProperties.getProperty(i2.ServicePropertiesPropertyName, "{}"));
        r3[e3] = t3, this.privSpeechProperties.setProperty(i2.ServicePropertiesPropertyName, JSON.stringify(r3));
      }
      setProfanity(e3) {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceResponse_ProfanityOption, s.ProfanityOption[e3]);
      }
      enableAudioLogging() {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_EnableAudioLogging, "true");
      }
      requestWordLevelTimestamps() {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceResponse_RequestWordLevelTimestamps, "true");
      }
      enableDictation() {
        this.privSpeechProperties.setProperty(i2.ForceDictationPropertyName, "true");
      }
      get speechSynthesisLanguage() {
        return this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_SynthLanguage);
      }
      set speechSynthesisLanguage(e3) {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_SynthLanguage, e3);
      }
      get speechSynthesisVoiceName() {
        return this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_SynthVoice);
      }
      set speechSynthesisVoiceName(e3) {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_SynthVoice, e3);
      }
      get speechSynthesisOutputFormat() {
        return s.SpeechSynthesisOutputFormat[this.privSpeechProperties.getProperty(s.PropertyId.SpeechServiceConnection_SynthOutputFormat, void 0)];
      }
      set speechSynthesisOutputFormat(e3) {
        this.privSpeechProperties.setProperty(s.PropertyId.SpeechServiceConnection_SynthOutputFormat, s.SpeechSynthesisOutputFormat[e3]);
      }
    }
    t2.SpeechTranslationConfigImpl = a;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PropertyCollection = void 0;
    const i2 = r2(80);
    class n {
      constructor() {
        this.privKeys = [], this.privValues = [];
      }
      getProperty(e3, t3) {
        let r3;
        r3 = "string" == typeof e3 ? e3 : i2.PropertyId[e3];
        for (let e4 = 0; e4 < this.privKeys.length; e4++) if (this.privKeys[e4] === r3) return this.privValues[e4];
        if (void 0 !== t3) return String(t3);
      }
      setProperty(e3, t3) {
        let r3;
        r3 = "string" == typeof e3 ? e3 : i2.PropertyId[e3];
        for (let e4 = 0; e4 < this.privKeys.length; e4++) if (this.privKeys[e4] === r3) return void (this.privValues[e4] = t3);
        this.privKeys.push(r3), this.privValues.push(t3);
      }
      clone() {
        const e3 = new n();
        for (let t3 = 0; t3 < this.privKeys.length; t3++) e3.privKeys.push(this.privKeys[t3]), e3.privValues.push(this.privValues[t3]);
        return e3;
      }
      mergeTo(e3) {
        this.privKeys.forEach((t3) => {
          if (void 0 === e3.getProperty(t3, void 0)) {
            const r3 = this.getProperty(t3);
            e3.setProperty(t3, r3);
          }
        });
      }
      get keys() {
        return this.privKeys;
      }
    }
    t2.PropertyCollection = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PropertyId = void 0, function(e3) {
      e3[e3.SpeechServiceConnection_Key = 0] = "SpeechServiceConnection_Key", e3[e3.SpeechServiceConnection_Endpoint = 1] = "SpeechServiceConnection_Endpoint", e3[e3.SpeechServiceConnection_Region = 2] = "SpeechServiceConnection_Region", e3[e3.SpeechServiceAuthorization_Token = 3] = "SpeechServiceAuthorization_Token", e3[e3.SpeechServiceAuthorization_Type = 4] = "SpeechServiceAuthorization_Type", e3[e3.SpeechServiceConnection_EndpointId = 5] = "SpeechServiceConnection_EndpointId", e3[e3.SpeechServiceConnection_TranslationToLanguages = 6] = "SpeechServiceConnection_TranslationToLanguages", e3[e3.SpeechServiceConnection_TranslationVoice = 7] = "SpeechServiceConnection_TranslationVoice", e3[e3.SpeechServiceConnection_TranslationFeatures = 8] = "SpeechServiceConnection_TranslationFeatures", e3[e3.SpeechServiceConnection_TranslationCategoryId = 9] = "SpeechServiceConnection_TranslationCategoryId", e3[e3.SpeechServiceConnection_ProxyHostName = 10] = "SpeechServiceConnection_ProxyHostName", e3[e3.SpeechServiceConnection_ProxyPort = 11] = "SpeechServiceConnection_ProxyPort", e3[e3.SpeechServiceConnection_ProxyUserName = 12] = "SpeechServiceConnection_ProxyUserName", e3[e3.SpeechServiceConnection_ProxyPassword = 13] = "SpeechServiceConnection_ProxyPassword", e3[e3.SpeechServiceConnection_RecoMode = 14] = "SpeechServiceConnection_RecoMode", e3[e3.SpeechServiceConnection_RecoLanguage = 15] = "SpeechServiceConnection_RecoLanguage", e3[e3.Speech_SessionId = 16] = "Speech_SessionId", e3[e3.SpeechServiceConnection_SynthLanguage = 17] = "SpeechServiceConnection_SynthLanguage", e3[e3.SpeechServiceConnection_SynthVoice = 18] = "SpeechServiceConnection_SynthVoice", e3[e3.SpeechServiceConnection_SynthOutputFormat = 19] = "SpeechServiceConnection_SynthOutputFormat", e3[e3.SpeechServiceConnection_AutoDetectSourceLanguages = 20] = "SpeechServiceConnection_AutoDetectSourceLanguages", e3[e3.SpeechServiceResponse_RequestDetailedResultTrueFalse = 21] = "SpeechServiceResponse_RequestDetailedResultTrueFalse", e3[e3.SpeechServiceResponse_RequestProfanityFilterTrueFalse = 22] = "SpeechServiceResponse_RequestProfanityFilterTrueFalse", e3[e3.SpeechServiceResponse_JsonResult = 23] = "SpeechServiceResponse_JsonResult", e3[e3.SpeechServiceResponse_JsonErrorDetails = 24] = "SpeechServiceResponse_JsonErrorDetails", e3[e3.CancellationDetails_Reason = 25] = "CancellationDetails_Reason", e3[e3.CancellationDetails_ReasonText = 26] = "CancellationDetails_ReasonText", e3[e3.CancellationDetails_ReasonDetailedText = 27] = "CancellationDetails_ReasonDetailedText", e3[e3.SpeechServiceConnection_Url = 28] = "SpeechServiceConnection_Url", e3[e3.SpeechServiceConnection_InitialSilenceTimeoutMs = 29] = "SpeechServiceConnection_InitialSilenceTimeoutMs", e3[e3.SpeechServiceConnection_EndSilenceTimeoutMs = 30] = "SpeechServiceConnection_EndSilenceTimeoutMs", e3[e3.Speech_SegmentationSilenceTimeoutMs = 31] = "Speech_SegmentationSilenceTimeoutMs", e3[e3.Speech_SegmentationMaximumTimeMs = 32] = "Speech_SegmentationMaximumTimeMs", e3[e3.Speech_SegmentationStrategy = 33] = "Speech_SegmentationStrategy", e3[e3.Speech_StartEventSensitivity = 34] = "Speech_StartEventSensitivity", e3[e3.SpeechServiceConnection_EnableAudioLogging = 35] = "SpeechServiceConnection_EnableAudioLogging", e3[e3.SpeechServiceConnection_LanguageIdMode = 36] = "SpeechServiceConnection_LanguageIdMode", e3[e3.SpeechServiceConnection_RecognitionEndpointVersion = 37] = "SpeechServiceConnection_RecognitionEndpointVersion", e3[e3.SpeechServiceResponse_ProfanityOption = 38] = "SpeechServiceResponse_ProfanityOption", e3[e3.SpeechServiceResponse_PostProcessingOption = 39] = "SpeechServiceResponse_PostProcessingOption", e3[e3.SpeechServiceResponse_RequestWordLevelTimestamps = 40] = "SpeechServiceResponse_RequestWordLevelTimestamps", e3[e3.SpeechServiceResponse_StablePartialResultThreshold = 41] = "SpeechServiceResponse_StablePartialResultThreshold", e3[e3.SpeechServiceResponse_OutputFormatOption = 42] = "SpeechServiceResponse_OutputFormatOption", e3[e3.SpeechServiceResponse_TranslationRequestStablePartialResult = 43] = "SpeechServiceResponse_TranslationRequestStablePartialResult", e3[e3.SpeechServiceResponse_RequestWordBoundary = 44] = "SpeechServiceResponse_RequestWordBoundary", e3[e3.SpeechServiceResponse_RequestPunctuationBoundary = 45] = "SpeechServiceResponse_RequestPunctuationBoundary", e3[e3.SpeechServiceResponse_RequestSentenceBoundary = 46] = "SpeechServiceResponse_RequestSentenceBoundary", e3[e3.SpeechServiceResponse_DiarizeIntermediateResults = 47] = "SpeechServiceResponse_DiarizeIntermediateResults", e3[e3.Conversation_ApplicationId = 48] = "Conversation_ApplicationId", e3[e3.Conversation_DialogType = 49] = "Conversation_DialogType", e3[e3.Conversation_Initial_Silence_Timeout = 50] = "Conversation_Initial_Silence_Timeout", e3[e3.Conversation_From_Id = 51] = "Conversation_From_Id", e3[e3.Conversation_Conversation_Id = 52] = "Conversation_Conversation_Id", e3[e3.Conversation_Custom_Voice_Deployment_Ids = 53] = "Conversation_Custom_Voice_Deployment_Ids", e3[e3.Conversation_Speech_Activity_Template = 54] = "Conversation_Speech_Activity_Template", e3[e3.Conversation_Request_Bot_Status_Messages = 55] = "Conversation_Request_Bot_Status_Messages", e3[e3.Conversation_Agent_Connection_Id = 56] = "Conversation_Agent_Connection_Id", e3[e3.SpeechServiceConnection_Host = 57] = "SpeechServiceConnection_Host", e3[e3.ConversationTranslator_Host = 58] = "ConversationTranslator_Host", e3[e3.ConversationTranslator_Name = 59] = "ConversationTranslator_Name", e3[e3.ConversationTranslator_CorrelationId = 60] = "ConversationTranslator_CorrelationId", e3[e3.ConversationTranslator_Token = 61] = "ConversationTranslator_Token", e3[e3.PronunciationAssessment_ReferenceText = 62] = "PronunciationAssessment_ReferenceText", e3[e3.PronunciationAssessment_GradingSystem = 63] = "PronunciationAssessment_GradingSystem", e3[e3.PronunciationAssessment_Granularity = 64] = "PronunciationAssessment_Granularity", e3[e3.PronunciationAssessment_EnableMiscue = 65] = "PronunciationAssessment_EnableMiscue", e3[e3.PronunciationAssessment_Json = 66] = "PronunciationAssessment_Json", e3[e3.PronunciationAssessment_Params = 67] = "PronunciationAssessment_Params", e3[e3.WebWorkerLoadType = 68] = "WebWorkerLoadType", e3[e3.TalkingAvatarService_WebRTC_SDP = 69] = "TalkingAvatarService_WebRTC_SDP";
    }(t2.PropertyId || (t2.PropertyId = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Recognizer = void 0;
    const i2 = r2(2), n = r2(4), s = r2(65), o = r2(80);
    class a {
      constructor(e3, t3, r3, i3) {
        this.audioConfig = void 0 !== e3 ? e3 : o.AudioConfig.fromDefaultMicrophoneInput(), this.privDisposed = false, this.privProperties = t3.clone(), this.privConnectionFactory = r3, this.tokenCredential = i3, this.implCommonRecognizerSetup();
      }
      close(e3, t3) {
        s.Contracts.throwIfDisposed(this.privDisposed), (0, n.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      get internalData() {
        return this.privReco;
      }
      async dispose(e3) {
        this.privDisposed || (this.privDisposed = true, e3 && this.privReco && (await this.privReco.audioSource.turnOff(), await this.privReco.dispose()));
      }
      static get telemetryEnabled() {
        return i2.ServiceRecognizerBase.telemetryDataEnabled;
      }
      static enableTelemetry(e3) {
        i2.ServiceRecognizerBase.telemetryDataEnabled = e3;
      }
      implCommonRecognizerSetup() {
        let e3 = "undefined" != typeof window ? "Browser" : "Node", t3 = "unknown", r3 = "unknown";
        "undefined" != typeof navigator && (e3 = e3 + "/" + navigator.platform, t3 = navigator.userAgent, r3 = navigator.appVersion);
        const n2 = this.createRecognizerConfig(new i2.SpeechServiceConfig(new i2.Context(new i2.OS(e3, t3, r3))));
        this.privReco = this.createServiceRecognizer(a.getAuth(this.privProperties, this.tokenCredential), this.privConnectionFactory, this.audioConfig, n2);
      }
      async recognizeOnceAsyncImpl(e3) {
        s.Contracts.throwIfDisposed(this.privDisposed);
        const t3 = new n.Deferred();
        await this.implRecognizerStop(), await this.privReco.recognize(e3, t3.resolve, t3.reject);
        const r3 = await t3.promise;
        return await this.implRecognizerStop(), r3;
      }
      async startContinuousRecognitionAsyncImpl(e3) {
        s.Contracts.throwIfDisposed(this.privDisposed), await this.implRecognizerStop(), await this.privReco.recognize(e3, void 0, void 0);
      }
      async stopContinuousRecognitionAsyncImpl() {
        s.Contracts.throwIfDisposed(this.privDisposed), await this.implRecognizerStop();
      }
      async implRecognizerStop() {
        this.privReco && await this.privReco.stopRecognizing();
      }
      static getAuth(e3, t3) {
        const r3 = e3.getProperty(o.PropertyId.SpeechServiceConnection_Key, void 0);
        return r3 && "" !== r3 ? new i2.CognitiveSubscriptionKeyAuthentication(r3) : t3 ? new i2.CognitiveTokenAuthentication(async () => {
          try {
            const e4 = await t3.getToken("https://cognitiveservices.azure.com/.default");
            return e4?.token ?? "";
          } catch (e4) {
            throw e4;
          }
        }, async () => {
          try {
            const e4 = await t3.getToken("https://cognitiveservices.azure.com/.default");
            return e4?.token ?? "";
          } catch (e4) {
            throw e4;
          }
        }) : new i2.CognitiveTokenAuthentication(() => {
          const t4 = e3.getProperty(o.PropertyId.SpeechServiceAuthorization_Token, void 0);
          return Promise.resolve(t4);
        }, () => {
          const t4 = e3.getProperty(o.PropertyId.SpeechServiceAuthorization_Token, void 0);
          return Promise.resolve(t4);
        });
      }
    }
    t2.Recognizer = a;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechRecognizer = void 0;
    const i2 = r2(2), n = r2(111), s = r2(4), o = r2(65), a = r2(80);
    class c extends a.Recognizer {
      constructor(e3, t3) {
        const r3 = e3;
        o.Contracts.throwIfNull(r3, "speechConfig"), o.Contracts.throwIfNullOrWhitespace(r3.properties.getProperty(a.PropertyId.SpeechServiceConnection_RecoLanguage), a.PropertyId[a.PropertyId.SpeechServiceConnection_RecoLanguage]), super(t3, r3.properties, new i2.SpeechConnectionFactory(), e3.tokenCredential), this.privDisposedRecognizer = false;
      }
      static FromConfig(e3, t3, r3) {
        const i3 = e3;
        t3.properties.mergeTo(i3.properties);
        return new c(e3, r3);
      }
      get endpointId() {
        return o.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(a.PropertyId.SpeechServiceConnection_EndpointId, "00000000-0000-0000-0000-000000000000");
      }
      get authorizationToken() {
        return this.properties.getProperty(a.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        o.Contracts.throwIfNullOrWhitespace(e3, "token"), this.properties.setProperty(a.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      get speechRecognitionLanguage() {
        return o.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(a.PropertyId.SpeechServiceConnection_RecoLanguage);
      }
      get outputFormat() {
        return o.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(i2.OutputFormatPropertyName, a.OutputFormat[a.OutputFormat.Simple]) === a.OutputFormat[a.OutputFormat.Simple] ? a.OutputFormat.Simple : a.OutputFormat.Detailed;
      }
      get properties() {
        return this.privProperties;
      }
      recognizeOnceAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.recognizeOnceAsyncImpl(n.RecognitionMode.Interactive), e3, t3);
      }
      startContinuousRecognitionAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.startContinuousRecognitionAsyncImpl(void 0 === this.properties.getProperty(i2.ForceDictationPropertyName, void 0) ? n.RecognitionMode.Conversation : n.RecognitionMode.Dictation), e3, t3);
      }
      stopContinuousRecognitionAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.stopContinuousRecognitionAsyncImpl(), e3, t3);
      }
      startKeywordRecognitionAsync(e3, t3, r3) {
        o.Contracts.throwIfNull(e3, "model"), r3 && r3("Not yet implemented.");
      }
      stopKeywordRecognitionAsync(e3) {
        e3 && e3();
      }
      close(e3, t3) {
        o.Contracts.throwIfDisposed(this.privDisposedRecognizer), (0, s.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      async dispose(e3) {
        this.privDisposedRecognizer || (e3 && (this.privDisposedRecognizer = true, await this.implRecognizerStop()), await super.dispose(e3));
      }
      createRecognizerConfig(e3) {
        return new i2.RecognizerConfig(e3, this.privProperties);
      }
      createServiceRecognizer(e3, t3, r3, n2) {
        const s2 = r3;
        return new i2.SpeechServiceRecognizer(e3, t3, s2, n2, this);
      }
    }
    t2.SpeechRecognizer = c;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechStartEventSensitivity = t2.RecognitionMode = void 0, function(e3) {
      e3.Interactive = "Interactive", e3.Dictation = "Dictation", e3.Conversation = "Conversation", e3.None = "None";
    }(t2.RecognitionMode || (t2.RecognitionMode = {})), function(e3) {
      e3.Low = "low", e3.Medium = "medium", e3.High = "high";
    }(t2.SpeechStartEventSensitivity || (t2.SpeechStartEventSensitivity = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationRecognizer = void 0;
    const i2 = r2(2), n = r2(111), s = r2(4), o = r2(113), a = r2(65), c = r2(80);
    class p extends c.Recognizer {
      constructor(e3, t3, r3) {
        const n2 = e3;
        a.Contracts.throwIfNull(n2, "speechConfig"), super(t3, n2.properties, r3 || new i2.TranslationConnectionFactory(), e3.tokenCredential), this.privDisposedTranslationRecognizer = false, void 0 !== this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationVoice, void 0) && a.Contracts.throwIfNullOrWhitespace(this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationVoice), c.PropertyId[c.PropertyId.SpeechServiceConnection_TranslationVoice]), a.Contracts.throwIfNullOrWhitespace(this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages), c.PropertyId[c.PropertyId.SpeechServiceConnection_TranslationToLanguages]), a.Contracts.throwIfNullOrWhitespace(this.properties.getProperty(c.PropertyId.SpeechServiceConnection_RecoLanguage), c.PropertyId[c.PropertyId.SpeechServiceConnection_RecoLanguage]);
      }
      static FromConfig(e3, t3, r3) {
        const n2 = e3;
        return t3.properties.mergeTo(n2.properties), t3.properties.getProperty(c.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages, void 0) === i2.AutoDetectSourceLanguagesOpenRangeOptionName && n2.properties.setProperty(c.PropertyId.SpeechServiceConnection_RecoLanguage, "en-US"), new p(e3, r3);
      }
      get speechRecognitionLanguage() {
        return a.Contracts.throwIfDisposed(this.privDisposedTranslationRecognizer), this.properties.getProperty(c.PropertyId.SpeechServiceConnection_RecoLanguage);
      }
      get targetLanguages() {
        return a.Contracts.throwIfDisposed(this.privDisposedTranslationRecognizer), this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages).split(",");
      }
      get voiceName() {
        return a.Contracts.throwIfDisposed(this.privDisposedTranslationRecognizer), this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationVoice, void 0);
      }
      get properties() {
        return this.privProperties;
      }
      get authorizationToken() {
        return this.properties.getProperty(c.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        this.properties.setProperty(c.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      recognizeOnceAsync(e3, t3) {
        a.Contracts.throwIfDisposed(this.privDisposedTranslationRecognizer), (0, s.marshalPromiseToCallbacks)(this.recognizeOnceAsyncImpl(n.RecognitionMode.Interactive), e3, t3);
      }
      startContinuousRecognitionAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.startContinuousRecognitionAsyncImpl(n.RecognitionMode.Conversation), e3, t3);
      }
      stopContinuousRecognitionAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.stopContinuousRecognitionAsyncImpl(), e3, t3);
      }
      removeTargetLanguage(e3) {
        if (a.Contracts.throwIfNullOrUndefined(e3, "language to be removed"), void 0 !== this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages, void 0)) {
          const t3 = this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages).split(","), r3 = t3.indexOf(e3);
          r3 > -1 && (t3.splice(r3, 1), this.properties.setProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages, t3.join(",")), this.updateLanguages(t3));
        }
      }
      addTargetLanguage(e3) {
        a.Contracts.throwIfNullOrUndefined(e3, "language to be added");
        let t3 = [];
        void 0 !== this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages, void 0) ? (t3 = this.properties.getProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages).split(","), t3.includes(e3) || (t3.push(e3), this.properties.setProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages, t3.join(",")))) : (this.properties.setProperty(c.PropertyId.SpeechServiceConnection_TranslationToLanguages, e3), t3 = [e3]), this.updateLanguages(t3);
      }
      close(e3, t3) {
        a.Contracts.throwIfDisposed(this.privDisposedTranslationRecognizer), (0, s.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      onConnection() {
      }
      async dispose(e3) {
        this.privDisposedTranslationRecognizer || (this.privDisposedTranslationRecognizer = true, e3 && (await this.implRecognizerStop(), await super.dispose(e3)));
      }
      createRecognizerConfig(e3) {
        return new i2.RecognizerConfig(e3, this.privProperties);
      }
      createServiceRecognizer(e3, t3, r3, n2) {
        const s2 = r3;
        return new i2.TranslationServiceRecognizer(e3, t3, s2, n2, this);
      }
      updateLanguages(e3) {
        const t3 = o.Connection.fromRecognizer(this);
        t3 && (t3.setMessageProperty("speech.context", "translationcontext", { to: e3 }), t3.sendMessageAsync("event", JSON.stringify({ id: "translation", name: "updateLanguage", to: e3 })));
      }
    }
    t2.TranslationRecognizer = p;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Connection = void 0;
    const i2 = r2(2), n = r2(4), s = r2(114), o = r2(65), a = r2(80);
    class c {
      static fromRecognizer(e3) {
        const t3 = e3.internalData, r3 = new c();
        return r3.privInternalData = t3, r3.setupEvents(), r3;
      }
      static fromSynthesizer(e3) {
        const t3 = e3.internalData, r3 = new c();
        return r3.privInternalData = t3, r3.setupEvents(), r3;
      }
      openConnection(e3, t3) {
        (0, n.marshalPromiseToCallbacks)(this.privInternalData.connect(), e3, t3);
      }
      closeConnection(e3, t3) {
        if (this.privInternalData instanceof i2.SynthesisAdapterBase) throw new Error("Disconnecting a synthesizer's connection is currently not supported");
        (0, n.marshalPromiseToCallbacks)(this.privInternalData.disconnect(), e3, t3);
      }
      setMessageProperty(e3, t3, r3) {
        if (o.Contracts.throwIfNullOrWhitespace(t3, "propertyName"), this.privInternalData instanceof i2.ServiceRecognizerBase) {
          if ("speech.context" !== e3.toLowerCase()) throw new Error("Only speech.context message property sets are currently supported for recognizer");
          this.privInternalData.speechContext.getContext()[t3] = r3;
        } else if (this.privInternalData instanceof i2.SynthesisAdapterBase) {
          if ("speech.config" !== e3.toLowerCase() && "synthesis.context" !== e3.toLowerCase()) throw new Error("Only speech.config and synthesis.context message paths are currently supported for synthesizer");
          if ("speech.config" === e3.toLowerCase()) {
            if ("context" !== t3.toLowerCase()) throw new Error("Only context property is currently supported for speech.config message path for synthesizer");
            this.privInternalData.synthesizerConfig.setContextFromJson(r3);
          } else this.privInternalData.synthesisContext.setSection(t3, r3);
        }
      }
      sendMessageAsync(e3, t3, r3, i3) {
        (0, n.marshalPromiseToCallbacks)(this.privInternalData.sendNetworkMessage(e3, t3), r3, i3);
      }
      close() {
      }
      setupEvents() {
        this.privEventListener = this.privInternalData.connectionEvents.attach((e3) => {
          "ConnectionEstablishedEvent" === e3.name ? this.connected && this.connected(new a.ConnectionEventArgs(e3.connectionId)) : "ConnectionClosedEvent" === e3.name ? this.disconnected && this.disconnected(new a.ConnectionEventArgs(e3.connectionId)) : "ConnectionMessageSentEvent" === e3.name ? this.messageSent && this.messageSent(new a.ConnectionMessageEventArgs(new s.ConnectionMessageImpl(e3.message))) : "ConnectionMessageReceivedEvent" === e3.name && this.messageReceived && this.messageReceived(new a.ConnectionMessageEventArgs(new s.ConnectionMessageImpl(e3.message)));
        }), this.privServiceEventListener = this.privInternalData.serviceEvents.attach((e3) => {
          this.receivedServiceMessage && this.receivedServiceMessage(new a.ServiceEventArgs(e3.jsonString, e3.name));
        });
      }
    }
    t2.Connection = c;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionMessageImpl = t2.ConnectionMessage = void 0;
    const i2 = r2(54), n = r2(4), s = r2(107), o = r2(108);
    t2.ConnectionMessage = class {
    };
    t2.ConnectionMessageImpl = class {
      constructor(e3) {
        this.privConnectionMessage = e3, this.privProperties = new s.PropertyCollection(), this.privConnectionMessage.headers[i2.HeaderNames.ConnectionId] && this.privProperties.setProperty(o.PropertyId.Speech_SessionId, this.privConnectionMessage.headers[i2.HeaderNames.ConnectionId]), Object.keys(this.privConnectionMessage.headers).forEach((e4) => {
          this.privProperties.setProperty(e4, this.privConnectionMessage.headers[e4]);
        });
      }
      get path() {
        return this.privConnectionMessage.headers[Object.keys(this.privConnectionMessage.headers).find((e3) => e3.toLowerCase() === "path".toLowerCase())];
      }
      get isTextMessage() {
        return this.privConnectionMessage.messageType === n.MessageType.Text;
      }
      get isBinaryMessage() {
        return this.privConnectionMessage.messageType === n.MessageType.Binary;
      }
      get TextMessage() {
        return this.privConnectionMessage.textBody;
      }
      get binaryMessage() {
        return this.privConnectionMessage.binaryBody;
      }
      get properties() {
        return this.privProperties;
      }
      toString() {
        return "";
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Translations = void 0;
    const i2 = r2(80);
    t2.Translations = class {
      constructor() {
        this.privMap = new i2.PropertyCollection();
      }
      get languages() {
        return this.privMap.keys;
      }
      get(e3, t3) {
        return this.privMap.getProperty(e3, t3);
      }
      set(e3, t3) {
        this.privMap.setProperty(e3, t3);
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.NoMatchReason = void 0, function(e3) {
      e3[e3.NotRecognized = 0] = "NotRecognized", e3[e3.InitialSilenceTimeout = 1] = "InitialSilenceTimeout", e3[e3.InitialBabbleTimeout = 2] = "InitialBabbleTimeout";
    }(t2.NoMatchReason || (t2.NoMatchReason = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.NoMatchDetails = void 0;
    const i2 = r2(2), n = r2(80);
    class s {
      constructor(e3) {
        this.privReason = e3;
      }
      static fromResult(e3) {
        const t3 = i2.SimpleSpeechPhrase.fromJSON(e3.json, 0);
        let r3 = n.NoMatchReason.NotRecognized;
        switch (t3.RecognitionStatus) {
          case i2.RecognitionStatus.BabbleTimeout:
            r3 = n.NoMatchReason.InitialBabbleTimeout;
            break;
          case i2.RecognitionStatus.InitialSilenceTimeout:
            r3 = n.NoMatchReason.InitialSilenceTimeout;
            break;
          default:
            r3 = n.NoMatchReason.NotRecognized;
        }
        return new s(r3);
      }
      get reason() {
        return this.privReason;
      }
    }
    t2.NoMatchDetails = s;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationRecognitionCanceledEventArgs = void 0;
    t2.TranslationRecognitionCanceledEventArgs = class {
      constructor(e3, t3, r2, i2, n) {
        this.privCancelReason = t3, this.privErrorDetails = r2, this.privResult = n, this.privSessionId = e3, this.privErrorCode = i2;
      }
      get result() {
        return this.privResult;
      }
      get sessionId() {
        return this.privSessionId;
      }
      get reason() {
        return this.privCancelReason;
      }
      get errorCode() {
        return this.privErrorCode;
      }
      get errorDetails() {
        return this.privErrorDetails;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CancellationDetailsBase = void 0;
    t2.CancellationDetailsBase = class {
      constructor(e3, t3, r2) {
        this.privReason = e3, this.privErrorDetails = t3, this.privErrorCode = r2;
      }
      get reason() {
        return this.privReason;
      }
      get errorDetails() {
        return this.privErrorDetails;
      }
      get ErrorCode() {
        return this.privErrorCode;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CancellationDetails = void 0;
    const i2 = r2(2), n = r2(119), s = r2(80);
    class o extends n.CancellationDetailsBase {
      constructor(e3, t3, r3) {
        super(e3, t3, r3);
      }
      static fromResult(e3) {
        let t3 = s.CancellationReason.Error, r3 = s.CancellationErrorCode.NoError;
        if (e3 instanceof s.RecognitionResult && e3.json) {
          const r4 = i2.SimpleSpeechPhrase.fromJSON(e3.json, 0);
          t3 = i2.EnumTranslation.implTranslateCancelResult(r4.RecognitionStatus);
        }
        return e3.properties && (r3 = s.CancellationErrorCode[e3.properties.getProperty(i2.CancellationErrorCodePropertyName, s.CancellationErrorCode[s.CancellationErrorCode.NoError])]), new o(t3, e3.errorDetails || i2.EnumTranslation.implTranslateErrorDetails(r3), r3);
      }
    }
    t2.CancellationDetails = o;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CancellationErrorCode = void 0, function(e3) {
      e3[e3.NoError = 0] = "NoError", e3[e3.AuthenticationFailure = 1] = "AuthenticationFailure", e3[e3.BadRequestParameters = 2] = "BadRequestParameters", e3[e3.TooManyRequests = 3] = "TooManyRequests", e3[e3.ConnectionFailure = 4] = "ConnectionFailure", e3[e3.ServiceTimeout = 5] = "ServiceTimeout", e3[e3.ServiceError = 6] = "ServiceError", e3[e3.RuntimeError = 7] = "RuntimeError", e3[e3.Forbidden = 8] = "Forbidden";
    }(t2.CancellationErrorCode || (t2.CancellationErrorCode = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
    }
    t2.ConnectionEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ServiceEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
      constructor(e3, t3, r3) {
        super(r3), this.privJsonResult = e3, this.privEventName = t3;
      }
      get jsonString() {
        return this.privJsonResult;
      }
      get eventName() {
        return this.privEventName;
      }
    }
    t2.ServiceEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PhraseListGrammar = void 0;
    const i2 = r2(65);
    class n {
      constructor(e3) {
        this.privGrammerBuilder = e3.dynamicGrammar;
      }
      static fromRecognizer(e3) {
        const t3 = e3.internalData;
        return new n(t3);
      }
      addPhrase(e3) {
        this.privGrammerBuilder.addPhrase(e3);
      }
      addPhrases(e3) {
        this.privGrammerBuilder.addPhrase(e3);
      }
      clear() {
        this.privGrammerBuilder.clearPhrases();
      }
      setWeight(e3) {
        i2.Contracts.throwIfNumberOutOfRange(e3, "weight", 0, 2), this.privGrammerBuilder.setWeight(e3);
      }
    }
    t2.PhraseListGrammar = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DialogServiceConfigImpl = t2.DialogServiceConfig = void 0;
    const i2 = r2(65), n = r2(80);
    class s {
      constructor() {
      }
      set applicationId(e3) {
      }
      static get DialogTypes() {
        return { BotFramework: "bot_framework", CustomCommands: "custom_commands" };
      }
    }
    t2.DialogServiceConfig = s;
    t2.DialogServiceConfigImpl = class extends s {
      constructor() {
        super(), this.privSpeechConfig = new n.SpeechConfigImpl();
      }
      get properties() {
        return this.privSpeechConfig.properties;
      }
      get speechRecognitionLanguage() {
        return this.privSpeechConfig.speechRecognitionLanguage;
      }
      set speechRecognitionLanguage(e3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "value"), this.privSpeechConfig.speechRecognitionLanguage = e3;
      }
      get outputFormat() {
        return this.privSpeechConfig.outputFormat;
      }
      set outputFormat(e3) {
        this.privSpeechConfig.outputFormat = e3;
      }
      setProperty(e3, t3) {
        this.privSpeechConfig.setProperty(e3, t3);
      }
      getProperty(e3, t3) {
        return this.privSpeechConfig.getProperty(e3);
      }
      setProxy(e3, t3, r3, i3) {
        this.setProperty(n.PropertyId.SpeechServiceConnection_ProxyHostName, e3), this.setProperty(n.PropertyId.SpeechServiceConnection_ProxyPort, `${t3}`), r3 && this.setProperty(n.PropertyId.SpeechServiceConnection_ProxyUserName, r3), i3 && this.setProperty(n.PropertyId.SpeechServiceConnection_ProxyPassword, i3);
      }
      setServiceProperty(e3, t3, r3) {
        this.privSpeechConfig.setServiceProperty(e3, t3);
      }
      close() {
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.BotFrameworkConfig = void 0;
    const i2 = r2(65), n = r2(125), s = r2(80);
    class o extends n.DialogServiceConfigImpl {
      constructor() {
        super();
      }
      static fromSubscription(e3, t3, r3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "subscription"), i2.Contracts.throwIfNullOrWhitespace(t3, "region");
        const o2 = new n.DialogServiceConfigImpl();
        return o2.setProperty(s.PropertyId.Conversation_DialogType, n.DialogServiceConfig.DialogTypes.BotFramework), o2.setProperty(s.PropertyId.SpeechServiceConnection_Key, e3), o2.setProperty(s.PropertyId.SpeechServiceConnection_Region, t3), r3 && o2.setProperty(s.PropertyId.Conversation_ApplicationId, r3), o2;
      }
      static fromAuthorizationToken(e3, t3, r3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "authorizationToken"), i2.Contracts.throwIfNullOrWhitespace(t3, "region");
        const o2 = new n.DialogServiceConfigImpl();
        return o2.setProperty(s.PropertyId.Conversation_DialogType, n.DialogServiceConfig.DialogTypes.BotFramework), o2.setProperty(s.PropertyId.SpeechServiceAuthorization_Token, e3), o2.setProperty(s.PropertyId.SpeechServiceConnection_Region, t3), r3 && o2.setProperty(s.PropertyId.Conversation_ApplicationId, r3), o2;
      }
      static fromHost(e3, t3, r3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "host");
        const o2 = e3 instanceof URL ? e3 : new URL(`wss://${e3}.convai.speech.azure.us`);
        i2.Contracts.throwIfNullOrUndefined(o2, "resolvedHost");
        const a = new n.DialogServiceConfigImpl();
        return a.setProperty(s.PropertyId.Conversation_DialogType, n.DialogServiceConfig.DialogTypes.BotFramework), a.setProperty(s.PropertyId.SpeechServiceConnection_Host, o2.toString()), void 0 !== t3 && a.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3), a;
      }
      static fromEndpoint(e3, t3) {
        i2.Contracts.throwIfNull(e3, "endpoint");
        const r3 = new n.DialogServiceConfigImpl();
        return r3.setProperty(s.PropertyId.Conversation_DialogType, n.DialogServiceConfig.DialogTypes.BotFramework), r3.setProperty(s.PropertyId.SpeechServiceConnection_Endpoint, e3.toString()), void 0 !== t3 && r3.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3), r3;
      }
    }
    t2.BotFrameworkConfig = o;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CustomCommandsConfig = void 0;
    const i2 = r2(65), n = r2(125), s = r2(80);
    class o extends n.DialogServiceConfigImpl {
      constructor() {
        super();
      }
      static fromSubscription(e3, t3, r3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "applicationId"), i2.Contracts.throwIfNullOrWhitespace(t3, "subscription"), i2.Contracts.throwIfNullOrWhitespace(r3, "region");
        const o2 = new n.DialogServiceConfigImpl();
        return o2.setProperty(s.PropertyId.Conversation_DialogType, n.DialogServiceConfig.DialogTypes.CustomCommands), o2.setProperty(s.PropertyId.Conversation_ApplicationId, e3), o2.setProperty(s.PropertyId.SpeechServiceConnection_Key, t3), o2.setProperty(s.PropertyId.SpeechServiceConnection_Region, r3), o2;
      }
      static fromAuthorizationToken(e3, t3, r3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "applicationId"), i2.Contracts.throwIfNullOrWhitespace(t3, "authorizationToken"), i2.Contracts.throwIfNullOrWhitespace(r3, "region");
        const o2 = new n.DialogServiceConfigImpl();
        return o2.setProperty(s.PropertyId.Conversation_DialogType, n.DialogServiceConfig.DialogTypes.CustomCommands), o2.setProperty(s.PropertyId.Conversation_ApplicationId, e3), o2.setProperty(s.PropertyId.SpeechServiceAuthorization_Token, t3), o2.setProperty(s.PropertyId.SpeechServiceConnection_Region, r3), o2;
      }
      set applicationId(e3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "value"), this.setProperty(s.PropertyId.Conversation_ApplicationId, e3);
      }
      get applicationId() {
        return this.getProperty(s.PropertyId.Conversation_ApplicationId);
      }
    }
    t2.CustomCommandsConfig = o;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DialogServiceConnector = void 0;
    const i2 = r2(129), n = r2(2), s = r2(111), o = r2(4), a = r2(65), c = r2(80), p = r2(108);
    class h extends c.Recognizer {
      constructor(e3, t3) {
        const r3 = e3;
        a.Contracts.throwIfNull(e3, "dialogConfig"), super(t3, r3.properties, new i2.DialogConnectionFactory()), this.isTurnComplete = true, this.privIsDisposed = false, this.privProperties = r3.properties.clone();
        const n2 = this.buildAgentConfig();
        this.privReco.agentConfig.set(n2);
      }
      connect(e3, t3) {
        (0, o.marshalPromiseToCallbacks)(this.privReco.connect(), e3, t3);
      }
      disconnect(e3, t3) {
        (0, o.marshalPromiseToCallbacks)(this.privReco.disconnect(), e3, t3);
      }
      get authorizationToken() {
        return this.properties.getProperty(p.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        a.Contracts.throwIfNullOrWhitespace(e3, "token"), this.properties.setProperty(p.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      get properties() {
        return this.privProperties;
      }
      get speechActivityTemplate() {
        return this.properties.getProperty(p.PropertyId.Conversation_Speech_Activity_Template);
      }
      set speechActivityTemplate(e3) {
        this.properties.setProperty(p.PropertyId.Conversation_Speech_Activity_Template, e3);
      }
      listenOnceAsync(e3, t3) {
        if (this.isTurnComplete) {
          a.Contracts.throwIfDisposed(this.privIsDisposed);
          const r3 = (async () => {
            await this.privReco.connect(), await this.implRecognizerStop(), this.isTurnComplete = false;
            const e4 = new o.Deferred();
            await this.privReco.recognize(s.RecognitionMode.Conversation, e4.resolve, e4.reject);
            const t4 = await e4.promise;
            return await this.implRecognizerStop(), t4;
          })();
          r3.catch(() => {
            this.dispose(true).catch(() => {
            });
          }), (0, o.marshalPromiseToCallbacks)(r3.finally(() => {
            this.isTurnComplete = true;
          }), e3, t3);
        }
      }
      sendActivityAsync(e3, t3, r3) {
        (0, o.marshalPromiseToCallbacks)(this.privReco.sendMessage(e3), t3, r3);
      }
      close(e3, t3) {
        a.Contracts.throwIfDisposed(this.privIsDisposed), (0, o.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      async dispose(e3) {
        this.privIsDisposed || e3 && (this.privIsDisposed = true, await this.implRecognizerStop(), await super.dispose(e3));
      }
      createRecognizerConfig(e3) {
        return new n.RecognizerConfig(e3, this.privProperties);
      }
      createServiceRecognizer(e3, t3, r3, i3) {
        const s2 = r3;
        return new n.DialogServiceAdapter(e3, t3, s2, i3, this);
      }
      buildAgentConfig() {
        return { botInfo: { commType: this.properties.getProperty("Conversation_Communication_Type", "Default"), commandsCulture: void 0, connectionId: this.properties.getProperty(p.PropertyId.Conversation_Agent_Connection_Id), conversationId: this.properties.getProperty(p.PropertyId.Conversation_Conversation_Id, void 0), fromId: this.properties.getProperty(p.PropertyId.Conversation_From_Id, void 0), ttsAudioFormat: this.properties.getProperty(p.PropertyId.SpeechServiceConnection_SynthOutputFormat, void 0) }, version: 0.2 };
      }
    }
    t2.DialogServiceConnector = h;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DialogConnectionFactory = void 0;
    const i2 = r2(61), n = r2(2), s = r2(80), o = r2(130), a = r2(2), c = r2(54), p = r2(131);
    class h extends o.ConnectionFactoryBase {
      create(e3, t3, r3) {
        const u = e3.parameters.getProperty(s.PropertyId.Conversation_ApplicationId, ""), d = e3.parameters.getProperty(s.PropertyId.Conversation_DialogType), v = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Region), l = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage, "en-US"), g = e3.parameters.getProperty(s.PropertyId.Conversation_Request_Bot_Status_Messages, "true"), m = {};
        m[c.HeaderNames.ConnectionId] = r3, m[p.QueryParameterNames.Format] = e3.parameters.getProperty(n.OutputFormatPropertyName, s.OutputFormat[s.OutputFormat.Simple]).toLowerCase(), m[p.QueryParameterNames.Language] = l, m[p.QueryParameterNames.RequestBotStatusMessages] = g, u && (m[p.QueryParameterNames.BotId] = u, d === s.DialogServiceConfig.DialogTypes.CustomCommands && (m[c.HeaderNames.CustomCommandsAppId] = u));
        const S = d === s.DialogServiceConfig.DialogTypes.CustomCommands ? "commands/" : "", f = d === s.DialogServiceConfig.DialogTypes.CustomCommands ? "v1" : d === s.DialogServiceConfig.DialogTypes.BotFramework ? "v3" : "v0", y = {};
        null != t3.token && "" !== t3.token && (y[t3.headerName] = t3.token);
        let C = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Endpoint, "");
        if (!C) {
          const t4 = o.ConnectionFactoryBase.getHostSuffix(v), r4 = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Host, `wss://${v}.${h.BaseUrl}${t4}`);
          C = `${r4.endsWith("/") ? r4 : r4 + "/"}${S}${h.ApiKey}/${f}`;
        }
        this.setCommonUrlParams(e3, m, C);
        const P = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false");
        return Promise.resolve(new i2.WebsocketConnection(C, m, y, new a.WebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), P, r3));
      }
    }
    t2.DialogConnectionFactory = h, h.ApiKey = "api", h.BaseUrl = "convai.speech";
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionFactoryBase = void 0;
    const i2 = r2(2), n = r2(4), s = r2(80), o = r2(131);
    t2.ConnectionFactoryBase = class {
      static getHostSuffix(e3) {
        if (e3) {
          if (e3.toLowerCase().startsWith("china")) return ".azure.cn";
          if (e3.toLowerCase().startsWith("usgov")) return ".azure.us";
        }
        return ".microsoft.com";
      }
      setCommonUrlParams(e3, t3, r3) {
        (/* @__PURE__ */ new Map([[s.PropertyId.Speech_SegmentationSilenceTimeoutMs, o.QueryParameterNames.SegmentationSilenceTimeoutMs], [s.PropertyId.SpeechServiceConnection_EnableAudioLogging, o.QueryParameterNames.EnableAudioLogging], [s.PropertyId.SpeechServiceConnection_EndSilenceTimeoutMs, o.QueryParameterNames.EndSilenceTimeoutMs], [s.PropertyId.SpeechServiceConnection_InitialSilenceTimeoutMs, o.QueryParameterNames.InitialSilenceTimeoutMs], [s.PropertyId.SpeechServiceResponse_PostProcessingOption, o.QueryParameterNames.Postprocessing], [s.PropertyId.SpeechServiceResponse_ProfanityOption, o.QueryParameterNames.Profanity], [s.PropertyId.SpeechServiceResponse_RequestWordLevelTimestamps, o.QueryParameterNames.EnableWordLevelTimestamps], [s.PropertyId.SpeechServiceResponse_StablePartialResultThreshold, o.QueryParameterNames.StableIntermediateThreshold]])).forEach((i3, n3) => {
          this.setUrlParameter(n3, i3, e3, t3, r3);
        });
        const n2 = JSON.parse(e3.parameters.getProperty(i2.ServicePropertiesPropertyName, "{}"));
        Object.keys(n2).forEach((e4) => {
          t3[e4] = n2[e4];
        });
      }
      setUrlParameter(e3, t3, r3, i3, n2) {
        const s2 = r3.parameters.getProperty(e3, void 0);
        !s2 || n2 && -1 !== n2.search(t3) || (i3[t3] = s2.toLocaleLowerCase());
      }
      static async getRedirectUrlFromEndpoint(e3) {
        const t3 = new URL(e3);
        t3.protocol = "https:", t3.port = "443";
        t3.searchParams.append("GenerateRedirectResponse", "true");
        const r3 = t3.toString();
        n.Events.instance.onEvent(new n.ConnectionRedirectEvent("", r3, void 0, "ConnectionFactoryBase: redirectUrl request"));
        const i3 = await fetch(r3);
        if (200 !== i3.status) return e3;
        const s2 = await i3.text();
        n.Events.instance.onEvent(new n.ConnectionRedirectEvent("", s2, e3, "ConnectionFactoryBase: redirectUrlString"));
        try {
          return new URL(s2.trim()).toString();
        } catch (t4) {
          return e3;
        }
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.QueryParameterNames = void 0;
    class r2 {
    }
    t2.QueryParameterNames = r2, r2.BotId = "botid", r2.CustomSpeechDeploymentId = "cid", r2.CustomVoiceDeploymentId = "deploymentId", r2.EnableAudioLogging = "storeAudio", r2.EnableLanguageId = "lidEnabled", r2.EnableWordLevelTimestamps = "wordLevelTimestamps", r2.EndSilenceTimeoutMs = "endSilenceTimeoutMs", r2.SegmentationSilenceTimeoutMs = "segmentationSilenceTimeoutMs", r2.SegmentationMaximumTimeMs = "segmentationMaximumTimeMs", r2.SegmentationStrategy = "segmentationStrategy", r2.Format = "format", r2.InitialSilenceTimeoutMs = "initialSilenceTimeoutMs", r2.Language = "language", r2.Profanity = "profanity", r2.RequestBotStatusMessages = "enableBotMessageStatus", r2.StableIntermediateThreshold = "stableIntermediateThreshold", r2.StableTranslation = "stableTranslation", r2.TestHooks = "testhooks", r2.Postprocessing = "postprocessing", r2.CtsMeetingId = "meetingId", r2.CtsDeviceId = "deviceId", r2.CtsIsParticipant = "isParticipant", r2.EnableAvatar = "enableTalkingAvatar";
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ActivityReceivedEventArgs = void 0;
    t2.ActivityReceivedEventArgs = class {
      constructor(e3, t3) {
        this.privActivity = e3, this.privAudioStream = t3;
      }
      get activity() {
        return this.privActivity;
      }
      get audioStream() {
        return this.privAudioStream;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TurnStatusReceivedEventArgs = void 0;
    const i2 = r2(134);
    t2.TurnStatusReceivedEventArgs = class {
      constructor(e3) {
        this.privTurnStatus = i2.TurnStatusResponsePayload.fromJSON(e3);
      }
      get interactionId() {
        return this.privTurnStatus.interactionId;
      }
      get conversationId() {
        return this.privTurnStatus.conversationId;
      }
      get statusCode() {
        return this.privTurnStatus.statusCode;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TurnStatusResponsePayload = void 0;
    class r2 {
      constructor(e3) {
        this.privMessageStatusResponse = JSON.parse(e3);
      }
      static fromJSON(e3) {
        return new r2(e3);
      }
      get interactionId() {
        return this.privMessageStatusResponse.interactionId;
      }
      get conversationId() {
        return this.privMessageStatusResponse.conversationId;
      }
      get statusCode() {
        switch (this.privMessageStatusResponse.statusCode) {
          case "Success":
            return 200;
          case "Failed":
            return 400;
          case "TimedOut":
            return 429;
          default:
            return this.privMessageStatusResponse.statusCode;
        }
      }
    }
    t2.TurnStatusResponsePayload = r2;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ServicePropertyChannel = void 0, function(e3) {
      e3[e3.UriQueryParameter = 0] = "UriQueryParameter";
    }(t2.ServicePropertyChannel || (t2.ServicePropertyChannel = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ProfanityOption = void 0, function(e3) {
      e3[e3.Masked = 0] = "Masked", e3[e3.Removed = 1] = "Removed", e3[e3.Raw = 2] = "Raw";
    }(t2.ProfanityOption || (t2.ProfanityOption = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.BaseAudioPlayer = void 0;
    const i2 = r2(26), n = r2(80), s = r2(68);
    t2.BaseAudioPlayer = class {
      constructor(e3) {
        this.audioContext = null, this.gainNode = null, this.autoUpdateBufferTimer = 0, void 0 === e3 && (e3 = n.AudioStreamFormat.getDefaultInputFormat()), this.init(e3);
      }
      playAudioSample(e3, t3, r3) {
        try {
          this.ensureInitializedContext();
          const r4 = this.formatAudioData(e3), i3 = new Float32Array(this.samples.length + r4.length);
          i3.set(this.samples, 0), i3.set(r4, this.samples.length), this.samples = i3, t3 && t3();
        } catch (e4) {
          r3 && r3(e4);
        }
      }
      stopAudio(e3, t3) {
        null !== this.audioContext && (this.samples = new Float32Array(), clearInterval(this.autoUpdateBufferTimer), this.audioContext.close().then(() => {
          e3 && e3();
        }, (e4) => {
          t3 && t3(e4);
        }), this.audioContext = null);
      }
      init(e3) {
        this.audioFormat = e3, this.samples = new Float32Array();
      }
      ensureInitializedContext() {
        if (null === this.audioContext) {
          this.createAudioContext();
          const e3 = 200;
          this.autoUpdateBufferTimer = setInterval(() => {
            this.updateAudioBuffer();
          }, e3);
        }
      }
      createAudioContext() {
        this.audioContext = s.AudioStreamFormatImpl.getAudioContext(), this.gainNode = this.audioContext.createGain(), this.gainNode.gain.value = 1, this.gainNode.connect(this.audioContext.destination), this.startTime = this.audioContext.currentTime;
      }
      formatAudioData(e3) {
        switch (this.audioFormat.bitsPerSample) {
          case 8:
            return this.formatArrayBuffer(new Int8Array(e3), 128);
          case 16:
            return this.formatArrayBuffer(new Int16Array(e3), 32768);
          case 32:
            return this.formatArrayBuffer(new Int32Array(e3), 2147483648);
          default:
            throw new i2.InvalidOperationError("Only WAVE_FORMAT_PCM (8/16/32 bps) format supported at this time");
        }
      }
      formatArrayBuffer(e3, t3) {
        const r3 = new Float32Array(e3.length);
        for (let i3 = 0; i3 < e3.length; i3++) r3[i3] = e3[i3] / t3;
        return r3;
      }
      updateAudioBuffer() {
        if (0 === this.samples.length) return;
        const e3 = this.audioFormat.channels, t3 = this.audioContext.createBufferSource(), r3 = this.samples.length / e3, i3 = this.audioContext.createBuffer(e3, r3, this.audioFormat.samplesPerSec);
        for (let t4 = 0; t4 < e3; t4++) {
          let r4 = t4;
          const n2 = i3.getChannelData(t4);
          for (let t5 = 0; t5 < this.samples.length; t5++, r4 += e3) n2[t5] = this.samples[r4];
        }
        this.startTime < this.audioContext.currentTime && (this.startTime = this.audioContext.currentTime), t3.buffer = i3, t3.connect(this.gainNode), t3.start(this.startTime), this.startTime += i3.duration, this.samples = new Float32Array();
      }
      async playAudio(e3) {
        null === this.audioContext && this.createAudioContext();
        const t3 = this.audioContext.createBufferSource(), r3 = this.audioContext.destination;
        await this.audioContext.decodeAudioData(e3, (e4) => {
          t3.buffer = e4, t3.connect(r3), t3.start(0);
        });
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConnectionMessageEventArgs = void 0;
    t2.ConnectionMessageEventArgs = class {
      constructor(e3) {
        this.privConnectionMessage = e3;
      }
      get message() {
        return this.privConnectionMessage;
      }
      toString() {
        return "Message: " + this.privConnectionMessage.toString();
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AutoDetectSourceLanguageConfig = void 0;
    const i2 = r2(2), n = r2(65), s = r2(80), o = r2(140);
    class a {
      constructor() {
        this.privProperties = new s.PropertyCollection(), this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_LanguageIdMode, "AtStart"), this.privLanguageIdMode = o.LanguageIdMode.AtStart;
      }
      static fromOpenRange() {
        const e3 = new a();
        return e3.properties.setProperty(s.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages, i2.AutoDetectSourceLanguagesOpenRangeOptionName), e3.properties.setProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage, "en-US"), e3;
      }
      static fromLanguages(e3) {
        n.Contracts.throwIfArrayEmptyOrWhitespace(e3, "languages");
        const t3 = new a();
        return t3.properties.setProperty(s.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages, e3.join()), t3;
      }
      static fromSourceLanguageConfigs(e3) {
        if (e3.length < 1) throw new Error("Expected non-empty SourceLanguageConfig array.");
        const t3 = new a(), r3 = [];
        return e3.forEach((e4) => {
          if (r3.push(e4.language), void 0 !== e4.endpointId && "" !== e4.endpointId) {
            const r4 = e4.language + s.PropertyId.SpeechServiceConnection_EndpointId.toString();
            t3.properties.setProperty(r4, e4.endpointId);
          }
        }), t3.properties.setProperty(s.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages, r3.join()), t3;
      }
      get properties() {
        return this.privProperties;
      }
      set mode(e3) {
        e3 === o.LanguageIdMode.Continuous ? (this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, "2"), this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_LanguageIdMode, "Continuous")) : (this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, "1"), this.privProperties.setProperty(s.PropertyId.SpeechServiceConnection_LanguageIdMode, "AtStart")), this.privLanguageIdMode = e3;
      }
    }
    t2.AutoDetectSourceLanguageConfig = a;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.LanguageIdMode = void 0, function(e3) {
      e3[e3.AtStart = 0] = "AtStart", e3[e3.Continuous = 1] = "Continuous";
    }(t2.LanguageIdMode || (t2.LanguageIdMode = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AutoDetectSourceLanguageResult = void 0;
    const i2 = r2(65);
    class n {
      constructor(e3, t3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "language"), i2.Contracts.throwIfNullOrUndefined(t3, "languageDetectionConfidence"), this.privLanguage = e3, this.privLanguageDetectionConfidence = t3;
      }
      static fromResult(e3) {
        return new n(e3.language, e3.languageDetectionConfidence);
      }
      static fromConversationTranscriptionResult(e3) {
        return new n(e3.language, e3.languageDetectionConfidence);
      }
      get language() {
        return this.privLanguage;
      }
      get languageDetectionConfidence() {
        return this.privLanguageDetectionConfidence;
      }
    }
    t2.AutoDetectSourceLanguageResult = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SourceLanguageConfig = void 0;
    const i2 = r2(65);
    class n {
      constructor(e3, t3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "language"), this.privLanguage = e3, this.privEndpointId = t3;
      }
      static fromLanguage(e3, t3) {
        return new n(e3, t3);
      }
      get language() {
        return this.privLanguage;
      }
      get endpointId() {
        return this.privEndpointId;
      }
    }
    t2.SourceLanguageConfig = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranscriptionResult = t2.MeetingTranscriber = t2.MeetingTranscriptionCanceledEventArgs = t2.MeetingImpl = t2.Meeting = t2.ParticipantChangedReason = t2.User = t2.Participant = t2.ConversationTranscriber = t2.ConversationTranslator = t2.ConversationTranslationResult = t2.ConversationTranslationEventArgs = t2.ConversationTranslationCanceledEventArgs = t2.ConversationParticipantsChangedEventArgs = t2.ConversationExpirationEventArgs = t2.ConversationCommon = t2.ConversationImpl = t2.Conversation = void 0;
    var i2 = r2(144);
    Object.defineProperty(t2, "Conversation", { enumerable: true, get: function() {
      return i2.Conversation;
    } }), Object.defineProperty(t2, "ConversationImpl", { enumerable: true, get: function() {
      return i2.ConversationImpl;
    } });
    var n = r2(145);
    Object.defineProperty(t2, "ConversationCommon", { enumerable: true, get: function() {
      return n.ConversationCommon;
    } });
    var s = r2(146);
    Object.defineProperty(t2, "ConversationExpirationEventArgs", { enumerable: true, get: function() {
      return s.ConversationExpirationEventArgs;
    } });
    var o = r2(147);
    Object.defineProperty(t2, "ConversationParticipantsChangedEventArgs", { enumerable: true, get: function() {
      return o.ConversationParticipantsChangedEventArgs;
    } });
    var a = r2(148);
    Object.defineProperty(t2, "ConversationTranslationCanceledEventArgs", { enumerable: true, get: function() {
      return a.ConversationTranslationCanceledEventArgs;
    } });
    var c = r2(149);
    Object.defineProperty(t2, "ConversationTranslationEventArgs", { enumerable: true, get: function() {
      return c.ConversationTranslationEventArgs;
    } });
    var p = r2(150);
    Object.defineProperty(t2, "ConversationTranslationResult", { enumerable: true, get: function() {
      return p.ConversationTranslationResult;
    } });
    var h = r2(151);
    Object.defineProperty(t2, "ConversationTranslator", { enumerable: true, get: function() {
      return h.ConversationTranslator;
    } });
    var u = r2(154);
    Object.defineProperty(t2, "ConversationTranscriber", { enumerable: true, get: function() {
      return u.ConversationTranscriber;
    } });
    var d = r2(155);
    Object.defineProperty(t2, "Participant", { enumerable: true, get: function() {
      return d.Participant;
    } }), Object.defineProperty(t2, "User", { enumerable: true, get: function() {
      return d.User;
    } });
    var v = r2(156);
    Object.defineProperty(t2, "ParticipantChangedReason", { enumerable: true, get: function() {
      return v.ParticipantChangedReason;
    } });
    var l = r2(157);
    Object.defineProperty(t2, "Meeting", { enumerable: true, get: function() {
      return l.Meeting;
    } }), Object.defineProperty(t2, "MeetingImpl", { enumerable: true, get: function() {
      return l.MeetingImpl;
    } });
    var g = r2(158);
    Object.defineProperty(t2, "MeetingTranscriptionCanceledEventArgs", { enumerable: true, get: function() {
      return g.MeetingTranscriptionCanceledEventArgs;
    } });
    var m = r2(159);
    Object.defineProperty(t2, "MeetingTranscriber", { enumerable: true, get: function() {
      return m.MeetingTranscriber;
    } });
    var S = r2(160);
    Object.defineProperty(t2, "ConversationTranscriptionResult", { enumerable: true, get: function() {
      return S.ConversationTranscriptionResult;
    } });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationImpl = t2.Conversation = void 0;
    const i2 = r2(2), n = r2(4), s = r2(65), o = r2(80);
    class a {
      constructor() {
      }
      static createConversationAsync(e3, t3, r3, a2) {
        let p, h, u;
        return s.Contracts.throwIfNullOrUndefined(e3, i2.ConversationConnectionConfig.restErrors.invalidArgs.replace("{arg}", "config")), s.Contracts.throwIfNullOrUndefined(e3.region, i2.ConversationConnectionConfig.restErrors.invalidArgs.replace("{arg}", "SpeechServiceConnection_Region")), e3.subscriptionKey || e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceAuthorization_Token]) || s.Contracts.throwIfNullOrUndefined(e3.subscriptionKey, i2.ConversationConnectionConfig.restErrors.invalidArgs.replace("{arg}", "SpeechServiceConnection_Key")), "string" == typeof t3 ? (p = new c(e3, t3), (0, n.marshalPromiseToCallbacks)((async () => {
        })(), r3, a2)) : (p = new c(e3), h = t3, u = r3, p.createConversationAsync(() => {
          h && h();
        }, (e4) => {
          u && u(e4);
        })), p;
      }
    }
    t2.Conversation = a;
    class c extends a {
      constructor(e3, t3) {
        super(), this.privErrors = i2.ConversationConnectionConfig.restErrors, this.onConnected = (e4) => {
          this.privIsConnected = true;
          try {
            this.privConversationTranslator?.sessionStarted && this.privConversationTranslator.sessionStarted(this.privConversationTranslator, e4);
          } catch (e5) {
          }
        }, this.onDisconnected = (e4) => {
          try {
            this.privConversationTranslator?.sessionStopped && this.privConversationTranslator.sessionStopped(this.privConversationTranslator, e4);
          } catch (e5) {
          } finally {
            this.close(false);
          }
        }, this.onCanceled = (e4, t4) => {
          try {
            this.privConversationTranslator?.canceled && this.privConversationTranslator.canceled(this.privConversationTranslator, t4);
          } catch (t5) {
          }
        }, this.onParticipantUpdateCommandReceived = (e4, t4) => {
          try {
            const e5 = this.privParticipants.getParticipant(t4.id);
            if (void 0 !== e5) {
              switch (t4.key) {
                case i2.ConversationTranslatorCommandTypes.changeNickname:
                  e5.displayName = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setUseTTS:
                  e5.isUsingTts = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setProfanityFiltering:
                  e5.profanity = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setMute:
                  e5.isMuted = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setTranslateToLanguages:
                  e5.translateToLanguages = t4.value;
              }
              this.privParticipants.addOrUpdateParticipant(e5), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.Updated, [this.toParticipant(e5)], t4.sessionId));
            }
          } catch (t5) {
          }
        }, this.onLockRoomCommandReceived = () => {
        }, this.onMuteAllCommandReceived = (e4, t4) => {
          try {
            this.privParticipants.participants.forEach((e5) => e5.isMuted = !e5.isHost && t4.isMuted), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.Updated, this.toParticipants(false), t4.sessionId));
          } catch (t5) {
          }
        }, this.onParticipantJoinCommandReceived = (e4, t4) => {
          try {
            const e5 = this.privParticipants.addOrUpdateParticipant(t4.participant);
            void 0 !== e5 && this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.JoinedConversation, [this.toParticipant(e5)], t4.sessionId));
          } catch (t5) {
          }
        }, this.onParticipantLeaveCommandReceived = (e4, t4) => {
          try {
            const e5 = this.privParticipants.getParticipant(t4.participant.id);
            void 0 !== e5 && (this.privParticipants.deleteParticipant(t4.participant.id), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.LeftConversation, [this.toParticipant(e5)], t4.sessionId)));
          } catch (t5) {
          }
        }, this.onTranslationReceived = (e4, t4) => {
          try {
            switch (t4.command) {
              case i2.ConversationTranslatorMessageTypes.final:
                this.privConversationTranslator && this.privConversationTranslator.transcribed(this.privConversationTranslator, new o.ConversationTranslationEventArgs(t4.payload, void 0, t4.sessionId));
                break;
              case i2.ConversationTranslatorMessageTypes.partial:
                this.privConversationTranslator && this.privConversationTranslator.transcribing(this.privConversationTranslator, new o.ConversationTranslationEventArgs(t4.payload, void 0, t4.sessionId));
                break;
              case i2.ConversationTranslatorMessageTypes.instantMessage:
                this.privConversationTranslator && this.privConversationTranslator.textMessageReceived(this.privConversationTranslator, new o.ConversationTranslationEventArgs(t4.payload, void 0, t4.sessionId));
            }
          } catch (t5) {
          }
        }, this.onParticipantsListReceived = (e4, t4) => {
          try {
            if (void 0 !== t4.sessionToken && null !== t4.sessionToken && (this.privRoom.token = t4.sessionToken), this.privParticipants.participants = [...t4.participants], void 0 !== this.privParticipants.me && (this.privIsReady = true), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.JoinedConversation, this.toParticipants(true), t4.sessionId)), this.me.isHost) {
              const e5 = this.privConversationTranslator?.properties.getProperty(o.PropertyId.ConversationTranslator_Name);
              void 0 !== e5 && e5.length > 0 && e5 !== this.me.displayName && this.changeNicknameAsync(e5);
            }
          } catch (t5) {
          }
        }, this.onConversationExpiration = (e4, t4) => {
          try {
            this.privConversationTranslator && this.privConversationTranslator.conversationExpiration(this.privConversationTranslator, t4);
          } catch (t5) {
          }
        }, this.privIsConnected = false, this.privIsDisposed = false, this.privConversationId = "", this.privProperties = new o.PropertyCollection(), this.privManager = new i2.ConversationManager();
        if (e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceConnection_RecoLanguage]) || e3.setProperty(o.PropertyId[o.PropertyId.SpeechServiceConnection_RecoLanguage], i2.ConversationConnectionConfig.defaultLanguageCode), this.privLanguage = e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceConnection_RecoLanguage]), t3) this.privConversationId = t3;
        else {
          0 === e3.targetLanguages.length && e3.addTargetLanguage(this.privLanguage);
          e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceResponse_ProfanityOption]) || e3.setProfanity(o.ProfanityOption.Masked);
          let t4 = e3.getProperty(o.PropertyId[o.PropertyId.ConversationTranslator_Name]);
          null == t4 && (t4 = "Host"), s.Contracts.throwIfNullOrTooLong(t4, "nickname", 50), s.Contracts.throwIfNullOrTooShort(t4, "nickname", 2), e3.setProperty(o.PropertyId[o.PropertyId.ConversationTranslator_Name], t4);
        }
        this.privConfig = e3;
        const r3 = e3;
        s.Contracts.throwIfNull(r3, "speechConfig"), this.privProperties = r3.properties.clone(), this.privIsConnected = false, this.privParticipants = new i2.InternalParticipants(), this.privIsReady = false, this.privTextMessageMaxLength = 1e3;
      }
      get room() {
        return this.privRoom;
      }
      get connection() {
        return this.privConversationRecognizer;
      }
      get config() {
        return this.privConfig;
      }
      get conversationId() {
        return this.privRoom ? this.privRoom.roomId : this.privConversationId;
      }
      get properties() {
        return this.privProperties;
      }
      get speechRecognitionLanguage() {
        return this.privLanguage;
      }
      get isMutedByHost() {
        return !this.privParticipants.me?.isHost && this.privParticipants.me?.isMuted;
      }
      get isConnected() {
        return this.privIsConnected && this.privIsReady;
      }
      get participants() {
        return this.toParticipants(true);
      }
      get me() {
        return this.toParticipant(this.privParticipants.me);
      }
      get host() {
        return this.toParticipant(this.privParticipants.host);
      }
      get transcriberRecognizer() {
        return this.privTranscriberRecognizer;
      }
      get conversationInfo() {
        const e3 = this.conversationId, t3 = this.participants.map((e4) => ({ id: e4.id, preferredLanguage: e4.preferredLanguage, voice: e4.voice })), r3 = {};
        for (const e4 of i2.ConversationConnectionConfig.transcriptionEventKeys) {
          const t4 = this.properties.getProperty(e4, "");
          "" !== t4 && (r3[e4] = t4);
        }
        return { id: e3, participants: t3, conversationProperties: r3 };
      }
      get canSend() {
        return this.privIsConnected && !this.privParticipants.me?.isMuted;
      }
      get canSendAsHost() {
        return this.privIsConnected && this.privParticipants.me?.isHost;
      }
      get authorizationToken() {
        return this.privToken;
      }
      set authorizationToken(e3) {
        s.Contracts.throwIfNullOrWhitespace(e3, "authorizationToken"), this.privToken = e3;
      }
      set conversationTranslator(e3) {
        this.privConversationTranslator = e3;
      }
      onToken(e3) {
        this.privConversationTranslator.onToken(e3);
      }
      createConversationAsync(e3, t3) {
        try {
          this.privConversationRecognizer && this.handleError(new Error(this.privErrors.permissionDeniedStart), t3), this.privManager.createOrJoin(this.privProperties, void 0, (r3) => {
            r3 || this.handleError(new Error(this.privErrors.permissionDeniedConnect), t3), this.privRoom = r3, this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      startConversationAsync(e3, t3) {
        try {
          this.privConversationRecognizer && this.handleError(new Error(this.privErrors.permissionDeniedStart), t3), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedConnect), this.privParticipants.meId = this.privRoom.participantId, this.privConversationRecognizer = i2.ConversationRecognizerFactory.fromConfig(this, this.privConfig), this.privConversationRecognizer.connected = this.onConnected, this.privConversationRecognizer.disconnected = this.onDisconnected, this.privConversationRecognizer.canceled = this.onCanceled, this.privConversationRecognizer.participantUpdateCommandReceived = this.onParticipantUpdateCommandReceived, this.privConversationRecognizer.lockRoomCommandReceived = this.onLockRoomCommandReceived, this.privConversationRecognizer.muteAllCommandReceived = this.onMuteAllCommandReceived, this.privConversationRecognizer.participantJoinCommandReceived = this.onParticipantJoinCommandReceived, this.privConversationRecognizer.participantLeaveCommandReceived = this.onParticipantLeaveCommandReceived, this.privConversationRecognizer.translationReceived = this.onTranslationReceived, this.privConversationRecognizer.participantsListReceived = this.onParticipantsListReceived, this.privConversationRecognizer.conversationExpiration = this.onConversationExpiration, this.privConversationRecognizer.connect(this.privRoom.token, () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      addParticipantAsync(e3, t3, r3) {
        s.Contracts.throwIfNullOrUndefined(e3, "Participant"), (0, n.marshalPromiseToCallbacks)(this.addParticipantImplAsync(e3), t3, r3);
      }
      joinConversationAsync(e3, t3, r3, i3, n2) {
        try {
          s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "conversationId")), s.Contracts.throwIfNullOrWhitespace(t3, this.privErrors.invalidArgs.replace("{arg}", "nickname")), s.Contracts.throwIfNullOrWhitespace(r3, this.privErrors.invalidArgs.replace("{arg}", "language")), this.privManager.createOrJoin(this.privProperties, e3, (e4) => {
            s.Contracts.throwIfNullOrUndefined(e4, this.privErrors.permissionDeniedConnect), this.privRoom = e4, this.privConfig.authorizationToken = e4.cognitiveSpeechAuthToken, i3 && i3(e4.cognitiveSpeechAuthToken);
          }, (e4) => {
            this.handleError(e4, n2);
          });
        } catch (e4) {
          this.handleError(e4, n2);
        }
      }
      deleteConversationAsync(e3, t3) {
        (0, n.marshalPromiseToCallbacks)(this.deleteConversationImplAsync(), e3, t3);
      }
      async deleteConversationImplAsync() {
        s.Contracts.throwIfNullOrUndefined(this.privProperties, this.privErrors.permissionDeniedConnect), s.Contracts.throwIfNullOrWhitespace(this.privRoom.token, this.privErrors.permissionDeniedConnect), await this.privManager.leave(this.privProperties, this.privRoom.token), this.dispose();
      }
      endConversationAsync(e3, t3) {
        (0, n.marshalPromiseToCallbacks)(this.endConversationImplAsync(), e3, t3);
      }
      endConversationImplAsync() {
        return this.close(true);
      }
      lockConversationAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "lock")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getLockCommand(true), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      muteAllParticipantsAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privConversationRecognizer, this.privErrors.permissionDeniedSend), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "mute")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteAllCommand(true), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      muteParticipantAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "userId")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.me.isHost || this.me.id === e3 || this.handleError(new Error(this.privErrors.permissionDeniedParticipant.replace("{command}", "mute")), r3);
          -1 === this.privParticipants.getParticipantIndex(e3) && this.handleError(new Error(this.privErrors.invalidParticipantRequest), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteCommand(e3, true), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      removeParticipantAsync(e3, t3, r3) {
        try {
          if (s.Contracts.throwIfDisposed(this.privIsDisposed), this.privTranscriberRecognizer && e3.hasOwnProperty("id")) (0, n.marshalPromiseToCallbacks)(this.removeParticipantImplAsync(e3), t3, r3);
          else {
            s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedParticipant.replace("{command}", "remove")), r3);
            let i3 = "";
            if ("string" == typeof e3) i3 = e3;
            else if (e3.hasOwnProperty("id")) {
              i3 = e3.id;
            } else if (e3.hasOwnProperty("userId")) {
              i3 = e3.userId;
            }
            s.Contracts.throwIfNullOrWhitespace(i3, this.privErrors.invalidArgs.replace("{arg}", "userId"));
            -1 === this.participants.findIndex((e4) => e4.id === i3) && this.handleError(new Error(this.privErrors.invalidParticipantRequest), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getEjectCommand(i3), () => {
              this.handleCallback(t3, r3);
            }, (e4) => {
              this.handleError(e4, r3);
            });
          }
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      unlockConversationAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "unlock")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getLockCommand(false), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      unmuteAllParticipantsAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "unmute all")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteAllCommand(false), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      unmuteParticipantAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "userId")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.me.isHost || this.me.id === e3 || this.handleError(new Error(this.privErrors.permissionDeniedParticipant.replace("{command}", "mute")), r3);
          -1 === this.privParticipants.getParticipantIndex(e3) && this.handleError(new Error(this.privErrors.invalidParticipantRequest), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteCommand(e3, false), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      sendTextMessageAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "message")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), e3.length > this.privTextMessageMaxLength && this.handleError(new Error(this.privErrors.invalidArgs.replace("{arg}", "message length")), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMessageCommand(e3), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      setTranslatedLanguagesAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfArrayEmptyOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "languages")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getSetTranslateToLanguagesCommand(e3), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      changeNicknameAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "nickname")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getChangeNicknameCommand(e3), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      dispose() {
        this.isDisposed || (this.privIsDisposed = true, this.config && this.config.close(), this.privConfig = void 0, this.privLanguage = void 0, this.privProperties = void 0, this.privRoom = void 0, this.privToken = void 0, this.privManager = void 0, this.privIsConnected = false, this.privIsReady = false, this.privParticipants = void 0);
      }
      async connectTranscriberRecognizer(e3) {
        this.privTranscriberRecognizer && await this.privTranscriberRecognizer.close(), await e3.enforceAudioGating(), this.privTranscriberRecognizer = e3, this.privTranscriberRecognizer.conversation = this;
      }
      getKeepAlive() {
        const e3 = this.me ? this.me.displayName : "default_nickname";
        return JSON.stringify({ id: "0", nickname: e3, participantId: this.privRoom.participantId, roomId: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.keepAlive });
      }
      addParticipantImplAsync(e3) {
        if (void 0 !== this.privParticipants.addOrUpdateParticipant(e3) && this.privTranscriberRecognizer) {
          const t3 = this.conversationInfo;
          return t3.participants = [e3], this.privTranscriberRecognizer.pushConversationEvent(t3, "join");
        }
      }
      removeParticipantImplAsync(e3) {
        this.privParticipants.deleteParticipant(e3.id);
        const t3 = this.conversationInfo;
        return t3.participants = [e3], this.privTranscriberRecognizer.pushConversationEvent(t3, "leave");
      }
      async close(e3) {
        try {
          this.privIsConnected = false, await this.privConversationRecognizer?.close(), this.privConversationRecognizer = void 0, this.privConversationTranslator && this.privConversationTranslator.dispose();
        } catch (e4) {
          throw e4;
        }
        e3 && this.dispose();
      }
      handleCallback(e3, t3) {
        if (e3) {
          try {
            e3();
          } catch (e4) {
            t3 && t3(e4);
          }
          e3 = void 0;
        }
      }
      handleError(e3, t3) {
        if (t3) if (e3 instanceof Error) {
          const r3 = e3;
          t3(r3.name + ": " + r3.message);
        } else t3(e3);
      }
      toParticipants(e3) {
        const t3 = this.privParticipants.participants.map((e4) => this.toParticipant(e4));
        return e3 ? t3 : t3.filter((e4) => false === e4.isHost);
      }
      toParticipant(e3) {
        return new o.Participant(e3.id, e3.avatar, e3.displayName, e3.isHost, e3.isMuted, e3.isUsingTts, e3.preferredLanguage, e3.voice);
      }
      getMuteAllCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setMuteAll, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getMuteCommand(e3, t3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(e3, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setMute, participantId: e3, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: t3 });
      }
      getLockCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setLockState, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getEjectCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(e3, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.ejectParticipant, participantId: e3, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand });
      }
      getSetTranslateToLanguagesCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setTranslateToLanguages, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getChangeNicknameCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(e3, "nickname"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.changeNickname, nickname: e3, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getMessageCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), s.Contracts.throwIfNullOrWhitespace(e3, "message"), JSON.stringify({ participantId: this.privRoom.participantId, roomId: this.privRoom.roomId, text: e3, type: i2.ConversationTranslatorMessageTypes.instantMessage });
      }
    }
    t2.ConversationImpl = c;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationCommon = void 0;
    t2.ConversationCommon = class {
      constructor(e3) {
        this.privAudioConfig = e3;
      }
      handleCallback(e3, t3) {
        if (e3) {
          try {
            e3();
          } catch (e4) {
            t3 && t3(e4);
          }
          e3 = void 0;
        }
      }
      handleError(e3, t3) {
        if (t3) if (e3 instanceof Error) {
          const r2 = e3;
          t3(r2.name + ": " + r2.message);
        } else t3(e3);
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationExpirationEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
      constructor(e3, t3) {
        super(t3), this.privExpirationTime = e3;
      }
      get expirationTime() {
        return this.privExpirationTime;
      }
    }
    t2.ConversationExpirationEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationParticipantsChangedEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
      constructor(e3, t3, r3) {
        super(r3), this.privReason = e3, this.privParticipant = t3;
      }
      get reason() {
        return this.privReason;
      }
      get participants() {
        return this.privParticipant;
      }
    }
    t2.ConversationParticipantsChangedEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslationCanceledEventArgs = void 0;
    const i2 = r2(99);
    class n extends i2.CancellationEventArgsBase {
    }
    t2.ConversationTranslationCanceledEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslationEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.RecognitionEventArgs {
      constructor(e3, t3, r3) {
        super(t3, r3), this.privResult = e3;
      }
      get result() {
        return this.privResult;
      }
    }
    t2.ConversationTranslationEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslationResult = void 0;
    const i2 = r2(102);
    class n extends i2.TranslationRecognitionResult {
      constructor(e3, t3, r3, i3, n2, s, o, a, c, p, h) {
        super(t3, i3, n2, s, o, a, void 0, void 0, c, p, h), this.privId = e3, this.privOrigLang = r3;
      }
      get participantId() {
        return this.privId;
      }
      get originalLang() {
        return this.privOrigLang;
      }
    }
    t2.ConversationTranslationResult = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslator = t2.SpeechState = void 0;
    const i2 = r2(2), n = r2(152), s = r2(4), o = r2(65), a = r2(80), c = r2(144), p = r2(143);
    var h;
    !function(e3) {
      e3[e3.Inactive = 0] = "Inactive", e3[e3.Connecting = 1] = "Connecting", e3[e3.Connected = 2] = "Connected";
    }(h = t2.SpeechState || (t2.SpeechState = {}));
    class u extends a.TranslationRecognizer {
      constructor(e3, t3, r3, i3) {
        super(e3, t3, new n.ConversationTranslatorConnectionFactory(i3)), this.privSpeechState = h.Inactive, r3 && (this.privTranslator = r3, this.sessionStarted = () => {
          this.privSpeechState = h.Connected;
        }, this.sessionStopped = () => {
          this.privSpeechState = h.Inactive;
        }, this.recognizing = (e4, t4) => {
          this.privTranslator.recognizing && this.privTranslator.recognizing(this.privTranslator, t4);
        }, this.recognized = async (e4, t4) => {
          t4.result?.errorDetails ? (await this.cancelSpeech(), this.fireCancelEvent(t4.result.errorDetails)) : this.privTranslator.recognized && this.privTranslator.recognized(this.privTranslator, t4);
        }, this.canceled = async () => {
          if (this.privSpeechState !== h.Inactive) try {
            await this.cancelSpeech();
          } catch (e4) {
            this.privSpeechState = h.Inactive;
          }
        });
      }
      get state() {
        return this.privSpeechState;
      }
      set state(e3) {
        this.privSpeechState = e3;
      }
      set authentication(e3) {
        this.privReco.authentication = e3;
      }
      onConnection() {
        this.privSpeechState = h.Connected;
      }
      async onCancelSpeech() {
        this.privSpeechState = h.Inactive, await this.cancelSpeech();
      }
      fireCancelEvent(e3) {
        try {
          if (this.privTranslator.canceled) {
            const t3 = new p.ConversationTranslationCanceledEventArgs(a.CancellationReason.Error, e3, a.CancellationErrorCode.RuntimeError);
            this.privTranslator.canceled(this.privTranslator, t3);
          }
        } catch (e4) {
        }
      }
      async cancelSpeech() {
        try {
          this.stopContinuousRecognitionAsync(), await this.privReco?.disconnect(), this.privSpeechState = h.Inactive;
        } catch (e3) {
        }
      }
    }
    class d extends p.ConversationCommon {
      constructor(e3) {
        super(e3), this.privErrors = i2.ConversationConnectionConfig.restErrors, this.privIsDisposed = false, this.privIsSpeaking = false, this.privPlaceholderKey = "abcdefghijklmnopqrstuvwxyz012345", this.privPlaceholderRegion = "westus", this.privProperties = new a.PropertyCollection();
      }
      get properties() {
        return this.privProperties;
      }
      get speechRecognitionLanguage() {
        return this.privSpeechRecognitionLanguage;
      }
      get participants() {
        return this.privConversation?.participants;
      }
      get canSpeak() {
        return !(!this.privConversation.isConnected || !this.privCTRecognizer) && (!this.privIsSpeaking && this.privCTRecognizer.state !== h.Connected && this.privCTRecognizer.state !== h.Connecting && !this.privConversation.isMutedByHost);
      }
      onToken(e3) {
        this.privCTRecognizer.authentication = e3;
      }
      setServiceProperty(e3, t3) {
        const r3 = JSON.parse(this.privProperties.getProperty(i2.ServicePropertiesPropertyName, "{}"));
        r3[e3] = t3, this.privProperties.setProperty(i2.ServicePropertiesPropertyName, JSON.stringify(r3));
      }
      joinConversationAsync(e3, t3, r3, n2, s2) {
        try {
          if ("string" == typeof e3) {
            o.Contracts.throwIfNullOrUndefined(e3, this.privErrors.invalidArgs.replace("{arg}", "conversation id")), o.Contracts.throwIfNullOrWhitespace(t3, this.privErrors.invalidArgs.replace("{arg}", "nickname")), this.privConversation && this.handleError(new Error(this.privErrors.permissionDeniedStart), s2);
            let p2 = r3;
            null != p2 && "" !== p2 || (p2 = i2.ConversationConnectionConfig.defaultLanguageCode), this.privSpeechTranslationConfig = a.SpeechTranslationConfig.fromSubscription(this.privPlaceholderKey, this.privPlaceholderRegion), this.privSpeechTranslationConfig.setProfanity(a.ProfanityOption.Masked), this.privSpeechTranslationConfig.addTargetLanguage(p2), this.privSpeechTranslationConfig.setProperty(a.PropertyId[a.PropertyId.SpeechServiceConnection_RecoLanguage], p2), this.privSpeechTranslationConfig.setProperty(a.PropertyId[a.PropertyId.ConversationTranslator_Name], t3);
            const h2 = [a.PropertyId.SpeechServiceConnection_Host, a.PropertyId.ConversationTranslator_Host, a.PropertyId.SpeechServiceConnection_Endpoint, a.PropertyId.SpeechServiceConnection_ProxyHostName, a.PropertyId.SpeechServiceConnection_ProxyPassword, a.PropertyId.SpeechServiceConnection_ProxyPort, a.PropertyId.SpeechServiceConnection_ProxyUserName, "ConversationTranslator_MultiChannelAudio", "ConversationTranslator_Region"];
            for (const e4 of h2) {
              const t4 = this.privProperties.getProperty(e4);
              if (t4) {
                const r4 = "string" == typeof e4 ? e4 : a.PropertyId[e4];
                this.privSpeechTranslationConfig.setProperty(r4, t4);
              }
            }
            const u2 = JSON.parse(this.privProperties.getProperty(i2.ServicePropertiesPropertyName, "{}"));
            for (const e4 of Object.keys(u2)) this.privSpeechTranslationConfig.setServiceProperty(e4, u2[e4], a.ServicePropertyChannel.UriQueryParameter);
            this.privConversation = new c.ConversationImpl(this.privSpeechTranslationConfig), this.privConversation.conversationTranslator = this, this.privConversation.joinConversationAsync(e3, t3, p2, (e4) => {
              e4 || this.handleError(new Error(this.privErrors.permissionDeniedConnect), s2), this.privSpeechTranslationConfig.authorizationToken = e4, this.privConversation.room.isHost = false, this.privConversation.startConversationAsync(() => {
                this.handleCallback(n2, s2);
              }, (e5) => {
                this.handleError(e5, s2);
              });
            }, (e4) => {
              this.handleError(e4, s2);
            });
          } else "object" == typeof e3 ? (o.Contracts.throwIfNullOrUndefined(e3, this.privErrors.invalidArgs.replace("{arg}", "conversation id")), o.Contracts.throwIfNullOrWhitespace(t3, this.privErrors.invalidArgs.replace("{arg}", "nickname")), this.privProperties.setProperty(a.PropertyId.ConversationTranslator_Name, t3), this.privConversation = e3, this.privConversation.conversationTranslator = this, this.privConversation.room.isHost = true, o.Contracts.throwIfNullOrUndefined(this.privConversation, this.privErrors.permissionDeniedConnect), o.Contracts.throwIfNullOrUndefined(this.privConversation.room.token, this.privErrors.permissionDeniedConnect), this.privSpeechTranslationConfig = e3.config, this.handleCallback(r3, n2)) : this.handleError(new Error(this.privErrors.invalidArgs.replace("{arg}", "invalid conversation type")), n2);
        } catch (e4) {
          this.handleError(e4, "string" == typeof r3 ? s2 : n2);
        }
      }
      leaveConversationAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)((async () => {
          await this.cancelSpeech(), await this.privConversation.endConversationImplAsync(), await this.privConversation.deleteConversationImplAsync(), this.dispose();
        })(), e3, t3);
      }
      sendTextMessageAsync(e3, t3, r3) {
        try {
          o.Contracts.throwIfNullOrUndefined(this.privConversation, this.privErrors.permissionDeniedSend), o.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", e3)), this.privConversation.sendTextMessageAsync(e3, t3, r3);
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      startTranscribingAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)((async () => {
          try {
            o.Contracts.throwIfNullOrUndefined(this.privConversation, this.privErrors.permissionDeniedSend), o.Contracts.throwIfNullOrUndefined(this.privConversation.room.token, this.privErrors.permissionDeniedConnect), void 0 === this.privCTRecognizer && await this.connectTranslatorRecognizer(), o.Contracts.throwIfNullOrUndefined(this.privCTRecognizer, this.privErrors.permissionDeniedSend), this.canSpeak || this.handleError(new Error(this.privErrors.permissionDeniedSend), t3), await this.startContinuousRecognition(), this.privIsSpeaking = true;
          } catch (e4) {
            throw this.privIsSpeaking = false, await this.cancelSpeech(), e4;
          }
        })(), e3, t3);
      }
      stopTranscribingAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)((async () => {
          try {
            if (!this.privIsSpeaking) return void await this.cancelSpeech();
            this.privIsSpeaking = false, await new Promise((e4, t4) => {
              this.privCTRecognizer.stopContinuousRecognitionAsync(e4, t4);
            });
          } catch (e4) {
            await this.cancelSpeech();
          }
        })(), e3, t3);
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      dispose(e3, t3, r3) {
        (0, s.marshalPromiseToCallbacks)((async () => {
          this.isDisposed && !this.privIsSpeaking || (await this.cancelSpeech(), this.privIsDisposed = true, this.privSpeechTranslationConfig.close(), this.privSpeechRecognitionLanguage = void 0, this.privProperties = void 0, this.privAudioConfig = void 0, this.privSpeechTranslationConfig = void 0, this.privConversation.dispose(), this.privConversation = void 0);
        })(), t3, r3);
      }
      async cancelSpeech() {
        try {
          this.privIsSpeaking = false, await this.privCTRecognizer?.onCancelSpeech(), this.privCTRecognizer = void 0;
        } catch (e3) {
        }
      }
      async connectTranslatorRecognizer() {
        try {
          void 0 === this.privAudioConfig && (this.privAudioConfig = a.AudioConfig.fromDefaultMicrophoneInput()), this.privSpeechTranslationConfig.getProperty(a.PropertyId[a.PropertyId.SpeechServiceConnection_Key]) === this.privPlaceholderKey && this.privSpeechTranslationConfig.setProperty(a.PropertyId[a.PropertyId.SpeechServiceConnection_Key], "");
          const e3 = () => this.privConversation;
          this.privCTRecognizer = new u(this.privSpeechTranslationConfig, this.privAudioConfig, this, e3);
        } catch (e3) {
          throw await this.cancelSpeech(), e3;
        }
      }
      startContinuousRecognition() {
        return new Promise((e3, t3) => {
          this.privCTRecognizer.startContinuousRecognitionAsync(e3, t3);
        });
      }
    }
    t2.ConversationTranslator = d;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslatorConnectionFactory = void 0;
    const i2 = r2(61), n = r2(153), s = r2(65), o = r2(80), a = r2(54), c = r2(131), p = r2(130), h = r2(2);
    class u extends p.ConnectionFactoryBase {
      constructor(e3) {
        super(), s.Contracts.throwIfNullOrUndefined(e3, "convGetter"), this.privConvGetter = e3;
      }
      create(e3, t3, r3) {
        const s2 = "TRUE" === e3.parameters.getProperty("ConversationTranslator_MultiChannelAudio", "").toUpperCase(), d = this.privConvGetter().room, v = d.cognitiveSpeechRegion || e3.parameters.getProperty(o.PropertyId.SpeechServiceConnection_Region, ""), l = { hostSuffix: p.ConnectionFactoryBase.getHostSuffix(v), path: u.CTS_VIRT_MIC_PATH, region: encodeURIComponent(v) };
        l[c.QueryParameterNames.Language] = encodeURIComponent(e3.parameters.getProperty(o.PropertyId.SpeechServiceConnection_RecoLanguage, "")), l[c.QueryParameterNames.CtsMeetingId] = encodeURIComponent(d.roomId), l[c.QueryParameterNames.CtsDeviceId] = encodeURIComponent(d.participantId), l[c.QueryParameterNames.CtsIsParticipant] = d.isHost ? "" : "&" + c.QueryParameterNames.CtsIsParticipant;
        let g = "";
        const m = {}, S = {};
        if (s2) {
          if (g = e3.parameters.getProperty(o.PropertyId.SpeechServiceConnection_Endpoint), !g) {
            g = "wss://" + e3.parameters.getProperty(o.PropertyId.SpeechServiceConnection_Host, "transcribe.{region}.cts.speech{hostSuffix}") + "{path}";
          }
          g = n.StringUtils.formatString(g, l);
          const t4 = new URL(g);
          t4.searchParams.forEach((e4, t5) => {
            m[t5] = e4;
          });
          new h.TranscriberConnectionFactory().setQueryParams(m, e3, g), m[c.QueryParameterNames.CtsMeetingId] = l[c.QueryParameterNames.CtsMeetingId], m[c.QueryParameterNames.CtsDeviceId] = l[c.QueryParameterNames.CtsDeviceId], d.isHost || (m[c.QueryParameterNames.CtsIsParticipant] = ""), c.QueryParameterNames.Format in m || (m[c.QueryParameterNames.Format] = "simple"), t4.searchParams.forEach((e4, r4) => {
            t4.searchParams.set(r4, m[r4]), delete m[r4];
          }), g = t4.toString();
        } else {
          const t4 = new h.TranslationConnectionFactory();
          g = t4.getEndpointUrl(e3, true), g = n.StringUtils.formatString(g, l), t4.setQueryParams(m, e3, g);
        }
        S[a.HeaderNames.ConnectionId] = r3, S[i2.RestConfigBase.configParams.token] = d.token, t3.token && (S[t3.headerName] = t3.token);
        const f = "TRUE" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "").toUpperCase();
        return Promise.resolve(new i2.WebsocketConnection(g, m, S, new h.WebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), f, r3));
      }
    }
    t2.ConversationTranslatorConnectionFactory = u, u.CTS_VIRT_MIC_PATH = "/speech/recognition/dynamicaudio";
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.StringUtils = void 0;
    t2.StringUtils = class {
      static formatString(e3, t3) {
        if (!e3) return "";
        if (!t3) return e3;
        let r2 = "", i2 = "";
        const n = (e4) => {
          r2 += e4;
        }, s = (e4) => {
          i2 += e4;
        };
        let o = n;
        for (let a = 0; a < e3.length; a++) {
          const c = e3[a], p = a + 1 < e3.length ? e3[a + 1] : "";
          switch (c) {
            case "{":
              "{" === p ? (o("{"), a++) : o = s;
              break;
            case "}":
              "}" === p ? (o("}"), a++) : (t3.hasOwnProperty(i2) && (r2 += t3[i2]), o = n, i2 = "");
              break;
            default:
              o(c);
          }
        }
        return r2;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranscriber = void 0;
    const i2 = r2(2), n = r2(111), s = r2(4), o = r2(65), a = r2(80);
    class c extends a.Recognizer {
      constructor(e3, t3) {
        const r3 = e3;
        o.Contracts.throwIfNull(r3, "speechConfig"), o.Contracts.throwIfNullOrWhitespace(r3.properties.getProperty(a.PropertyId.SpeechServiceConnection_RecoLanguage), a.PropertyId[a.PropertyId.SpeechServiceConnection_RecoLanguage]), super(t3, r3.properties, new i2.ConversationTranscriberConnectionFactory(), e3.tokenCredential), this.privProperties.setProperty(a.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, "2"), this.privDisposedRecognizer = false;
      }
      static FromConfig(e3, t3, r3) {
        const i3 = e3;
        t3.properties.mergeTo(i3.properties);
        return new c(e3, r3);
      }
      get endpointId() {
        return o.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(a.PropertyId.SpeechServiceConnection_EndpointId, "00000000-0000-0000-0000-000000000000");
      }
      get authorizationToken() {
        return this.properties.getProperty(a.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        o.Contracts.throwIfNullOrWhitespace(e3, "token"), this.properties.setProperty(a.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      get speechRecognitionLanguage() {
        return o.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(a.PropertyId.SpeechServiceConnection_RecoLanguage);
      }
      get outputFormat() {
        return o.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(i2.OutputFormatPropertyName, a.OutputFormat[a.OutputFormat.Simple]) === a.OutputFormat[a.OutputFormat.Simple] ? a.OutputFormat.Simple : a.OutputFormat.Detailed;
      }
      get properties() {
        return this.privProperties;
      }
      startTranscribingAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.startContinuousRecognitionAsyncImpl(n.RecognitionMode.Conversation), e3, t3);
      }
      stopTranscribingAsync(e3, t3) {
        (0, s.marshalPromiseToCallbacks)(this.stopContinuousRecognitionAsyncImpl(), e3, t3);
      }
      close(e3, t3) {
        o.Contracts.throwIfDisposed(this.privDisposedRecognizer), (0, s.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      async dispose(e3) {
        this.privDisposedRecognizer || (e3 && (this.privDisposedRecognizer = true, await this.implRecognizerStop()), await super.dispose(e3));
      }
      createRecognizerConfig(e3) {
        return new i2.RecognizerConfig(e3, this.privProperties);
      }
      createServiceRecognizer(e3, t3, r3, n2) {
        const s2 = r3;
        return n2.isSpeakerDiarizationEnabled = true, new i2.ConversationTranscriptionServiceRecognizer(e3, t3, s2, n2, this);
      }
    }
    t2.ConversationTranscriber = c;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Participant = t2.User = void 0;
    const i2 = r2(80);
    t2.User = class {
      constructor(e3) {
        this.privUserId = e3;
      }
      get userId() {
        return this.privUserId;
      }
    };
    class n {
      constructor(e3, t3, r3, n2, s, o, a, c) {
        this.privId = e3, this.privAvatar = t3, this.privDisplayName = r3, this.privIsHost = n2, this.privIsMuted = s, this.privIsUsingTts = o, this.privPreferredLanguage = a, this.privVoice = c, this.privProperties = new i2.PropertyCollection();
      }
      get avatar() {
        return this.privAvatar;
      }
      get displayName() {
        return this.privDisplayName;
      }
      get id() {
        return this.privId;
      }
      get preferredLanguage() {
        return this.privPreferredLanguage;
      }
      get isHost() {
        return this.privIsHost;
      }
      get isMuted() {
        return this.privIsMuted;
      }
      get isUsingTts() {
        return this.privIsUsingTts;
      }
      get voice() {
        return this.privVoice;
      }
      get properties() {
        return this.privProperties;
      }
      static From(e3, t3, r3) {
        return new n(e3, "", e3, false, false, false, t3, r3);
      }
    }
    t2.Participant = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ParticipantChangedReason = void 0, function(e3) {
      e3[e3.JoinedConversation = 0] = "JoinedConversation", e3[e3.LeftConversation = 1] = "LeftConversation", e3[e3.Updated = 2] = "Updated";
    }(t2.ParticipantChangedReason || (t2.ParticipantChangedReason = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MeetingImpl = t2.Meeting = void 0;
    const i2 = r2(2), n = r2(4), s = r2(65), o = r2(80);
    class a {
      constructor() {
      }
      static createMeetingAsync(e3, t3, r3, a2) {
        if (s.Contracts.throwIfNullOrUndefined(e3, i2.ConversationConnectionConfig.restErrors.invalidArgs.replace("{arg}", "config")), s.Contracts.throwIfNullOrUndefined(e3.region, i2.ConversationConnectionConfig.restErrors.invalidArgs.replace("{arg}", "SpeechServiceConnection_Region")), s.Contracts.throwIfNull(t3, "meetingId"), 0 === t3.length) throw new Error("meetingId cannot be empty");
        e3.subscriptionKey || e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceAuthorization_Token]) || s.Contracts.throwIfNullOrUndefined(e3.subscriptionKey, i2.ConversationConnectionConfig.restErrors.invalidArgs.replace("{arg}", "SpeechServiceConnection_Key"));
        const p = new c(e3, t3);
        return (0, n.marshalPromiseToCallbacks)((async () => {
        })(), r3, a2), p;
      }
    }
    t2.Meeting = a;
    class c extends a {
      constructor(e3, t3) {
        super(), this.privErrors = i2.ConversationConnectionConfig.restErrors, this.onConnected = (e4) => {
          this.privIsConnected = true;
          try {
            this.privConversationTranslator?.sessionStarted && this.privConversationTranslator.sessionStarted(this.privConversationTranslator, e4);
          } catch (e5) {
          }
        }, this.onDisconnected = (e4) => {
          try {
            this.privConversationTranslator?.sessionStopped && this.privConversationTranslator.sessionStopped(this.privConversationTranslator, e4);
          } catch (e5) {
          } finally {
            this.close(false);
          }
        }, this.onCanceled = (e4, t4) => {
          try {
            this.privConversationTranslator?.canceled && this.privConversationTranslator.canceled(this.privConversationTranslator, t4);
          } catch (t5) {
          }
        }, this.onParticipantUpdateCommandReceived = (e4, t4) => {
          try {
            const e5 = this.privParticipants.getParticipant(t4.id);
            if (void 0 !== e5) {
              switch (t4.key) {
                case i2.ConversationTranslatorCommandTypes.changeNickname:
                  e5.displayName = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setUseTTS:
                  e5.isUsingTts = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setProfanityFiltering:
                  e5.profanity = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setMute:
                  e5.isMuted = t4.value;
                  break;
                case i2.ConversationTranslatorCommandTypes.setTranslateToLanguages:
                  e5.translateToLanguages = t4.value;
              }
              this.privParticipants.addOrUpdateParticipant(e5), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.Updated, [this.toParticipant(e5)], t4.sessionId));
            }
          } catch (t5) {
          }
        }, this.onLockRoomCommandReceived = () => {
        }, this.onMuteAllCommandReceived = (e4, t4) => {
          try {
            this.privParticipants.participants.forEach((e5) => e5.isMuted = !e5.isHost && t4.isMuted), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.Updated, this.toParticipants(false), t4.sessionId));
          } catch (t5) {
          }
        }, this.onParticipantJoinCommandReceived = (e4, t4) => {
          try {
            const e5 = this.privParticipants.addOrUpdateParticipant(t4.participant);
            void 0 !== e5 && this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.JoinedConversation, [this.toParticipant(e5)], t4.sessionId));
          } catch (t5) {
          }
        }, this.onParticipantLeaveCommandReceived = (e4, t4) => {
          try {
            const e5 = this.privParticipants.getParticipant(t4.participant.id);
            void 0 !== e5 && (this.privParticipants.deleteParticipant(t4.participant.id), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.LeftConversation, [this.toParticipant(e5)], t4.sessionId)));
          } catch (t5) {
          }
        }, this.onTranslationReceived = (e4, t4) => {
          try {
            switch (t4.command) {
              case i2.ConversationTranslatorMessageTypes.final:
                this.privConversationTranslator && this.privConversationTranslator.transcribed(this.privConversationTranslator, new o.ConversationTranslationEventArgs(t4.payload, void 0, t4.sessionId));
                break;
              case i2.ConversationTranslatorMessageTypes.partial:
                this.privConversationTranslator && this.privConversationTranslator.transcribing(this.privConversationTranslator, new o.ConversationTranslationEventArgs(t4.payload, void 0, t4.sessionId));
                break;
              case i2.ConversationTranslatorMessageTypes.instantMessage:
                this.privConversationTranslator && this.privConversationTranslator.textMessageReceived(this.privConversationTranslator, new o.ConversationTranslationEventArgs(t4.payload, void 0, t4.sessionId));
            }
          } catch (t5) {
          }
        }, this.onParticipantsListReceived = (e4, t4) => {
          try {
            if (void 0 !== t4.sessionToken && null !== t4.sessionToken && (this.privRoom.token = t4.sessionToken), this.privParticipants.participants = [...t4.participants], void 0 !== this.privParticipants.me && (this.privIsReady = true), this.privConversationTranslator && this.privConversationTranslator.participantsChanged(this.privConversationTranslator, new o.ConversationParticipantsChangedEventArgs(o.ParticipantChangedReason.JoinedConversation, this.toParticipants(true), t4.sessionId)), this.me.isHost) {
              const e5 = this.privConversationTranslator?.properties.getProperty(o.PropertyId.ConversationTranslator_Name);
              void 0 !== e5 && e5.length > 0 && e5 !== this.me.displayName && this.changeNicknameAsync(e5);
            }
          } catch (t5) {
          }
        }, this.onConversationExpiration = (e4, t4) => {
          try {
            this.privConversationTranslator && this.privConversationTranslator.conversationExpiration(this.privConversationTranslator, t4);
          } catch (t5) {
          }
        }, this.privIsConnected = false, this.privIsDisposed = false, this.privConversationId = "", this.privProperties = new o.PropertyCollection(), this.privManager = new i2.ConversationManager();
        e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceConnection_RecoLanguage]) || e3.setProperty(o.PropertyId[o.PropertyId.SpeechServiceConnection_RecoLanguage], i2.ConversationConnectionConfig.defaultLanguageCode), this.privLanguage = e3.getProperty(o.PropertyId[o.PropertyId.SpeechServiceConnection_RecoLanguage]), this.privConversationId = t3, this.privConfig = e3;
        const r3 = e3;
        s.Contracts.throwIfNull(r3, "speechConfig"), this.privProperties = r3.properties.clone(), this.privIsConnected = false, this.privParticipants = new i2.InternalParticipants(), this.privIsReady = false, this.privTextMessageMaxLength = 1e3;
      }
      get room() {
        return this.privRoom;
      }
      get connection() {
        return this.privConversationRecognizer;
      }
      get config() {
        return this.privConfig;
      }
      get meetingId() {
        return this.privRoom ? this.privRoom.roomId : this.privConversationId;
      }
      get properties() {
        return this.privProperties;
      }
      get speechRecognitionLanguage() {
        return this.privLanguage;
      }
      get isMutedByHost() {
        return !this.privParticipants.me?.isHost && this.privParticipants.me?.isMuted;
      }
      get isConnected() {
        return this.privIsConnected && this.privIsReady;
      }
      get participants() {
        return this.toParticipants(true);
      }
      get me() {
        return this.toParticipant(this.privParticipants.me);
      }
      get host() {
        return this.toParticipant(this.privParticipants.host);
      }
      get transcriberRecognizer() {
        return this.privTranscriberRecognizer;
      }
      get meetingInfo() {
        const e3 = this.meetingId, t3 = this.participants.map((e4) => ({ id: e4.id, preferredLanguage: e4.preferredLanguage, voice: e4.voice })), r3 = {};
        for (const e4 of i2.ConversationConnectionConfig.transcriptionEventKeys) {
          const t4 = this.properties.getProperty(e4, "");
          "" !== t4 && (r3[e4] = t4);
        }
        return { id: e3, participants: t3, meetingProperties: r3 };
      }
      get canSend() {
        return this.privIsConnected && !this.privParticipants.me?.isMuted;
      }
      get canSendAsHost() {
        return this.privIsConnected && this.privParticipants.me?.isHost;
      }
      get authorizationToken() {
        return this.privToken;
      }
      set authorizationToken(e3) {
        s.Contracts.throwIfNullOrWhitespace(e3, "authorizationToken"), this.privToken = e3;
      }
      createMeetingAsync(e3, t3) {
        try {
          this.privConversationRecognizer && this.handleError(new Error(this.privErrors.permissionDeniedStart), t3), this.privManager.createOrJoin(this.privProperties, void 0, (r3) => {
            r3 || this.handleError(new Error(this.privErrors.permissionDeniedConnect), t3), this.privRoom = r3, this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      startMeetingAsync(e3, t3) {
        try {
          this.privConversationRecognizer && this.handleError(new Error(this.privErrors.permissionDeniedStart), t3), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedConnect), this.privParticipants.meId = this.privRoom.participantId, this.privConversationRecognizer.connected = this.onConnected, this.privConversationRecognizer.disconnected = this.onDisconnected, this.privConversationRecognizer.canceled = this.onCanceled, this.privConversationRecognizer.participantUpdateCommandReceived = this.onParticipantUpdateCommandReceived, this.privConversationRecognizer.lockRoomCommandReceived = this.onLockRoomCommandReceived, this.privConversationRecognizer.muteAllCommandReceived = this.onMuteAllCommandReceived, this.privConversationRecognizer.participantJoinCommandReceived = this.onParticipantJoinCommandReceived, this.privConversationRecognizer.participantLeaveCommandReceived = this.onParticipantLeaveCommandReceived, this.privConversationRecognizer.translationReceived = this.onTranslationReceived, this.privConversationRecognizer.participantsListReceived = this.onParticipantsListReceived, this.privConversationRecognizer.conversationExpiration = this.onConversationExpiration, this.privConversationRecognizer.connect(this.privRoom.token, () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      addParticipantAsync(e3, t3, r3) {
        s.Contracts.throwIfNullOrUndefined(e3, "Participant"), (0, n.marshalPromiseToCallbacks)(this.addParticipantImplAsync(e3), t3, r3);
      }
      joinMeetingAsync(e3, t3, r3, i3, n2) {
        try {
          s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "conversationId")), s.Contracts.throwIfNullOrWhitespace(t3, this.privErrors.invalidArgs.replace("{arg}", "nickname")), s.Contracts.throwIfNullOrWhitespace(r3, this.privErrors.invalidArgs.replace("{arg}", "language")), this.privManager.createOrJoin(this.privProperties, e3, (e4) => {
            s.Contracts.throwIfNullOrUndefined(e4, this.privErrors.permissionDeniedConnect), this.privRoom = e4, this.privConfig.authorizationToken = e4.cognitiveSpeechAuthToken, i3 && i3(e4.cognitiveSpeechAuthToken);
          }, (e4) => {
            this.handleError(e4, n2);
          });
        } catch (e4) {
          this.handleError(e4, n2);
        }
      }
      deleteMeetingAsync(e3, t3) {
        (0, n.marshalPromiseToCallbacks)(this.deleteMeetingImplAsync(), e3, t3);
      }
      async deleteMeetingImplAsync() {
        s.Contracts.throwIfNullOrUndefined(this.privProperties, this.privErrors.permissionDeniedConnect), s.Contracts.throwIfNullOrWhitespace(this.privRoom.token, this.privErrors.permissionDeniedConnect), await this.privManager.leave(this.privProperties, this.privRoom.token), this.dispose();
      }
      endMeetingAsync(e3, t3) {
        (0, n.marshalPromiseToCallbacks)(this.endMeetingImplAsync(), e3, t3);
      }
      endMeetingImplAsync() {
        return this.close(true);
      }
      lockMeetingAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "lock")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getLockCommand(true), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      muteAllParticipantsAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privConversationRecognizer, this.privErrors.permissionDeniedSend), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "mute")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteAllCommand(true), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      muteParticipantAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "userId")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.me.isHost || this.me.id === e3 || this.handleError(new Error(this.privErrors.permissionDeniedParticipant.replace("{command}", "mute")), r3);
          -1 === this.privParticipants.getParticipantIndex(e3) && this.handleError(new Error(this.privErrors.invalidParticipantRequest), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteCommand(e3, true), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      removeParticipantAsync(e3, t3, r3) {
        try {
          if (s.Contracts.throwIfDisposed(this.privIsDisposed), this.privTranscriberRecognizer && e3.hasOwnProperty("id")) (0, n.marshalPromiseToCallbacks)(this.removeParticipantImplAsync(e3), t3, r3);
          else {
            s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedParticipant.replace("{command}", "remove")), r3);
            let i3 = "";
            if ("string" == typeof e3) i3 = e3;
            else if (e3.hasOwnProperty("id")) {
              i3 = e3.id;
            } else if (e3.hasOwnProperty("userId")) {
              i3 = e3.userId;
            }
            s.Contracts.throwIfNullOrWhitespace(i3, this.privErrors.invalidArgs.replace("{arg}", "userId"));
            -1 === this.participants.findIndex((e4) => e4.id === i3) && this.handleError(new Error(this.privErrors.invalidParticipantRequest), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getEjectCommand(i3), () => {
              this.handleCallback(t3, r3);
            }, (e4) => {
              this.handleError(e4, r3);
            });
          }
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      unlockMeetingAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "unlock")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getLockCommand(false), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      unmuteAllParticipantsAsync(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSendAsHost || this.handleError(new Error(this.privErrors.permissionDeniedConversation.replace("{command}", "unmute all")), t3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteAllCommand(false), () => {
            this.handleCallback(e3, t3);
          }, (e4) => {
            this.handleError(e4, t3);
          });
        } catch (e4) {
          this.handleError(e4, t3);
        }
      }
      unmuteParticipantAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "userId")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.me.isHost || this.me.id === e3 || this.handleError(new Error(this.privErrors.permissionDeniedParticipant.replace("{command}", "mute")), r3);
          -1 === this.privParticipants.getParticipantIndex(e3) && this.handleError(new Error(this.privErrors.invalidParticipantRequest), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMuteCommand(e3, false), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      sendTextMessageAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "message")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), e3.length > this.privTextMessageMaxLength && this.handleError(new Error(this.privErrors.invalidArgs.replace("{arg}", "message length")), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getMessageCommand(e3), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      setTranslatedLanguagesAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfArrayEmptyOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "languages")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getSetTranslateToLanguagesCommand(e3), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      changeNicknameAsync(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfDisposed(this.privConversationRecognizer.isDisposed()), s.Contracts.throwIfNullOrWhitespace(e3, this.privErrors.invalidArgs.replace("{arg}", "nickname")), s.Contracts.throwIfNullOrUndefined(this.privRoom, this.privErrors.permissionDeniedSend), this.canSend || this.handleError(new Error(this.privErrors.permissionDeniedSend), r3), this.privConversationRecognizer && this.privConversationRecognizer.sendRequest(this.getChangeNicknameCommand(e3), () => {
            this.handleCallback(t3, r3);
          }, (e4) => {
            this.handleError(e4, r3);
          });
        } catch (e4) {
          this.handleError(e4, r3);
        }
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      dispose() {
        this.isDisposed || (this.privIsDisposed = true, this.config && this.config.close(), this.privConfig = void 0, this.privLanguage = void 0, this.privProperties = void 0, this.privRoom = void 0, this.privToken = void 0, this.privManager = void 0, this.privIsConnected = false, this.privIsReady = false, this.privParticipants = void 0);
      }
      async connectTranscriberRecognizer(e3) {
        this.privTranscriberRecognizer && await this.privTranscriberRecognizer.close(), await e3.enforceAudioGating(), this.privTranscriberRecognizer = e3, this.privTranscriberRecognizer.meeting = this;
      }
      getKeepAlive() {
        const e3 = this.me ? this.me.displayName : "default_nickname";
        return JSON.stringify({ id: "0", nickname: e3, participantId: this.privRoom.participantId, roomId: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.keepAlive });
      }
      addParticipantImplAsync(e3) {
        if (void 0 !== this.privParticipants.addOrUpdateParticipant(e3) && this.privTranscriberRecognizer) {
          const t3 = this.meetingInfo;
          return t3.participants = [e3], this.privTranscriberRecognizer.pushMeetingEvent(t3, "join");
        }
      }
      removeParticipantImplAsync(e3) {
        this.privParticipants.deleteParticipant(e3.id);
        const t3 = this.meetingInfo;
        return t3.participants = [e3], this.privTranscriberRecognizer.pushMeetingEvent(t3, "leave");
      }
      async close(e3) {
        try {
          this.privIsConnected = false, await this.privConversationRecognizer?.close(), this.privConversationRecognizer = void 0, this.privConversationTranslator && this.privConversationTranslator.dispose();
        } catch (e4) {
          throw e4;
        }
        e3 && this.dispose();
      }
      handleCallback(e3, t3) {
        if (e3) {
          try {
            e3();
          } catch (e4) {
            t3 && t3(e4);
          }
          e3 = void 0;
        }
      }
      handleError(e3, t3) {
        if (t3) if (e3 instanceof Error) {
          const r3 = e3;
          t3(r3.name + ": " + r3.message);
        } else t3(e3);
      }
      toParticipants(e3) {
        const t3 = this.privParticipants.participants.map((e4) => this.toParticipant(e4));
        return e3 ? t3 : t3.filter((e4) => false === e4.isHost);
      }
      toParticipant(e3) {
        return new o.Participant(e3.id, e3.avatar, e3.displayName, e3.isHost, e3.isMuted, e3.isUsingTts, e3.preferredLanguage, e3.voice);
      }
      getMuteAllCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "meetingd"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setMuteAll, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getMuteCommand(e3, t3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "conversationId"), s.Contracts.throwIfNullOrWhitespace(e3, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setMute, participantId: e3, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: t3 });
      }
      getLockCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "meetingId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setLockState, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getEjectCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "meetingId"), s.Contracts.throwIfNullOrWhitespace(e3, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.ejectParticipant, participantId: e3, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand });
      }
      getSetTranslateToLanguagesCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "meetingId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.setTranslateToLanguages, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getChangeNicknameCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "meetingId"), s.Contracts.throwIfNullOrWhitespace(e3, "nickname"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), JSON.stringify({ command: i2.ConversationTranslatorCommandTypes.changeNickname, nickname: e3, participantId: this.privRoom.participantId, roomid: this.privRoom.roomId, type: i2.ConversationTranslatorMessageTypes.participantCommand, value: e3 });
      }
      getMessageCommand(e3) {
        return s.Contracts.throwIfNullOrWhitespace(this.privRoom.roomId, "meetingId"), s.Contracts.throwIfNullOrWhitespace(this.privRoom.participantId, "participantId"), s.Contracts.throwIfNullOrWhitespace(e3, "message"), JSON.stringify({ participantId: this.privRoom.participantId, roomId: this.privRoom.roomId, text: e3, type: i2.ConversationTranslatorMessageTypes.instantMessage });
      }
    }
    t2.MeetingImpl = c;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MeetingTranscriptionCanceledEventArgs = void 0;
    const i2 = r2(99);
    class n extends i2.CancellationEventArgsBase {
    }
    t2.MeetingTranscriptionCanceledEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MeetingTranscriber = void 0;
    const i2 = r2(2), n = r2(4), s = r2(65), o = r2(80), a = r2(143);
    t2.MeetingTranscriber = class {
      constructor(e3) {
        this.privAudioConfig = e3, this.privProperties = new o.PropertyCollection(), this.privRecognizer = void 0, this.privDisposedRecognizer = false;
      }
      get speechRecognitionLanguage() {
        return s.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(o.PropertyId.SpeechServiceConnection_RecoLanguage);
      }
      get properties() {
        return this.privProperties;
      }
      get internalData() {
        return this.privRecognizer.internalData;
      }
      get connection() {
        return o.Connection.fromRecognizer(this.privRecognizer);
      }
      get authorizationToken() {
        return this.properties.getProperty(o.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        s.Contracts.throwIfNullOrWhitespace(e3, "token"), this.properties.setProperty(o.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      joinMeetingAsync(e3, t3, r3) {
        const o2 = e3;
        s.Contracts.throwIfNullOrUndefined(a.MeetingImpl, "Meeting"), this.privRecognizer = new i2.TranscriberRecognizer(e3.config, this.privAudioConfig), s.Contracts.throwIfNullOrUndefined(this.privRecognizer, "Recognizer"), this.privRecognizer.connectMeetingCallbacks(this), (0, n.marshalPromiseToCallbacks)(o2.connectTranscriberRecognizer(this.privRecognizer), t3, r3);
      }
      startTranscribingAsync(e3, t3) {
        this.privRecognizer.startContinuousRecognitionAsync(e3, t3);
      }
      stopTranscribingAsync(e3, t3) {
        this.privRecognizer.stopContinuousRecognitionAsync(e3, t3);
      }
      leaveMeetingAsync(e3, t3) {
        this.privRecognizer.disconnectCallbacks(), (0, n.marshalPromiseToCallbacks)((async () => {
        })(), e3, t3);
      }
      close(e3, t3) {
        s.Contracts.throwIfDisposed(this.privDisposedRecognizer), (0, n.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      async dispose(e3) {
        this.privDisposedRecognizer || (this.privRecognizer && (await this.privRecognizer.close(), this.privRecognizer = void 0), e3 && (this.privDisposedRecognizer = true));
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranscriptionResult = void 0;
    const i2 = r2(80);
    class n extends i2.RecognitionResult {
      constructor(e3, t3, r3, i3, n2, s, o, a, c, p, h) {
        super(e3, t3, r3, i3, n2, s, o, c, p, h), this.privSpeakerId = a;
      }
      get speakerId() {
        return this.privSpeakerId;
      }
    }
    t2.ConversationTranscriptionResult = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisRequest = t2.Synthesizer = void 0;
    const i2 = r2(2), n = r2(4), s = r2(65), o = r2(80);
    class a {
      constructor(e3) {
        const t3 = e3;
        s.Contracts.throwIfNull(t3, "speechConfig"), this.privProperties = t3.properties.clone(), this.privDisposed = false, this.privSynthesizing = false, this.synthesisRequestQueue = new n.Queue(), this.tokenCredential = e3.tokenCredential;
      }
      get authorizationToken() {
        return this.properties.getProperty(o.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        s.Contracts.throwIfNullOrWhitespace(e3, "token"), this.properties.setProperty(o.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      get properties() {
        return this.privProperties;
      }
      get autoDetectSourceLanguage() {
        return this.properties.getProperty(o.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages) === i2.AutoDetectSourceLanguagesOpenRangeOptionName;
      }
      buildSsml(e3) {
        const t3 = { "af-ZA": "af-ZA-AdriNeural", "am-ET": "am-ET-AmehaNeural", "ar-AE": "ar-AE-FatimaNeural", "ar-BH": "ar-BH-AliNeural", "ar-DZ": "ar-DZ-AminaNeural", "ar-EG": "ar-EG-SalmaNeural", "ar-IQ": "ar-IQ-BasselNeural", "ar-JO": "ar-JO-SanaNeural", "ar-KW": "ar-KW-FahedNeural", "ar-LY": "ar-LY-ImanNeural", "ar-MA": "ar-MA-JamalNeural", "ar-QA": "ar-QA-AmalNeural", "ar-SA": "ar-SA-HamedNeural", "ar-SY": "ar-SY-AmanyNeural", "ar-TN": "ar-TN-HediNeural", "ar-YE": "ar-YE-MaryamNeural", "bg-BG": "bg-BG-BorislavNeural", "bn-BD": "bn-BD-NabanitaNeural", "bn-IN": "bn-IN-BashkarNeural", "ca-ES": "ca-ES-JoanaNeural", "cs-CZ": "cs-CZ-AntoninNeural", "cy-GB": "cy-GB-AledNeural", "da-DK": "da-DK-ChristelNeural", "de-AT": "de-AT-IngridNeural", "de-CH": "de-CH-JanNeural", "de-DE": "de-DE-KatjaNeural", "el-GR": "el-GR-AthinaNeural", "en-AU": "en-AU-NatashaNeural", "en-CA": "en-CA-ClaraNeural", "en-GB": "en-GB-LibbyNeural", "en-HK": "en-HK-SamNeural", "en-IE": "en-IE-ConnorNeural", "en-IN": "en-IN-NeerjaNeural", "en-KE": "en-KE-AsiliaNeural", "en-NG": "en-NG-AbeoNeural", "en-NZ": "en-NZ-MitchellNeural", "en-PH": "en-PH-JamesNeural", "en-SG": "en-SG-LunaNeural", "en-TZ": "en-TZ-ElimuNeural", "en-US": "en-US-AvaMultilingualNeural", "en-ZA": "en-ZA-LeahNeural", "es-AR": "es-AR-ElenaNeural", "es-BO": "es-BO-MarceloNeural", "es-CL": "es-CL-CatalinaNeural", "es-CO": "es-CO-GonzaloNeural", "es-CR": "es-CR-JuanNeural", "es-CU": "es-CU-BelkysNeural", "es-DO": "es-DO-EmilioNeural", "es-EC": "es-EC-AndreaNeural", "es-ES": "es-ES-AlvaroNeural", "es-GQ": "es-GQ-JavierNeural", "es-GT": "es-GT-AndresNeural", "es-HN": "es-HN-CarlosNeural", "es-MX": "es-MX-DaliaNeural", "es-NI": "es-NI-FedericoNeural", "es-PA": "es-PA-MargaritaNeural", "es-PE": "es-PE-AlexNeural", "es-PR": "es-PR-KarinaNeural", "es-PY": "es-PY-MarioNeural", "es-SV": "es-SV-LorenaNeural", "es-US": "es-US-AlonsoNeural", "es-UY": "es-UY-MateoNeural", "es-VE": "es-VE-PaolaNeural", "et-EE": "et-EE-AnuNeural", "fa-IR": "fa-IR-DilaraNeural", "fi-FI": "fi-FI-SelmaNeural", "fil-PH": "fil-PH-AngeloNeural", "fr-BE": "fr-BE-CharlineNeural", "fr-CA": "fr-CA-SylvieNeural", "fr-CH": "fr-CH-ArianeNeural", "fr-FR": "fr-FR-DeniseNeural", "ga-IE": "ga-IE-ColmNeural", "gl-ES": "gl-ES-RoiNeural", "gu-IN": "gu-IN-DhwaniNeural", "he-IL": "he-IL-AvriNeural", "hi-IN": "hi-IN-MadhurNeural", "hr-HR": "hr-HR-GabrijelaNeural", "hu-HU": "hu-HU-NoemiNeural", "id-ID": "id-ID-ArdiNeural", "is-IS": "is-IS-GudrunNeural", "it-IT": "it-IT-IsabellaNeural", "ja-JP": "ja-JP-NanamiNeural", "jv-ID": "jv-ID-DimasNeural", "kk-KZ": "kk-KZ-AigulNeural", "km-KH": "km-KH-PisethNeural", "kn-IN": "kn-IN-GaganNeural", "ko-KR": "ko-KR-SunHiNeural", "lo-LA": "lo-LA-ChanthavongNeural", "lt-LT": "lt-LT-LeonasNeural", "lv-LV": "lv-LV-EveritaNeural", "mk-MK": "mk-MK-AleksandarNeural", "ml-IN": "ml-IN-MidhunNeural", "mr-IN": "mr-IN-AarohiNeural", "ms-MY": "ms-MY-OsmanNeural", "mt-MT": "mt-MT-GraceNeural", "my-MM": "my-MM-NilarNeural", "nb-NO": "nb-NO-PernilleNeural", "nl-BE": "nl-BE-ArnaudNeural", "nl-NL": "nl-NL-ColetteNeural", "pl-PL": "pl-PL-AgnieszkaNeural", "ps-AF": "ps-AF-GulNawazNeural", "pt-BR": "pt-BR-FranciscaNeural", "pt-PT": "pt-PT-DuarteNeural", "ro-RO": "ro-RO-AlinaNeural", "ru-RU": "ru-RU-SvetlanaNeural", "si-LK": "si-LK-SameeraNeural", "sk-SK": "sk-SK-LukasNeural", "sl-SI": "sl-SI-PetraNeural", "so-SO": "so-SO-MuuseNeural", "sr-RS": "sr-RS-NicholasNeural", "su-ID": "su-ID-JajangNeural", "sv-SE": "sv-SE-SofieNeural", "sw-KE": "sw-KE-RafikiNeural", "sw-TZ": "sw-TZ-DaudiNeural", "ta-IN": "ta-IN-PallaviNeural", "ta-LK": "ta-LK-KumarNeural", "ta-SG": "ta-SG-AnbuNeural", "te-IN": "te-IN-MohanNeural", "th-TH": "th-TH-PremwadeeNeural", "tr-TR": "tr-TR-AhmetNeural", "uk-UA": "uk-UA-OstapNeural", "ur-IN": "ur-IN-GulNeural", "ur-PK": "ur-PK-AsadNeural", "uz-UZ": "uz-UZ-MadinaNeural", "vi-VN": "vi-VN-HoaiMyNeural", "zh-CN": "zh-CN-XiaoxiaoNeural", "zh-HK": "zh-HK-HiuMaanNeural", "zh-TW": "zh-TW-HsiaoChenNeural", "zu-ZA": "zu-ZA-ThandoNeural" };
        let r3 = this.properties.getProperty(o.PropertyId.SpeechServiceConnection_SynthLanguage, "en-US"), i3 = this.properties.getProperty(o.PropertyId.SpeechServiceConnection_SynthVoice, ""), n2 = a.XMLEncode(e3);
        return this.autoDetectSourceLanguage ? r3 = "en-US" : i3 = i3 || t3[r3], i3 && (n2 = `<voice name='${i3}'>${n2}</voice>`), n2 = `<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xmlns:mstts='http://www.w3.org/2001/mstts' xmlns:emo='http://www.w3.org/2009/10/emotionml' xml:lang='${r3}'>${n2}</speak>`, n2;
      }
      async dispose(e3) {
        this.privDisposed || (e3 && this.privAdapter && await this.privAdapter.dispose(), this.privDisposed = true);
      }
      async adapterSpeak() {
        if (!this.privDisposed && !this.privSynthesizing) {
          this.privSynthesizing = true;
          const e3 = await this.synthesisRequestQueue.dequeue();
          return this.privAdapter.Speak(e3.text, e3.isSSML, e3.requestId, e3.cb, e3.err, e3.dataStream);
        }
      }
      createSynthesizerConfig(e3) {
        return new i2.SynthesizerConfig(e3, this.privProperties);
      }
      implCommonSynthesizeSetup() {
        let e3 = "undefined" != typeof window ? "Browser" : "Node", t3 = "unknown", r3 = "unknown";
        "undefined" != typeof navigator && (e3 = e3 + "/" + navigator.platform, t3 = navigator.userAgent, r3 = navigator.appVersion);
        const n2 = this.createSynthesizerConfig(new i2.SpeechServiceConfig(new i2.Context(new i2.OS(e3, t3, r3)))), s2 = this.privProperties.getProperty(o.PropertyId.SpeechServiceConnection_Key, void 0), a2 = s2 && "" !== s2 ? new i2.CognitiveSubscriptionKeyAuthentication(s2) : this.tokenCredential ? new i2.CognitiveTokenAuthentication(async () => {
          try {
            const e4 = await this.tokenCredential.getToken("https://cognitiveservices.azure.com/.default");
            return e4?.token ?? "";
          } catch (e4) {
            throw e4;
          }
        }, async () => {
          try {
            const e4 = await this.tokenCredential.getToken("https://cognitiveservices.azure.com/.default");
            return e4?.token ?? "";
          } catch (e4) {
            throw e4;
          }
        }) : new i2.CognitiveTokenAuthentication(() => {
          const e4 = this.privProperties.getProperty(o.PropertyId.SpeechServiceAuthorization_Token, void 0);
          return Promise.resolve(e4);
        }, () => {
          const e4 = this.privProperties.getProperty(o.PropertyId.SpeechServiceAuthorization_Token, void 0);
          return Promise.resolve(e4);
        });
        this.privAdapter = this.createSynthesisAdapter(a2, this.privConnectionFactory, n2), this.privRestAdapter = this.createRestSynthesisAdapter(a2, n2);
      }
      static XMLEncode(e3) {
        return e3.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&apos;");
      }
    }
    t2.Synthesizer = a;
    t2.SynthesisRequest = class {
      constructor(e3, t3, r3, i3, n2, s2) {
        this.requestId = e3, this.text = t3, this.isSSML = r3, this.cb = i3, this.err = n2, this.dataStream = s2;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesizer = void 0;
    const i2 = r2(2), n = r2(4), s = r2(82), o = r2(86), a = r2(85), c = r2(65), p = r2(80), h = r2(161);
    class u extends p.Synthesizer {
      constructor(e3, t3) {
        super(e3), null !== t3 && (this.audioConfig = void 0 === t3 ? "undefined" == typeof window ? void 0 : p.AudioConfig.fromDefaultSpeakerOutput() : t3), this.privConnectionFactory = new i2.SpeechSynthesisConnectionFactory(), this.implCommonSynthesizeSetup();
      }
      static FromConfig(e3, t3, r3) {
        const i3 = e3;
        return t3.properties.mergeTo(i3.properties), new u(e3, r3);
      }
      speakTextAsync(e3, t3, r3, i3) {
        this.speakImpl(e3, false, t3, r3, i3);
      }
      speakSsmlAsync(e3, t3, r3, i3) {
        this.speakImpl(e3, true, t3, r3, i3);
      }
      async getVoicesAsync(e3 = "") {
        return this.getVoices(e3);
      }
      close(e3, t3) {
        c.Contracts.throwIfDisposed(this.privDisposed), (0, n.marshalPromiseToCallbacks)(this.dispose(true), e3, t3);
      }
      get internalData() {
        return this.privAdapter;
      }
      createSynthesisAdapter(e3, t3, r3) {
        return new i2.SpeechSynthesisAdapter(e3, t3, r3, this, this.audioConfig);
      }
      createRestSynthesisAdapter(e3, t3) {
        return new i2.SynthesisRestAdapter(t3, e3);
      }
      implCommonSynthesizeSetup() {
        super.implCommonSynthesizeSetup(), this.privAdapter.audioOutputFormat = o.AudioOutputFormatImpl.fromSpeechSynthesisOutputFormat(p.SpeechSynthesisOutputFormat[this.properties.getProperty(p.PropertyId.SpeechServiceConnection_SynthOutputFormat, void 0)]);
      }
      speakImpl(e3, t3, r3, i3, o2) {
        try {
          c.Contracts.throwIfDisposed(this.privDisposed);
          const u2 = (0, n.createNoDashGuid)();
          let d;
          d = o2 instanceof p.PushAudioOutputStreamCallback ? new a.PushAudioOutputStreamImpl(o2) : o2 instanceof p.PullAudioOutputStream ? o2 : void 0 !== o2 ? new s.AudioFileWriter(o2) : void 0, this.synthesisRequestQueue.enqueue(new h.SynthesisRequest(u2, e3, t3, (e4) => {
            if (this.privSynthesizing = false, r3) try {
              r3(e4);
            } catch (e5) {
              i3 && i3(e5);
            }
            r3 = void 0, this.adapterSpeak().catch(() => {
            });
          }, (e4) => {
            i3 && i3(e4);
          }, d)), this.adapterSpeak().catch(() => {
          });
        } catch (e4) {
          if (i3) if (e4 instanceof Error) {
            const t4 = e4;
            i3(t4.name + ": " + t4.message);
          } else i3(e4);
          this.dispose(true).catch(() => {
          });
        }
      }
      async getVoices(e3) {
        const t3 = (0, n.createNoDashGuid)(), r3 = await this.privRestAdapter.getVoicesList(t3);
        if (r3.ok && Array.isArray(r3.json)) {
          let i3 = r3.json;
          return e3 && e3.length > 0 && (i3 = i3.filter((t4) => !!t4.Locale && t4.Locale.toLowerCase() === e3.toLowerCase())), new p.SynthesisVoicesResult(t3, i3, void 0);
        }
        return new p.SynthesisVoicesResult(t3, void 0, `Error: ${r3.status}: ${r3.statusText}`);
      }
    }
    t2.SpeechSynthesizer = u;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisResult = void 0;
    t2.SynthesisResult = class {
      constructor(e3, t3, r2, i2) {
        this.privResultId = e3, this.privReason = t3, this.privErrorDetails = r2, this.privProperties = i2;
      }
      get resultId() {
        return this.privResultId;
      }
      get reason() {
        return this.privReason;
      }
      get errorDetails() {
        return this.privErrorDetails;
      }
      get properties() {
        return this.privProperties;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisResult = void 0;
    const i2 = r2(80);
    class n extends i2.SynthesisResult {
      constructor(e3, t3, r3, i3, n2, s) {
        super(e3, t3, i3, n2), this.privAudioData = r3, this.privAudioDuration = s;
      }
      get audioData() {
        return this.privAudioData;
      }
      get audioDuration() {
        return this.privAudioDuration;
      }
    }
    t2.SpeechSynthesisResult = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisEventArgs = void 0;
    t2.SpeechSynthesisEventArgs = class {
      constructor(e3) {
        this.privResult = e3;
      }
      get result() {
        return this.privResult;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisWordBoundaryEventArgs = void 0;
    t2.SpeechSynthesisWordBoundaryEventArgs = class {
      constructor(e3, t3, r2, i2, n, s) {
        this.privAudioOffset = e3, this.privDuration = t3, this.privText = r2, this.privWordLength = i2, this.privTextOffset = n, this.privBoundaryType = s;
      }
      get audioOffset() {
        return this.privAudioOffset;
      }
      get duration() {
        return this.privDuration;
      }
      get text() {
        return this.privText;
      }
      get wordLength() {
        return this.privWordLength;
      }
      get textOffset() {
        return this.privTextOffset;
      }
      get boundaryType() {
        return this.privBoundaryType;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisBookmarkEventArgs = void 0;
    t2.SpeechSynthesisBookmarkEventArgs = class {
      constructor(e3, t3) {
        this.privAudioOffset = e3, this.privText = t3;
      }
      get audioOffset() {
        return this.privAudioOffset;
      }
      get text() {
        return this.privText;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisVisemeEventArgs = void 0;
    t2.SpeechSynthesisVisemeEventArgs = class {
      constructor(e3, t3, r2) {
        this.privAudioOffset = e3, this.privVisemeId = t3, this.privAnimation = r2;
      }
      get audioOffset() {
        return this.privAudioOffset;
      }
      get visemeId() {
        return this.privVisemeId;
      }
      get animation() {
        return this.privAnimation;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisBoundaryType = void 0, function(e3) {
      e3.Word = "WordBoundary", e3.Punctuation = "PunctuationBoundary", e3.Sentence = "SentenceBoundary";
    }(t2.SpeechSynthesisBoundaryType || (t2.SpeechSynthesisBoundaryType = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisVoicesResult = void 0;
    const i2 = r2(80);
    class n extends i2.SynthesisResult {
      constructor(e3, t3, r3) {
        if (Array.isArray(t3)) {
          super(e3, i2.ResultReason.VoicesListRetrieved, void 0, new i2.PropertyCollection()), this.privVoices = [];
          for (const e4 of t3) this.privVoices.push(new i2.VoiceInfo(e4));
        } else super(e3, i2.ResultReason.Canceled, r3 || "Error information unavailable", new i2.PropertyCollection());
      }
      get voices() {
        return this.privVoices;
      }
    }
    t2.SynthesisVoicesResult = n;
  }, (e2, t2) => {
    "use strict";
    var r2, i2;
    Object.defineProperty(t2, "__esModule", { value: true }), t2.VoiceInfo = t2.SynthesisVoiceType = t2.SynthesisVoiceGender = void 0, function(e3) {
      e3[e3.Unknown = 0] = "Unknown", e3[e3.Female = 1] = "Female", e3[e3.Male = 2] = "Male", e3[e3.Neutral = 3] = "Neutral";
    }(r2 = t2.SynthesisVoiceGender || (t2.SynthesisVoiceGender = {})), function(e3) {
      e3[e3.Unknown = 0] = "Unknown", e3[e3.OnlineNeural = 1] = "OnlineNeural", e3[e3.OnlineStandard = 2] = "OnlineStandard", e3[e3.OfflineNeural = 3] = "OfflineNeural", e3[e3.OfflineStandard = 4] = "OfflineStandard", e3[e3.OnlineNeuralHD = 5] = "OnlineNeuralHD";
    }(i2 = t2.SynthesisVoiceType || (t2.SynthesisVoiceType = {}));
    const n = { [r2[r2.Neutral]]: r2.Neutral, [r2[r2.Male]]: r2.Male, [r2[r2.Female]]: r2.Female }, s = { Neural: i2.OnlineNeural, NeuralHD: i2.OnlineNeuralHD };
    t2.VoiceInfo = class {
      constructor(e3) {
        if (this.privStyleList = [], e3) {
          if (this.privName = e3.Name, this.privLocale = e3.Locale, this.privShortName = e3.ShortName, this.privLocaleName = e3.LocaleName, this.privDisplayName = e3.DisplayName, this.privLocalName = e3.LocalName, this.privVoiceType = s[e3.VoiceType] || i2.Unknown, this.privGender = n[e3.Gender] || r2.Unknown, e3.StyleList && Array.isArray(e3.StyleList)) for (const t3 of e3.StyleList) this.privStyleList.push(t3);
          this.privSampleRateHertz = e3.SampleRateHertz, this.privStatus = e3.Status, e3.ExtendedPropertyMap && (this.privExtendedPropertyMap = e3.ExtendedPropertyMap), this.privWordsPerMinute = e3.WordsPerMinute, Array.isArray(e3.SecondaryLocaleList) && (this.privSecondaryLocaleList = [...e3.SecondaryLocaleList]), Array.isArray(e3.RolePlayList) && (this.privRolePlayList = [...e3.RolePlayList]), e3.VoiceTag && (this.privVoiceTag = e3.VoiceTag);
        }
      }
      get name() {
        return this.privName;
      }
      get locale() {
        return this.privLocale;
      }
      get shortName() {
        return this.privShortName;
      }
      get displayName() {
        return this.privDisplayName;
      }
      get localName() {
        return this.privLocalName;
      }
      get localeName() {
        return this.privLocaleName;
      }
      get gender() {
        return this.privGender;
      }
      get voiceType() {
        return this.privVoiceType;
      }
      get styleList() {
        return this.privStyleList;
      }
      get sampleRateHertz() {
        return this.privSampleRateHertz;
      }
      get status() {
        return this.privStatus;
      }
      get extendedPropertyMap() {
        return this.privExtendedPropertyMap;
      }
      get wordsPerMinute() {
        return this.privWordsPerMinute;
      }
      get secondaryLocaleList() {
        return this.privSecondaryLocaleList;
      }
      get rolePlayList() {
        return this.privRolePlayList;
      }
      get voiceTag() {
        return this.privVoiceTag;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeakerAudioDestination = void 0;
    const i2 = r2(4), n = r2(85), s = r2(68), o = { [s.AudioFormatTag.PCM]: "audio/wav", [s.AudioFormatTag.MuLaw]: "audio/x-wav", [s.AudioFormatTag.MP3]: "audio/mpeg", [s.AudioFormatTag.OGG_OPUS]: "audio/ogg", [s.AudioFormatTag.WEBM_OPUS]: "audio/webm; codecs=opus", [s.AudioFormatTag.ALaw]: "audio/x-wav", [s.AudioFormatTag.FLAC]: "audio/flac", [s.AudioFormatTag.AMR_WB]: "audio/amr-wb", [s.AudioFormatTag.G722]: "audio/G722" };
    t2.SpeakerAudioDestination = class {
      constructor(e3) {
        this.privPlaybackStarted = false, this.privAppendingToBuffer = false, this.privMediaSourceOpened = false, this.privBytesReceived = 0, this.privId = e3 || (0, i2.createNoDashGuid)(), this.privIsPaused = false, this.privIsClosed = false;
      }
      id() {
        return this.privId;
      }
      write(e3, t3, r3) {
        void 0 !== this.privAudioBuffer ? (this.privAudioBuffer.push(e3), this.updateSourceBuffer().then(() => {
          t3 && t3();
        }, (e4) => {
          r3 && r3(e4);
        })) : void 0 !== this.privAudioOutputStream && (this.privAudioOutputStream.write(e3), this.privBytesReceived += e3.byteLength);
      }
      close(e3, t3) {
        if (this.privIsClosed = true, void 0 !== this.privSourceBuffer) this.handleSourceBufferUpdateEnd().then(() => {
          e3 && e3();
        }, (e4) => {
          t3 && t3(e4);
        });
        else if (void 0 !== this.privAudioOutputStream && "undefined" != typeof window) if (this.privFormat.formatTag !== s.AudioFormatTag.PCM && this.privFormat.formatTag !== s.AudioFormatTag.MuLaw && this.privFormat.formatTag !== s.AudioFormatTag.ALaw || false !== this.privFormat.hasHeader) {
          let r3 = new ArrayBuffer(this.privBytesReceived);
          this.privAudioOutputStream.read(r3).then(() => {
            r3 = this.privFormat.addHeader(r3);
            const i3 = new Blob([r3], { type: o[this.privFormat.formatTag] });
            this.privAudio.src = window.URL.createObjectURL(i3), this.notifyPlayback().then(() => {
              e3 && e3();
            }, (e4) => {
              t3 && t3(e4);
            });
          }, (e4) => {
            t3 && t3(e4);
          });
        } else console.warn("Play back is not supported for raw PCM, mulaw or alaw format without header."), this.onAudioEnd && this.onAudioEnd(this);
        else this.onAudioEnd && this.onAudioEnd(this);
      }
      set format(e3) {
        if ("undefined" != typeof AudioContext || "undefined" != typeof window && void 0 !== window.webkitAudioContext) {
          this.privFormat = e3;
          const t3 = o[this.privFormat.formatTag];
          void 0 === t3 ? console.warn(`Unknown mimeType for format ${s.AudioFormatTag[this.privFormat.formatTag]}; playback is not supported.`) : "undefined" != typeof MediaSource && MediaSource.isTypeSupported(t3) ? (this.privAudio = new Audio(), this.privAudioBuffer = [], this.privMediaSource = new MediaSource(), this.privAudio.src = URL.createObjectURL(this.privMediaSource), this.privAudio.load(), this.privMediaSource.onsourceopen = () => {
            this.privMediaSourceOpened = true, this.privMediaSource.duration = 1800, this.privSourceBuffer = this.privMediaSource.addSourceBuffer(t3), this.privSourceBuffer.onupdate = () => {
              this.updateSourceBuffer().catch((e4) => {
                i2.Events.instance.onEvent(new i2.BackgroundEvent(e4));
              });
            }, this.privSourceBuffer.onupdateend = () => {
              this.handleSourceBufferUpdateEnd().catch((e4) => {
                i2.Events.instance.onEvent(new i2.BackgroundEvent(e4));
              });
            }, this.privSourceBuffer.onupdatestart = () => {
              this.privAppendingToBuffer = false;
            };
          }, this.updateSourceBuffer().catch((e4) => {
            i2.Events.instance.onEvent(new i2.BackgroundEvent(e4));
          })) : (console.warn(`Format ${s.AudioFormatTag[this.privFormat.formatTag]} could not be played by MSE, streaming playback is not enabled.`), this.privAudioOutputStream = new n.PullAudioOutputStreamImpl(), this.privAudioOutputStream.format = this.privFormat, this.privAudio = new Audio());
        }
      }
      get volume() {
        return this.privAudio?.volume ?? -1;
      }
      set volume(e3) {
        this.privAudio && (this.privAudio.volume = e3);
      }
      mute() {
        this.privAudio && (this.privAudio.muted = true);
      }
      unmute() {
        this.privAudio && (this.privAudio.muted = false);
      }
      get isClosed() {
        return this.privIsClosed;
      }
      get currentTime() {
        return void 0 !== this.privAudio ? this.privAudio.currentTime : -1;
      }
      pause() {
        this.privIsPaused || void 0 === this.privAudio || (this.privAudio.pause(), this.privIsPaused = true);
      }
      resume(e3, t3) {
        this.privIsPaused && void 0 !== this.privAudio && (this.privAudio.play().then(() => {
          e3 && e3();
        }, (e4) => {
          t3 && t3(e4);
        }), this.privIsPaused = false);
      }
      get internalAudio() {
        return this.privAudio;
      }
      async updateSourceBuffer() {
        if (void 0 !== this.privAudioBuffer && this.privAudioBuffer.length > 0 && this.sourceBufferAvailable()) {
          this.privAppendingToBuffer = true;
          const e3 = this.privAudioBuffer.shift();
          try {
            this.privSourceBuffer.appendBuffer(e3);
          } catch (t3) {
            return this.privAudioBuffer.unshift(e3), void console.log("buffer filled, pausing addition of binaries until space is made");
          }
          await this.notifyPlayback();
        } else this.canEndStream() && await this.handleSourceBufferUpdateEnd();
      }
      async handleSourceBufferUpdateEnd() {
        this.canEndStream() && this.sourceBufferAvailable() && (this.privMediaSource.endOfStream(), await this.notifyPlayback());
      }
      async notifyPlayback() {
        this.privPlaybackStarted || void 0 === this.privAudio || (this.privPlaybackStarted = true, this.onAudioStart && this.onAudioStart(this), this.privAudio.onended = () => {
          this.onAudioEnd && this.onAudioEnd(this);
        }, this.privIsPaused || await this.privAudio.play());
      }
      canEndStream() {
        return this.isClosed && void 0 !== this.privSourceBuffer && 0 === this.privAudioBuffer.length && this.privMediaSourceOpened && !this.privAppendingToBuffer && "open" === this.privMediaSource.readyState;
      }
      sourceBufferAvailable() {
        return void 0 !== this.privSourceBuffer && !this.privSourceBuffer.updating;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranscriptionCanceledEventArgs = void 0;
    const i2 = r2(99);
    class n extends i2.CancellationEventArgsBase {
    }
    t2.ConversationTranscriptionCanceledEventArgs = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MeetingTranscriptionCanceledEventArgs = void 0;
    const i2 = r2(99);
    class n extends i2.CancellationEventArgsBase {
    }
    t2.MeetingTranscriptionCanceledEventArgs = n;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PronunciationAssessmentGradingSystem = void 0, function(e3) {
      e3[e3.FivePoint = 1] = "FivePoint", e3[e3.HundredMark = 2] = "HundredMark";
    }(t2.PronunciationAssessmentGradingSystem || (t2.PronunciationAssessmentGradingSystem = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PronunciationAssessmentGranularity = void 0, function(e3) {
      e3[e3.Phoneme = 1] = "Phoneme", e3[e3.Word = 2] = "Word", e3[e3.FullText = 3] = "FullText";
    }(t2.PronunciationAssessmentGranularity || (t2.PronunciationAssessmentGranularity = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PronunciationAssessmentConfig = void 0;
    const i2 = r2(65), n = r2(80);
    class s {
      constructor(e3, t3 = n.PronunciationAssessmentGradingSystem.FivePoint, r3 = n.PronunciationAssessmentGranularity.Phoneme, s2 = false) {
        i2.Contracts.throwIfNullOrUndefined(e3, "referenceText"), this.privProperties = new n.PropertyCollection(), this.privProperties.setProperty(n.PropertyId.PronunciationAssessment_ReferenceText, e3), this.privProperties.setProperty(n.PropertyId.PronunciationAssessment_GradingSystem, n.PronunciationAssessmentGradingSystem[t3]), this.privProperties.setProperty(n.PropertyId.PronunciationAssessment_Granularity, n.PronunciationAssessmentGranularity[r3]), this.privProperties.setProperty(n.PropertyId.PronunciationAssessment_EnableMiscue, String(s2));
      }
      static fromJSON(e3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "json");
        const t3 = new s("");
        return t3.privProperties = new n.PropertyCollection(), t3.properties.setProperty(n.PropertyId.PronunciationAssessment_Json, e3), t3;
      }
      toJSON() {
        return this.updateJson(), this.privProperties.getProperty(n.PropertyId.PronunciationAssessment_Params);
      }
      applyTo(e3) {
        this.updateJson();
        const t3 = e3.internalData;
        t3.speechContext.setPronunciationAssessmentParams(this.properties.getProperty(n.PropertyId.PronunciationAssessment_Params), t3.isSpeakerDiarizationEnabled);
      }
      get referenceText() {
        return this.properties.getProperty(n.PropertyId.PronunciationAssessment_ReferenceText);
      }
      set referenceText(e3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "referenceText"), this.properties.setProperty(n.PropertyId.PronunciationAssessment_ReferenceText, e3);
      }
      set phonemeAlphabet(e3) {
        i2.Contracts.throwIfNullOrWhitespace(e3, "phonemeAlphabet"), this.privPhonemeAlphabet = e3;
      }
      set enableMiscue(e3) {
        const t3 = e3 ? "true" : "false";
        this.properties.setProperty(n.PropertyId.PronunciationAssessment_EnableMiscue, t3);
      }
      get enableMiscue() {
        return "true" === this.properties.getProperty(n.PropertyId.PronunciationAssessment_EnableMiscue, "false").toLowerCase();
      }
      set nbestPhonemeCount(e3) {
        this.privNBestPhonemeCount = e3;
      }
      set enableProsodyAssessment(e3) {
        this.privEnableProsodyAssessment = e3;
      }
      get properties() {
        return this.privProperties;
      }
      updateJson() {
        const e3 = this.privProperties.getProperty(n.PropertyId.PronunciationAssessment_Json, "{}"), t3 = JSON.parse(e3), r3 = this.privProperties.getProperty(n.PropertyId.PronunciationAssessment_ReferenceText);
        r3 && (t3.referenceText = r3);
        const i3 = this.privProperties.getProperty(n.PropertyId.PronunciationAssessment_GradingSystem);
        i3 && (t3.gradingSystem = i3);
        const s2 = this.privProperties.getProperty(n.PropertyId.PronunciationAssessment_Granularity);
        s2 && (t3.granularity = s2), this.privPhonemeAlphabet && (t3.phonemeAlphabet = this.privPhonemeAlphabet), this.privNBestPhonemeCount && (t3.nbestPhonemeCount = this.privNBestPhonemeCount), t3.enableProsodyAssessment = this.privEnableProsodyAssessment, t3.dimension = "Comprehensive";
        this.privProperties.getProperty(n.PropertyId.PronunciationAssessment_EnableMiscue) && (t3.enableMiscue = this.enableMiscue), this.privProperties.setProperty(n.PropertyId.PronunciationAssessment_Params, JSON.stringify(t3));
      }
    }
    t2.PronunciationAssessmentConfig = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PronunciationAssessmentResult = void 0;
    const i2 = r2(65), n = r2(80);
    class s {
      constructor(e3) {
        const t3 = JSON.parse(e3);
        i2.Contracts.throwIfNullOrUndefined(t3.NBest[0], "NBest"), this.privPronJson = t3.NBest[0];
      }
      static fromResult(e3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "result");
        const t3 = e3.properties.getProperty(n.PropertyId.SpeechServiceResponse_JsonResult);
        return i2.Contracts.throwIfNullOrUndefined(t3, "json"), new s(t3);
      }
      get detailResult() {
        return this.privPronJson;
      }
      get accuracyScore() {
        return this.detailResult.PronunciationAssessment?.AccuracyScore;
      }
      get pronunciationScore() {
        return this.detailResult.PronunciationAssessment?.PronScore;
      }
      get completenessScore() {
        return this.detailResult.PronunciationAssessment?.CompletenessScore;
      }
      get fluencyScore() {
        return this.detailResult.PronunciationAssessment?.FluencyScore;
      }
      get prosodyScore() {
        return this.detailResult.PronunciationAssessment?.ProsodyScore;
      }
    }
    t2.PronunciationAssessmentResult = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AvatarConfig = void 0;
    const i2 = r2(65), n = r2(80);
    t2.AvatarConfig = class {
      constructor(e3, t3, r3) {
        this.privCustomized = false, this.privUseBuiltInVoice = false, i2.Contracts.throwIfNullOrWhitespace(e3, "character"), this.character = e3, this.style = t3, void 0 === r3 && (r3 = new n.AvatarVideoFormat()), this.videoFormat = r3;
      }
      get customized() {
        return this.privCustomized;
      }
      set customized(e3) {
        this.privCustomized = e3;
      }
      get useBuiltInVoice() {
        return this.privUseBuiltInVoice;
      }
      set useBuiltInVoice(e3) {
        this.privUseBuiltInVoice = e3;
      }
      get photoAvatarBaseModel() {
        return this.privPhotoAvatarBaseModel;
      }
      set photoAvatarBaseModel(e3) {
        this.privPhotoAvatarBaseModel = e3;
      }
      get backgroundColor() {
        return this.privBackgroundColor;
      }
      set backgroundColor(e3) {
        this.privBackgroundColor = e3;
      }
      get backgroundImage() {
        return this.privBackgroundImage;
      }
      set backgroundImage(e3) {
        this.privBackgroundImage = e3;
      }
      get remoteIceServers() {
        return this.privRemoteIceServers;
      }
      set remoteIceServers(e3) {
        this.privRemoteIceServers = e3;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AvatarEventArgs = t2.AvatarEventTypes = void 0, function(e3) {
      e3.SwitchedToSpeaking = "SwitchedToSpeaking", e3.SwitchedToIdle = "SwitchedToIdle", e3.SessionClosed = "SessionClosed";
    }(t2.AvatarEventTypes || (t2.AvatarEventTypes = {}));
    t2.AvatarEventArgs = class {
      constructor(e3, t3) {
        this.privOffset = e3, this.privDescription = t3;
      }
      get type() {
        return this.privType;
      }
      get offset() {
        return this.privOffset;
      }
      get description() {
        return this.privDescription;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AvatarSynthesizer = void 0;
    const i2 = r2(182), n = r2(2), s = r2(4), o = r2(86), a = r2(80), c = r2(65), p = r2(161);
    class h extends a.Synthesizer {
      constructor(e3, t3) {
        super(e3), c.Contracts.throwIfNullOrUndefined(t3, "avatarConfig"), this.privConnectionFactory = new i2.SpeechSynthesisConnectionFactory(), this.privAvatarConfig = t3, this.implCommonSynthesizeSetup();
      }
      implCommonSynthesizeSetup() {
        super.implCommonSynthesizeSetup(), this.privAdapter.audioOutputFormat = o.AudioOutputFormatImpl.fromSpeechSynthesisOutputFormat(a.SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm);
      }
      async startAvatarAsync(e3) {
        c.Contracts.throwIfNullOrUndefined(e3, "peerConnection"), this.privIceServers = e3.getConfiguration().iceServers, c.Contracts.throwIfNullOrUndefined(this.privIceServers, "Ice servers must be set.");
        const t3 = new s.Deferred();
        e3.onicegatheringstatechange = () => {
          s.Events.instance.onEvent(new s.PlatformEvent("peer connection: ice gathering state: " + e3.iceGatheringState, s.EventType.Debug)), "complete" === e3.iceGatheringState && (s.Events.instance.onEvent(new s.PlatformEvent("peer connection: ice gathering complete.", s.EventType.Info)), t3.resolve());
        }, e3.onicecandidate = (e4) => {
          e4.candidate ? s.Events.instance.onEvent(new s.PlatformEvent("peer connection: ice candidate: " + e4.candidate.candidate, s.EventType.Debug)) : (s.Events.instance.onEvent(new s.PlatformEvent("peer connection: ice candidate: complete", s.EventType.Debug)), t3.resolve());
        }, setTimeout(() => {
          "complete" !== e3.iceGatheringState && (s.Events.instance.onEvent(new s.PlatformEvent("peer connection: ice gathering timeout.", s.EventType.Warning)), t3.resolve());
        }, 2e3);
        const r3 = await e3.createOffer();
        await e3.setLocalDescription(r3), await t3.promise, s.Events.instance.onEvent(new s.PlatformEvent("peer connection: got local SDP.", s.EventType.Info)), this.privProperties.setProperty(a.PropertyId.TalkingAvatarService_WebRTC_SDP, JSON.stringify(e3.localDescription));
        const i3 = await this.speak("", false);
        if (i3.reason !== a.ResultReason.SynthesizingAudioCompleted) return new a.SynthesisResult(i3.resultId, i3.reason, i3.errorDetails, i3.properties);
        const n2 = atob(i3.properties.getProperty(a.PropertyId.TalkingAvatarService_WebRTC_SDP)), o2 = new RTCSessionDescription(JSON.parse(n2));
        return await e3.setRemoteDescription(o2), new a.SynthesisResult(i3.resultId, i3.reason, void 0, i3.properties);
      }
      async speakTextAsync(e3) {
        const t3 = await this.speak(e3, false);
        return new a.SynthesisResult(t3.resultId, t3.reason, t3.errorDetails, t3.properties);
      }
      async speakSsmlAsync(e3) {
        const t3 = await this.speak(e3, true);
        return new a.SynthesisResult(t3.resultId, t3.reason, t3.errorDetails, t3.properties);
      }
      async stopSpeakingAsync() {
        for (; this.synthesisRequestQueue.length() > 0; ) {
          (await this.synthesisRequestQueue.dequeue()).err("Synthesis is canceled by user.");
        }
        return this.privAdapter.stopSpeaking();
      }
      async stopAvatarAsync() {
        return c.Contracts.throwIfDisposed(this.privDisposed), this.dispose(true);
      }
      async close() {
        if (!this.privDisposed) return this.dispose(true);
      }
      get iceServers() {
        return this.privIceServers;
      }
      createSynthesisAdapter(e3, t3, r3) {
        return new n.AvatarSynthesisAdapter(e3, t3, r3, this, this.privAvatarConfig);
      }
      createRestSynthesisAdapter(e3, t3) {
      }
      createSynthesizerConfig(e3) {
        const t3 = super.createSynthesizerConfig(e3);
        return t3.avatarEnabled = true, t3;
      }
      async speak(e3, t3) {
        const r3 = (0, s.createNoDashGuid)(), i3 = new s.Deferred();
        return this.synthesisRequestQueue.enqueue(new p.SynthesisRequest(r3, e3, t3, (e4) => {
          i3.resolve(e4), this.privSynthesizing = false, this.adapterSpeak();
        }, (e4) => {
          i3.reject(e4), this.privSynthesizing = false;
        })), this.adapterSpeak(), i3.promise;
      }
    }
    t2.AvatarSynthesizer = h;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisConnectionFactory = void 0;
    const i2 = r2(61), n = r2(80), s = r2(130), o = r2(2), a = r2(54), c = r2(131);
    t2.SpeechSynthesisConnectionFactory = class {
      constructor() {
        this.synthesisUri = "/tts/cognitiveservices/websocket/v1";
      }
      async create(e3, t3, r3) {
        let p = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Endpoint, void 0);
        const h = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Region, void 0), u = s.ConnectionFactoryBase.getHostSuffix(h), d = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_EndpointId, void 0), v = void 0 === d ? "tts" : "voice", l = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Host, "wss://" + h + "." + v + ".speech" + u), g = {}, m = {};
        if (void 0 !== t3.token && "" !== t3.token && (m[t3.headerName] = t3.token), m[a.HeaderNames.ConnectionId] = r3, void 0 !== d && "" !== d && (p && -1 !== p.search(c.QueryParameterNames.CustomVoiceDeploymentId) || (g[c.QueryParameterNames.CustomVoiceDeploymentId] = d)), e3.avatarEnabled && (p && -1 !== p.search(c.QueryParameterNames.EnableAvatar) || (g[c.QueryParameterNames.EnableAvatar] = "true")), p) {
          const e4 = new URL(p), t4 = e4.pathname;
          "" !== t4 && "/" !== t4 || (e4.pathname = this.synthesisUri, p = await s.ConnectionFactoryBase.getRedirectUrlFromEndpoint(e4.toString()));
        }
        p || (p = l + this.synthesisUri), e3.parameters.setProperty(n.PropertyId.SpeechServiceConnection_Url, p);
        const S = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false");
        return new i2.WebsocketConnection(p, g, m, new o.WebsocketMessageFormatter(), i2.ProxyInfo.fromParameters(e3.parameters), S, r3);
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AvatarVideoFormat = t2.Coordinate = void 0;
    t2.Coordinate = class {
      constructor(e3, t3) {
        this.x = e3, this.y = t3;
      }
    };
    t2.AvatarVideoFormat = class {
      constructor(e3 = "H264", t3 = 2e6, r2 = 1920, i2 = 1080) {
        this.codec = e3, this.bitrate = t3, this.width = r2, this.height = i2;
      }
      setCropRange(e3, t3) {
        this.cropRange = { bottomRight: t3, topLeft: e3 };
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AvatarWebRTCConnectionResult = void 0;
    const i2 = r2(80);
    class n extends i2.SynthesisResult {
      constructor(e3, t3, r3, i3, n2) {
        super(t3, r3, i3, n2), this.privSDPAnswer = e3;
      }
      get SDPAnswer() {
        return this.privSDPAnswer;
      }
    }
    t2.AvatarWebRTCConnectionResult = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Diagnostics = void 0;
    const i2 = r2(61), n = r2(4);
    class s {
      static SetLoggingLevel(e3) {
        this.privListener = new i2.ConsoleLoggingListener(e3), n.Events.instance.attachConsoleListener(this.privListener);
      }
      static StartConsoleOutput() {
        this.privListener && (this.privListener.enableConsoleOutput = true);
      }
      static StopConsoleOutput() {
        this.privListener && (this.privListener.enableConsoleOutput = false);
      }
      static SetLogOutputPath(e3) {
        if ("undefined" != typeof window) throw new Error("File system logging not available in browser.");
        this.privListener && (this.privListener.logPath = e3);
      }
      static set onLogOutput(e3) {
        this.privListener && (this.privListener.logCallback = e3);
      }
    }
    t2.Diagnostics = s, s.privListener = void 0;
  }, function(e2, t2, r2) {
    "use strict";
    var i2 = this && this.__importDefault || function(e3) {
      return e3 && e3.__esModule ? e3 : { default: e3 };
    };
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RestMessageAdapter = t2.RestRequestType = void 0;
    const n = i2(r2(187)), s = r2(4);
    var o;
    !function(e3) {
      e3.Get = "GET", e3.Post = "POST", e3.Delete = "DELETE", e3.File = "file";
    }(o = t2.RestRequestType || (t2.RestRequestType = {}));
    t2.RestMessageAdapter = class {
      constructor(e3) {
        if (!e3) throw new s.ArgumentNullError("configParams");
        this.privHeaders = e3.headers, this.privIgnoreCache = e3.ignoreCache;
      }
      static extractHeaderValue(e3, t3) {
        let r3 = "";
        try {
          const i3 = t3.trim().split(/[\r\n]+/), n2 = {};
          i3.forEach((e4) => {
            const t4 = e4.split(": "), r4 = t4.shift().toLowerCase(), i4 = t4.join(": ");
            n2[r4] = i4;
          }), r3 = n2[e3.toLowerCase()];
        } catch (e4) {
        }
        return r3;
      }
      set options(e3) {
        this.privHeaders = e3.headers, this.privIgnoreCache = e3.ignoreCache;
      }
      setHeaders(e3, t3) {
        this.privHeaders[e3] = t3;
      }
      request(e3, t3, r3 = {}, i3 = null) {
        const a = new s.Deferred(), c = e3 === o.File ? "POST" : e3, p = (e4, t4 = {}) => {
          const r4 = e4;
          return { data: JSON.stringify(t4), headers: JSON.stringify(e4.headers), json: t4, ok: e4.statusCode >= 200 && e4.statusCode < 300, status: e4.statusCode, statusText: t4.error ? t4.error.message : r4.statusText ? r4.statusText : r4.statusMessage };
        };
        return this.privIgnoreCache && (this.privHeaders["Cache-Control"] = "no-cache"), e3 === o.Post && i3 && (this.privHeaders["content-type"] = "application/json", this.privHeaders["Content-Type"] = "application/json"), ((i4) => {
          (0, n.default)(t3, c, this.privHeaders, 200, 201, 202, 204, 400, 401, 402, 403, 404)("" === this.queryParams(r3) ? "" : `?${this.queryParams(r3)}`, i4).then(async (t4) => {
            if (e3 === o.Delete || 204 === t4.statusCode) a.resolve(p(t4));
            else try {
              const e4 = await t4.json();
              a.resolve(p(t4, e4));
            } catch {
              a.resolve(p(t4));
            }
          }).catch((e4) => {
            a.reject(e4);
          });
        })(i3), a.promise;
      }
      queryParams(e3 = {}) {
        return Object.keys(e3).map((t3) => encodeURIComponent(t3) + "=" + encodeURIComponent(e3[t3])).join("&");
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    const i2 = r2(188);
    class n extends Error {
      constructor(e3, ...t3) {
        let r3;
        super(...t3), Error.captureStackTrace && Error.captureStackTrace(this, n), this.name = "StatusError", this.message = e3.statusMessage, this.statusCode = e3.status, this.res = e3, this.json = e3.json.bind(e3), this.text = e3.text.bind(e3), this.arrayBuffer = e3.arrayBuffer.bind(e3);
        Object.defineProperty(this, "responseBody", { get: () => (r3 || (r3 = this.arrayBuffer()), r3) }), this.headers = {};
        for (const [t4, r4] of e3.headers.entries()) this.headers[t4.toLowerCase()] = r4;
      }
    }
    e2.exports = i2((e3, t3, r3, i3, s) => async (o, a, c = {}) => {
      o = s + (o || "");
      let p = new URL(o);
      if (i3 || (i3 = {}), p.username && (i3.Authorization = "Basic " + btoa(p.username + ":" + p.password), p = new URL(p.protocol + "//" + p.host + p.pathname + p.search)), "https:" !== p.protocol && "http:" !== p.protocol) throw new Error(`Unknown protocol, ${p.protocol}`);
      if (a) if (a instanceof ArrayBuffer || ArrayBuffer.isView(a) || "string" == typeof a) ;
      else {
        if ("object" != typeof a) throw new Error("Unknown body type.");
        a = JSON.stringify(a), i3["Content-Type"] = "application/json";
      }
      c = new Headers({ ...i3 || {}, ...c });
      const h = await fetch(p, { method: t3, headers: c, body: a });
      if (h.statusCode = h.status, !e3.has(h.status)) throw new n(h);
      return "json" === r3 ? h.json() : "buffer" === r3 ? h.arrayBuffer() : "string" === r3 ? h.text() : h;
    });
  }, (e2) => {
    "use strict";
    const t2 = /* @__PURE__ */ new Set(["json", "buffer", "string"]);
    e2.exports = (e3) => (...r2) => {
      const i2 = /* @__PURE__ */ new Set();
      let n, s, o, a = "";
      return r2.forEach((e4) => {
        if ("string" == typeof e4) if (e4.toUpperCase() === e4) {
          if (n) {
            throw new Error(`Can't set method to ${e4}, already set to ${n}.`);
          }
          n = e4;
        } else if (e4.startsWith("http:") || e4.startsWith("https:")) a = e4;
        else {
          if (!t2.has(e4)) throw new Error(`Unknown encoding, ${e4}`);
          s = e4;
        }
        else if ("number" == typeof e4) i2.add(e4);
        else {
          if ("object" != typeof e4) throw new Error("Unknown type: " + typeof e4);
          if (Array.isArray(e4) || e4 instanceof Set) e4.forEach((e5) => i2.add(e5));
          else {
            if (o) throw new Error("Cannot set headers twice.");
            o = e4;
          }
        }
      }), n || (n = "GET"), 0 === i2.size && i2.add(200), e3(i2, n, s, o, a);
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RestConfigBase = void 0;
    class r2 {
      static get requestOptions() {
        return r2.privDefaultRequestOptions;
      }
      static get configParams() {
        return r2.privDefaultParams;
      }
      static get restErrors() {
        return r2.privRestErrors;
      }
    }
    t2.RestConfigBase = r2, r2.privDefaultRequestOptions = { headers: { Accept: "application/json" }, ignoreCache: false, timeout: 1e4 }, r2.privRestErrors = { authInvalidSubscriptionKey: "You must specify either an authentication token to use, or a Cognitive Speech subscription key.", authInvalidSubscriptionRegion: "You must specify the Cognitive Speech region to use.", invalidArgs: "Required input not found: {arg}.", invalidCreateJoinConversationResponse: "Creating/Joining conversation failed with HTTP {status}.", invalidParticipantRequest: "The requested participant was not found.", permissionDeniedConnect: "Required credentials not found.", permissionDeniedConversation: "Invalid operation: only the host can {command} the conversation.", permissionDeniedParticipant: "Invalid operation: only the host can {command} a participant.", permissionDeniedSend: "Invalid operation: the conversation is not in a connected state.", permissionDeniedStart: "Invalid operation: there is already an active conversation." }, r2.privDefaultParams = { apiVersion: "api-version", authorization: "Authorization", clientAppId: "X-ClientAppId", contentTypeKey: "Content-Type", correlationId: "X-CorrelationId", languageCode: "language", nickname: "nickname", profanity: "profanity", requestId: "X-RequestId", roomId: "roomid", sessionToken: "token", subscriptionKey: "Ocp-Apim-Subscription-Key", subscriptionRegion: "Ocp-Apim-Subscription-Region", token: "X-CapitoToken" };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechConnectionMessage = void 0;
    const i2 = r2(4), n = r2(54);
    class s extends i2.ConnectionMessage {
      constructor(e3, t3, r3, s2, o, a, c, p) {
        if (!t3) throw new i2.ArgumentNullError("path");
        if (!r3) throw new i2.ArgumentNullError("requestId");
        const h = {};
        if (h[n.HeaderNames.Path] = t3, h[n.HeaderNames.RequestId] = r3, h[n.HeaderNames.RequestTimestamp] = (/* @__PURE__ */ new Date()).toISOString(), s2 && (h[n.HeaderNames.ContentType] = s2), a && (h[n.HeaderNames.RequestStreamId] = a), c) for (const e4 in c) e4 && (h[e4] = c[e4]);
        p ? super(e3, o, h, p) : super(e3, o, h), this.privPath = t3, this.privRequestId = r3, this.privContentType = s2, this.privStreamId = a, this.privAdditionalHeaders = c;
      }
      get path() {
        return this.privPath;
      }
      get requestId() {
        return this.privRequestId;
      }
      get contentType() {
        return this.privContentType;
      }
      get streamId() {
        return this.privStreamId;
      }
      get additionalHeaders() {
        return this.privAdditionalHeaders;
      }
      static fromConnectionMessage(e3) {
        let t3 = null, r3 = null, i3 = null, o = null;
        const a = {};
        if (e3.headers) for (const s2 in e3.headers) s2 && (s2.toLowerCase() === n.HeaderNames.Path.toLowerCase() ? t3 = e3.headers[s2] : s2.toLowerCase() === n.HeaderNames.RequestId.toLowerCase() ? r3 = e3.headers[s2] : s2.toLowerCase() === n.HeaderNames.ContentType.toLowerCase() ? i3 = e3.headers[s2] : s2.toLowerCase() === n.HeaderNames.RequestStreamId.toLowerCase() ? o = e3.headers[s2] : a[s2] = e3.headers[s2]);
        return new s(e3.messageType, t3, r3, i3, e3.body, o, a, e3.id);
      }
    }
    t2.SpeechConnectionMessage = s;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SegmentationMode = void 0, function(e3) {
      e3.Normal = "Normal", e3.Disabled = "Disabled", e3.Custom = "Custom", e3.Semantic = "Semantic";
    }(t2.SegmentationMode || (t2.SegmentationMode = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.NextAction = void 0, function(e3) {
      e3.None = "None", e3.Synthesize = "Synthesize";
    }(t2.NextAction || (t2.NextAction = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.Mode = void 0, function(e3) {
      e3.None = "None", e3.Always = "Always";
    }(t2.Mode || (t2.Mode = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.LanguageIdDetectionPriority = t2.LanguageIdDetectionMode = void 0, function(e3) {
      e3.DetectAtAudioStart = "DetectAtAudioStart", e3.DetectContinuous = "DetectContinuous", e3.DetectSegments = "DetectSegments";
    }(t2.LanguageIdDetectionMode || (t2.LanguageIdDetectionMode = {})), function(e3) {
      e3.Auto = "Auto", e3.PrioritizeLatency = "PrioritizeLatency", e3.PrioritizeAccuracy = "PrioritizeAccuracy";
    }(t2.LanguageIdDetectionPriority || (t2.LanguageIdDetectionPriority = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.NextAction = void 0, function(e3) {
      e3.Recognize = "Recognize", e3.None = "None";
    }(t2.NextAction || (t2.NextAction = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.OnUnknownAction = void 0, function(e3) {
      e3.RecognizeWithDefaultLanguage = "RecognizeWithDefaultLanguage", e3.None = "None";
    }(t2.OnUnknownAction || (t2.OnUnknownAction = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ResultType = void 0, function(e3) {
      e3.Auto = "Auto", e3.StableFragment = "StableFragment", e3.Hypothesis = "Hypothesis", e3.None = "None";
    }(t2.ResultType || (t2.ResultType = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.PhraseResultOutputType = void 0, function(e3) {
      e3.Always = "Always", e3.None = "None";
    }(t2.PhraseResultOutputType || (t2.PhraseResultOutputType = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.NextAction = void 0, function(e3) {
      e3.None = "None", e3.Translate = "Translate";
    }(t2.NextAction || (t2.NextAction = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationServiceRecognizer = void 0;
    const i2 = r2(80), n = r2(2);
    class s extends n.ServiceRecognizerBase {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, i3, n2), this.handleSpeechPhraseMessage = async (e4) => this.handleSpeechPhrase(e4), this.handleSpeechHypothesisMessage = (e4) => this.handleSpeechHypothesis(e4);
      }
      processTypeSpecificMessages(e3) {
      }
      handleRecognizedCallback(e3, t3, r3) {
      }
      handleRecognizingCallback(e3, t3, r3) {
      }
      async processSpeechMessages(e3) {
        let t3 = false;
        switch (e3.path.toLowerCase()) {
          case "speech.hypothesis":
          case "speech.fragment":
            this.handleSpeechHypothesisMessage && this.handleSpeechHypothesisMessage(e3.textBody), t3 = true;
            break;
          case "speech.phrase":
            this.handleSpeechPhraseMessage && await this.handleSpeechPhraseMessage(e3.textBody), t3 = true;
        }
        return t3;
      }
      cancelRecognition(e3, t3, r3, i3, n2) {
      }
      async handleSpeechPhrase(e3) {
        const t3 = n.SimpleSpeechPhrase.fromJSON(e3, this.privRequestSession.currentTurnAudioOffset), r3 = n.EnumTranslation.implTranslateRecognitionResult(t3.RecognitionStatus);
        let s2;
        const o = new i2.PropertyCollection();
        if (o.setProperty(i2.PropertyId.SpeechServiceResponse_JsonResult, e3), this.privRequestSession.onPhraseRecognized(t3.Offset + t3.Duration), i2.ResultReason.Canceled === r3) {
          const e4 = n.EnumTranslation.implTranslateCancelResult(t3.RecognitionStatus), r4 = n.EnumTranslation.implTranslateCancelErrorCode(t3.RecognitionStatus);
          await this.cancelRecognitionLocal(e4, r4, n.EnumTranslation.implTranslateErrorDetails(r4));
        } else if (t3.RecognitionStatus !== n.RecognitionStatus.EndOfDictation) {
          if (this.privRecognizerConfig.parameters.getProperty(n.OutputFormatPropertyName) === i2.OutputFormat[i2.OutputFormat.Simple]) s2 = new i2.SpeechRecognitionResult(this.privRequestSession.requestId, r3, t3.DisplayText, t3.Duration, t3.Offset, t3.Language, t3.LanguageDetectionConfidence, t3.SpeakerId, void 0, t3.asJson(), o);
          else {
            const t4 = n.DetailedSpeechPhrase.fromJSON(e3, this.privRequestSession.currentTurnAudioOffset);
            s2 = new i2.SpeechRecognitionResult(this.privRequestSession.requestId, r3, t4.Text, t4.Duration, t4.Offset, t4.Language, t4.LanguageDetectionConfidence, t4.SpeakerId, void 0, t4.asJson(), o);
          }
          this.handleRecognizedCallback(s2, s2.offset, this.privRequestSession.sessionId);
        }
      }
      handleSpeechHypothesis(e3) {
        const t3 = n.SpeechHypothesis.fromJSON(e3, this.privRequestSession.currentTurnAudioOffset), r3 = new i2.PropertyCollection();
        r3.setProperty(i2.PropertyId.SpeechServiceResponse_JsonResult, e3);
        const s2 = new i2.SpeechRecognitionResult(this.privRequestSession.requestId, i2.ResultReason.RecognizingSpeech, t3.Text, t3.Duration, t3.Offset, t3.Language, t3.LanguageDetectionConfidence, t3.SpeakerId, void 0, t3.asJson(), r3);
        this.privRequestSession.onHypothesis(t3.Offset), this.handleRecognizingCallback(s2, t3.Duration, this.privRequestSession.sessionId);
      }
    }
    t2.ConversationServiceRecognizer = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RecognizerConfig = t2.SpeechResultFormat = void 0;
    const i2 = r2(80), n = r2(2), s = r2(111);
    !function(e3) {
      e3[e3.Simple = 0] = "Simple", e3[e3.Detailed = 1] = "Detailed";
    }(t2.SpeechResultFormat || (t2.SpeechResultFormat = {}));
    t2.RecognizerConfig = class {
      constructor(e3, t3) {
        this.privSpeechServiceConfig = e3 || new n.SpeechServiceConfig(new n.Context(null)), this.privParameters = t3, this.privMaxRetryCount = parseInt(t3.getProperty("SPEECH-Error-MaxRetryCount", "4"), 10), this.privLanguageIdMode = t3.getProperty(i2.PropertyId.SpeechServiceConnection_LanguageIdMode, void 0), this.privEnableSpeakerId = false;
      }
      get parameters() {
        return this.privParameters;
      }
      get recognitionMode() {
        return this.privRecognitionMode;
      }
      set recognitionMode(e3) {
        this.privRecognitionMode = e3, this.privRecognitionActivityTimeout = e3 === s.RecognitionMode.Interactive ? 8e3 : 25e3, this.privSpeechServiceConfig.Recognition = s.RecognitionMode[e3];
      }
      get SpeechServiceConfig() {
        return this.privSpeechServiceConfig;
      }
      get recognitionActivityTimeout() {
        return this.privRecognitionActivityTimeout;
      }
      get isContinuousRecognition() {
        return this.privRecognitionMode !== s.RecognitionMode.Interactive;
      }
      get languageIdMode() {
        return this.privLanguageIdMode;
      }
      get autoDetectSourceLanguages() {
        return this.parameters.getProperty(i2.PropertyId.SpeechServiceConnection_AutoDetectSourceLanguages, void 0);
      }
      get recognitionEndpointVersion() {
        return this.parameters.getProperty(i2.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, "2");
      }
      set recognitionEndpointVersion(e3) {
        this.parameters.setProperty(i2.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, e3);
      }
      get sourceLanguageModels() {
        const e3 = [];
        let t3 = false;
        if (void 0 !== this.autoDetectSourceLanguages) for (const r3 of this.autoDetectSourceLanguages.split(",")) {
          const n2 = r3 + i2.PropertyId.SpeechServiceConnection_EndpointId.toString(), s2 = this.parameters.getProperty(n2, void 0);
          void 0 !== s2 ? (e3.push({ language: r3, endpoint: s2 }), t3 = true) : e3.push({ language: r3, endpoint: "" });
        }
        return t3 ? e3 : void 0;
      }
      get maxRetryCount() {
        return this.privMaxRetryCount;
      }
      get isSpeakerDiarizationEnabled() {
        return this.privEnableSpeakerId;
      }
      set isSpeakerDiarizationEnabled(e3) {
        this.privEnableSpeakerId = e3;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.WebsocketMessageFormatter = void 0;
    const i2 = r2(4);
    t2.WebsocketMessageFormatter = class {
      toConnectionMessage(e3) {
        const t3 = new i2.Deferred();
        try {
          if (e3.messageType === i2.MessageType.Text) {
            const r3 = e3.textContent;
            let n = {}, s = null;
            if (r3) {
              const e4 = r3.split("\r\n\r\n");
              e4 && e4.length > 0 && (n = this.parseHeaders(e4[0]), e4.length > 1 && (s = e4[1]));
            }
            t3.resolve(new i2.ConnectionMessage(e3.messageType, s, n, e3.id));
          } else if (e3.messageType === i2.MessageType.Binary) {
            const r3 = e3.binaryContent;
            let n = {}, s = null;
            if (!r3 || r3.byteLength < 2) throw new Error("Invalid binary message format. Header length missing.");
            const o = new DataView(r3), a = o.getInt16(0);
            if (r3.byteLength < a + 2) throw new Error("Invalid binary message format. Header content missing.");
            let c = "";
            for (let e4 = 0; e4 < a; e4++) c += String.fromCharCode(o.getInt8(e4 + 2));
            n = this.parseHeaders(c), r3.byteLength > a + 2 && (s = r3.slice(2 + a)), t3.resolve(new i2.ConnectionMessage(e3.messageType, s, n, e3.id));
          }
        } catch (e4) {
          t3.reject(`Error formatting the message. Error: ${e4}`);
        }
        return t3.promise;
      }
      fromConnectionMessage(e3) {
        const t3 = new i2.Deferred();
        try {
          if (e3.messageType === i2.MessageType.Text) {
            const r3 = `${this.makeHeaders(e3)}\r
${e3.textBody ? e3.textBody : ""}`;
            t3.resolve(new i2.RawWebsocketMessage(i2.MessageType.Text, r3, e3.id));
          } else if (e3.messageType === i2.MessageType.Binary) {
            const r3 = this.makeHeaders(e3), n = e3.binaryBody, s = this.stringToArrayBuffer(r3), o = new Int8Array(s), a = o.byteLength, c = new Int8Array(2 + a + (n ? n.byteLength : 0));
            if (c[0] = a >> 8 & 255, c[1] = 255 & a, c.set(o, 2), n) {
              const e4 = new Int8Array(n);
              c.set(e4, 2 + a);
            }
            const p = c.buffer;
            t3.resolve(new i2.RawWebsocketMessage(i2.MessageType.Binary, p, e3.id));
          }
        } catch (e4) {
          t3.reject(`Error formatting the message. ${e4}`);
        }
        return t3.promise;
      }
      makeHeaders(e3) {
        let t3 = "";
        if (e3.headers) for (const r3 in e3.headers) r3 && (t3 += `${r3}: ${e3.headers[r3]}\r
`);
        return t3;
      }
      parseHeaders(e3) {
        const t3 = {};
        if (e3) {
          const r3 = e3.match(/[^\r\n]+/g);
          if (t3) {
            for (const e4 of r3) if (e4) {
              const r4 = e4.indexOf(":"), i3 = r4 > 0 ? e4.substr(0, r4).trim().toLowerCase() : e4, n = r4 > 0 && e4.length > r4 + 1 ? e4.substr(r4 + 1).trim() : "";
              t3[i3] = n;
            }
          }
        }
        return t3;
      }
      stringToArrayBuffer(e3) {
        const t3 = new ArrayBuffer(e3.length), r3 = new DataView(t3);
        for (let t4 = 0; t4 < e3.length; t4++) r3.setUint8(t4, e3.charCodeAt(t4));
        return t3;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechConnectionFactory = void 0;
    const i2 = r2(61), n = r2(2), s = r2(80), o = r2(130), a = r2(2), c = r2(54), p = r2(131), h = r2(111);
    class u extends o.ConnectionFactoryBase {
      constructor() {
        super(...arguments), this.interactiveRelativeUri = "/speech/recognition/interactive/cognitiveservices/v1", this.conversationRelativeUri = "/speech/recognition/conversation/cognitiveservices/v1", this.dictationRelativeUri = "/speech/recognition/dictation/cognitiveservices/v1", this.universalUri = "/stt/speech/universal/v";
      }
      async create(e3, t3, r3) {
        let u2 = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Endpoint, void 0);
        const d = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Region, void 0), v = o.ConnectionFactoryBase.getHostSuffix(d), l = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Host, "wss://" + d + ".stt.speech" + v), g = {}, m = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_EndpointId, void 0), S = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage, void 0);
        if (m ? u2 && -1 !== u2.search(p.QueryParameterNames.CustomSpeechDeploymentId) || (g[p.QueryParameterNames.CustomSpeechDeploymentId] = m) : S && (u2 && -1 !== u2.search(p.QueryParameterNames.Language) || (g[p.QueryParameterNames.Language] = S)), u2 && -1 !== u2.search(p.QueryParameterNames.Format) || (g[p.QueryParameterNames.Format] = e3.parameters.getProperty(n.OutputFormatPropertyName, s.OutputFormat[s.OutputFormat.Simple]).toLowerCase()), void 0 !== e3.autoDetectSourceLanguages && (g[p.QueryParameterNames.EnableLanguageId] = "true"), this.setCommonUrlParams(e3, g, u2), u2) {
          const t4 = new URL(u2), r4 = t4.pathname;
          "" !== r4 && "/" !== r4 || (t4.pathname = this.universalUri + e3.recognitionEndpointVersion, u2 = await o.ConnectionFactoryBase.getRedirectUrlFromEndpoint(t4.toString()));
        }
        if (!u2) switch (e3.recognitionMode) {
          case h.RecognitionMode.Conversation:
            u2 = "true" === e3.parameters.getProperty(n.ForceDictationPropertyName, "false") ? l + this.dictationRelativeUri : void 0 !== e3.recognitionEndpointVersion && parseInt(e3.recognitionEndpointVersion, 10) > 1 ? `${l}${this.universalUri}${e3.recognitionEndpointVersion}` : l + this.conversationRelativeUri;
            break;
          case h.RecognitionMode.Dictation:
            u2 = l + this.dictationRelativeUri;
            break;
          default:
            u2 = void 0 !== e3.recognitionEndpointVersion && parseInt(e3.recognitionEndpointVersion, 10) > 1 ? `${l}${this.universalUri}${e3.recognitionEndpointVersion}` : l + this.interactiveRelativeUri;
        }
        const f = {};
        void 0 !== t3.token && "" !== t3.token && (f[t3.headerName] = t3.token), f[c.HeaderNames.ConnectionId] = r3, f.connectionId = r3;
        const y = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false"), C = new i2.WebsocketConnection(u2, g, f, new a.WebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), y, r3), P = C.uri;
        return e3.parameters.setProperty(s.PropertyId.SpeechServiceConnection_Url, P), C;
      }
    }
    t2.SpeechConnectionFactory = u;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranscriberConnectionFactory = void 0;
    const i2 = r2(61), n = r2(80), s = r2(2), o = r2(130), a = r2(2), c = r2(54), p = r2(131);
    class h extends o.ConnectionFactoryBase {
      constructor() {
        super(...arguments), this.universalUri = "/stt/speech/universal/v2", this.conversationRelativeUriV1 = "/speech/recognition/conversation/cognitiveservices/v1";
      }
      async create(e3, t3, r3) {
        let s2 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Endpoint, void 0);
        const h2 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Region, void 0), u = o.ConnectionFactoryBase.getHostSuffix(h2), d = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Host, "wss://" + h2 + ".stt.speech" + u), v = {}, l = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_EndpointId, void 0), g = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_RecoLanguage, void 0);
        l ? s2 && -1 !== s2.search(p.QueryParameterNames.CustomSpeechDeploymentId) || (v[p.QueryParameterNames.CustomSpeechDeploymentId] = l) : g && (s2 && -1 !== s2.search(p.QueryParameterNames.Language) || (v[p.QueryParameterNames.Language] = g)), void 0 !== e3.autoDetectSourceLanguages && (v[p.QueryParameterNames.EnableLanguageId] = "true");
        if ("1" === e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_RecognitionEndpointVersion, void 0)) s2 = `${d}${this.universalUri}`;
        else {
          if (this.setV2UrlParams(e3, v, s2), s2) {
            const e4 = new URL(s2), t4 = e4.pathname;
            "" !== t4 && "/" !== t4 || (e4.pathname = this.universalUri, s2 = await o.ConnectionFactoryBase.getRedirectUrlFromEndpoint(e4.toString()));
          }
          s2 || (s2 = `${d}${this.conversationRelativeUriV1}`);
        }
        const m = {};
        void 0 !== t3.token && "" !== t3.token && (m[t3.headerName] = t3.token), m[c.HeaderNames.ConnectionId] = r3;
        const S = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false"), f = new i2.WebsocketConnection(s2, v, m, new a.WebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), S, r3), y = f.uri;
        return e3.parameters.setProperty(n.PropertyId.SpeechServiceConnection_Url, y), f;
      }
      setV2UrlParams(e3, t3, r3) {
        (/* @__PURE__ */ new Map([[n.PropertyId.Speech_SegmentationSilenceTimeoutMs, p.QueryParameterNames.SegmentationSilenceTimeoutMs], [n.PropertyId.SpeechServiceConnection_EnableAudioLogging, p.QueryParameterNames.EnableAudioLogging], [n.PropertyId.SpeechServiceConnection_EndSilenceTimeoutMs, p.QueryParameterNames.EndSilenceTimeoutMs], [n.PropertyId.SpeechServiceConnection_InitialSilenceTimeoutMs, p.QueryParameterNames.InitialSilenceTimeoutMs], [n.PropertyId.SpeechServiceResponse_PostProcessingOption, p.QueryParameterNames.Postprocessing], [n.PropertyId.SpeechServiceResponse_ProfanityOption, p.QueryParameterNames.Profanity], [n.PropertyId.SpeechServiceResponse_StablePartialResultThreshold, p.QueryParameterNames.StableIntermediateThreshold]])).forEach((i4, n2) => {
          this.setUrlParameter(n2, i4, e3, t3, r3);
        });
        const i3 = JSON.parse(e3.parameters.getProperty(s.ServicePropertiesPropertyName, "{}"));
        Object.keys(i3).forEach((e4) => {
          t3[e4] = i3[e4];
        });
      }
    }
    t2.ConversationTranscriberConnectionFactory = h;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranscriberConnectionFactory = void 0;
    const i2 = r2(61), n = r2(80), s = r2(130), o = r2(2), a = r2(54), c = r2(131);
    class p extends s.ConnectionFactoryBase {
      constructor() {
        super(...arguments), this.multiaudioRelativeUri = "/speech/recognition/multiaudio";
      }
      create(e3, t3, r3) {
        let c2 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Endpoint, void 0);
        const p2 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Region, "centralus"), h = "wss://transcribe." + p2 + ".cts.speech" + s.ConnectionFactoryBase.getHostSuffix(p2) + this.multiaudioRelativeUri, u = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Host, h), d = {};
        this.setQueryParams(d, e3, c2), c2 || (c2 = u);
        const v = {};
        void 0 !== t3.token && "" !== t3.token && (v[t3.headerName] = t3.token), v[a.HeaderNames.ConnectionId] = r3, e3.parameters.setProperty(n.PropertyId.SpeechServiceConnection_Url, c2);
        const l = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false");
        return Promise.resolve(new i2.WebsocketConnection(c2, d, v, new o.WebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), l, r3));
      }
      setQueryParams(e3, t3, r3) {
        const i3 = t3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_EndpointId, void 0), s2 = t3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_RecoLanguage, void 0);
        i3 && !(c.QueryParameterNames.CustomSpeechDeploymentId in e3) && (e3[c.QueryParameterNames.CustomSpeechDeploymentId] = i3), s2 && !(c.QueryParameterNames.Language in e3) && (e3[c.QueryParameterNames.Language] = s2);
        const a2 = "true" === t3.parameters.getProperty(n.PropertyId.SpeechServiceResponse_RequestWordLevelTimestamps, "false").toLowerCase(), p2 = t3.parameters.getProperty(o.OutputFormatPropertyName, n.OutputFormat[n.OutputFormat.Simple]) !== n.OutputFormat[n.OutputFormat.Simple];
        (a2 || p2) && (e3[c.QueryParameterNames.Format] = n.OutputFormat[n.OutputFormat.Detailed].toLowerCase()), this.setCommonUrlParams(t3, e3, r3);
      }
    }
    t2.TranscriberConnectionFactory = p;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationConnectionFactory = void 0;
    const i2 = r2(61), n = r2(153), s = r2(80), o = r2(130), a = r2(2), c = r2(54), p = r2(131), h = r2(111);
    class u extends o.ConnectionFactoryBase {
      constructor() {
        super(...arguments), this.universalUri = "/stt/speech/universal/v2", this.translationV1Uri = "/speech/translation/cognitiveservices/v1";
      }
      async create(e3, t3, r3) {
        let n2 = this.getEndpointUrl(e3);
        const p2 = {};
        if (this.setQueryParams(p2, e3, n2), n2) {
          const e4 = new URL(n2), t4 = e4.pathname;
          "" !== t4 && "/" !== t4 || (e4.pathname = this.universalUri, n2 = await o.ConnectionFactoryBase.getRedirectUrlFromEndpoint(e4.toString()));
        }
        const h2 = {};
        void 0 !== t3.token && "" !== t3.token && (h2[t3.headerName] = t3.token), h2[c.HeaderNames.ConnectionId] = r3, e3.parameters.setProperty(s.PropertyId.SpeechServiceConnection_Url, n2);
        const u2 = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false");
        return new i2.WebsocketConnection(n2, p2, h2, new a.WebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), u2, r3);
      }
      getEndpointUrl(e3, t3) {
        const r3 = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Region), i3 = o.ConnectionFactoryBase.getHostSuffix(r3);
        let a2 = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Endpoint, void 0);
        if (a2) return true === t3 ? a2 : n.StringUtils.formatString(a2, { region: r3 });
        if ("true" === e3.parameters.getProperty("SPEECH-ForceV1Endpoint", "false")) {
          a2 = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Host, "wss://{region}.s2s.speech" + i3) + this.translationV1Uri;
        } else {
          a2 = e3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_Host, "wss://{region}.stt.speech" + i3) + this.universalUri;
        }
        return true === t3 ? a2 : n.StringUtils.formatString(a2, { region: r3 });
      }
      setQueryParams(e3, t3, r3) {
        e3.from = t3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage), e3.to = t3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationToLanguages), e3.scenario = t3.recognitionMode === h.RecognitionMode.Interactive ? "interactive" : t3.recognitionMode === h.RecognitionMode.Conversation ? "conversation" : "", this.setCommonUrlParams(t3, e3, r3), this.setUrlParameter(s.PropertyId.SpeechServiceResponse_TranslationRequestStablePartialResult, p.QueryParameterNames.StableTranslation, t3, e3, r3);
        const i3 = t3.parameters.getProperty(s.PropertyId.SpeechServiceConnection_TranslationVoice, void 0);
        void 0 !== i3 && (e3.voice = i3, e3.features = "requireVoice");
      }
    }
    t2.TranslationConnectionFactory = u;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.EnumTranslation = void 0;
    const i2 = r2(80), n = r2(2);
    t2.EnumTranslation = class {
      static implTranslateRecognitionResult(e3, t3 = false) {
        let r3 = i2.ResultReason.Canceled;
        switch (e3) {
          case n.RecognitionStatus.Success:
            r3 = i2.ResultReason.RecognizedSpeech;
            break;
          case n.RecognitionStatus.EndOfDictation:
            r3 = t3 ? i2.ResultReason.RecognizedSpeech : i2.ResultReason.NoMatch;
            break;
          case n.RecognitionStatus.NoMatch:
          case n.RecognitionStatus.InitialSilenceTimeout:
          case n.RecognitionStatus.BabbleTimeout:
            r3 = i2.ResultReason.NoMatch;
            break;
          case n.RecognitionStatus.Error:
          case n.RecognitionStatus.BadRequest:
          case n.RecognitionStatus.Forbidden:
          default:
            r3 = i2.ResultReason.Canceled;
        }
        return r3;
      }
      static implTranslateCancelResult(e3) {
        let t3 = i2.CancellationReason.EndOfStream;
        switch (e3) {
          case n.RecognitionStatus.Success:
          case n.RecognitionStatus.EndOfDictation:
          case n.RecognitionStatus.NoMatch:
            t3 = i2.CancellationReason.EndOfStream;
            break;
          case n.RecognitionStatus.InitialSilenceTimeout:
          case n.RecognitionStatus.BabbleTimeout:
          case n.RecognitionStatus.Error:
          case n.RecognitionStatus.BadRequest:
          case n.RecognitionStatus.Forbidden:
          default:
            t3 = i2.CancellationReason.Error;
        }
        return t3;
      }
      static implTranslateCancelErrorCode(e3) {
        let t3 = i2.CancellationErrorCode.NoError;
        switch (e3) {
          case n.RecognitionStatus.Error:
            t3 = i2.CancellationErrorCode.ServiceError;
            break;
          case n.RecognitionStatus.TooManyRequests:
            t3 = i2.CancellationErrorCode.TooManyRequests;
            break;
          case n.RecognitionStatus.BadRequest:
            t3 = i2.CancellationErrorCode.BadRequestParameters;
            break;
          case n.RecognitionStatus.Forbidden:
            t3 = i2.CancellationErrorCode.Forbidden;
            break;
          default:
            t3 = i2.CancellationErrorCode.NoError;
        }
        return t3;
      }
      static implTranslateErrorDetails(e3) {
        let t3 = "The speech service encountered an internal error and could not continue.";
        switch (e3) {
          case i2.CancellationErrorCode.Forbidden:
            t3 = "The recognizer is using a free subscription that ran out of quota.";
            break;
          case i2.CancellationErrorCode.BadRequestParameters:
            t3 = "Invalid parameter or unsupported audio format in the request.";
            break;
          case i2.CancellationErrorCode.TooManyRequests:
            t3 = "The number of parallel requests exceeded the number of allowed concurrent transcriptions.";
        }
        return t3;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RecognitionStatus = t2.SynthesisStatus = void 0, function(e3) {
      e3[e3.Success = 0] = "Success", e3[e3.SynthesisEnd = 1] = "SynthesisEnd", e3[e3.Error = 2] = "Error";
    }(t2.SynthesisStatus || (t2.SynthesisStatus = {})), function(e3) {
      e3[e3.Success = 0] = "Success", e3[e3.NoMatch = 1] = "NoMatch", e3[e3.InitialSilenceTimeout = 2] = "InitialSilenceTimeout", e3[e3.BabbleTimeout = 3] = "BabbleTimeout", e3[e3.Error = 4] = "Error", e3[e3.EndOfDictation = 5] = "EndOfDictation", e3[e3.TooManyRequests = 6] = "TooManyRequests", e3[e3.BadRequest = 7] = "BadRequest", e3[e3.Forbidden = 8] = "Forbidden";
    }(t2.RecognitionStatus || (t2.RecognitionStatus = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationSynthesisEnd = void 0;
    const i2 = r2(2);
    class n {
      constructor(e3) {
        this.privSynthesisEnd = JSON.parse(e3), this.privSynthesisEnd.SynthesisStatus && (this.privSynthesisEnd.SynthesisStatus = i2.SynthesisStatus[this.privSynthesisEnd.SynthesisStatus]), this.privSynthesisEnd.Status && (this.privSynthesisEnd.SynthesisStatus = i2.SynthesisStatus[this.privSynthesisEnd.Status]);
      }
      static fromJSON(e3) {
        return new n(e3);
      }
      get SynthesisStatus() {
        return this.privSynthesisEnd.SynthesisStatus;
      }
      get FailureReason() {
        return this.privSynthesisEnd.FailureReason;
      }
    }
    t2.TranslationSynthesisEnd = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationHypothesis = void 0;
    const i2 = r2(65), n = r2(48);
    class s {
      constructor(e3, t3) {
        this.privTranslationHypothesis = e3, this.privTranslationHypothesis.Offset += t3, this.privTranslationHypothesis.Translation.TranslationStatus = this.mapTranslationStatus(this.privTranslationHypothesis.Translation.TranslationStatus);
      }
      static fromJSON(e3, t3) {
        return new s(JSON.parse(e3), t3);
      }
      static fromTranslationResponse(e3, t3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "translationHypothesis");
        const r3 = e3.SpeechHypothesis;
        return e3.SpeechHypothesis = void 0, r3.Translation = e3, new s(r3, t3);
      }
      get Duration() {
        return this.privTranslationHypothesis.Duration;
      }
      get Offset() {
        return this.privTranslationHypothesis.Offset;
      }
      get Text() {
        return this.privTranslationHypothesis.Text;
      }
      get Translation() {
        return this.privTranslationHypothesis.Translation;
      }
      get Language() {
        return this.privTranslationHypothesis.PrimaryLanguage?.Language;
      }
      asJson() {
        const e3 = { ...this.privTranslationHypothesis };
        return void 0 !== e3.Translation ? JSON.stringify({ ...e3, TranslationStatus: n.TranslationStatus[e3.Translation.TranslationStatus] }) : JSON.stringify(e3);
      }
      mapTranslationStatus(e3) {
        return "string" == typeof e3 ? n.TranslationStatus[e3] : "number" == typeof e3 ? e3 : void 0;
      }
    }
    t2.TranslationHypothesis = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationPhrase = void 0;
    const i2 = r2(65), n = r2(2), s = r2(48);
    class o {
      constructor(e3, t3) {
        this.privTranslationPhrase = e3, this.privTranslationPhrase.Offset += t3, this.privTranslationPhrase.RecognitionStatus = this.mapRecognitionStatus(this.privTranslationPhrase.RecognitionStatus), void 0 !== this.privTranslationPhrase.Translation && (this.privTranslationPhrase.Translation.TranslationStatus = this.mapTranslationStatus(this.privTranslationPhrase.Translation.TranslationStatus));
      }
      static fromJSON(e3, t3) {
        return new o(JSON.parse(e3), t3);
      }
      static fromTranslationResponse(e3, t3) {
        i2.Contracts.throwIfNullOrUndefined(e3, "translationResponse");
        const r3 = e3.SpeechPhrase;
        return e3.SpeechPhrase = void 0, r3.Translation = e3, r3.Text = r3.DisplayText, new o(r3, t3);
      }
      get RecognitionStatus() {
        return this.privTranslationPhrase.RecognitionStatus;
      }
      get Offset() {
        return this.privTranslationPhrase.Offset;
      }
      get Duration() {
        return this.privTranslationPhrase.Duration;
      }
      get Text() {
        return this.privTranslationPhrase.Text;
      }
      get Language() {
        return this.privTranslationPhrase.PrimaryLanguage?.Language;
      }
      get Confidence() {
        return this.privTranslationPhrase.PrimaryLanguage?.Confidence;
      }
      get Translation() {
        return this.privTranslationPhrase.Translation;
      }
      asJson() {
        const e3 = { ...this.privTranslationPhrase }, t3 = { ...e3, RecognitionStatus: n.RecognitionStatus[e3.RecognitionStatus] };
        return e3.Translation && (t3.Translation = { ...e3.Translation, TranslationStatus: s.TranslationStatus[e3.Translation.TranslationStatus] }), JSON.stringify(t3);
      }
      mapRecognitionStatus(e3) {
        return "string" == typeof e3 ? n.RecognitionStatus[e3] : "number" == typeof e3 ? e3 : void 0;
      }
      mapTranslationStatus(e3) {
        return "string" == typeof e3 ? s.TranslationStatus[e3] : "number" == typeof e3 ? e3 : void 0;
      }
    }
    t2.TranslationPhrase = o;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranslationServiceRecognizer = void 0;
    const i2 = r2(4), n = r2(80), s = r2(2);
    class o extends s.ConversationServiceRecognizer {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, i3, n2), this.privTranslationRecognizer = n2, this.connectionEvents.attach((e4) => {
          "ConnectionEstablishedEvent" === e4.name && this.privTranslationRecognizer.onConnection();
        });
      }
      async processTypeSpecificMessages(e3) {
        const t3 = new n.PropertyCollection();
        let r3 = await this.processSpeechMessages(e3);
        if (r3) return true;
        const o2 = async (e4) => {
          if (t3.setProperty(n.PropertyId.SpeechServiceResponse_JsonResult, e4.asJson()), this.privRequestSession.onPhraseRecognized(e4.Offset + e4.Duration), e4.RecognitionStatus === s.RecognitionStatus.Success) {
            const r4 = this.fireEventForResult(e4, t3);
            if (this.privTranslationRecognizer.recognized) try {
              this.privTranslationRecognizer.recognized(this.privTranslationRecognizer, r4);
            } catch (e5) {
            }
            if (this.privSuccessCallback) {
              try {
                this.privSuccessCallback(r4.result);
              } catch (e5) {
                this.privErrorCallback && this.privErrorCallback(e5);
              }
              this.privSuccessCallback = void 0, this.privErrorCallback = void 0;
            }
          } else {
            const i3 = s.EnumTranslation.implTranslateRecognitionResult(e4.RecognitionStatus), o3 = new n.TranslationRecognitionResult(void 0, this.privRequestSession.requestId, i3, e4.Text, e4.Duration, e4.Offset, e4.Language, e4.Confidence, void 0, e4.asJson(), t3);
            if (i3 === n.ResultReason.Canceled) {
              const t4 = s.EnumTranslation.implTranslateCancelResult(e4.RecognitionStatus), r4 = s.EnumTranslation.implTranslateCancelErrorCode(e4.RecognitionStatus);
              await this.cancelRecognitionLocal(t4, r4, s.EnumTranslation.implTranslateErrorDetails(r4));
            } else if (e4.RecognitionStatus !== s.RecognitionStatus.EndOfDictation) {
              const e5 = new n.TranslationRecognitionEventArgs(o3, o3.offset, this.privRequestSession.sessionId);
              if (this.privTranslationRecognizer.recognized) try {
                this.privTranslationRecognizer.recognized(this.privTranslationRecognizer, e5);
              } catch (e6) {
              }
              if (this.privSuccessCallback) {
                try {
                  this.privSuccessCallback(o3);
                } catch (e6) {
                  this.privErrorCallback && this.privErrorCallback(e6);
                }
                this.privSuccessCallback = void 0, this.privErrorCallback = void 0;
              }
            }
            r3 = true;
          }
        }, a = (e4) => {
          t3.setProperty(n.PropertyId.SpeechServiceResponse_JsonResult, e4.asJson());
          const i3 = this.fireEventForResult(e4, t3);
          if (this.privRequestSession.onHypothesis(i3.offset), this.privTranslationRecognizer.recognizing) try {
            this.privTranslationRecognizer.recognizing(this.privTranslationRecognizer, i3);
          } catch (e5) {
          }
          r3 = true;
        };
        switch (e3.messageType === i2.MessageType.Text && t3.setProperty(n.PropertyId.SpeechServiceResponse_JsonResult, e3.textBody), e3.path.toLowerCase()) {
          case "translation.hypothesis":
            a(s.TranslationHypothesis.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset));
            break;
          case "translation.response":
            const t4 = JSON.parse(e3.textBody);
            if (t4.SpeechPhrase) await o2(s.TranslationPhrase.fromTranslationResponse(t4, this.privRequestSession.currentTurnAudioOffset));
            else {
              const t5 = JSON.parse(e3.textBody);
              t5.SpeechHypothesis && a(s.TranslationHypothesis.fromTranslationResponse(t5, this.privRequestSession.currentTurnAudioOffset));
            }
            break;
          case "translation.phrase":
            await o2(s.TranslationPhrase.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset));
            break;
          case "translation.synthesis":
          case "audio":
            this.sendSynthesisAudio(e3.binaryBody, this.privRequestSession.sessionId), r3 = true;
            break;
          case "audio.end":
          case "translation.synthesis.end":
            const i3 = s.TranslationSynthesisEnd.fromJSON(e3.textBody);
            switch (i3.SynthesisStatus) {
              case s.SynthesisStatus.Error:
                if (this.privTranslationRecognizer.synthesizing) {
                  const e4 = new n.TranslationSynthesisResult(n.ResultReason.Canceled, void 0), t5 = new n.TranslationSynthesisEventArgs(e4, this.privRequestSession.sessionId);
                  try {
                    this.privTranslationRecognizer.synthesizing(this.privTranslationRecognizer, t5);
                  } catch (e5) {
                  }
                }
                if (this.privTranslationRecognizer.canceled) {
                  const e4 = new n.TranslationRecognitionCanceledEventArgs(this.privRequestSession.sessionId, n.CancellationReason.Error, i3.FailureReason, n.CancellationErrorCode.ServiceError, null);
                  try {
                    this.privTranslationRecognizer.canceled(this.privTranslationRecognizer, e4);
                  } catch (e5) {
                  }
                }
                break;
              case s.SynthesisStatus.Success:
                this.sendSynthesisAudio(void 0, this.privRequestSession.sessionId);
            }
            r3 = true;
        }
        return r3;
      }
      cancelRecognition(e3, t3, r3, i3, o2) {
        const a = new n.PropertyCollection();
        if (a.setProperty(s.CancellationErrorCodePropertyName, n.CancellationErrorCode[i3]), this.privTranslationRecognizer.canceled) {
          const t4 = new n.TranslationRecognitionCanceledEventArgs(e3, r3, o2, i3, void 0);
          try {
            this.privTranslationRecognizer.canceled(this.privTranslationRecognizer, t4);
          } catch {
          }
        }
        if (this.privSuccessCallback) {
          const e4 = new n.TranslationRecognitionResult(void 0, t3, n.ResultReason.Canceled, void 0, void 0, void 0, void 0, void 0, o2, void 0, a);
          try {
            this.privSuccessCallback(e4), this.privSuccessCallback = void 0;
          } catch {
          }
        }
      }
      handleRecognizingCallback(e3, t3, r3) {
        try {
          const i3 = new n.TranslationRecognitionEventArgs(n.TranslationRecognitionResult.fromSpeechRecognitionResult(e3), t3, r3);
          this.privTranslationRecognizer.recognizing(this.privTranslationRecognizer, i3);
        } catch (e4) {
        }
      }
      handleRecognizedCallback(e3, t3, r3) {
        try {
          const i3 = new n.TranslationRecognitionEventArgs(n.TranslationRecognitionResult.fromSpeechRecognitionResult(e3), t3, r3);
          this.privTranslationRecognizer.recognized(this.privTranslationRecognizer, i3);
        } catch (e4) {
        }
      }
      fireEventForResult(e3, t3) {
        let r3, o2, a;
        if (void 0 !== e3.Translation.Translations) {
          r3 = new n.Translations();
          for (const t4 of e3.Translation.Translations) r3.set(t4.Language, t4.Text || t4.DisplayText);
        }
        e3 instanceof s.TranslationPhrase ? (o2 = e3.Translation && e3.Translation.TranslationStatus === i2.TranslationStatus.Success ? n.ResultReason.TranslatedSpeech : n.ResultReason.RecognizedSpeech, a = e3.Confidence) : o2 = n.ResultReason.TranslatingSpeech;
        const c = e3.Language, p = new n.TranslationRecognitionResult(r3, this.privRequestSession.requestId, o2, e3.Text, e3.Duration, e3.Offset, c, a, e3.Translation.FailureReason, e3.asJson(), t3);
        return new n.TranslationRecognitionEventArgs(p, e3.Offset, this.privRequestSession.sessionId);
      }
      sendSynthesisAudio(e3, t3) {
        const r3 = void 0 === e3 ? n.ResultReason.SynthesizingAudioCompleted : n.ResultReason.SynthesizingAudio, i3 = new n.TranslationSynthesisResult(r3, e3), s2 = new n.TranslationSynthesisEventArgs(i3, t3);
        if (this.privTranslationRecognizer.synthesizing) try {
          this.privTranslationRecognizer.synthesizing(this.privTranslationRecognizer, s2);
        } catch (e4) {
        }
      }
    }
    t2.TranslationServiceRecognizer = o;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechDetected = void 0;
    class r2 {
      constructor(e3, t3) {
        this.privSpeechStartDetected = JSON.parse(e3), this.privSpeechStartDetected.Offset += t3;
      }
      static fromJSON(e3, t3) {
        return new r2(e3, t3);
      }
      get Offset() {
        return this.privSpeechStartDetected.Offset;
      }
    }
    t2.SpeechDetected = r2;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechHypothesis = void 0;
    class r2 {
      constructor(e3, t3) {
        this.privSpeechHypothesis = JSON.parse(e3), this.updateOffset(t3);
      }
      static fromJSON(e3, t3) {
        return new r2(e3, t3);
      }
      updateOffset(e3) {
        this.privSpeechHypothesis.Offset += e3;
      }
      asJson() {
        return JSON.stringify(this.privSpeechHypothesis);
      }
      get Text() {
        return this.privSpeechHypothesis.Text;
      }
      get Offset() {
        return this.privSpeechHypothesis.Offset;
      }
      get Duration() {
        return this.privSpeechHypothesis.Duration;
      }
      get Language() {
        return void 0 === this.privSpeechHypothesis.PrimaryLanguage ? void 0 : this.privSpeechHypothesis.PrimaryLanguage.Language;
      }
      get LanguageDetectionConfidence() {
        return void 0 === this.privSpeechHypothesis.PrimaryLanguage ? void 0 : this.privSpeechHypothesis.PrimaryLanguage.Confidence;
      }
      get SpeakerId() {
        return this.privSpeechHypothesis.SpeakerId;
      }
    }
    t2.SpeechHypothesis = r2;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechKeyword = void 0;
    class r2 {
      constructor(e3, t3) {
        this.privSpeechKeyword = JSON.parse(e3), this.privSpeechKeyword.Offset += t3;
      }
      static fromJSON(e3, t3) {
        return new r2(e3, t3);
      }
      get Status() {
        return this.privSpeechKeyword.Status;
      }
      get Text() {
        return this.privSpeechKeyword.Text;
      }
      get Offset() {
        return this.privSpeechKeyword.Offset;
      }
      get Duration() {
        return this.privSpeechKeyword.Duration;
      }
      asJson() {
        return JSON.stringify(this.privSpeechKeyword);
      }
    }
    t2.SpeechKeyword = r2;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechServiceRecognizer = void 0;
    const i2 = r2(80), n = r2(2);
    class s extends n.ServiceRecognizerBase {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, i3, n2), this.privSpeechRecognizer = n2;
      }
      async processTypeSpecificMessages(e3) {
        let t3;
        const r3 = new i2.PropertyCollection();
        let s2 = false;
        switch (e3.path.toLowerCase()) {
          case "speech.hypothesis":
          case "speech.fragment":
            const o = n.SpeechHypothesis.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
            r3.setProperty(i2.PropertyId.SpeechServiceResponse_JsonResult, o.asJson()), t3 = new i2.SpeechRecognitionResult(this.privRequestSession.requestId, i2.ResultReason.RecognizingSpeech, o.Text, o.Duration, o.Offset, o.Language, o.LanguageDetectionConfidence, void 0, void 0, o.asJson(), r3), this.privRequestSession.onHypothesis(o.Offset);
            const a = new i2.SpeechRecognitionEventArgs(t3, o.Offset, this.privRequestSession.sessionId);
            if (this.privSpeechRecognizer.recognizing) try {
              this.privSpeechRecognizer.recognizing(this.privSpeechRecognizer, a);
            } catch (e4) {
            }
            s2 = true;
            break;
          case "speech.phrase":
            const c = n.SimpleSpeechPhrase.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
            r3.setProperty(i2.PropertyId.SpeechServiceResponse_JsonResult, c.asJson());
            const p = n.EnumTranslation.implTranslateRecognitionResult(c.RecognitionStatus, this.privExpectContentAssessmentResponse);
            if (this.privRequestSession.onPhraseRecognized(c.Offset + c.Duration), i2.ResultReason.Canceled === p) {
              const e4 = n.EnumTranslation.implTranslateCancelResult(c.RecognitionStatus), t4 = n.EnumTranslation.implTranslateCancelErrorCode(c.RecognitionStatus);
              await this.cancelRecognitionLocal(e4, t4, n.EnumTranslation.implTranslateErrorDetails(t4));
            } else {
              if (c.RecognitionStatus === n.RecognitionStatus.EndOfDictation) break;
              if (this.privRecognizerConfig.parameters.getProperty(n.OutputFormatPropertyName) === i2.OutputFormat[i2.OutputFormat.Simple]) t3 = new i2.SpeechRecognitionResult(this.privRequestSession.requestId, p, c.DisplayText, c.Duration, c.Offset, c.Language, c.LanguageDetectionConfidence, void 0, void 0, c.asJson(), r3);
              else {
                const s4 = n.DetailedSpeechPhrase.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
                r3.setProperty(i2.PropertyId.SpeechServiceResponse_JsonResult, s4.asJson()), t3 = new i2.SpeechRecognitionResult(this.privRequestSession.requestId, p, s4.RecognitionStatus === n.RecognitionStatus.Success ? s4.NBest[0].Display : "", s4.Duration, s4.Offset, s4.Language, s4.LanguageDetectionConfidence, void 0, void 0, s4.asJson(), r3);
              }
              const s3 = new i2.SpeechRecognitionEventArgs(t3, t3.offset, this.privRequestSession.sessionId);
              if (this.privSpeechRecognizer.recognized) try {
                this.privSpeechRecognizer.recognized(this.privSpeechRecognizer, s3);
              } catch (e4) {
              }
              if (this.privSuccessCallback) {
                try {
                  this.privSuccessCallback(t3);
                } catch (e4) {
                  this.privErrorCallback && this.privErrorCallback(e4);
                }
                this.privSuccessCallback = void 0, this.privErrorCallback = void 0;
              }
            }
            s2 = true;
        }
        return s2;
      }
      cancelRecognition(e3, t3, r3, s2, o) {
        const a = new i2.PropertyCollection();
        if (a.setProperty(n.CancellationErrorCodePropertyName, i2.CancellationErrorCode[s2]), this.privSpeechRecognizer.canceled) {
          const t4 = new i2.SpeechRecognitionCanceledEventArgs(r3, o, s2, void 0, e3);
          try {
            this.privSpeechRecognizer.canceled(this.privSpeechRecognizer, t4);
          } catch {
          }
        }
        if (this.privSuccessCallback) {
          const e4 = new i2.SpeechRecognitionResult(t3, i2.ResultReason.Canceled, void 0, void 0, void 0, void 0, void 0, void 0, o, void 0, a);
          try {
            this.privSuccessCallback(e4), this.privSuccessCallback = void 0;
          } catch {
          }
        }
      }
    }
    t2.SpeechServiceRecognizer = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranscriptionServiceRecognizer = void 0;
    const i2 = r2(80), n = r2(2), s = r2(219), o = r2(111);
    class a extends n.ServiceRecognizerBase {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, i3, n2), this.privConversationTranscriber = n2, this.setSpeakerDiarizationJson();
      }
      setSpeakerDiarizationJson() {
        if (this.privEnableSpeakerId) {
          const e3 = this.privSpeechContext.getContext().phraseDetection || {};
          e3.mode = o.RecognitionMode.Conversation;
          const t3 = {};
          t3.mode = s.SpeakerDiarizationMode.Anonymous, t3.audioSessionId = this.privDiarizationSessionId, t3.audioOffsetMs = 0, t3.diarizeIntermediates = "true" === this.privRecognizerConfig.parameters.getProperty(i2.PropertyId.SpeechServiceResponse_DiarizeIntermediateResults, "false"), e3.speakerDiarization = t3, this.privSpeechContext.getContext().phraseDetection = e3;
        }
      }
      async processTypeSpecificMessages(e3) {
        let t3;
        const r3 = new i2.PropertyCollection();
        r3.setProperty(i2.PropertyId.SpeechServiceResponse_JsonResult, e3.textBody);
        let s2 = false;
        switch (e3.path.toLowerCase()) {
          case "speech.hypothesis":
          case "speech.fragment":
            const o2 = n.SpeechHypothesis.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
            t3 = new i2.ConversationTranscriptionResult(this.privRequestSession.requestId, i2.ResultReason.RecognizingSpeech, o2.Text, o2.Duration, o2.Offset, o2.Language, o2.LanguageDetectionConfidence, o2.SpeakerId, void 0, o2.asJson(), r3), this.privRequestSession.onHypothesis(o2.Offset);
            const a2 = new i2.ConversationTranscriptionEventArgs(t3, o2.Duration, this.privRequestSession.sessionId);
            if (this.privConversationTranscriber.transcribing) try {
              this.privConversationTranscriber.transcribing(this.privConversationTranscriber, a2);
            } catch (e4) {
            }
            s2 = true;
            break;
          case "speech.phrase":
            const c = n.SimpleSpeechPhrase.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset), p = n.EnumTranslation.implTranslateRecognitionResult(c.RecognitionStatus);
            if (this.privRequestSession.onPhraseRecognized(c.Offset + c.Duration), i2.ResultReason.Canceled === p) {
              const e4 = n.EnumTranslation.implTranslateCancelResult(c.RecognitionStatus), t4 = n.EnumTranslation.implTranslateCancelErrorCode(c.RecognitionStatus);
              await this.cancelRecognitionLocal(e4, t4, n.EnumTranslation.implTranslateErrorDetails(t4));
            } else if (!this.privRequestSession.isSpeechEnded || p !== i2.ResultReason.NoMatch || c.RecognitionStatus === n.RecognitionStatus.InitialSilenceTimeout) {
              if (this.privRecognizerConfig.parameters.getProperty(n.OutputFormatPropertyName) === i2.OutputFormat[i2.OutputFormat.Simple]) t3 = new i2.ConversationTranscriptionResult(this.privRequestSession.requestId, p, c.DisplayText, c.Duration, c.Offset, c.Language, c.LanguageDetectionConfidence, c.SpeakerId, void 0, c.asJson(), r3);
              else {
                const s4 = n.DetailedSpeechPhrase.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
                t3 = new i2.ConversationTranscriptionResult(this.privRequestSession.requestId, p, s4.RecognitionStatus === n.RecognitionStatus.Success ? s4.NBest[0].Display : void 0, s4.Duration, s4.Offset, s4.Language, s4.LanguageDetectionConfidence, c.SpeakerId, void 0, s4.asJson(), r3);
              }
              const s3 = new i2.ConversationTranscriptionEventArgs(t3, t3.offset, this.privRequestSession.sessionId);
              if (this.privConversationTranscriber.transcribed) try {
                this.privConversationTranscriber.transcribed(this.privConversationTranscriber, s3);
              } catch (e4) {
              }
            }
            s2 = true;
        }
        return s2;
      }
      cancelRecognition(e3, t3, r3, s2, o2) {
        if (new i2.PropertyCollection().setProperty(n.CancellationErrorCodePropertyName, i2.CancellationErrorCode[s2]), this.privConversationTranscriber.canceled) {
          const t4 = new i2.ConversationTranscriptionCanceledEventArgs(r3, o2, s2, void 0, e3);
          try {
            this.privConversationTranscriber.canceled(this.privConversationTranscriber, t4);
          } catch {
          }
        }
      }
    }
    t2.ConversationTranscriptionServiceRecognizer = a;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.IdentityProvider = t2.SpeakerDiarizationMode = void 0, function(e3) {
      e3.None = "None", e3.Identity = "Identity", e3.Anonymous = "Anonymous";
    }(t2.SpeakerDiarizationMode || (t2.SpeakerDiarizationMode = {})), function(e3) {
      e3.CallCenter = "CallCenter";
    }(t2.IdentityProvider || (t2.IdentityProvider = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranscriptionServiceRecognizer = void 0;
    const i2 = r2(4), n = r2(80), s = r2(2), o = r2(190);
    class a extends s.ConversationServiceRecognizer {
      constructor(e3, t3, r3, i3, s2) {
        super(e3, t3, r3, i3, s2), this.privTranscriberRecognizer = s2, this.sendPrePayloadJSONOverride = (e4) => this.sendTranscriptionStartJSON(e4), "true" === this.privRecognizerConfig.parameters.getProperty(n.PropertyId.SpeechServiceResponse_RequestWordLevelTimestamps) && this.privSpeechContext.setWordLevelTimings();
      }
      async sendSpeechEventAsync(e3, t3) {
        if (this.privRequestSession.isRecognizing) {
          const r3 = await this.fetchConnection();
          await this.sendSpeechEvent(r3, this.createSpeechEventPayload(e3, t3));
        }
      }
      async sendMeetingSpeechEventAsync(e3, t3) {
        if (this.privRequestSession.isRecognizing) {
          const r3 = await this.fetchConnection();
          await this.sendSpeechEvent(r3, this.createMeetingSpeechEventPayload(e3, t3));
        }
      }
      processTypeSpecificMessages(e3) {
        return this.processSpeechMessages(e3);
      }
      handleRecognizedCallback(e3, t3, r3) {
        try {
          const i3 = new n.SpeechRecognitionEventArgs(e3, t3, r3);
          if (this.privTranscriberRecognizer.recognized(this.privTranscriberRecognizer, i3), this.privSuccessCallback) {
            try {
              this.privSuccessCallback(e3);
            } catch (e4) {
              this.privErrorCallback && this.privErrorCallback(e4);
            }
            this.privSuccessCallback = void 0, this.privErrorCallback = void 0;
          }
        } catch (e4) {
        }
      }
      handleRecognizingCallback(e3, t3, r3) {
        try {
          const i3 = new n.SpeechRecognitionEventArgs(e3, t3, r3);
          this.privTranscriberRecognizer.recognizing(this.privTranscriberRecognizer, i3);
        } catch (e4) {
        }
      }
      cancelRecognition(e3, t3, r3, i3, o2) {
        const a2 = new n.PropertyCollection();
        if (a2.setProperty(s.CancellationErrorCodePropertyName, n.CancellationErrorCode[i3]), this.privTranscriberRecognizer.IsMeetingRecognizer()) {
          if (this.privTranscriberRecognizer.canceled) {
            const t4 = new n.MeetingTranscriptionCanceledEventArgs(r3, o2, i3, void 0, e3);
            try {
              this.privTranscriberRecognizer.canceled(this.privTranscriberRecognizer, t4);
            } catch {
            }
          }
        } else if (this.privTranscriberRecognizer.canceled) {
          const t4 = new n.ConversationTranscriptionCanceledEventArgs(r3, o2, i3, void 0, e3);
          try {
            this.privTranscriberRecognizer.canceled(this.privTranscriberRecognizer, t4);
          } catch {
          }
        }
        if (this.privSuccessCallback) {
          const e4 = new n.SpeechRecognitionResult(t3, n.ResultReason.Canceled, void 0, void 0, void 0, void 0, void 0, void 0, o2, void 0, a2);
          try {
            this.privSuccessCallback(e4), this.privSuccessCallback = void 0;
          } catch {
          }
        }
      }
      async sendTranscriptionStartJSON(e3) {
        if (await this.sendSpeechContext(e3, true), this.privTranscriberRecognizer.IsMeetingRecognizer()) {
          const t3 = this.privTranscriberRecognizer.getMeetingInfo(), r3 = this.createMeetingSpeechEventPayload(t3, "start");
          await this.sendSpeechEvent(e3, r3);
        } else {
          const t3 = this.privTranscriberRecognizer.getConversationInfo(), r3 = this.createSpeechEventPayload(t3, "start");
          await this.sendSpeechEvent(e3, r3);
        }
        await this.sendWaveHeader(e3);
      }
      sendSpeechEvent(e3, t3) {
        const r3 = JSON.stringify(t3);
        if (r3) return e3.send(new o.SpeechConnectionMessage(i2.MessageType.Text, "speech.event", this.privRequestSession.requestId, "application/json", r3));
      }
      createSpeechEventPayload(e3, t3) {
        const r3 = { id: "meeting", name: t3, meeting: e3.conversationProperties };
        return r3.meeting.id = e3.id, r3.meeting.attendees = e3.participants, r3;
      }
      createMeetingSpeechEventPayload(e3, t3) {
        const r3 = { id: "meeting", name: t3, meeting: e3.meetingProperties };
        return r3.meeting.id = e3.id, r3.meeting.attendees = e3.participants, r3;
      }
    }
    t2.TranscriptionServiceRecognizer = a;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DetailedSpeechPhrase = void 0;
    const i2 = r2(2);
    class n {
      constructor(e3, t3) {
        this.privDetailedSpeechPhrase = JSON.parse(e3), this.privDetailedSpeechPhrase.RecognitionStatus = this.mapRecognitionStatus(this.privDetailedSpeechPhrase.RecognitionStatus), this.updateOffsets(t3);
      }
      static fromJSON(e3, t3) {
        return new n(e3, t3);
      }
      updateOffsets(e3) {
        if (this.privDetailedSpeechPhrase.Offset += e3, this.privDetailedSpeechPhrase.NBest) for (const t3 of this.privDetailedSpeechPhrase.NBest) {
          if (t3.Words) for (const r3 of t3.Words) r3.Offset += e3;
          if (t3.DisplayWords) for (const r3 of t3.DisplayWords) r3.Offset += e3;
        }
      }
      asJson() {
        const e3 = { ...this.privDetailedSpeechPhrase };
        return JSON.stringify({ ...e3, RecognitionStatus: i2.RecognitionStatus[e3.RecognitionStatus] });
      }
      get RecognitionStatus() {
        return this.privDetailedSpeechPhrase.RecognitionStatus;
      }
      get NBest() {
        return this.privDetailedSpeechPhrase.NBest;
      }
      get Duration() {
        return this.privDetailedSpeechPhrase.Duration;
      }
      get Offset() {
        return this.privDetailedSpeechPhrase.Offset;
      }
      get Language() {
        return void 0 === this.privDetailedSpeechPhrase.PrimaryLanguage ? void 0 : this.privDetailedSpeechPhrase.PrimaryLanguage.Language;
      }
      get LanguageDetectionConfidence() {
        return void 0 === this.privDetailedSpeechPhrase.PrimaryLanguage ? void 0 : this.privDetailedSpeechPhrase.PrimaryLanguage.Confidence;
      }
      get Text() {
        return this.privDetailedSpeechPhrase.NBest && this.privDetailedSpeechPhrase.NBest[0] ? this.privDetailedSpeechPhrase.NBest[0].Display || this.privDetailedSpeechPhrase.NBest[0].DisplayText : this.privDetailedSpeechPhrase.DisplayText;
      }
      get SpeakerId() {
        return this.privDetailedSpeechPhrase.SpeakerId;
      }
      mapRecognitionStatus(e3) {
        return "string" == typeof e3 ? i2.RecognitionStatus[e3] : "number" == typeof e3 ? e3 : void 0;
      }
    }
    t2.DetailedSpeechPhrase = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SimpleSpeechPhrase = void 0;
    const i2 = r2(2);
    class n {
      constructor(e3, t3 = 0) {
        this.privSimpleSpeechPhrase = JSON.parse(e3), this.privSimpleSpeechPhrase.RecognitionStatus = this.mapRecognitionStatus(this.privSimpleSpeechPhrase.RecognitionStatus), this.updateOffset(t3);
      }
      static fromJSON(e3, t3) {
        return new n(e3, t3);
      }
      updateOffset(e3) {
        this.privSimpleSpeechPhrase.Offset += e3;
      }
      asJson() {
        const e3 = { ...this.privSimpleSpeechPhrase };
        return JSON.stringify({ ...e3, RecognitionStatus: i2.RecognitionStatus[e3.RecognitionStatus] });
      }
      get RecognitionStatus() {
        return this.privSimpleSpeechPhrase.RecognitionStatus;
      }
      get DisplayText() {
        return this.privSimpleSpeechPhrase.DisplayText;
      }
      get Offset() {
        return this.privSimpleSpeechPhrase.Offset;
      }
      get Duration() {
        return this.privSimpleSpeechPhrase.Duration;
      }
      get Language() {
        return void 0 === this.privSimpleSpeechPhrase.PrimaryLanguage ? void 0 : this.privSimpleSpeechPhrase.PrimaryLanguage.Language;
      }
      get LanguageDetectionConfidence() {
        return void 0 === this.privSimpleSpeechPhrase.PrimaryLanguage ? void 0 : this.privSimpleSpeechPhrase.PrimaryLanguage.Confidence;
      }
      get SpeakerId() {
        return this.privSimpleSpeechPhrase.SpeakerId;
      }
      mapRecognitionStatus(e3) {
        return "string" == typeof e3 ? i2.RecognitionStatus[e3] : "number" == typeof e3 ? e3 : void 0;
      }
    }
    t2.SimpleSpeechPhrase = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.RequestSession = void 0;
    const i2 = r2(4), n = r2(59), s = r2(224);
    t2.RequestSession = class {
      constructor(e3) {
        this.privIsDisposed = false, this.privDetachables = new Array(), this.privIsAudioNodeDetached = false, this.privIsRecognizing = false, this.privIsSpeechEnded = false, this.privTurnStartAudioOffset = 0, this.privLastRecoOffset = 0, this.privHypothesisReceived = false, this.privBytesSent = 0, this.privRecognitionBytesSent = 0, this.privRecogNumber = 0, this.privInTurn = false, this.privConnectionAttempts = 0, this.privAudioSourceId = e3, this.privRequestId = (0, i2.createNoDashGuid)(), this.privAudioNodeId = (0, i2.createNoDashGuid)(), this.privTurnDeferral = new i2.Deferred(), this.privTurnDeferral.resolve();
      }
      get sessionId() {
        return this.privSessionId;
      }
      get requestId() {
        return this.privRequestId;
      }
      get audioNodeId() {
        return this.privAudioNodeId;
      }
      get turnCompletionPromise() {
        return this.privTurnDeferral.promise;
      }
      get isSpeechEnded() {
        return this.privIsSpeechEnded;
      }
      get isRecognizing() {
        return this.privIsRecognizing;
      }
      get currentTurnAudioOffset() {
        return this.privTurnStartAudioOffset;
      }
      get recogNumber() {
        return this.privRecogNumber;
      }
      get numConnectionAttempts() {
        return this.privConnectionAttempts;
      }
      get bytesSent() {
        return this.privBytesSent;
      }
      get recognitionBytesSent() {
        return this.privRecognitionBytesSent;
      }
      listenForServiceTelemetry(e3) {
        this.privServiceTelemetryListener && this.privDetachables.push(e3.attachListener(this.privServiceTelemetryListener));
      }
      startNewRecognition() {
        this.privRecognitionBytesSent = 0, this.privIsSpeechEnded = false, this.privIsRecognizing = true, this.privTurnStartAudioOffset = 0, this.privLastRecoOffset = 0, this.privRecogNumber++, this.privServiceTelemetryListener = new s.ServiceTelemetryListener(this.privRequestId, this.privAudioSourceId, this.privAudioNodeId), this.onEvent(new n.RecognitionTriggeredEvent(this.requestId, this.privSessionId, this.privAudioSourceId, this.privAudioNodeId));
      }
      async onAudioSourceAttachCompleted(e3, t3) {
        this.privAudioNode = e3, this.privIsAudioNodeDetached = false, t3 ? await this.onComplete() : this.onEvent(new n.ListeningStartedEvent(this.privRequestId, this.privSessionId, this.privAudioSourceId, this.privAudioNodeId));
      }
      onPreConnectionStart(e3, t3) {
        this.privAuthFetchEventId = e3, this.privSessionId = t3, this.onEvent(new n.ConnectingToServiceEvent(this.privRequestId, this.privAuthFetchEventId, this.privSessionId));
      }
      async onAuthCompleted(e3) {
        e3 && await this.onComplete();
      }
      async onConnectionEstablishCompleted(e3, t3) {
        if (200 === e3) return this.onEvent(new n.RecognitionStartedEvent(this.requestId, this.privAudioSourceId, this.privAudioNodeId, this.privAuthFetchEventId, this.privSessionId)), this.privAudioNode && this.privAudioNode.replay(), this.privTurnStartAudioOffset = this.privLastRecoOffset, void (this.privBytesSent = 0);
        403 === e3 && await this.onComplete();
      }
      async onServiceTurnEndResponse(e3) {
        this.privTurnDeferral.resolve(), !e3 || this.isSpeechEnded ? (await this.onComplete(), this.privInTurn = false) : (this.privTurnStartAudioOffset = this.privLastRecoOffset, this.privAudioNode.replay());
      }
      onSpeechContext() {
        this.privRequestId = (0, i2.createNoDashGuid)();
      }
      onServiceTurnStartResponse() {
        this.privTurnDeferral && this.privInTurn && (this.privTurnDeferral.reject("Another turn started before current completed."), this.privTurnDeferral.promise.then().catch(() => {
        })), this.privInTurn = true, this.privTurnDeferral = new i2.Deferred();
      }
      onHypothesis(e3) {
        this.privHypothesisReceived || (this.privHypothesisReceived = true, this.privServiceTelemetryListener.hypothesisReceived(this.privAudioNode.findTimeAtOffset(e3)));
      }
      onPhraseRecognized(e3) {
        this.privServiceTelemetryListener.phraseReceived(this.privAudioNode.findTimeAtOffset(e3)), this.onServiceRecognized(e3);
      }
      onServiceRecognized(e3) {
        this.privLastRecoOffset = e3, this.privHypothesisReceived = false, this.privAudioNode.shrinkBuffers(e3), this.privConnectionAttempts = 0;
      }
      onAudioSent(e3) {
        this.privBytesSent += e3, this.privRecognitionBytesSent += e3;
      }
      onRetryConnection() {
        this.privConnectionAttempts++;
      }
      async dispose() {
        if (!this.privIsDisposed) {
          this.privIsDisposed = true;
          for (const e3 of this.privDetachables) await e3.detach();
          this.privServiceTelemetryListener && this.privServiceTelemetryListener.dispose(), this.privIsRecognizing = false;
        }
      }
      getTelemetry() {
        return this.privServiceTelemetryListener.hasTelemetry ? this.privServiceTelemetryListener.getTelemetry() : null;
      }
      async onStopRecognizing() {
        await this.onComplete();
      }
      onSpeechEnded() {
        this.privIsSpeechEnded = true;
      }
      onEvent(e3) {
        this.privServiceTelemetryListener && this.privServiceTelemetryListener.onEvent(e3), i2.Events.instance.onEvent(e3);
      }
      async onComplete() {
        this.privIsRecognizing && (this.privIsRecognizing = false, await this.detachAudioNode());
      }
      async detachAudioNode() {
        this.privIsAudioNodeDetached || (this.privIsAudioNodeDetached = true, this.privAudioNode && await this.privAudioNode.detach());
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ServiceTelemetryListener = void 0;
    const i2 = r2(4), n = r2(59);
    t2.ServiceTelemetryListener = class {
      constructor(e3, t3, r3) {
        this.privIsDisposed = false, this.privListeningTriggerMetric = null, this.privMicMetric = null, this.privConnectionEstablishMetric = null, this.privRequestId = e3, this.privAudioSourceId = t3, this.privAudioNodeId = r3, this.privReceivedMessages = {}, this.privPhraseLatencies = [], this.privHypothesisLatencies = [];
      }
      phraseReceived(e3) {
        e3 > 0 && this.privPhraseLatencies.push(Date.now() - e3);
      }
      hypothesisReceived(e3) {
        e3 > 0 && this.privHypothesisLatencies.push(Date.now() - e3);
      }
      onEvent(e3) {
        if (!this.privIsDisposed && (e3 instanceof n.RecognitionTriggeredEvent && e3.requestId === this.privRequestId && (this.privListeningTriggerMetric = { End: e3.eventTime, Name: "ListeningTrigger", Start: e3.eventTime }), e3 instanceof i2.AudioStreamNodeAttachingEvent && e3.audioSourceId === this.privAudioSourceId && e3.audioNodeId === this.privAudioNodeId && (this.privMicStartTime = e3.eventTime), e3 instanceof i2.AudioStreamNodeAttachedEvent && e3.audioSourceId === this.privAudioSourceId && e3.audioNodeId === this.privAudioNodeId && (this.privMicStartTime = e3.eventTime), e3 instanceof i2.AudioSourceErrorEvent && e3.audioSourceId === this.privAudioSourceId && (this.privMicMetric || (this.privMicMetric = { End: e3.eventTime, Error: e3.error, Name: "Microphone", Start: this.privMicStartTime })), e3 instanceof i2.AudioStreamNodeErrorEvent && e3.audioSourceId === this.privAudioSourceId && e3.audioNodeId === this.privAudioNodeId && (this.privMicMetric || (this.privMicMetric = { End: e3.eventTime, Error: e3.error, Name: "Microphone", Start: this.privMicStartTime })), e3 instanceof i2.AudioStreamNodeDetachedEvent && e3.audioSourceId === this.privAudioSourceId && e3.audioNodeId === this.privAudioNodeId && (this.privMicMetric || (this.privMicMetric = { End: e3.eventTime, Name: "Microphone", Start: this.privMicStartTime })), e3 instanceof n.ConnectingToServiceEvent && e3.requestId === this.privRequestId && (this.privConnectionId = e3.sessionId), e3 instanceof i2.ConnectionStartEvent && e3.connectionId === this.privConnectionId && (this.privConnectionStartTime = e3.eventTime), e3 instanceof i2.ConnectionEstablishedEvent && e3.connectionId === this.privConnectionId && (this.privConnectionEstablishMetric || (this.privConnectionEstablishMetric = { End: e3.eventTime, Id: this.privConnectionId, Name: "Connection", Start: this.privConnectionStartTime })), e3 instanceof i2.ConnectionEstablishErrorEvent && e3.connectionId === this.privConnectionId && (this.privConnectionEstablishMetric || (this.privConnectionEstablishMetric = { End: e3.eventTime, Error: this.getConnectionError(e3.statusCode), Id: this.privConnectionId, Name: "Connection", Start: this.privConnectionStartTime })), e3 instanceof i2.ConnectionMessageReceivedEvent && e3.connectionId === this.privConnectionId && e3.message && e3.message.headers && e3.message.headers.path)) {
          this.privReceivedMessages[e3.message.headers.path] || (this.privReceivedMessages[e3.message.headers.path] = new Array());
          const t3 = 50;
          this.privReceivedMessages[e3.message.headers.path].length < t3 && this.privReceivedMessages[e3.message.headers.path].push(e3.networkReceivedTime);
        }
      }
      getTelemetry() {
        const e3 = new Array();
        this.privListeningTriggerMetric && e3.push(this.privListeningTriggerMetric), this.privMicMetric && e3.push(this.privMicMetric), this.privConnectionEstablishMetric && e3.push(this.privConnectionEstablishMetric), this.privPhraseLatencies.length > 0 && e3.push({ PhraseLatencyMs: this.privPhraseLatencies }), this.privHypothesisLatencies.length > 0 && e3.push({ FirstHypothesisLatencyMs: this.privHypothesisLatencies });
        const t3 = { Metrics: e3, ReceivedMessages: this.privReceivedMessages }, r3 = JSON.stringify(t3);
        return this.privReceivedMessages = {}, this.privListeningTriggerMetric = null, this.privMicMetric = null, this.privConnectionEstablishMetric = null, this.privPhraseLatencies = [], this.privHypothesisLatencies = [], r3;
      }
      get hasTelemetry() {
        return 0 !== Object.keys(this.privReceivedMessages).length || null !== this.privListeningTriggerMetric || null !== this.privMicMetric || null !== this.privConnectionEstablishMetric || 0 !== this.privPhraseLatencies.length || 0 !== this.privHypothesisLatencies.length;
      }
      dispose() {
        this.privIsDisposed = true;
      }
      getConnectionError(e3) {
        switch (e3) {
          case 400:
          case 1002:
          case 1003:
          case 1005:
          case 1007:
          case 1008:
          case 1009:
            return "BadRequest";
          case 401:
            return "Unauthorized";
          case 403:
            return "Forbidden";
          case 503:
          case 1001:
            return "ServerUnavailable";
          case 500:
          case 1011:
            return "ServerError";
          case 408:
          case 504:
            return "Timeout";
          default:
            return "statuscode:" + e3.toString();
        }
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechContext = void 0;
    const i2 = r2(111), n = r2(226);
    t2.SpeechContext = class {
      constructor(e3) {
        this.privContext = {}, this.privDynamicGrammar = e3;
      }
      getContext() {
        return this.privContext;
      }
      setPronunciationAssessmentParams(e3, t3 = false) {
        void 0 === this.privContext.phraseDetection && (this.privContext.phraseDetection = { enrichment: { pronunciationAssessment: {} } }), void 0 === this.privContext.phraseDetection.enrichment && (this.privContext.phraseDetection.enrichment = { pronunciationAssessment: {} }), this.privContext.phraseDetection.enrichment.pronunciationAssessment = JSON.parse(e3) || {}, t3 && (this.privContext.phraseDetection.mode = i2.RecognitionMode.Conversation), this.setWordLevelTimings(), this.privContext.phraseOutput.detailed.options.push(n.PhraseOption.PronunciationAssessment), -1 === this.privContext.phraseOutput.detailed.options.indexOf(n.PhraseOption.SNR) && this.privContext.phraseOutput.detailed.options.push(n.PhraseOption.SNR);
      }
      setDetailedOutputFormat() {
        void 0 === this.privContext.phraseOutput && (this.privContext.phraseOutput = { detailed: { options: [] } }), void 0 === this.privContext.phraseOutput.detailed && (this.privContext.phraseOutput.detailed = { options: [] }), this.privContext.phraseOutput.format = n.OutputFormat.Detailed;
      }
      setWordLevelTimings() {
        void 0 === this.privContext.phraseOutput && (this.privContext.phraseOutput = { detailed: { options: [] } }), void 0 === this.privContext.phraseOutput.detailed && (this.privContext.phraseOutput.detailed = { options: [] }), this.privContext.phraseOutput.format = n.OutputFormat.Detailed, -1 === this.privContext.phraseOutput.detailed.options.indexOf(n.PhraseOption.WordTimings) && this.privContext.phraseOutput.detailed.options.push(n.PhraseOption.WordTimings);
      }
      setSpeakerDiarizationAudioOffsetMs(e3) {
        this.privContext.phraseDetection.speakerDiarization.audioOffsetMs = e3;
      }
      toJSON() {
        const e3 = this.privDynamicGrammar.generateGrammarObject();
        this.privContext.dgi = e3;
        return JSON.stringify(this.privContext);
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TentativePhraseResultsOption = t2.OutputFormat = t2.PhraseExtension = t2.PhraseOption = void 0, function(e3) {
      e3.WordTimings = "WordTimings", e3.SNR = "SNR", e3.Pronunciation = "Pronunciation", e3.WordPronunciation = "WordPronunciation", e3.WordConfidence = "WordConfidence", e3.Words = "Words", e3.Sentiment = "Sentiment", e3.PronunciationAssessment = "PronunciationAssessment", e3.ContentAssessment = "ContentAssessment", e3.PhraseAMScore = "PhraseAMScore", e3.PhraseLMScore = "PhraseLMScore", e3.WordAMScore = "WordAMScore", e3.WordLMScore = "WordLMScore", e3.RuleTree = "RuleTree", e3.NBestTimings = "NBestTimings", e3.DecoderDiagnostics = "DecoderDiagnostics", e3.DisplayWordTimings = "DisplayWordTimings", e3.DisplayWords = "DisplayWords";
    }(t2.PhraseOption || (t2.PhraseOption = {})), function(e3) {
      e3.Graph = "Graph", e3.Corrections = "Corrections", e3.Sentiment = "Sentiment";
    }(t2.PhraseExtension || (t2.PhraseExtension = {})), function(e3) {
      e3.Simple = "Simple", e3.Detailed = "Detailed";
    }(t2.OutputFormat || (t2.OutputFormat = {})), function(e3) {
      e3.None = "None", e3.Always = "Always";
    }(t2.TentativePhraseResultsOption || (t2.TentativePhraseResultsOption = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DynamicGrammarBuilder = void 0;
    const i2 = r2(228);
    t2.DynamicGrammarBuilder = class {
      constructor() {
        this.privWeight = 1;
      }
      addPhrase(e3) {
        this.privPhrases || (this.privPhrases = []), e3 instanceof Array ? this.privPhrases = this.privPhrases.concat(e3) : this.privPhrases.push(e3);
      }
      clearPhrases() {
        this.privPhrases = void 0;
      }
      addReferenceGrammar(e3) {
        this.privGrammars || (this.privGrammars = []), e3 instanceof Array ? this.privGrammars = this.privGrammars.concat(e3) : this.privGrammars.push(e3);
      }
      clearGrammars() {
        this.privGrammars = void 0;
      }
      setWeight(e3) {
        this.privWeight = e3;
      }
      generateGrammarObject() {
        if (void 0 === this.privGrammars && void 0 === this.privPhrases) return;
        const e3 = {};
        if (e3.referenceGrammars = this.privGrammars, void 0 !== this.privPhrases && 0 !== this.privPhrases.length) {
          const t3 = [];
          this.privPhrases.forEach((e4) => {
            t3.push({ text: e4 });
          }), e3.groups = [{ type: i2.GroupType.Generic, items: t3 }], e3.bias = this.privWeight;
        }
        return e3;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SubstringMatchType = t2.GroupType = void 0, function(e3) {
      e3.IntentText = "IntentText", e3.IntentEntity = "IntentEntity", e3.Generic = "Generic", e3.People = "People", e3.Place = "Place", e3.DynamicEntity = "DynamicEntity";
    }(t2.GroupType || (t2.GroupType = {})), function(e3) {
      e3.None = "None", e3.LeftRooted = "LeftRooted", e3.PartialName = "PartialName", e3.MiddleOfSentence = "MiddleOfSentence";
    }(t2.SubstringMatchType || (t2.SubstringMatchType = {}));
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DialogServiceAdapter = void 0;
    const i2 = r2(61), n = r2(29), s = r2(4), o = r2(86), a = r2(80), c = r2(230), p = r2(2), h = r2(232), u = r2(233), d = r2(234), v = r2(190);
    class l extends p.ServiceRecognizerBase {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, i3, n2), this.privEvents = new s.EventSource(), this.privDialogServiceConnector = n2, this.receiveMessageOverride = () => this.receiveDialogMessageOverride(), this.privTurnStateManager = new c.DialogServiceTurnStateManager(), this.recognizeOverride = (e4, t4, r4) => this.listenOnce(e4, t4, r4), this.postConnectImplOverride = (e4) => this.dialogConnectImpl(e4), this.configConnectionOverride = (e4) => this.configConnection(e4), this.disconnectOverride = () => this.privDisconnect(), this.privDialogAudioSource = r3, this.agentConfigSent = false, this.privLastResult = null, this.connectionEvents.attach((e4) => {
          "ConnectionClosedEvent" === e4.name && (this.terminateMessageLoop = true);
        });
      }
      async sendMessage(e3) {
        const t3 = (0, s.createGuid)(), r3 = (0, s.createNoDashGuid)(), i3 = { context: { interactionId: t3 }, messagePayload: JSON.parse(e3), version: 0.5 }, n2 = JSON.stringify(i3), o2 = await this.fetchConnection();
        await o2.send(new v.SpeechConnectionMessage(s.MessageType.Text, "agent", r3, "application/json", n2));
      }
      async privDisconnect() {
        await this.cancelRecognition(this.privRequestSession.sessionId, this.privRequestSession.requestId, a.CancellationReason.Error, a.CancellationErrorCode.NoError, "Disconnecting"), this.terminateMessageLoop = true, this.agentConfigSent = false;
      }
      processTypeSpecificMessages(e3) {
        const t3 = new a.PropertyCollection();
        let r3, i3;
        switch (e3.messageType === s.MessageType.Text && t3.setProperty(a.PropertyId.SpeechServiceResponse_JsonResult, e3.textBody), e3.path.toLowerCase()) {
          case "speech.phrase":
            const n3 = p.SimpleSpeechPhrase.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
            if (this.privRequestSession.onPhraseRecognized(n3.Offset + n3.Duration), n3.RecognitionStatus !== p.RecognitionStatus.TooManyRequests && n3.RecognitionStatus !== p.RecognitionStatus.Error) {
              const e4 = this.fireEventForResult(n3, t3);
              if (this.privLastResult = e4.result, this.privDialogServiceConnector.recognized) try {
                this.privDialogServiceConnector.recognized(this.privDialogServiceConnector, e4);
              } catch (e5) {
              }
            }
            i3 = true;
            break;
          case "speech.hypothesis":
            const s2 = p.SpeechHypothesis.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
            r3 = new a.SpeechRecognitionResult(this.privRequestSession.requestId, a.ResultReason.RecognizingSpeech, s2.Text, s2.Duration, s2.Offset, s2.Language, s2.LanguageDetectionConfidence, void 0, void 0, s2.asJson(), t3), this.privRequestSession.onHypothesis(s2.Offset);
            const o2 = new a.SpeechRecognitionEventArgs(r3, s2.Offset, this.privRequestSession.sessionId);
            if (this.privDialogServiceConnector.recognizing) try {
              this.privDialogServiceConnector.recognizing(this.privDialogServiceConnector, o2);
            } catch (e4) {
            }
            i3 = true;
            break;
          case "speech.keyword":
            const c2 = p.SpeechKeyword.fromJSON(e3.textBody, this.privRequestSession.currentTurnAudioOffset);
            r3 = new a.SpeechRecognitionResult(this.privRequestSession.requestId, "Accepted" === c2.Status ? a.ResultReason.RecognizedKeyword : a.ResultReason.NoMatch, c2.Text, c2.Duration, c2.Offset, void 0, void 0, void 0, void 0, c2.asJson(), t3), "Accepted" !== c2.Status && (this.privLastResult = r3);
            const h2 = new a.SpeechRecognitionEventArgs(r3, r3.duration, r3.resultId);
            if (this.privDialogServiceConnector.recognized) try {
              this.privDialogServiceConnector.recognized(this.privDialogServiceConnector, h2);
            } catch (e4) {
            }
            i3 = true;
            break;
          case "audio":
            {
              const t4 = e3.requestId.toUpperCase(), r4 = this.privTurnStateManager.GetTurn(t4);
              try {
                e3.binaryBody ? r4.audioStream.write(e3.binaryBody) : r4.endAudioStream();
              } catch (e4) {
              }
            }
            i3 = true;
            break;
          case "response":
            this.handleResponseMessage(e3), i3 = true;
        }
        const n2 = new s.Deferred();
        return n2.resolve(i3), n2.promise;
      }
      async cancelRecognition(e3, t3, r3, i3, n2) {
        if (this.terminateMessageLoop = true, this.privRequestSession.isRecognizing && await this.privRequestSession.onStopRecognizing(), this.privDialogServiceConnector.canceled) {
          const t4 = new a.PropertyCollection();
          t4.setProperty(p.CancellationErrorCodePropertyName, a.CancellationErrorCode[i3]);
          const s2 = new a.SpeechRecognitionCanceledEventArgs(r3, n2, i3, void 0, e3);
          try {
            this.privDialogServiceConnector.canceled(this.privDialogServiceConnector, s2);
          } catch {
          }
          if (this.privSuccessCallback) {
            const e4 = new a.SpeechRecognitionResult(void 0, a.ResultReason.Canceled, void 0, void 0, void 0, void 0, void 0, void 0, n2, void 0, t4);
            try {
              this.privSuccessCallback(e4), this.privSuccessCallback = void 0;
            } catch {
            }
          }
        }
      }
      async listenOnce(e3, t3, r3) {
        this.privRecognizerConfig.recognitionMode = e3, this.privSuccessCallback = t3, this.privErrorCallback = r3, this.privRequestSession.startNewRecognition(), this.privRequestSession.listenForServiceTelemetry(this.privDialogAudioSource.events), this.privRecognizerConfig.parameters.setProperty(a.PropertyId.Speech_SessionId, this.privRequestSession.sessionId);
        const n2 = this.connectImpl(), s2 = this.sendPreAudioMessages(), o2 = await this.privDialogAudioSource.attach(this.privRequestSession.audioNodeId), c2 = await this.privDialogAudioSource.format, p2 = await this.privDialogAudioSource.deviceInfo, h2 = new i2.ReplayableAudioNode(o2, c2.avgBytesPerSec);
        await this.privRequestSession.onAudioSourceAttachCompleted(h2, false), this.privRecognizerConfig.SpeechServiceConfig.Context.audio = { source: p2 };
        try {
          await n2, await s2;
        } catch (e4) {
          return await this.cancelRecognition(this.privRequestSession.sessionId, this.privRequestSession.requestId, a.CancellationReason.Error, a.CancellationErrorCode.ConnectionFailure, e4), Promise.resolve();
        }
        const u2 = new a.SessionEventArgs(this.privRequestSession.sessionId);
        this.privRecognizer.sessionStarted && this.privRecognizer.sessionStarted(this.privRecognizer, u2);
        this.sendAudio(h2).then(() => {
        }, async (e4) => {
          await this.cancelRecognition(this.privRequestSession.sessionId, this.privRequestSession.requestId, a.CancellationReason.Error, a.CancellationErrorCode.RuntimeError, e4);
        });
      }
      dialogConnectImpl(e3) {
        return this.privConnectionLoop = this.startMessageLoop(), e3;
      }
      receiveDialogMessageOverride() {
        const e3 = new s.Deferred(), t3 = async () => {
          try {
            const r3 = this.isDisposed(), i3 = !this.isDisposed() && this.terminateMessageLoop;
            if (r3 || i3) return void e3.resolve(void 0);
            const n2 = await this.fetchConnection(), o2 = await n2.read();
            if (!o2) return t3();
            const c2 = v.SpeechConnectionMessage.fromConnectionMessage(o2);
            switch (c2.path.toLowerCase()) {
              case "turn.start":
                {
                  const e5 = c2.requestId.toUpperCase();
                  e5 !== this.privRequestSession.requestId.toUpperCase() ? this.privTurnStateManager.StartTurn(e5) : this.privRequestSession.onServiceTurnStartResponse();
                }
                break;
              case "speech.startdetected":
                const e4 = p.SpeechDetected.fromJSON(c2.textBody, this.privRequestSession.currentTurnAudioOffset), t4 = new a.RecognitionEventArgs(e4.Offset, this.privRequestSession.sessionId);
                this.privRecognizer.speechStartDetected && this.privRecognizer.speechStartDetected(this.privRecognizer, t4);
                break;
              case "speech.enddetected":
                let r4;
                r4 = c2.textBody.length > 0 ? c2.textBody : "{ Offset: 0 }";
                const i4 = p.SpeechDetected.fromJSON(r4, this.privRequestSession.currentTurnAudioOffset);
                this.privRequestSession.onServiceRecognized(i4.Offset);
                const n3 = new a.RecognitionEventArgs(i4.Offset, this.privRequestSession.sessionId);
                this.privRecognizer.speechEndDetected && this.privRecognizer.speechEndDetected(this.privRecognizer, n3);
                break;
              case "turn.end":
                {
                  const e5 = c2.requestId.toUpperCase();
                  if (e5 !== this.privRequestSession.requestId.toUpperCase()) this.privTurnStateManager.CompleteTurn(e5);
                  else {
                    const e6 = new a.SessionEventArgs(this.privRequestSession.sessionId);
                    if (await this.privRequestSession.onServiceTurnEndResponse(false), this.privRecognizerConfig.isContinuousRecognition && !this.privRequestSession.isSpeechEnded && this.privRequestSession.isRecognizing || this.privRecognizer.sessionStopped && this.privRecognizer.sessionStopped(this.privRecognizer, e6), this.privSuccessCallback && this.privLastResult) {
                      try {
                        this.privSuccessCallback(this.privLastResult), this.privLastResult = null;
                      } catch (e7) {
                        this.privErrorCallback && this.privErrorCallback(e7);
                      }
                      this.privSuccessCallback = void 0, this.privErrorCallback = void 0;
                    }
                  }
                }
                break;
              default:
                try {
                  await this.processTypeSpecificMessages(c2) || this.serviceEvents && this.serviceEvents.onEvent(new s.ServiceEvent(c2.path.toLowerCase(), c2.textBody));
                } catch (e5) {
                }
            }
            return t3();
          } catch (t4) {
            this.terminateMessageLoop = true, e3.resolve();
          }
        };
        return t3().catch((e4) => {
          s.Events.instance.onEvent(new s.BackgroundEvent(e4));
        }), e3.promise;
      }
      async startMessageLoop() {
        this.terminateMessageLoop = false;
        try {
          await this.receiveDialogMessageOverride();
        } catch (e3) {
          await this.cancelRecognition(this.privRequestSession.sessionId, this.privRequestSession.requestId, a.CancellationReason.Error, a.CancellationErrorCode.RuntimeError, e3);
        }
        return Promise.resolve();
      }
      async configConnection(e3) {
        return this.terminateMessageLoop ? (this.terminateMessageLoop = false, Promise.reject("Connection to service terminated.")) : (await this.sendSpeechServiceConfig(e3, this.privRequestSession, this.privRecognizerConfig.SpeechServiceConfig.serialize()), await this.sendAgentConfig(e3), e3);
      }
      async sendPreAudioMessages() {
        const e3 = await this.fetchConnection();
        this.addKeywordContextData(), await this.sendSpeechContext(e3, true), await this.sendAgentContext(e3), await this.sendWaveHeader(e3);
      }
      sendAgentConfig(e3) {
        if (this.agentConfig && !this.agentConfigSent) {
          if (this.privRecognizerConfig.parameters.getProperty(a.PropertyId.Conversation_DialogType) === a.DialogServiceConfig.DialogTypes.CustomCommands) {
            const e4 = this.agentConfig.get();
            e4.botInfo.commandsCulture = this.privRecognizerConfig.parameters.getProperty(a.PropertyId.SpeechServiceConnection_RecoLanguage, "en-us"), this.agentConfig.set(e4);
          }
          this.onEvent(new n.SendingAgentContextMessageEvent(this.agentConfig));
          const t3 = this.agentConfig.toJsonString();
          return this.agentConfigSent = true, e3.send(new v.SpeechConnectionMessage(s.MessageType.Text, "agent.config", this.privRequestSession.requestId, "application/json", t3));
        }
      }
      sendAgentContext(e3) {
        const t3 = (0, s.createGuid)(), r3 = this.privDialogServiceConnector.properties.getProperty(a.PropertyId.Conversation_Speech_Activity_Template), i3 = { channelData: "", context: { interactionId: t3 }, messagePayload: void 0 === typeof r3 ? void 0 : r3, version: 0.5 }, n2 = JSON.stringify(i3);
        return e3.send(new v.SpeechConnectionMessage(s.MessageType.Text, "speech.agent.context", this.privRequestSession.requestId, "application/json", n2));
      }
      fireEventForResult(e3, t3) {
        const r3 = p.EnumTranslation.implTranslateRecognitionResult(e3.RecognitionStatus), i3 = new a.SpeechRecognitionResult(this.privRequestSession.requestId, r3, e3.DisplayText, e3.Duration, e3.Offset, e3.Language, e3.LanguageDetectionConfidence, void 0, void 0, e3.asJson(), t3);
        return new a.SpeechRecognitionEventArgs(i3, e3.Offset, this.privRequestSession.sessionId);
      }
      handleResponseMessage(e3) {
        const t3 = JSON.parse(e3.textBody);
        switch (t3.messageType.toLowerCase()) {
          case "message":
            const r3 = e3.requestId.toUpperCase(), i3 = h.ActivityPayloadResponse.fromJSON(e3.textBody), n2 = this.privTurnStateManager.GetTurn(r3);
            if (i3.conversationId) {
              const e4 = this.agentConfig.get();
              e4.botInfo.conversationId = i3.conversationId, this.agentConfig.set(e4);
            }
            const c2 = n2.processActivityPayload(i3, o.AudioOutputFormatImpl.fromSpeechSynthesisOutputFormatString(this.privDialogServiceConnector.properties.getProperty(a.PropertyId.SpeechServiceConnection_SynthOutputFormat, void 0))), p2 = new a.ActivityReceivedEventArgs(i3.messagePayload, c2);
            if (this.privDialogServiceConnector.activityReceived) try {
              this.privDialogServiceConnector.activityReceived(this.privDialogServiceConnector, p2);
            } catch (e4) {
            }
            break;
          case "messagestatus":
            if (this.privDialogServiceConnector.turnStatusReceived) try {
              this.privDialogServiceConnector.turnStatusReceived(this.privDialogServiceConnector, new a.TurnStatusReceivedEventArgs(e3.textBody));
            } catch (e4) {
            }
            break;
          default:
            s.Events.instance.onEvent(new s.BackgroundEvent(`Unexpected response of type ${t3.messageType}. Ignoring.`));
        }
      }
      onEvent(e3) {
        this.privEvents.onEvent(e3), s.Events.instance.onEvent(e3);
      }
      addKeywordContextData() {
        const e3 = this.privRecognizerConfig.parameters.getProperty("SPEECH-KeywordsToDetect");
        if (void 0 === e3) return;
        const t3 = this.privRecognizerConfig.parameters.getProperty("SPEECH-KeywordsToDetect-Offsets"), r3 = this.privRecognizerConfig.parameters.getProperty("SPEECH-KeywordsToDetect-Durations"), i3 = e3.split(";"), n2 = void 0 === t3 ? [] : t3.split(";"), s2 = void 0 === r3 ? [] : r3.split(";"), o2 = [];
        for (let e4 = 0; e4 < i3.length; e4++) {
          const t4 = { text: i3[e4] };
          e4 < n2.length && (t4.startOffset = Number(n2[e4])), e4 < s2.length && (t4.duration = Number(s2[e4])), o2.push(t4);
        }
        this.speechContext.getContext().invocationSource = u.InvocationSource.VoiceActivationWithKeyword, this.speechContext.getContext().keywordDetection = [{ clientDetectedKeywords: o2, onReject: { action: d.OnRejectAction.EndOfTurn }, type: d.KeywordDetectionType.StartTrigger }];
      }
    }
    t2.DialogServiceAdapter = l;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DialogServiceTurnStateManager = void 0;
    const i2 = r2(26), n = r2(231);
    t2.DialogServiceTurnStateManager = class {
      constructor() {
        this.privTurnMap = /* @__PURE__ */ new Map();
      }
      StartTurn(e3) {
        if (this.privTurnMap.has(e3)) throw new i2.InvalidOperationError("Service error: There is already a turn with id:" + e3);
        const t3 = new n.DialogServiceTurnState(this, e3);
        return this.privTurnMap.set(e3, t3), this.privTurnMap.get(e3);
      }
      GetTurn(e3) {
        return this.privTurnMap.get(e3);
      }
      CompleteTurn(e3) {
        if (!this.privTurnMap.has(e3)) throw new i2.InvalidOperationError("Service error: Received turn end for an unknown turn id:" + e3);
        const t3 = this.privTurnMap.get(e3);
        return t3.complete(), this.privTurnMap.delete(e3), t3;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.DialogServiceTurnState = void 0;
    const i2 = r2(86), n = r2(85), s = r2(232);
    t2.DialogServiceTurnState = class {
      constructor(e3, t3) {
        this.privRequestId = t3, this.privIsCompleted = false, this.privAudioStream = null, this.privTurnManager = e3, this.resetTurnEndTimeout();
      }
      get audioStream() {
        return this.resetTurnEndTimeout(), this.privAudioStream;
      }
      processActivityPayload(e3, t3) {
        return e3.messageDataStreamType === s.MessageDataStreamType.TextToSpeechAudio && (this.privAudioStream = n.AudioOutputStream.createPullStream(), this.privAudioStream.format = void 0 !== t3 ? t3 : i2.AudioOutputFormatImpl.getDefaultOutputFormat()), this.privAudioStream;
      }
      endAudioStream() {
        null === this.privAudioStream || this.privAudioStream.isClosed || this.privAudioStream.close();
      }
      complete() {
        void 0 !== this.privTimeoutToken && clearTimeout(this.privTimeoutToken), this.endAudioStream();
      }
      resetTurnEndTimeout() {
        void 0 !== this.privTimeoutToken && clearTimeout(this.privTimeoutToken), this.privTimeoutToken = setTimeout(() => {
          this.privTurnManager.CompleteTurn(this.privRequestId);
        }, 2e3);
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.MessageDataStreamType = t2.ActivityPayloadResponse = void 0;
    class r2 {
      constructor(e3) {
        this.privActivityResponse = JSON.parse(e3);
      }
      static fromJSON(e3) {
        return new r2(e3);
      }
      get conversationId() {
        return this.privActivityResponse.conversationId;
      }
      get messageDataStreamType() {
        return this.privActivityResponse.messageDataStreamType;
      }
      get messagePayload() {
        return this.privActivityResponse.messagePayload;
      }
      get version() {
        return this.privActivityResponse.version;
      }
    }
    t2.ActivityPayloadResponse = r2, function(e3) {
      e3[e3.None = 0] = "None", e3[e3.TextToSpeechAudio = 1] = "TextToSpeechAudio";
    }(t2.MessageDataStreamType || (t2.MessageDataStreamType = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.InvocationSource = void 0, function(e3) {
      e3.None = "None", e3.VoiceActivationWithKeyword = "VoiceActivationWithKeyword";
    }(t2.InvocationSource || (t2.InvocationSource = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.OnRejectAction = t2.KeywordDetectionType = void 0, function(e3) {
      e3.StartTrigger = "StartTrigger";
    }(t2.KeywordDetectionType || (t2.KeywordDetectionType = {})), function(e3) {
      e3.EndOfTurn = "EndOfTurn", e3.Continue = "Continue";
    }(t2.OnRejectAction || (t2.OnRejectAction = {}));
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AgentConfig = void 0;
    t2.AgentConfig = class {
      toJsonString() {
        return JSON.stringify(this.iPrivConfig);
      }
      get() {
        return this.iPrivConfig;
      }
      set(e3) {
        this.iPrivConfig = e3;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.InternalParticipants = t2.ConversationTranslatorMessageTypes = t2.ConversationTranslatorCommandTypes = t2.ParticipantsListEventArgs = t2.ParticipantEventArgs = t2.ParticipantAttributeEventArgs = t2.MuteAllEventArgs = t2.LockRoomEventArgs = t2.ConversationReceivedTranslationEventArgs = t2.TranscriberRecognizer = t2.ConversationRecognizerFactory = t2.ConversationConnectionConfig = t2.ConversationManager = void 0;
    var i2 = r2(237);
    Object.defineProperty(t2, "ConversationManager", { enumerable: true, get: function() {
      return i2.ConversationManager;
    } });
    var n = r2(238);
    Object.defineProperty(t2, "ConversationConnectionConfig", { enumerable: true, get: function() {
      return n.ConversationConnectionConfig;
    } });
    var s = r2(239);
    Object.defineProperty(t2, "ConversationRecognizerFactory", { enumerable: true, get: function() {
      return s.ConversationRecognizerFactory;
    } });
    var o = r2(251);
    Object.defineProperty(t2, "TranscriberRecognizer", { enumerable: true, get: function() {
      return o.TranscriberRecognizer;
    } });
    var a = r2(245);
    Object.defineProperty(t2, "ConversationReceivedTranslationEventArgs", { enumerable: true, get: function() {
      return a.ConversationReceivedTranslationEventArgs;
    } }), Object.defineProperty(t2, "LockRoomEventArgs", { enumerable: true, get: function() {
      return a.LockRoomEventArgs;
    } }), Object.defineProperty(t2, "MuteAllEventArgs", { enumerable: true, get: function() {
      return a.MuteAllEventArgs;
    } }), Object.defineProperty(t2, "ParticipantAttributeEventArgs", { enumerable: true, get: function() {
      return a.ParticipantAttributeEventArgs;
    } }), Object.defineProperty(t2, "ParticipantEventArgs", { enumerable: true, get: function() {
      return a.ParticipantEventArgs;
    } }), Object.defineProperty(t2, "ParticipantsListEventArgs", { enumerable: true, get: function() {
      return a.ParticipantsListEventArgs;
    } });
    var c = r2(246);
    Object.defineProperty(t2, "ConversationTranslatorCommandTypes", { enumerable: true, get: function() {
      return c.ConversationTranslatorCommandTypes;
    } }), Object.defineProperty(t2, "ConversationTranslatorMessageTypes", { enumerable: true, get: function() {
      return c.ConversationTranslatorMessageTypes;
    } }), Object.defineProperty(t2, "InternalParticipants", { enumerable: true, get: function() {
      return c.InternalParticipants;
    } });
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationManager = void 0;
    const i2 = r2(61), n = r2(65), s = r2(80), o = r2(238);
    t2.ConversationManager = class {
      constructor() {
        this.privRequestParams = o.ConversationConnectionConfig.configParams, this.privErrors = o.ConversationConnectionConfig.restErrors, this.privHost = o.ConversationConnectionConfig.host, this.privApiVersion = o.ConversationConnectionConfig.apiVersion, this.privRestPath = o.ConversationConnectionConfig.restPath, this.privRestAdapter = new i2.RestMessageAdapter({});
      }
      createOrJoin(e3, t3, r3, a) {
        try {
          n.Contracts.throwIfNullOrUndefined(e3, "args");
          const c = e3.getProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage, o.ConversationConnectionConfig.defaultLanguageCode), p = e3.getProperty(s.PropertyId.ConversationTranslator_Name, "conversation_host"), h = e3.getProperty(s.PropertyId.ConversationTranslator_Host, this.privHost), u = e3.getProperty(s.PropertyId.ConversationTranslator_CorrelationId), d = e3.getProperty(s.PropertyId.SpeechServiceConnection_Key), v = e3.getProperty(s.PropertyId.SpeechServiceConnection_Region), l = e3.getProperty(s.PropertyId.SpeechServiceAuthorization_Token);
          n.Contracts.throwIfNullOrWhitespace(c, "languageCode"), n.Contracts.throwIfNullOrWhitespace(p, "nickname"), n.Contracts.throwIfNullOrWhitespace(h, "endpointHost");
          const g = {};
          g[this.privRequestParams.apiVersion] = this.privApiVersion, g[this.privRequestParams.languageCode] = c, g[this.privRequestParams.nickname] = p;
          const m = {};
          u && (m[this.privRequestParams.correlationId] = u), m[this.privRequestParams.clientAppId] = o.ConversationConnectionConfig.clientAppId, void 0 !== t3 ? g[this.privRequestParams.roomId] = t3 : (n.Contracts.throwIfNullOrUndefined(v, this.privErrors.authInvalidSubscriptionRegion), m[this.privRequestParams.subscriptionRegion] = v, d ? m[this.privRequestParams.subscriptionKey] = d : l ? m[this.privRequestParams.authorization] = `Bearer ${l}` : n.Contracts.throwIfNullOrUndefined(d, this.privErrors.authInvalidSubscriptionKey));
          const S = {};
          S.headers = m, this.privRestAdapter.options = S;
          const f = `https://${h}${this.privRestPath}`;
          this.privRestAdapter.request(i2.RestRequestType.Post, f, g, null).then((e4) => {
            const t4 = i2.RestMessageAdapter.extractHeaderValue(this.privRequestParams.requestId, e4.headers);
            if (!e4.ok) {
              if (a) {
                let r4, i3 = this.privErrors.invalidCreateJoinConversationResponse.replace("{status}", e4.status.toString());
                try {
                  r4 = JSON.parse(e4.data), i3 += ` [${r4.error.code}: ${r4.error.message}]`;
                } catch (t5) {
                  i3 += ` [${e4.data}]`;
                }
                t4 && (i3 += ` ${t4}`), a(i3);
              }
              return;
            }
            const n2 = JSON.parse(e4.data);
            if (n2 && (n2.requestId = t4), r3) {
              try {
                r3(n2);
              } catch (e5) {
                a && a(e5);
              }
              r3 = void 0;
            }
          }).catch(() => {
          });
        } catch (e4) {
          if (a) if (e4 instanceof Error) {
            const t4 = e4;
            a(t4.name + ": " + t4.message);
          } else a(e4);
        }
      }
      leave(e3, t3) {
        return new Promise((r3, o2) => {
          try {
            n.Contracts.throwIfNullOrUndefined(e3, this.privErrors.invalidArgs.replace("{arg}", "config")), n.Contracts.throwIfNullOrWhitespace(t3, this.privErrors.invalidArgs.replace("{arg}", "token"));
            const o3 = e3.getProperty(s.PropertyId.ConversationTranslator_Host, this.privHost), a = e3.getProperty(s.PropertyId.ConversationTranslator_CorrelationId), c = {};
            c[this.privRequestParams.apiVersion] = this.privApiVersion, c[this.privRequestParams.sessionToken] = t3;
            const p = {};
            a && (p[this.privRequestParams.correlationId] = a);
            const h = {};
            h.headers = p, this.privRestAdapter.options = h;
            const u = `https://${o3}${this.privRestPath}`;
            this.privRestAdapter.request(i2.RestRequestType.Delete, u, c, null).then((e4) => {
              e4.ok, r3();
            }).catch(() => {
            });
          } catch (e4) {
            if (e4 instanceof Error) {
              const t4 = e4;
              o2(t4.name + ": " + t4.message);
            } else o2(e4);
          }
        });
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationConnectionConfig = void 0;
    const i2 = r2(189);
    class n extends i2.RestConfigBase {
      static get host() {
        return n.privHost;
      }
      static get apiVersion() {
        return n.privApiVersion;
      }
      static get clientAppId() {
        return n.privClientAppId;
      }
      static get defaultLanguageCode() {
        return n.privDefaultLanguageCode;
      }
      static get restPath() {
        return n.privRestPath;
      }
      static get webSocketPath() {
        return n.privWebSocketPath;
      }
      static get transcriptionEventKeys() {
        return n.privTranscriptionEventKeys;
      }
    }
    t2.ConversationConnectionConfig = n, n.privHost = "dev.microsofttranslator.com", n.privRestPath = "/capito/room", n.privApiVersion = "2.0", n.privDefaultLanguageCode = "en-US", n.privClientAppId = "FC539C22-1767-4F1F-84BC-B4D811114F15", n.privWebSocketPath = "/capito/translate", n.privTranscriptionEventKeys = ["iCalUid", "callId", "organizer", "FLAC", "MTUri", "DifferentiateGuestSpeakers", "audiorecording", "Threadid", "OrganizerMri", "OrganizerTenantId", "UserToken"];
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslatorRecognizer = t2.ConversationRecognizerFactory = void 0;
    const i2 = r2(2), n = r2(4), s = r2(65), o = r2(80), a = r2(240), c = r2(243);
    t2.ConversationRecognizerFactory = class {
      static fromConfig(e3, t3, r3) {
        return new p(e3, t3, r3);
      }
    };
    class p extends o.Recognizer {
      constructor(e3, t3, r3) {
        const i3 = t3;
        s.Contracts.throwIfNull(i3, "speechConfig");
        const c2 = e3;
        s.Contracts.throwIfNull(c2, "conversationImpl"), super(r3, i3.properties, new a.ConversationConnectionFactory()), this.privConversation = c2, this.privIsDisposed = false, this.privProperties = i3.properties.clone(), this.privConnection = o.Connection.fromRecognizer(this);
        "on" === this.privProperties.getProperty(o.PropertyId.WebWorkerLoadType, "on").toLowerCase() && "undefined" != typeof Blob && "undefined" != typeof Worker ? (this.privSetTimeout = n.Timeout.setTimeout, this.privClearTimeout = n.Timeout.clearTimeout) : "undefined" != typeof window ? (this.privSetTimeout = window.setTimeout.bind(window), this.privClearTimeout = window.clearTimeout.bind(window)) : (this.privSetTimeout = setTimeout, this.privClearTimeout = clearTimeout);
      }
      set connected(e3) {
        this.privConnection.connected = e3;
      }
      set disconnected(e3) {
        this.privConnection.disconnected = e3;
      }
      get speechRecognitionLanguage() {
        return this.privSpeechRecognitionLanguage;
      }
      get properties() {
        return this.privProperties;
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      connect(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), s.Contracts.throwIfNullOrWhitespace(e3, "token"), this.privReco.conversationTranslatorToken = e3, this.resetConversationTimeout(), this.privReco.connectAsync(t3, r3);
        } catch (e4) {
          if (r3) if (e4 instanceof Error) {
            const t4 = e4;
            r3(t4.name + ": " + t4.message);
          } else r3(e4);
        }
      }
      disconnect(e3, t3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), void 0 !== this.privTimeoutToken && this.privClearTimeout(this.privTimeoutToken), this.privReco.disconnect().then(() => {
            e3 && e3();
          }, (e4) => {
            t3 && t3(e4);
          });
        } catch (e4) {
          if (t3) if (e4 instanceof Error) {
            const r3 = e4;
            t3(r3.name + ": " + r3.message);
          } else t3(e4);
          this.dispose(true).catch((e5) => {
            n.Events.instance.onEvent(new n.BackgroundEvent(e5));
          });
        }
      }
      sendRequest(e3, t3, r3) {
        try {
          s.Contracts.throwIfDisposed(this.privIsDisposed), this.sendMessage(e3, t3, r3);
        } catch (e4) {
          if (r3) if (e4 instanceof Error) {
            const t4 = e4;
            r3(t4.name + ": " + t4.message);
          } else r3(e4);
          this.dispose(true).catch((e5) => {
            n.Events.instance.onEvent(new n.BackgroundEvent(e5));
          });
        }
      }
      onToken(e3) {
        this.privConversation.onToken(e3);
      }
      async close() {
        this.privIsDisposed || (this.privConnection && (this.privConnection.closeConnection(), this.privConnection.close()), this.privConnection = void 0, await this.dispose(true));
      }
      async dispose(e3) {
        this.privIsDisposed || e3 && (void 0 !== this.privTimeoutToken && this.privClearTimeout(this.privTimeoutToken), this.privIsDisposed = true, this.privConnection && (this.privConnection.closeConnection(), this.privConnection.close(), this.privConnection = void 0), await super.dispose(e3));
      }
      createRecognizerConfig(e3) {
        return new i2.RecognizerConfig(e3, this.privProperties);
      }
      createServiceRecognizer(e3, t3, r3, i3) {
        const n2 = r3;
        return new c.ConversationServiceAdapter(e3, t3, n2, i3, this);
      }
      sendMessage(e3, t3, r3) {
        ((e4, t4, r4) => {
          void 0 !== e4 ? e4.then(() => {
            try {
              t4 && t4();
            } catch (e5) {
              r4 && r4(`'Unhandled error on promise callback: ${e5}'`);
            }
          }, (e5) => {
            try {
              r4 && r4(e5);
            } catch (e6) {
            }
          }) : r4 && r4("Null promise");
        })(this.privReco.sendMessageAsync(e3), t3, r3), this.resetConversationTimeout();
      }
      resetConversationTimeout() {
        void 0 !== this.privTimeoutToken && this.privClearTimeout(this.privTimeoutToken), this.privTimeoutToken = this.privSetTimeout(() => {
          this.sendRequest(this.privConversation.getKeepAlive());
        }, 6e4);
      }
    }
    t2.ConversationTranslatorRecognizer = p;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationConnectionFactory = void 0;
    const i2 = r2(61), n = r2(4), s = r2(65), o = r2(80), a = r2(130), c = r2(238), p = r2(241);
    class h extends a.ConnectionFactoryBase {
      create(e3, t3, r3) {
        const a2 = e3.parameters.getProperty(o.PropertyId.ConversationTranslator_Host, c.ConversationConnectionConfig.host), h2 = e3.parameters.getProperty(o.PropertyId.ConversationTranslator_CorrelationId, (0, n.createGuid)()), u = `wss://${a2}${c.ConversationConnectionConfig.webSocketPath}`, d = e3.parameters.getProperty(o.PropertyId.ConversationTranslator_Token, void 0);
        s.Contracts.throwIfNullOrUndefined(d, "token");
        const v = {};
        v[c.ConversationConnectionConfig.configParams.apiVersion] = c.ConversationConnectionConfig.apiVersion, v[c.ConversationConnectionConfig.configParams.token] = d, v[c.ConversationConnectionConfig.configParams.correlationId] = h2;
        const l = "true" === e3.parameters.getProperty("SPEECH-EnableWebsocketCompression", "false");
        return Promise.resolve(new i2.WebsocketConnection(u, v, {}, new p.ConversationWebsocketMessageFormatter(), i2.ProxyInfo.fromRecognizerConfig(e3), l, r3));
      }
    }
    t2.ConversationConnectionFactory = h;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationWebsocketMessageFormatter = void 0;
    const i2 = r2(4), n = r2(242);
    t2.ConversationWebsocketMessageFormatter = class {
      toConnectionMessage(e3) {
        const t3 = new i2.Deferred();
        try {
          if (e3.messageType === i2.MessageType.Text) {
            const r3 = new n.ConversationConnectionMessage(e3.messageType, e3.textContent, {}, e3.id);
            t3.resolve(r3);
          } else e3.messageType === i2.MessageType.Binary && t3.resolve(new n.ConversationConnectionMessage(e3.messageType, e3.binaryContent, void 0, e3.id));
        } catch (e4) {
          t3.reject(`Error formatting the message. Error: ${e4}`);
        }
        return t3.promise;
      }
      fromConnectionMessage(e3) {
        const t3 = new i2.Deferred();
        try {
          if (e3.messageType === i2.MessageType.Text) {
            const r3 = `${e3.textBody ? e3.textBody : ""}`;
            t3.resolve(new i2.RawWebsocketMessage(i2.MessageType.Text, r3, e3.id));
          }
        } catch (e4) {
          t3.reject(`Error formatting the message. ${e4}`);
        }
        return t3.promise;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationConnectionMessage = void 0;
    const i2 = r2(4);
    class n extends i2.ConnectionMessage {
      constructor(e3, t3, r3, i3) {
        super(e3, t3, r3, i3);
        const n2 = JSON.parse(this.textBody);
        void 0 !== n2.type && (this.privConversationMessageType = n2.type);
      }
      get conversationMessageType() {
        return this.privConversationMessageType;
      }
    }
    t2.ConversationConnectionMessage = n;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationServiceAdapter = void 0;
    const i2 = r2(4), n = r2(80), s = r2(2), o = r2(242), a = r2(244), c = r2(245), p = r2(246), h = r2(247);
    class u extends s.ServiceRecognizerBase {
      constructor(e3, t3, r3, n2, s2) {
        super(e3, t3, r3, n2, s2), this.privConnectionConfigPromise = void 0, this.privLastPartialUtteranceId = "", this.privConversationServiceConnector = s2, this.privConversationAuthentication = e3, this.receiveMessageOverride = () => this.receiveConversationMessageOverride(), this.recognizeOverride = () => this.noOp(), this.postConnectImplOverride = (e4) => this.conversationConnectImpl(e4), this.configConnectionOverride = () => this.configConnection(), this.disconnectOverride = () => this.privDisconnect(), this.privConversationRequestSession = new a.ConversationRequestSession((0, i2.createNoDashGuid)()), this.privConversationConnectionFactory = t3, this.privConversationIsDisposed = false;
      }
      isDisposed() {
        return super.isDisposed() || this.privConversationIsDisposed;
      }
      async dispose(e3) {
        if (this.privConversationIsDisposed = true, void 0 !== this.privConnectionConfigPromise) {
          const t3 = await this.privConnectionConfigPromise;
          await t3.dispose(e3);
        }
        await super.dispose(e3);
      }
      async sendMessage(e3) {
        return (await this.fetchConnection()).send(new o.ConversationConnectionMessage(i2.MessageType.Text, e3));
      }
      async sendMessageAsync(e3) {
        const t3 = await this.fetchConnection();
        await t3.send(new o.ConversationConnectionMessage(i2.MessageType.Text, e3));
      }
      privDisconnect() {
        if (!this.terminateMessageLoop) return this.cancelRecognition(this.privConversationRequestSession.sessionId, this.privConversationRequestSession.requestId, n.CancellationReason.Error, n.CancellationErrorCode.NoError, "Disconnecting"), this.terminateMessageLoop = true, Promise.resolve();
      }
      async processTypeSpecificMessages() {
        return true;
      }
      cancelRecognition(e3, t3, r3, i3, s2) {
        this.terminateMessageLoop = true;
        const o2 = new n.ConversationTranslationCanceledEventArgs(r3, s2, i3, void 0, e3);
        try {
          this.privConversationServiceConnector.canceled && this.privConversationServiceConnector.canceled(this.privConversationServiceConnector, o2);
        } catch {
        }
      }
      async conversationConnectImpl(e3) {
        return this.privConnectionLoop = this.startMessageLoop(), e3;
      }
      async receiveConversationMessageOverride() {
        if (this.isDisposed() || this.terminateMessageLoop) return Promise.resolve();
        const e3 = new i2.Deferred();
        try {
          const t3 = await this.fetchConnection(), r3 = await t3.read();
          if (this.isDisposed() || this.terminateMessageLoop) return e3.resolve(), Promise.resolve();
          if (!r3) return this.receiveConversationMessageOverride();
          const i3 = this.privConversationRequestSession.sessionId, o2 = r3.conversationMessageType.toLowerCase();
          let a2 = false;
          try {
            switch (o2) {
              case "info":
              case "participant_command":
              case "command":
                const e4 = h.CommandResponsePayload.fromJSON(r3.textBody);
                switch (e4.command.toLowerCase()) {
                  case "participantlist":
                    const t5 = h.ParticipantsListPayloadResponse.fromJSON(r3.textBody), o3 = t5.participants.map((e5) => ({ avatar: e5.avatar, displayName: e5.nickname, id: e5.participantId, isHost: e5.ishost, isMuted: e5.ismuted, isUsingTts: e5.usetts, preferredLanguage: e5.locale }));
                    this.privConversationServiceConnector.participantsListReceived && this.privConversationServiceConnector.participantsListReceived(this.privConversationServiceConnector, new c.ParticipantsListEventArgs(t5.roomid, t5.token, t5.translateTo, t5.profanityFilter, t5.roomProfanityFilter, t5.roomLocked, t5.muteAll, o3, i3));
                    break;
                  case "settranslatetolanguages":
                    this.privConversationServiceConnector.participantUpdateCommandReceived && this.privConversationServiceConnector.participantUpdateCommandReceived(this.privConversationServiceConnector, new c.ParticipantAttributeEventArgs(e4.participantId, p.ConversationTranslatorCommandTypes.setTranslateToLanguages, e4.value, i3));
                    break;
                  case "setprofanityfiltering":
                    this.privConversationServiceConnector.participantUpdateCommandReceived && this.privConversationServiceConnector.participantUpdateCommandReceived(this.privConversationServiceConnector, new c.ParticipantAttributeEventArgs(e4.participantId, p.ConversationTranslatorCommandTypes.setProfanityFiltering, e4.value, i3));
                    break;
                  case "setmute":
                    this.privConversationServiceConnector.participantUpdateCommandReceived && this.privConversationServiceConnector.participantUpdateCommandReceived(this.privConversationServiceConnector, new c.ParticipantAttributeEventArgs(e4.participantId, p.ConversationTranslatorCommandTypes.setMute, e4.value, i3));
                    break;
                  case "setmuteall":
                    this.privConversationServiceConnector.muteAllCommandReceived && this.privConversationServiceConnector.muteAllCommandReceived(this.privConversationServiceConnector, new c.MuteAllEventArgs(e4.value, i3));
                    break;
                  case "roomexpirationwarning":
                    this.privConversationServiceConnector.conversationExpiration && this.privConversationServiceConnector.conversationExpiration(this.privConversationServiceConnector, new n.ConversationExpirationEventArgs(e4.value, this.privConversationRequestSession.sessionId));
                    break;
                  case "setusetts":
                    this.privConversationServiceConnector.participantUpdateCommandReceived && this.privConversationServiceConnector.participantUpdateCommandReceived(this.privConversationServiceConnector, new c.ParticipantAttributeEventArgs(e4.participantId, p.ConversationTranslatorCommandTypes.setUseTTS, e4.value, i3));
                    break;
                  case "setlockstate":
                    this.privConversationServiceConnector.lockRoomCommandReceived && this.privConversationServiceConnector.lockRoomCommandReceived(this.privConversationServiceConnector, new c.LockRoomEventArgs(e4.value, i3));
                    break;
                  case "changenickname":
                    this.privConversationServiceConnector.participantUpdateCommandReceived && this.privConversationServiceConnector.participantUpdateCommandReceived(this.privConversationServiceConnector, new c.ParticipantAttributeEventArgs(e4.participantId, p.ConversationTranslatorCommandTypes.changeNickname, e4.value, i3));
                    break;
                  case "joinsession":
                    const a3 = h.ParticipantPayloadResponse.fromJSON(r3.textBody), u3 = { avatar: a3.avatar, displayName: a3.nickname, id: a3.participantId, isHost: a3.ishost, isMuted: a3.ismuted, isUsingTts: a3.usetts, preferredLanguage: a3.locale };
                    this.privConversationServiceConnector.participantJoinCommandReceived && this.privConversationServiceConnector.participantJoinCommandReceived(this.privConversationServiceConnector, new c.ParticipantEventArgs(u3, i3));
                    break;
                  case "leavesession":
                    const d2 = { id: e4.participantId };
                    this.privConversationServiceConnector.participantLeaveCommandReceived && this.privConversationServiceConnector.participantLeaveCommandReceived(this.privConversationServiceConnector, new c.ParticipantEventArgs(d2, i3));
                    break;
                  case "disconnectsession":
                    e4.participantId;
                    break;
                  case "token":
                    const v2 = new s.CognitiveTokenAuthentication(() => {
                      const t6 = e4.token;
                      return Promise.resolve(t6);
                    }, () => {
                      const t6 = e4.token;
                      return Promise.resolve(t6);
                    });
                    this.authentication = v2, this.privConversationServiceConnector.onToken(v2);
                }
                break;
              case "partial":
              case "final":
                const t4 = h.SpeechResponsePayload.fromJSON(r3.textBody), u2 = "final" === o2 ? n.ResultReason.TranslatedParticipantSpeech : n.ResultReason.TranslatingParticipantSpeech, d = new n.ConversationTranslationResult(t4.participantId, this.getTranslations(t4.translations), t4.language, t4.id, u2, t4.recognition, void 0, void 0, r3.textBody, void 0);
                t4.isFinal ? ((void 0 !== d.text && d.text.length > 0 || t4.id === this.privLastPartialUtteranceId) && (a2 = true), a2 && this.privConversationServiceConnector.translationReceived && this.privConversationServiceConnector.translationReceived(this.privConversationServiceConnector, new c.ConversationReceivedTranslationEventArgs(p.ConversationTranslatorMessageTypes.final, d, i3))) : void 0 !== d.text && (this.privLastPartialUtteranceId = t4.id, this.privConversationServiceConnector.translationReceived && this.privConversationServiceConnector.translationReceived(this.privConversationServiceConnector, new c.ConversationReceivedTranslationEventArgs(p.ConversationTranslatorMessageTypes.partial, d, i3)));
                break;
              case "translated_message":
                const v = h.TextResponsePayload.fromJSON(r3.textBody), l = new n.ConversationTranslationResult(v.participantId, this.getTranslations(v.translations), v.language, void 0, void 0, v.originalText, void 0, void 0, void 0, r3.textBody, void 0);
                this.privConversationServiceConnector.translationReceived && this.privConversationServiceConnector.translationReceived(this.privConversationServiceConnector, new c.ConversationReceivedTranslationEventArgs(p.ConversationTranslatorMessageTypes.instantMessage, l, i3));
            }
          } catch (e4) {
          }
          return this.receiveConversationMessageOverride();
        } catch (e4) {
          this.terminateMessageLoop = true;
        }
        return e3.promise;
      }
      async startMessageLoop() {
        if (this.isDisposed()) return Promise.resolve();
        this.terminateMessageLoop = false;
        const e3 = this.receiveConversationMessageOverride();
        try {
          return await e3;
        } catch (e4) {
          return this.cancelRecognition(this.privRequestSession ? this.privRequestSession.sessionId : "", this.privRequestSession ? this.privRequestSession.requestId : "", n.CancellationReason.Error, n.CancellationErrorCode.RuntimeError, e4), null;
        }
      }
      configConnection() {
        return this.isDisposed() ? Promise.resolve(void 0) : void 0 !== this.privConnectionConfigPromise ? this.privConnectionConfigPromise.then((e3) => e3.state() === i2.ConnectionState.Disconnected ? (this.privConnectionId = null, this.privConnectionConfigPromise = void 0, this.configConnection()) : this.privConnectionConfigPromise, () => (this.privConnectionId = null, this.privConnectionConfigPromise = void 0, this.configConnection())) : this.terminateMessageLoop ? Promise.resolve(void 0) : (this.privConnectionConfigPromise = this.connectImpl().then((e3) => e3), this.privConnectionConfigPromise);
      }
      getTranslations(e3) {
        let t3;
        if (void 0 !== e3) {
          t3 = new n.Translations();
          for (const r3 of e3) t3.set(r3.lang, r3.translation);
        }
        return t3;
      }
    }
    t2.ConversationServiceAdapter = u;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationRequestSession = void 0;
    const i2 = r2(4);
    t2.ConversationRequestSession = class {
      constructor(e3) {
        this.privIsDisposed = false, this.privDetachables = new Array(), this.privSessionId = e3, this.privRequestId = (0, i2.createNoDashGuid)(), this.privRequestCompletionDeferral = new i2.Deferred();
      }
      get sessionId() {
        return this.privSessionId;
      }
      get requestId() {
        return this.privRequestId;
      }
      get completionPromise() {
        return this.privRequestCompletionDeferral.promise;
      }
      onPreConnectionStart(e3, t3) {
        this.privSessionId = t3;
      }
      onAuthCompleted(e3) {
        e3 && this.onComplete();
      }
      onConnectionEstablishCompleted(e3) {
        200 !== e3 && 403 === e3 && this.onComplete();
      }
      onServiceTurnEndResponse(e3) {
        e3 ? this.privRequestId = (0, i2.createNoDashGuid)() : this.onComplete();
      }
      async dispose() {
        if (!this.privIsDisposed) {
          this.privIsDisposed = true;
          for (const e3 of this.privDetachables) await e3.detach();
        }
      }
      onComplete() {
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationReceivedTranslationEventArgs = t2.ParticipantsListEventArgs = t2.ParticipantAttributeEventArgs = t2.ParticipantEventArgs = t2.LockRoomEventArgs = t2.MuteAllEventArgs = void 0;
    const i2 = r2(80);
    class n extends i2.SessionEventArgs {
      constructor(e3, t3) {
        super(t3), this.privIsMuted = e3;
      }
      get isMuted() {
        return this.privIsMuted;
      }
    }
    t2.MuteAllEventArgs = n;
    class s extends i2.SessionEventArgs {
      constructor(e3, t3) {
        super(t3), this.privIsLocked = e3;
      }
      get isMuted() {
        return this.privIsLocked;
      }
    }
    t2.LockRoomEventArgs = s;
    class o extends i2.SessionEventArgs {
      constructor(e3, t3) {
        super(t3), this.privParticipant = e3;
      }
      get participant() {
        return this.privParticipant;
      }
    }
    t2.ParticipantEventArgs = o;
    class a extends i2.SessionEventArgs {
      constructor(e3, t3, r3, i3) {
        super(i3), this.privKey = t3, this.privValue = r3, this.privParticipantId = e3;
      }
      get value() {
        return this.privValue;
      }
      get key() {
        return this.privKey;
      }
      get id() {
        return this.privParticipantId;
      }
    }
    t2.ParticipantAttributeEventArgs = a;
    class c extends i2.SessionEventArgs {
      constructor(e3, t3, r3, i3, n2, s2, o2, a2, c2) {
        super(c2), this.privRoomId = e3, this.privSessionToken = t3, this.privTranslateTo = r3, this.privProfanityFilter = i3, this.privRoomProfanityFilter = n2, this.privIsRoomLocked = s2, this.privIsRoomLocked = o2, this.privParticipants = a2;
      }
      get sessionToken() {
        return this.privSessionToken;
      }
      get conversationId() {
        return this.privRoomId;
      }
      get translateTo() {
        return this.privTranslateTo;
      }
      get profanityFilter() {
        return this.privProfanityFilter;
      }
      get roomProfanityFilter() {
        return this.privRoomProfanityFilter;
      }
      get isRoomLocked() {
        return this.privIsRoomLocked;
      }
      get isMuteAll() {
        return this.privIsMuteAll;
      }
      get participants() {
        return this.privParticipants;
      }
    }
    t2.ParticipantsListEventArgs = c;
    t2.ConversationReceivedTranslationEventArgs = class {
      constructor(e3, t3, r3) {
        this.privPayload = t3, this.privCommand = e3, this.privSessionId = r3;
      }
      get payload() {
        return this.privPayload;
      }
      get command() {
        return this.privCommand;
      }
      get sessionId() {
        return this.privSessionId;
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ConversationTranslatorCommandTypes = t2.ConversationTranslatorMessageTypes = t2.InternalParticipants = void 0;
    t2.InternalParticipants = class {
      constructor(e3 = [], t3) {
        this.participants = e3, this.meId = t3;
      }
      addOrUpdateParticipant(e3) {
        if (void 0 === e3) return;
        const t3 = this.getParticipantIndex(e3.id);
        return t3 > -1 ? this.participants.splice(t3, 1, e3) : this.participants.push(e3), this.getParticipant(e3.id);
      }
      getParticipantIndex(e3) {
        return this.participants.findIndex((t3) => t3.id === e3);
      }
      getParticipant(e3) {
        return this.participants.find((t3) => t3.id === e3);
      }
      deleteParticipant(e3) {
        this.participants = this.participants.filter((t3) => t3.id !== e3);
      }
      get host() {
        return this.participants.find((e3) => true === e3.isHost);
      }
      get me() {
        return this.getParticipant(this.meId);
      }
    }, t2.ConversationTranslatorMessageTypes = { command: "command", final: "final", info: "info", instantMessage: "instant_message", keepAlive: "keep_alive", partial: "partial", participantCommand: "participant_command", translatedMessage: "translated_message" }, t2.ConversationTranslatorCommandTypes = { changeNickname: "ChangeNickname", disconnectSession: "DisconnectSession", ejectParticipant: "EjectParticipant", instant_message: "instant_message", joinSession: "JoinSession", leaveSession: "LeaveSession", participantList: "ParticipantList", roomExpirationWarning: "RoomExpirationWarning", setLockState: "SetLockState", setMute: "SetMute", setMuteAll: "SetMuteAll", setProfanityFiltering: "SetProfanityFiltering", setTranslateToLanguages: "SetTranslateToLanguages", setUseTTS: "SetUseTTS" };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TextResponsePayload = t2.SpeechResponsePayload = t2.ParticipantPayloadResponse = t2.ParticipantsListPayloadResponse = t2.CommandResponsePayload = void 0;
    var i2 = r2(248);
    Object.defineProperty(t2, "CommandResponsePayload", { enumerable: true, get: function() {
      return i2.CommandResponsePayload;
    } });
    var n = r2(249);
    Object.defineProperty(t2, "ParticipantsListPayloadResponse", { enumerable: true, get: function() {
      return n.ParticipantsListPayloadResponse;
    } }), Object.defineProperty(t2, "ParticipantPayloadResponse", { enumerable: true, get: function() {
      return n.ParticipantPayloadResponse;
    } });
    var s = r2(250);
    Object.defineProperty(t2, "SpeechResponsePayload", { enumerable: true, get: function() {
      return s.SpeechResponsePayload;
    } }), Object.defineProperty(t2, "TextResponsePayload", { enumerable: true, get: function() {
      return s.TextResponsePayload;
    } });
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.CommandResponsePayload = void 0;
    class r2 {
      constructor(e3) {
        this.privCommandResponse = ((e4) => JSON.parse(e4))(e3);
      }
      get type() {
        return this.privCommandResponse.type;
      }
      get command() {
        return this.privCommandResponse.command;
      }
      get id() {
        return this.privCommandResponse.id;
      }
      get nickname() {
        return this.privCommandResponse.nickname;
      }
      get participantId() {
        return this.privCommandResponse.participantId;
      }
      get roomid() {
        return this.privCommandResponse.roomid;
      }
      get value() {
        return this.privCommandResponse.value;
      }
      get token() {
        return this.privCommandResponse.token;
      }
      static fromJSON(e3) {
        return new r2(e3);
      }
    }
    t2.CommandResponsePayload = r2;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.ParticipantPayloadResponse = t2.ParticipantsListPayloadResponse = void 0;
    class r2 {
      constructor(e3) {
        this.privParticipantsPayloadResponse = ((e4) => JSON.parse(e4))(e3);
      }
      get roomid() {
        return this.privParticipantsPayloadResponse.roomid;
      }
      get id() {
        return this.privParticipantsPayloadResponse.id;
      }
      get command() {
        return this.privParticipantsPayloadResponse.command;
      }
      get participants() {
        return this.privParticipantsPayloadResponse.participants;
      }
      get token() {
        return this.privParticipantsPayloadResponse.token;
      }
      get translateTo() {
        return this.privParticipantsPayloadResponse.translateTo;
      }
      get profanityFilter() {
        return this.privParticipantsPayloadResponse.profanityFilter;
      }
      get roomProfanityFilter() {
        return this.privParticipantsPayloadResponse.roomProfanityFilter;
      }
      get roomLocked() {
        return this.privParticipantsPayloadResponse.roomLocked;
      }
      get muteAll() {
        return this.privParticipantsPayloadResponse.muteAll;
      }
      get type() {
        return this.privParticipantsPayloadResponse.type;
      }
      static fromJSON(e3) {
        return new r2(e3);
      }
    }
    t2.ParticipantsListPayloadResponse = r2;
    class i2 {
      constructor(e3) {
        this.privParticipantPayloadResponse = ((e4) => JSON.parse(e4))(e3);
      }
      get nickname() {
        return this.privParticipantPayloadResponse.nickname;
      }
      get locale() {
        return this.privParticipantPayloadResponse.locale;
      }
      get usetts() {
        return this.privParticipantPayloadResponse.usetts;
      }
      get ismuted() {
        return this.privParticipantPayloadResponse.ismuted;
      }
      get ishost() {
        return this.privParticipantPayloadResponse.ishost;
      }
      get participantId() {
        return this.privParticipantPayloadResponse.participantId;
      }
      get avatar() {
        return this.privParticipantPayloadResponse.avatar;
      }
      static fromJSON(e3) {
        return new i2(e3);
      }
    }
    t2.ParticipantPayloadResponse = i2;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TextResponsePayload = t2.SpeechResponsePayload = void 0;
    class r2 {
      constructor(e3) {
        this.privSpeechResponse = ((e4) => JSON.parse(e4))(e3);
      }
      get recognition() {
        return this.privSpeechResponse.recognition;
      }
      get translations() {
        return this.privSpeechResponse.translations;
      }
      get id() {
        return this.privSpeechResponse.id;
      }
      get language() {
        return this.privSpeechResponse.language;
      }
      get nickname() {
        return this.privSpeechResponse.nickname;
      }
      get participantId() {
        return this.privSpeechResponse.participantId;
      }
      get roomid() {
        return this.privSpeechResponse.roomid;
      }
      get timestamp() {
        return this.privSpeechResponse.timestamp;
      }
      get type() {
        return this.privSpeechResponse.type;
      }
      get isFinal() {
        return "final" === this.privSpeechResponse.type;
      }
      static fromJSON(e3) {
        return new r2(e3);
      }
    }
    t2.SpeechResponsePayload = r2;
    class i2 {
      constructor(e3) {
        this.privTextResponse = ((e4) => JSON.parse(e4))(e3);
      }
      get originalText() {
        return this.privTextResponse.originalText;
      }
      get translations() {
        return this.privTextResponse.translations;
      }
      get id() {
        return this.privTextResponse.id;
      }
      get language() {
        return this.privTextResponse.language;
      }
      get nickname() {
        return this.privTextResponse.nickname;
      }
      get participantId() {
        return this.privTextResponse.participantId;
      }
      get roomid() {
        return this.privTextResponse.roomid;
      }
      get timestamp() {
        return this.privTextResponse.timestamp;
      }
      get type() {
        return this.privTextResponse.type;
      }
      static fromJSON(e3) {
        return new i2(e3);
      }
    }
    t2.TextResponsePayload = i2;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.TranscriberRecognizer = void 0;
    const i2 = r2(4), n = r2(65), s = r2(80), o = r2(2), a = r2(111);
    class c extends s.Recognizer {
      constructor(e3, t3) {
        const r3 = e3;
        n.Contracts.throwIfNull(r3, "speechTranslationConfig");
        const i3 = t3;
        n.Contracts.throwIfNull(i3, "audioConfigImpl"), n.Contracts.throwIfNullOrWhitespace(r3.speechRecognitionLanguage, s.PropertyId[s.PropertyId.SpeechServiceConnection_RecoLanguage]), super(t3, r3.properties, new o.TranscriberConnectionFactory()), this.privDisposedRecognizer = false, this.isMeetingRecognizer = false;
      }
      get speechRecognitionLanguage() {
        return n.Contracts.throwIfDisposed(this.privDisposedRecognizer), this.properties.getProperty(s.PropertyId.SpeechServiceConnection_RecoLanguage);
      }
      get properties() {
        return this.privProperties;
      }
      get authorizationToken() {
        return this.properties.getProperty(s.PropertyId.SpeechServiceAuthorization_Token);
      }
      set authorizationToken(e3) {
        n.Contracts.throwIfNullOrWhitespace(e3, "token"), this.properties.setProperty(s.PropertyId.SpeechServiceAuthorization_Token, e3);
      }
      set conversation(e3) {
        n.Contracts.throwIfNullOrUndefined(e3, "Conversation"), this.isMeetingRecognizer = false, this.privConversation = e3;
      }
      getConversationInfo() {
        return n.Contracts.throwIfNullOrUndefined(this.privConversation, "Conversation"), this.privConversation.conversationInfo;
      }
      set meeting(e3) {
        n.Contracts.throwIfNullOrUndefined(e3, "Meeting"), this.isMeetingRecognizer = true, this.privMeeting = e3;
      }
      getMeetingInfo() {
        return n.Contracts.throwIfNullOrUndefined(this.privMeeting, "Meeting"), this.privMeeting.meetingInfo;
      }
      IsMeetingRecognizer() {
        return this.isMeetingRecognizer;
      }
      startContinuousRecognitionAsync(e3, t3) {
        (0, i2.marshalPromiseToCallbacks)(this.startContinuousRecognitionAsyncImpl(a.RecognitionMode.Conversation), e3, t3);
      }
      stopContinuousRecognitionAsync(e3, t3) {
        (0, i2.marshalPromiseToCallbacks)(this.stopContinuousRecognitionAsyncImpl(), e3, t3);
      }
      async close() {
        this.privDisposedRecognizer || await this.dispose(true);
      }
      async pushConversationEvent(e3, t3) {
        const r3 = this.privReco;
        n.Contracts.throwIfNullOrUndefined(r3, "serviceRecognizer"), await r3.sendSpeechEventAsync(e3, t3);
      }
      async pushMeetingEvent(e3, t3) {
        const r3 = this.privReco;
        n.Contracts.throwIfNullOrUndefined(r3, "serviceRecognizer"), await r3.sendMeetingSpeechEventAsync(e3, t3);
      }
      async enforceAudioGating() {
        const e3 = this.audioConfig, t3 = (await e3.format).channels;
        if (1 === t3) {
          if ("true" !== this.properties.getProperty("f0f5debc-f8c9-4892-ac4b-90a7ab359fd2", "false").toLowerCase()) throw new Error("Single channel audio configuration for MeetingTranscriber is currently under private preview, please contact diarizationrequest@microsoft.com for more details");
        } else if (8 !== t3) throw new Error(`Unsupported audio configuration: Detected ${t3}-channel audio`);
      }
      connectMeetingCallbacks(e3) {
        this.isMeetingRecognizer = true, this.canceled = (t3, r3) => {
          e3.canceled && e3.canceled(e3, r3);
        }, this.recognizing = (t3, r3) => {
          e3.transcribing && e3.transcribing(e3, r3);
        }, this.recognized = (t3, r3) => {
          e3.transcribed && e3.transcribed(e3, r3);
        }, this.sessionStarted = (t3, r3) => {
          e3.sessionStarted && e3.sessionStarted(e3, r3);
        }, this.sessionStopped = (t3, r3) => {
          e3.sessionStopped && e3.sessionStopped(e3, r3);
        };
      }
      disconnectCallbacks() {
        this.canceled = void 0, this.recognizing = void 0, this.recognized = void 0, this.sessionStarted = void 0, this.sessionStopped = void 0;
      }
      async dispose(e3) {
        this.privDisposedRecognizer || (e3 && (this.privDisposedRecognizer = true, await this.implRecognizerStop()), await super.dispose(e3));
      }
      createRecognizerConfig(e3) {
        return new o.RecognizerConfig(e3, this.properties);
      }
      createServiceRecognizer(e3, t3, r3, i3) {
        const n2 = r3;
        return new o.TranscriptionServiceRecognizer(e3, t3, n2, i3, this);
      }
    }
    t2.TranscriberRecognizer = c;
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisAudioMetadata = t2.MetadataType = void 0, function(e3) {
      e3.WordBoundary = "WordBoundary", e3.Bookmark = "Bookmark", e3.Viseme = "Viseme", e3.SentenceBoundary = "SentenceBoundary", e3.SessionEnd = "SessionEnd", e3.AvatarSignal = "TalkingAvatarSignal";
    }(t2.MetadataType || (t2.MetadataType = {}));
    class r2 {
      constructor(e3) {
        this.privSynthesisAudioMetadata = JSON.parse(e3);
      }
      static fromJSON(e3) {
        return new r2(e3);
      }
      get Metadata() {
        return this.privSynthesisAudioMetadata.Metadata;
      }
    }
    t2.SynthesisAudioMetadata = r2;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisTurn = void 0;
    const i2 = r2(4), n = r2(85), s = r2(80), o = r2(252), a = r2(254);
    class c {
      constructor() {
        this.privIsDisposed = false, this.privIsSynthesizing = false, this.privIsSynthesisEnded = false, this.privBytesReceived = 0, this.privInTurn = false, this.privTextOffset = 0, this.privNextSearchTextIndex = 0, this.privSentenceOffset = 0, this.privNextSearchSentenceIndex = 0, this.privRequestId = (0, i2.createNoDashGuid)(), this.privTurnDeferral = new i2.Deferred(), this.privTurnDeferral.resolve();
      }
      get requestId() {
        return this.privRequestId;
      }
      get streamId() {
        return this.privStreamId;
      }
      set streamId(e3) {
        this.privStreamId = e3;
      }
      get audioOutputFormat() {
        return this.privAudioOutputFormat;
      }
      set audioOutputFormat(e3) {
        this.privAudioOutputFormat = e3;
      }
      get turnCompletionPromise() {
        return this.privTurnDeferral.promise;
      }
      get isSynthesisEnded() {
        return this.privIsSynthesisEnded;
      }
      get isSynthesizing() {
        return this.privIsSynthesizing;
      }
      get currentTextOffset() {
        return this.privTextOffset;
      }
      get currentSentenceOffset() {
        return this.privSentenceOffset;
      }
      get bytesReceived() {
        return this.privBytesReceived;
      }
      get audioDuration() {
        return this.privAudioDuration;
      }
      get extraProperties() {
        if (this.privWebRTCSDP) {
          const e3 = new s.PropertyCollection();
          return e3.setProperty(s.PropertyId.TalkingAvatarService_WebRTC_SDP, this.privWebRTCSDP), e3;
        }
      }
      async getAllReceivedAudio() {
        return this.privReceivedAudio ? Promise.resolve(this.privReceivedAudio) : this.privIsSynthesisEnded ? (await this.readAllAudioFromStream(), Promise.resolve(this.privReceivedAudio)) : null;
      }
      async getAllReceivedAudioWithHeader() {
        if (this.privReceivedAudioWithHeader) return this.privReceivedAudioWithHeader;
        if (!this.privIsSynthesisEnded) return null;
        if (this.audioOutputFormat.hasHeader) {
          const e3 = await this.getAllReceivedAudio();
          return this.privReceivedAudioWithHeader = this.audioOutputFormat.addHeader(e3), this.privReceivedAudioWithHeader;
        }
        return this.getAllReceivedAudio();
      }
      startNewSynthesis(e3, t3, r3, i3) {
        this.privIsSynthesisEnded = false, this.privIsSynthesizing = true, this.privRequestId = e3, this.privRawText = t3, this.privIsSSML = r3, this.privAudioOutputStream = new n.PullAudioOutputStreamImpl(), this.privAudioOutputStream.format = this.privAudioOutputFormat, this.privReceivedAudio = null, this.privReceivedAudioWithHeader = null, this.privBytesReceived = 0, this.privTextOffset = 0, this.privNextSearchTextIndex = 0, this.privSentenceOffset = 0, this.privNextSearchSentenceIndex = 0, this.privPartialVisemeAnimation = "", this.privWebRTCSDP = "", void 0 !== i3 && (this.privTurnAudioDestination = i3, this.privTurnAudioDestination.format = this.privAudioOutputFormat), this.onEvent(new a.SynthesisTriggeredEvent(this.requestId, void 0, void 0 === i3 ? void 0 : i3.id()));
      }
      onPreConnectionStart(e3) {
        this.privAuthFetchEventId = e3, this.onEvent(new a.ConnectingToSynthesisServiceEvent(this.privRequestId, this.privAuthFetchEventId));
      }
      onAuthCompleted(e3) {
        e3 && this.onComplete();
      }
      onConnectionEstablishCompleted(e3) {
        if (200 === e3) return this.onEvent(new a.SynthesisStartedEvent(this.requestId, this.privAuthFetchEventId)), void (this.privBytesReceived = 0);
        403 === e3 && this.onComplete();
      }
      onServiceResponseMessage(e3) {
        const t3 = JSON.parse(e3);
        this.streamId = t3.audio.streamId;
      }
      onServiceTurnEndResponse() {
        this.privInTurn = false, this.privTurnDeferral.resolve(), this.onComplete();
      }
      onServiceTurnStartResponse(e3) {
        this.privTurnDeferral && this.privInTurn && (this.privTurnDeferral.reject("Another turn started before current completed."), this.privTurnDeferral.promise.then().catch(() => {
        })), this.privInTurn = true, this.privTurnDeferral = new i2.Deferred();
        const t3 = JSON.parse(e3);
        t3.webrtc && (this.privWebRTCSDP = t3.webrtc.connectionString);
      }
      onAudioChunkReceived(e3) {
        this.isSynthesizing && (this.privAudioOutputStream.write(e3), this.privBytesReceived += e3.byteLength, void 0 !== this.privTurnAudioDestination && this.privTurnAudioDestination.write(e3));
      }
      onTextBoundaryEvent(e3) {
        this.updateTextOffset(e3.Data.text.Text, e3.Type);
      }
      onVisemeMetadataReceived(e3) {
        void 0 !== e3.Data.AnimationChunk && (this.privPartialVisemeAnimation += e3.Data.AnimationChunk);
      }
      onSessionEnd(e3) {
        this.privAudioDuration = e3.Data.Offset;
      }
      async constructSynthesisResult() {
        const e3 = await this.getAllReceivedAudioWithHeader();
        return new s.SpeechSynthesisResult(this.requestId, s.ResultReason.SynthesizingAudioCompleted, e3, void 0, this.extraProperties, this.audioDuration);
      }
      dispose() {
        this.privIsDisposed || (this.privIsDisposed = true);
      }
      onStopSynthesizing() {
        this.onComplete();
      }
      getAndClearVisemeAnimation() {
        const e3 = this.privPartialVisemeAnimation;
        return this.privPartialVisemeAnimation = "", e3;
      }
      onEvent(e3) {
        i2.Events.instance.onEvent(e3);
      }
      static isXmlTag(e3) {
        return e3.length >= 2 && "<" === e3[0] && ">" === e3[e3.length - 1];
      }
      updateTextOffset(e3, t3) {
        t3 === o.MetadataType.WordBoundary ? (this.privTextOffset = this.privRawText.indexOf(e3, this.privNextSearchTextIndex), this.privTextOffset >= 0 && (this.privNextSearchTextIndex = this.privTextOffset + e3.length, this.privIsSSML && this.withinXmlTag(this.privTextOffset) && !c.isXmlTag(e3) && this.updateTextOffset(e3, t3))) : (this.privSentenceOffset = this.privRawText.indexOf(e3, this.privNextSearchSentenceIndex), this.privSentenceOffset >= 0 && (this.privNextSearchSentenceIndex = this.privSentenceOffset + e3.length, this.privIsSSML && this.withinXmlTag(this.privSentenceOffset) && !c.isXmlTag(e3) && this.updateTextOffset(e3, t3)));
      }
      onComplete() {
        this.privIsSynthesizing && (this.privIsSynthesizing = false, this.privIsSynthesisEnded = true, this.privAudioOutputStream.close(), this.privInTurn = false, void 0 !== this.privTurnAudioDestination && (this.privTurnAudioDestination.close(), this.privTurnAudioDestination = void 0));
      }
      async readAllAudioFromStream() {
        if (this.privIsSynthesisEnded) {
          this.privReceivedAudio = new ArrayBuffer(this.bytesReceived);
          try {
            await this.privAudioOutputStream.read(this.privReceivedAudio);
          } catch (e3) {
            this.privReceivedAudio = new ArrayBuffer(0);
          }
        }
      }
      withinXmlTag(e3) {
        return this.privRawText.indexOf("<", e3 + 1) > this.privRawText.indexOf(">", e3 + 1);
      }
    }
    t2.SynthesisTurn = c;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisStartedEvent = t2.ConnectingToSynthesisServiceEvent = t2.SynthesisTriggeredEvent = t2.SpeechSynthesisEvent = void 0;
    const i2 = r2(4);
    class n extends i2.PlatformEvent {
      constructor(e3, t3, r3 = i2.EventType.Info) {
        super(e3, r3), this.privRequestId = t3;
      }
      get requestId() {
        return this.privRequestId;
      }
    }
    t2.SpeechSynthesisEvent = n;
    t2.SynthesisTriggeredEvent = class extends n {
      constructor(e3, t3, r3) {
        super("SynthesisTriggeredEvent", e3), this.privSessionAudioDestinationId = t3, this.privTurnAudioDestinationId = r3;
      }
      get audioSessionDestinationId() {
        return this.privSessionAudioDestinationId;
      }
      get audioTurnDestinationId() {
        return this.privTurnAudioDestinationId;
      }
    };
    t2.ConnectingToSynthesisServiceEvent = class extends n {
      constructor(e3, t3) {
        super("ConnectingToSynthesisServiceEvent", e3), this.privAuthFetchEventId = t3;
      }
      get authFetchEventId() {
        return this.privAuthFetchEventId;
      }
    };
    t2.SynthesisStartedEvent = class extends n {
      constructor(e3, t3) {
        super("SynthesisStartedEvent", e3), this.privAuthFetchEventId = t3;
      }
      get authFetchEventId() {
        return this.privAuthFetchEventId;
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisAdapterBase = void 0;
    const i2 = r2(4), n = r2(80), s = r2(2), o = r2(190);
    class a {
      constructor(e3, t3, r3, o2) {
        if (this.speakOverride = void 0, this.receiveMessageOverride = void 0, this.connectImplOverride = void 0, this.configConnectionOverride = void 0, this.privConnectionConfigurationPromise = void 0, !e3) throw new i2.ArgumentNullError("authentication");
        if (!t3) throw new i2.ArgumentNullError("connectionFactory");
        if (!r3) throw new i2.ArgumentNullError("synthesizerConfig");
        this.privAuthentication = e3, this.privConnectionFactory = t3, this.privSynthesizerConfig = r3, this.privIsDisposed = false, this.privSessionAudioDestination = o2, this.privSynthesisTurn = new s.SynthesisTurn(), this.privConnectionEvents = new i2.EventSource(), this.privServiceEvents = new i2.EventSource(), this.privSynthesisContext = new s.SynthesisContext(), this.privAgentConfig = new s.AgentConfig(), this.connectionEvents.attach((e4) => {
          if ("ConnectionClosedEvent" === e4.name) {
            const t4 = e4;
            1e3 !== t4.statusCode && this.cancelSynthesisLocal(n.CancellationReason.Error, 1007 === t4.statusCode ? n.CancellationErrorCode.BadRequestParameters : n.CancellationErrorCode.ConnectionFailure, `${t4.reason} websocket error code: ${t4.statusCode}`);
          }
        });
      }
      get synthesizerConfig() {
        return this.privSynthesizerConfig;
      }
      get synthesisContext() {
        return this.privSynthesisContext;
      }
      get agentConfig() {
        return this.privAgentConfig;
      }
      get connectionEvents() {
        return this.privConnectionEvents;
      }
      get serviceEvents() {
        return this.privServiceEvents;
      }
      set activityTemplate(e3) {
        this.privActivityTemplate = e3;
      }
      get activityTemplate() {
        return this.privActivityTemplate;
      }
      set audioOutputFormat(e3) {
        this.privAudioOutputFormat = e3, this.privSynthesisTurn.audioOutputFormat = e3, void 0 !== this.privSessionAudioDestination && (this.privSessionAudioDestination.format = e3), void 0 !== this.synthesisContext && (this.synthesisContext.audioOutputFormat = e3);
      }
      isDisposed() {
        return this.privIsDisposed;
      }
      async dispose(e3) {
        if (this.privIsDisposed = true, void 0 !== this.privSessionAudioDestination && this.privSessionAudioDestination.close(), void 0 !== this.privConnectionConfigurationPromise) {
          const t3 = await this.privConnectionConfigurationPromise;
          await t3.dispose(e3);
        }
      }
      async connect() {
        await this.connectImpl();
      }
      async sendNetworkMessage(e3, t3) {
        const r3 = "string" == typeof t3 ? i2.MessageType.Text : i2.MessageType.Binary, n2 = "string" == typeof t3 ? "application/json" : "";
        return (await this.fetchConnection()).send(new o.SpeechConnectionMessage(r3, e3, this.privSynthesisTurn.requestId, n2, t3));
      }
      async Speak(e3, t3, r3, i3, s2, o2) {
        let a2;
        if (a2 = t3 ? e3 : this.privSynthesizer.buildSsml(e3), void 0 !== this.speakOverride) return this.speakOverride(a2, r3, i3, s2);
        this.privSuccessCallback = i3, this.privErrorCallback = s2, this.privSynthesisTurn.startNewSynthesis(r3, e3, t3, o2);
        try {
          await this.connectImpl();
          const e4 = await this.fetchConnection();
          await this.sendSynthesisContext(e4), await this.sendSsmlMessage(e4, a2, r3), this.onSynthesisStarted(r3), this.receiveMessage();
        } catch (e4) {
          return this.cancelSynthesisLocal(n.CancellationReason.Error, n.CancellationErrorCode.ConnectionFailure, e4), Promise.reject(e4);
        }
      }
      async stopSpeaking() {
        await this.connectImpl();
        return (await this.fetchConnection()).send(new o.SpeechConnectionMessage(i2.MessageType.Text, "synthesis.control", this.privSynthesisTurn.requestId, "application/json", JSON.stringify({ action: "stop" })));
      }
      cancelSynthesis(e3, t3, r3, i3) {
        const o2 = new n.PropertyCollection();
        o2.setProperty(s.CancellationErrorCodePropertyName, n.CancellationErrorCode[r3]);
        const a2 = new n.SpeechSynthesisResult(e3, n.ResultReason.Canceled, void 0, i3, o2);
        if (this.onSynthesisCancelled(a2), this.privSuccessCallback) try {
          this.privSuccessCallback(a2);
        } catch {
        }
      }
      cancelSynthesisLocal(e3, t3, r3) {
        this.privSynthesisTurn.isSynthesizing && (this.privSynthesisTurn.onStopSynthesizing(), this.cancelSynthesis(this.privSynthesisTurn.requestId, e3, t3, r3));
      }
      processTypeSpecificMessages(e3) {
        return true;
      }
      async receiveMessage() {
        try {
          const e3 = await this.fetchConnection(), t3 = await e3.read();
          if (void 0 !== this.receiveMessageOverride) return this.receiveMessageOverride();
          if (this.privIsDisposed) return;
          if (!t3) return this.privSynthesisTurn.isSynthesizing ? this.receiveMessage() : void 0;
          const r3 = o.SpeechConnectionMessage.fromConnectionMessage(t3);
          if (r3.requestId.toLowerCase() === this.privSynthesisTurn.requestId.toLowerCase()) switch (r3.path.toLowerCase()) {
            case "turn.start":
              this.privSynthesisTurn.onServiceTurnStartResponse(r3.textBody);
              break;
            case "response":
              this.privSynthesisTurn.onServiceResponseMessage(r3.textBody);
              break;
            case "audio":
              this.privSynthesisTurn.streamId.toLowerCase() === r3.streamId.toLowerCase() && r3.binaryBody && (this.privSynthesisTurn.onAudioChunkReceived(r3.binaryBody), this.onSynthesizing(r3.binaryBody), void 0 !== this.privSessionAudioDestination && this.privSessionAudioDestination.write(r3.binaryBody));
              break;
            case "audio.metadata":
              const e4 = s.SynthesisAudioMetadata.fromJSON(r3.textBody).Metadata;
              for (const t5 of e4) switch (t5.Type) {
                case s.MetadataType.WordBoundary:
                case s.MetadataType.SentenceBoundary:
                  this.privSynthesisTurn.onTextBoundaryEvent(t5);
                  const e5 = new n.SpeechSynthesisWordBoundaryEventArgs(t5.Data.Offset, t5.Data.Duration, t5.Data.text.Text, t5.Data.text.Length, t5.Type === s.MetadataType.WordBoundary ? this.privSynthesisTurn.currentTextOffset : this.privSynthesisTurn.currentSentenceOffset, t5.Data.text.BoundaryType);
                  this.onWordBoundary(e5);
                  break;
                case s.MetadataType.Bookmark:
                  const r4 = new n.SpeechSynthesisBookmarkEventArgs(t5.Data.Offset, t5.Data.Bookmark);
                  this.onBookmarkReached(r4);
                  break;
                case s.MetadataType.Viseme:
                  if (this.privSynthesisTurn.onVisemeMetadataReceived(t5), t5.Data.IsLastAnimation) {
                    const e6 = new n.SpeechSynthesisVisemeEventArgs(t5.Data.Offset, t5.Data.VisemeId, this.privSynthesisTurn.getAndClearVisemeAnimation());
                    this.onVisemeReceived(e6);
                  }
                  break;
                case s.MetadataType.AvatarSignal:
                  this.onAvatarEvent(t5);
                  break;
                case s.MetadataType.SessionEnd:
                  this.privSynthesisTurn.onSessionEnd(t5);
              }
              break;
            case "turn.end":
              let t4;
              this.privSynthesisTurn.onServiceTurnEndResponse();
              try {
                t4 = await this.privSynthesisTurn.constructSynthesisResult(), this.privSuccessCallback && this.privSuccessCallback(t4);
              } catch (e5) {
                this.privErrorCallback && this.privErrorCallback(e5);
              }
              this.onSynthesisCompleted(t4);
              break;
            default:
              this.processTypeSpecificMessages(r3) || this.privServiceEvents && this.serviceEvents.onEvent(new i2.ServiceEvent(r3.path.toLowerCase(), r3.textBody));
          }
          return this.receiveMessage();
        } catch (e3) {
        }
      }
      sendSynthesisContext(e3) {
        this.setSynthesisContextSynthesisSection();
        const t3 = this.synthesisContext.toJSON();
        if (t3) return e3.send(new o.SpeechConnectionMessage(i2.MessageType.Text, "synthesis.context", this.privSynthesisTurn.requestId, "application/json", t3));
      }
      setSpeechConfigSynthesisSection() {
      }
      connectImpl(e3 = false) {
        if (null != this.privConnectionPromise) return this.privConnectionPromise.then((e4) => e4.state() === i2.ConnectionState.Disconnected ? (this.privConnectionId = null, this.privConnectionPromise = null, this.connectImpl()) : this.privConnectionPromise, () => (this.privConnectionId = null, this.privConnectionPromise = null, this.connectImpl()));
        this.privAuthFetchEventId = (0, i2.createNoDashGuid)(), this.privConnectionId = (0, i2.createNoDashGuid)(), this.privSynthesisTurn.onPreConnectionStart(this.privAuthFetchEventId);
        const t3 = e3 ? this.privAuthentication.fetchOnExpiry(this.privAuthFetchEventId) : this.privAuthentication.fetch(this.privAuthFetchEventId);
        return this.privConnectionPromise = t3.then(async (t4) => {
          this.privSynthesisTurn.onAuthCompleted(false);
          const r3 = await this.privConnectionFactory.create(this.privSynthesizerConfig, t4, this.privConnectionId);
          r3.events.attach((e4) => {
            this.connectionEvents.onEvent(e4);
          });
          const i3 = await r3.open();
          return 200 === i3.statusCode ? (this.privSynthesisTurn.onConnectionEstablishCompleted(i3.statusCode), Promise.resolve(r3)) : 403 !== i3.statusCode || e3 ? (this.privSynthesisTurn.onConnectionEstablishCompleted(i3.statusCode), Promise.reject(`Unable to contact server. StatusCode: ${i3.statusCode},
                    ${this.privSynthesizerConfig.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Url)} Reason: ${i3.reason}`)) : this.connectImpl(true);
        }, (e4) => {
          throw this.privSynthesisTurn.onAuthCompleted(true), new Error(e4);
        }), this.privConnectionPromise.catch(() => {
        }), this.privConnectionPromise;
      }
      sendSpeechServiceConfig(e3, t3) {
        if (t3) return e3.send(new o.SpeechConnectionMessage(i2.MessageType.Text, "speech.config", this.privSynthesisTurn.requestId, "application/json", t3));
      }
      sendSsmlMessage(e3, t3, r3) {
        return e3.send(new o.SpeechConnectionMessage(i2.MessageType.Text, "ssml", r3, "application/ssml+xml", t3));
      }
      async fetchConnection() {
        return void 0 !== this.privConnectionConfigurationPromise ? this.privConnectionConfigurationPromise.then((e3) => e3.state() === i2.ConnectionState.Disconnected ? (this.privConnectionId = null, this.privConnectionConfigurationPromise = void 0, this.fetchConnection()) : this.privConnectionConfigurationPromise, () => (this.privConnectionId = null, this.privConnectionConfigurationPromise = void 0, this.fetchConnection())) : (this.privConnectionConfigurationPromise = this.configureConnection(), await this.privConnectionConfigurationPromise);
      }
      async configureConnection() {
        const e3 = await this.connectImpl();
        return void 0 !== this.configConnectionOverride ? this.configConnectionOverride(e3) : (this.setSpeechConfigSynthesisSection(), await this.sendSpeechServiceConfig(e3, this.privSynthesizerConfig.SpeechServiceConfig.serialize()), e3);
      }
      onAvatarEvent(e3) {
      }
      onSynthesisStarted(e3) {
      }
      onSynthesizing(e3) {
      }
      onSynthesisCancelled(e3) {
      }
      onSynthesisCompleted(e3) {
      }
      onWordBoundary(e3) {
      }
      onVisemeReceived(e3) {
      }
      onBookmarkReached(e3) {
      }
    }
    t2.SynthesisAdapterBase = a, a.telemetryDataEnabled = true;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.AvatarSynthesisAdapter = void 0;
    const i2 = r2(80), n = r2(2);
    class s extends n.SynthesisAdapterBase {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, void 0), this.privAvatarSynthesizer = i3, this.privSynthesizer = i3, this.privAvatarConfig = n2;
      }
      setSynthesisContextSynthesisSection() {
        this.privSynthesisContext.setSynthesisSection(void 0);
      }
      setSpeechConfigSynthesisSection() {
        this.privSynthesizerConfig.synthesisVideoSection = { format: { bitrate: this.privAvatarConfig.videoFormat?.bitrate, codec: this.privAvatarConfig.videoFormat?.codec, crop: { bottomRight: { x: this.privAvatarConfig.videoFormat?.cropRange?.bottomRight?.x, y: this.privAvatarConfig.videoFormat?.cropRange?.bottomRight?.y }, topLeft: { x: this.privAvatarConfig.videoFormat?.cropRange?.topLeft?.x, y: this.privAvatarConfig.videoFormat?.cropRange?.topLeft?.y } }, resolution: { height: this.privAvatarConfig.videoFormat?.height, width: this.privAvatarConfig.videoFormat?.width } }, protocol: { name: "WebRTC", webrtcConfig: { clientDescription: btoa(this.privSynthesizerConfig.parameters.getProperty(i2.PropertyId.TalkingAvatarService_WebRTC_SDP)), iceServers: this.privAvatarConfig.remoteIceServers ?? this.privAvatarSynthesizer.iceServers } }, talkingAvatar: { background: { color: this.privAvatarConfig.backgroundColor, image: { url: this.privAvatarConfig.backgroundImage?.toString() } }, character: this.privAvatarConfig.character, customized: this.privAvatarConfig.customized, photoAvatarBaseModel: this.privAvatarConfig.photoAvatarBaseModel, style: this.privAvatarConfig.style, useBuiltInVoice: this.privAvatarConfig.useBuiltInVoice } };
      }
      onAvatarEvent(e3) {
        if (this.privAvatarSynthesizer.avatarEventReceived) {
          const t3 = new i2.AvatarEventArgs(e3.Data.Offset, e3.Data.Name);
          try {
            this.privAvatarSynthesizer.avatarEventReceived(this.privAvatarSynthesizer, t3);
          } catch (e4) {
          }
        }
      }
    }
    t2.AvatarSynthesisAdapter = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SpeechSynthesisAdapter = void 0;
    const i2 = r2(80), n = r2(2);
    class s extends n.SynthesisAdapterBase {
      constructor(e3, t3, r3, i3, n2) {
        super(e3, t3, r3, n2), this.privSpeechSynthesizer = i3, this.privSynthesizer = i3;
      }
      setSynthesisContextSynthesisSection() {
        this.privSynthesisContext.setSynthesisSection(this.privSpeechSynthesizer);
      }
      onSynthesisStarted(e3) {
        const t3 = new i2.SpeechSynthesisEventArgs(new i2.SpeechSynthesisResult(e3, i2.ResultReason.SynthesizingAudioStarted));
        this.privSpeechSynthesizer.synthesisStarted && this.privSpeechSynthesizer.synthesisStarted(this.privSpeechSynthesizer, t3);
      }
      onSynthesizing(e3) {
        if (this.privSpeechSynthesizer.synthesizing) try {
          const t3 = this.privSynthesisTurn.audioOutputFormat.addHeader(e3), r3 = new i2.SpeechSynthesisEventArgs(new i2.SpeechSynthesisResult(this.privSynthesisTurn.requestId, i2.ResultReason.SynthesizingAudio, t3));
          this.privSpeechSynthesizer.synthesizing(this.privSpeechSynthesizer, r3);
        } catch (e4) {
        }
      }
      onSynthesisCancelled(e3) {
        if (this.privSpeechSynthesizer.SynthesisCanceled) {
          const t3 = new i2.SpeechSynthesisEventArgs(e3);
          try {
            this.privSpeechSynthesizer.SynthesisCanceled(this.privSpeechSynthesizer, t3);
          } catch {
          }
        }
      }
      onSynthesisCompleted(e3) {
        if (this.privSpeechSynthesizer.synthesisCompleted) try {
          this.privSpeechSynthesizer.synthesisCompleted(this.privSpeechSynthesizer, new i2.SpeechSynthesisEventArgs(e3));
        } catch (e4) {
        }
      }
      onWordBoundary(e3) {
        if (this.privSpeechSynthesizer.wordBoundary) try {
          this.privSpeechSynthesizer.wordBoundary(this.privSpeechSynthesizer, e3);
        } catch (e4) {
        }
      }
      onVisemeReceived(e3) {
        if (this.privSpeechSynthesizer.visemeReceived) try {
          this.privSpeechSynthesizer.visemeReceived(this.privSpeechSynthesizer, e3);
        } catch (e4) {
        }
      }
      onBookmarkReached(e3) {
        if (this.privSpeechSynthesizer.bookmarkReached) try {
          this.privSpeechSynthesizer.bookmarkReached(this.privSpeechSynthesizer, e3);
        } catch (e4) {
        }
      }
    }
    t2.SpeechSynthesisAdapter = s;
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisRestAdapter = void 0;
    const i2 = r2(61), n = r2(80), s = r2(130), o = r2(54);
    t2.SynthesisRestAdapter = class {
      constructor(e3, t3) {
        let r3 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Endpoint, void 0);
        if (!r3) {
          const t4 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Region, "westus"), i3 = s.ConnectionFactoryBase.getHostSuffix(t4);
          r3 = e3.parameters.getProperty(n.PropertyId.SpeechServiceConnection_Host, `https://${t4}.tts.speech${i3}`);
        }
        this.privUri = `${r3}/cognitiveservices/voices/list`;
        const o2 = i2.RestConfigBase.requestOptions;
        this.privRestAdapter = new i2.RestMessageAdapter(o2), this.privAuthentication = t3;
      }
      getVoicesList(e3) {
        return this.privRestAdapter.setHeaders(o.HeaderNames.ConnectionId, e3), this.privAuthentication.fetch(e3).then((e4) => (this.privRestAdapter.setHeaders(e4.headerName, e4.token), this.privRestAdapter.request(i2.RestRequestType.Get, this.privUri)));
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesizerConfig = t2.SynthesisServiceType = void 0;
    const i2 = r2(2);
    var n;
    !function(e3) {
      e3[e3.Standard = 0] = "Standard", e3[e3.Custom = 1] = "Custom";
    }(n = t2.SynthesisServiceType || (t2.SynthesisServiceType = {}));
    t2.SynthesizerConfig = class {
      constructor(e3, t3) {
        this.privSynthesisServiceType = n.Standard, this.avatarEnabled = false, this.privSpeechServiceConfig = e3 || new i2.SpeechServiceConfig(new i2.Context(null)), this.privParameters = t3;
      }
      get parameters() {
        return this.privParameters;
      }
      get synthesisServiceType() {
        return this.privSynthesisServiceType;
      }
      set synthesisServiceType(e3) {
        this.privSynthesisServiceType = e3;
      }
      set synthesisVideoSection(e3) {
        this.privSpeechServiceConfig.Context.synthesis = { video: e3 };
      }
      get SpeechServiceConfig() {
        return this.privSpeechServiceConfig;
      }
      setContextFromJson(e3) {
        const t3 = JSON.parse(e3);
        t3.system && (this.privSpeechServiceConfig.Context.system = t3.system), t3.os && (this.privSpeechServiceConfig.Context.os = t3.os), t3.audio && (this.privSpeechServiceConfig.Context.audio = t3.audio), t3.synthesis && (this.privSpeechServiceConfig.Context.synthesis = t3.synthesis);
      }
    };
  }, (e2, t2, r2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.SynthesisContext = void 0;
    const i2 = r2(80);
    t2.SynthesisContext = class {
      constructor() {
        this.privContext = {};
      }
      setSection(e3, t3) {
        this.privContext[e3] = t3;
      }
      set audioOutputFormat(e3) {
        this.privAudioOutputFormat = e3;
      }
      toJSON() {
        return JSON.stringify(this.privContext);
      }
      setSynthesisSection(e3) {
        const t3 = this.buildSynthesisContext(e3);
        this.setSection("synthesis", t3);
      }
      buildSynthesisContext(e3) {
        return { audio: { metadataOptions: { bookmarkEnabled: !!e3?.bookmarkReached, punctuationBoundaryEnabled: e3?.properties.getProperty(i2.PropertyId.SpeechServiceResponse_RequestPunctuationBoundary, !!e3?.wordBoundary), sentenceBoundaryEnabled: e3?.properties.getProperty(i2.PropertyId.SpeechServiceResponse_RequestSentenceBoundary, false), sessionEndEnabled: true, visemeEnabled: !!e3?.visemeReceived, wordBoundaryEnabled: e3?.properties.getProperty(i2.PropertyId.SpeechServiceResponse_RequestWordBoundary, !!e3?.wordBoundary) }, outputFormat: this.privAudioOutputFormat.requestAudioFormatString }, language: { autoDetection: e3?.autoDetectSourceLanguage } };
      }
    };
  }, (e2, t2) => {
    "use strict";
    Object.defineProperty(t2, "__esModule", { value: true }), t2.type = t2.connectivity = t2.Device = t2.OS = t2.System = t2.Context = t2.SpeechServiceConfig = void 0;
    t2.SpeechServiceConfig = class {
      constructor(e3) {
        this.context = e3;
      }
      serialize() {
        return JSON.stringify(this, (e3, t3) => {
          if (t3 && "object" == typeof t3 && !Array.isArray(t3)) {
            const e4 = {};
            for (const r3 in t3) Object.hasOwnProperty.call(t3, r3) && (e4[r3 && r3.charAt(0).toLowerCase() + r3.substring(1)] = t3[r3]);
            return e4;
          }
          return t3;
        });
      }
      get Context() {
        return this.context;
      }
      get Recognition() {
        return this.recognition;
      }
      set Recognition(e3) {
        this.recognition = e3.toLowerCase();
      }
    };
    t2.Context = class {
      constructor(e3) {
        this.system = new r2(), this.os = e3;
      }
    };
    class r2 {
      constructor() {
        this.name = "SpeechSDK", this.version = "1.47.0", this.build = "JavaScript", this.lang = "JavaScript";
      }
    }
    t2.System = r2;
    t2.OS = class {
      constructor(e3, t3, r3) {
        this.platform = e3, this.name = t3, this.version = r3;
      }
    };
    t2.Device = class {
      constructor(e3, t3, r3) {
        this.manufacturer = e3, this.model = t3, this.version = r3;
      }
    }, function(e3) {
      e3.Bluetooth = "Bluetooth", e3.Wired = "Wired", e3.WiFi = "WiFi", e3.Cellular = "Cellular", e3.InBuilt = "InBuilt", e3.Unknown = "Unknown";
    }(t2.connectivity || (t2.connectivity = {})), function(e3) {
      e3.Phone = "Phone", e3.Speaker = "Speaker", e3.Car = "Car", e3.Headset = "Headset", e3.Thermostat = "Thermostat", e3.Microphones = "Microphones", e3.Deskphone = "Deskphone", e3.RemoteControl = "RemoteControl", e3.Unknown = "Unknown", e3.File = "File", e3.Stream = "Stream";
    }(t2.type || (t2.type = {}));
  }], t = {};
  function r(i2) {
    var n = t[i2];
    if (void 0 !== n) return n.exports;
    var s = t[i2] = { exports: {} };
    return e[i2].call(s.exports, s, s.exports, r), s.exports;
  }
  r.n = (e2) => {
    var t2 = e2 && e2.__esModule ? () => e2.default : () => e2;
    return r.d(t2, { a: t2 }), t2;
  }, r.d = (e2, t2) => {
    for (var i2 in t2) r.o(t2, i2) && !r.o(e2, i2) && Object.defineProperty(e2, i2, { enumerable: true, get: t2[i2] });
  }, r.o = (e2, t2) => Object.prototype.hasOwnProperty.call(e2, t2), r.r = (e2) => {
    "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(e2, Symbol.toStringTag, { value: "Module" }), Object.defineProperty(e2, "__esModule", { value: true });
  };
  var i = {};
  (() => {
    "use strict";
    r.r(i);
    var e2 = r(1);
    window.SpeechSDK = e2;
  })();
})();
