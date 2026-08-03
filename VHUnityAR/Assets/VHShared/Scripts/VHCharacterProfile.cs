using Ride;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Where a composed prompt section (safety or domain) comes from:
/// the shared centrally-maintained default, a per-character custom string, or nothing.
/// </summary>
public enum PromptPartSource { SharedDefault, Custom, None }

/// <summary>
/// Contains voice configuration and prompt data for a Virtual Human character used in the RIDE framework.
/// </summary>
public class VHCharacterProfile : MonoBehaviour
{
    /// <summary>The AWS Polly voice name associated with this character. </summary>
    public string PollyVoiceName = "Matthew";

    /// <summary>The ElevenLabs voice name associated with this character.</summary>
    public string ElevenLabVoiceName = "Brian";

    /// <summary>The Piper voice name associated with this character.</summary>
    public string PiperVoiceName = "en_US-lessac-medium";

    /// <summary>The Kokoro voice name associated with this character.</summary>
    public string KokoroVoiceName = "af_heart";

    /// <summary>The XTTS voice name associated with this character.</summary>
    public string XTTSVoiceName = "Ana Florence";

    /// <summary>The Gemini TTS voice name associated with this character.</summary>
    public string GeminiVoiceName = "Puck";

    /// <summary>The OpenAI Realtime voice name associated with this character.</summary>
    public string OpenAIRealtimeVoiceName = "cedar";

    public NonverbalBehaviorGeneratorSystem NVBG;

    // --- LLM prompt (three-part: base safety + domain + character) ----------------
    // The effective system prompt is composed from a shared, centrally-maintained safety
    // baseline (VHPrompts.BaseSafety - not stored per character), a domain section (the
    // character's role/knowledge), and a per-character section (personal info, e.g. name).
    // Safety and Domain default to Shared Default (the centrally-maintained
    // VHPrompts.BaseSafety / DemoDomain); switch a Source to Custom or None per character as needed.

    [Header("LLM prompt")]
    [Tooltip("Per-character personal info (name, who they are, how they speak). Start from the " +
             "VHPrompts.CharacterPlaceholder template and fill in this character's details.")]
    [SerializeField, TextArea(1, 6)] private string m_characterPrompt = VHPrompts.CharacterPlaceholder;

    [Space(4)]
    [Tooltip("Domain source. Shared Default = the centrally-maintained VHPrompts.DemoDomain; " +
             "Custom = the Domain Prompt text below; None = no domain section.")]
    [SerializeField] private PromptPartSource m_domainSource = PromptPartSource.SharedDefault;

    [Tooltip("Used only when Domain Source = Custom: this character's role / knowledge.")]
    [SerializeField, TextArea(3, 12)] private string m_domainPrompt = "";

    [Space(4)]
    [Tooltip("Safety source. Shared Default = the centrally-maintained VHPrompts.BaseSafety; " +
             "Custom = the Safety Prompt text below; None = no safety section (IRB-approved exceptions only).")]
    [SerializeField] private PromptPartSource m_safetySource = PromptPartSource.SharedDefault;

    [Tooltip("Used only when Safety Source = Custom, e.g. an IRB-approved deception study.")]
    [SerializeField, TextArea(3, 12), FormerlySerializedAs("m_safetyOverride")] private string m_safetyPrompt = "";

    // Runtime override (e.g. the Study Wizard's composed persona). Not serialized;
    // wins over the composed prompt for the rest of the session once set.
    private string m_runtimePromptOverride;

    /// <summary>The safety section this character uses (per Safety Source).</summary>
    public string SafetyPrompt => Resolve(m_safetySource, m_safetyPrompt, VHPrompts.BaseSafety);

    /// <summary>The domain section this character uses (per Domain Source).</summary>
    public string DomainPrompt => Resolve(m_domainSource, m_domainPrompt, VHPrompts.DemoDomain);

    private static string Resolve(PromptPartSource source, string custom, string shared)
    {
        switch (source)
        {
            case PromptPartSource.Custom: return custom;
            case PromptPartSource.None:   return string.Empty;
            default:                      return shared; // SharedDefault
        }
    }

    /// <summary>
    /// The effective LLM system prompt. A runtime override (set via the setter) wins;
    /// otherwise the three-part composition of safety, domain, and character sections.
    /// Setting it installs a session-scoped runtime override (used by SetPrompt()).
    /// </summary>
    public string llmPrompt
    {
        get
        {
            if (!string.IsNullOrEmpty(m_runtimePromptOverride)) return m_runtimePromptOverride;
            return VHPrompts.Compose(SafetyPrompt, DomainPrompt, m_characterPrompt);
        }
        set => m_runtimePromptOverride = value;
    }
}
