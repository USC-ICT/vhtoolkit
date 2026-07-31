# Virtual Human Toolkit

**Any Character, Any Technology, Anywhere.**

Create conversational 3D characters for web, mobile, AR/VR, and desktop.

The [Virtual Human Toolkit](https://vhtoolkit.ict.usc.edu) (VHToolkit) is a research and development platform from the [USC Institute for Creative
Technologies](https://ict.usc.edu) for creating embodied conversational agents / socially intelligent agents: real-time, interactive characters that perceive real humans and respond both verbally and nonverbally. 

Try it out: 
- [Go to the live web demo](https://vhtoolkitwww.ict.usc.edu/demo/) and talk to a virtual human in your browser right now. 
- Download binaries for other platforms from the [Releases](https://github.com/USC-ICT/vhtoolkit/releases) page.
- Watch a detailed video demonstration [here](https://www.youtube.com/watch?v=QU_ItB4p_zc).


## Features


1. **Integrated system.** Clone, open in Unity, add your API keys, and press Play to converse with a character.
2. **Vendor agnostic.** Native support for 20+ vendors and technologies across audio-visual sensing, speech recognition, language models, text-to-speech, and nonverbal behavior generation.
3. **Runtime cloud and local provider selection.** Every capability sits behind a common API; one dropdown moves between cloud vendors, or runs fully local for data-sensitive studies.
4. **Custom characters.** In addition to the included character library, you can author your own characters and personalized avatars with Reallusion [Character Creator](https://www.reallusion.com/character-creator) (requires a separate license).
5. **Knowledge grounding.** Plain text files placed in the project become reference material. Keyword retrieval requires no API key or network access; optional OpenAI embeddings add semantic matching.
6. **Extensibility.** Implement the principled API to add your own technology.
7. **Cross-platform deployment.** Windows, macOS, Linux, WebGL (browser), Android, iOS, and Quest.

![VHToolkit characters](https://vhtoolkitwww.ict.usc.edu/demo/VHToolkit_July2026.png)


## Supported AI Services

| Capability | Cloud | Local / on-prem |
|---|---|---|
| Language models (NLP) | Anthropic Claude, AWS Lex, OpenAI ChatGPT, Google Gemini | Ollama, Rasa, vLLM (any OpenAI-compatible endpoint) |
| Speech recognition (ASR) | Azure Speech, Google Gemini, OpenAI Realtime | Faster-Whisper, platform-native (Windows/Android/iOS) |
| Speech synthesis (TTS) | ElevenLabs (incl. voice cloning), AWS Polly, Azure, Google Gemini | Kokoro, Piper, XTTS v2 |
| Knowledge grounding (RAG) | OpenAI semantic embeddings | Unity-embedded lexical, hybrid |
| Sensing | AWS Rekognition, Azure Face (requires MS approval) | DeepFace, OpenFace |

All cloud services use your own accounts and API keys. Note that not all technologies are supported for all hardware platforms. Mobile development requires 3rd party Unity packages. WebGL development requires custom AWS Lambdas due to CORS requirements. 


## Repository Layout

| Folder | What it is | Start here? |
|---|---|---|
| `VHUnityURP` | Main runtime Unity project (Universal Render Pipeline) - the demo you saw above | **Yes** |
| `VHUnityHDRP` | High-fidelity runtime Unity project (HDRP) with maximum visual quality, primarily for desktop | If you need HDRP |
| `VHUnityURP-Assets` | Character art asset Unity project (URP) - import and prepare new Character Creator characters | Only for character authoring |
| `VHUnityHDRP-Assets` | HDRP version of the character art asset Unity project | Only for character authoring |
| `VHUnityAR` | Dedicated Quest 3 Passthrough Unity project | Only for AR/VR development |
| `Services` | Docker containers for local AI services | Only for running local services |


## Getting Started

Prerequisites:
- [Unity Hub](https://unity.com/download). VHToolkit uses Unity 6. No need to download a specific Unity version, as VHToolkit scripts handle this automatically, see below.
- [git](https://git-scm.com/) on your PATH, to resolve packages.
- API keys for the services you want to use, see below. 

Steps:
1. Clone this repository. It is large; allow time for the art assets.

   ```bash
   git clone https://github.com/USC-ICT/vhtoolkit.git
   ```
2. Run `runUnity.bat` or `runUnity.sh` in the `VHUnityURP` folder. This will automatically download the correct Unity version through the Unity Hub, and then load the project.
3. Open the scene `Assets/Scenes/SampleScene`.
4. Add your API keys via the Unity Editor top menu **Ride > Config...**. At a minimum, set ASR, NLP, and TTS provider keys. The default services are openAIRealtime (ASR), openAIChatGPT (NLP), and elevenLabs (TTS). The two OpenAI services use the same key. 
5. Press Play and talk to your virtual human.

See the [Getting Started](https://github.com/USC-ICT/vhtoolkit/wiki/Getting-Started) Wiki page for more details.

Note that cloud AI services bill per use on your accounts. Typical development sessions don't cost much, but budget alerts on your provider accounts are recommended. For a zero-cloud setup, use the local Docker container AI services in the Services folder.

Also note that large language models carry an inherent risk where they can be talked into things their author did not intend. Each character's prompt includes a shared safety section you can inherit, replace, or disable, but validate a character against your requirements before running participants, and put any human-subjects work through your institution's review process.


## Built on RIDE

The VHToolkit is powered by [RIDE](https://ride.ict.usc.edu) (Rapid Integration & Development Environment), USC ICT's rapid prototyping modeling and simulation middleware platform. RIDE provides the system architecture, provider abstractions, web and local service integrations, and APIs. The  VHToolkit adds the virtual human layer: characters, the conversation loop, and examples for all hardware platforms. You do not need to clone RIDE separately; its packages are referenced from its [GitHub page](https://github.com/USC-ICT/ride).


## Documentation & Support
Detailed documentation can be found at this GitHub's [Wiki section](https://github.com/USC-ICT/vhtoolkit/wiki). Submit questions, bugs, and feature requests [here](https://github.com/USC-ICT/vhtoolkit/issues).


## License

The VHToolkit is licensed under the [USC-RL v3.0 license](https://github.com/USC-ICT/vhtoolkit?tab=License-1-ov-file), a permissive license for academic and personal use. For commercial and government purpose use, please [contact us](https://vhtoolkit.ict.usc.edu/vhtk-download.html).


## Citation

When publishing work that uses the VHToolkit, please cite one of the following papers:

- Hartholt, A., Fast, E., Leeds, A., Mozgai, S. (2026). "Demonstrating the Open Virtual
  Human Toolkit: Any Character, Any Technology, Anywhere." ACM International Conference on
  Intelligent Virtual Agents (IVA). (To be published)
- Hartholt, A., Fast, E., Li, Z., Kim, K., Leeds, A., Mozgai, S. (2022). "Re-architecting
  the Virtual Human Toolkit: Towards an Interoperable Platform for Embodied Conversational
  Agent Research and Development." 22nd ACM International Conference on Intelligent Virtual
  Agents (IVA).
  https://dl.acm.org/doi/10.1145/3514197.3549671  
- Hartholt, A., Traum, D., Marsella, S. C., Shapiro, A., Stratou, G., Leuski, A.,
  Morency, L.-P., Gratch, J. (2013). "All Together Now: Introducing the Virtual Human
  Toolkit." International Workshop on Intelligent Virtual Agents (IVA).
  https://link.springer.com/chapter/10.1007/978-3-642-40415-3_33

```bibtex
@inproceedings{hartholt2026demonstrating,
  title={Demonstrating the Open Virtual Human Toolkit: Any Character, Any Technology, Anywhere},
  author={Hartholt, Arno and Fast, Ed and Leeds, Andrew and Mozgai, Sharon},
  booktitle={Proceedings of the ACM International Conference on Intelligent Virtual Agents},
  year={2026},
  doi={10.1145/3806774.3832327}
}
```

```bibtex
@inproceedings{hartholt2022re,
  title={Re-architecting the virtual human toolkit: towards an interoperable
         platform for embodied conversational agent research and development},
  author={Hartholt, Arno and Fast, Ed and Li, Zongjian and Kim, Kevin and Leeds, Andrew and Mozgai, Sharon},
  booktitle={Proceedings of the 22nd ACM International Conference on Intelligent Virtual Agents},
  pages={1--8},
  year={2022}
}
```

```bibtex
@inproceedings{hartholt2013all,
  title={All together now: Introducing the virtual human toolkit},
  author={Hartholt, Arno and Traum, David and Marsella, Stacy C and Shapiro, Ari and Stratou, Giota 
		and Leuski, Anton and Morency, Louis-Philippe and Gratch, Jonathan},
  booktitle={International Workshop on Intelligent Virtual Agents},
  pages={368--381},
  year={2013},
  organization={Springer}
}
```