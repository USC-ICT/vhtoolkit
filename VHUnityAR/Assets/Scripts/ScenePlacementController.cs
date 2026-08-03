using UnityEngine;
using UnityEngine.XR;
using Ride.Examples;
using VHAssets;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Class to handle the initial spawn and setup logic, and update logic, in conjuction with DemoController
/// </summary>
public class ScenePlacementController : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject objectToSpawn; 
    public Transform rightHandController;
    public float surfaceOffset = 0.01f; 

    [Header("Raycast Settings")]
    public float maxRaycastDistance = 10f;
    public LayerMask surfaceLayerMask = ~0;
    public bool showDebugRay = true;

    [Header("Surface Filter")]
    [Tooltip("Max allowed tilt (deg) from world up. 0 = perfectly flat. 15-25 typical.")]
    public float maxSurfaceTiltDegrees = 15f;
    [Tooltip("If true, permite ceilings (down-facing normals) within the tilt band.")]
    public bool allowCeilings = false;
    
    [Header("Visual Feedback")]
    public LineRenderer laserPointer;

    [Header("Input Settings")]
    public XRNode inputSourceRight = XRNode.RightHand;
    public XRNode inputSourceLeft = XRNode.LeftHand;

    [Header("UI / Character")]
    public CharacterSelectionManager characterSelectionManager;
    [Tooltip("Portrait shown on the Kevin selection button while the army variant is active. " +
        "When not assigned, the variant is indicated by tinting the civilian portrait instead.")]
    [SerializeField] private Sprite m_kevinArmyPortrait;
    [Tooltip("Highlighted/pressed portrait for the Kevin selection button while the army variant is active.")]
    [SerializeField] private Sprite m_kevinArmySelectedPortrait;
    public GameObject startScreenPrefab;
    public float startScreenDistance = 1.2f;
    public Vector3 startScreenOffset = new Vector3(0f, 0f, 0f);
    private DemoControllerAR demoController;
    private PlacedExperienceRoot m_placedExperienceRoot;
    private ScrollRect scrollView;
    private InputDevice m_rightHandInputDevice;
    private InputDevice m_leftHandInputDevice;
    private bool m_triggerDownPrev = false;
    private bool m_asrButtonDownPrev = false;
    private GameObject m_placedObject;               
    private RaycastHit m_lastHit;
    private bool m_hasAnyHit = false;
    private bool m_hasValidTarget = false; 
    private bool m_canSpawn = true;
    private bool m_isPlaced = false;
    private bool m_isGripPressed = false;
    private bool m_useArmyKevin;
    private bool? m_characterSelectionInteractable;
    private bool m_startScreenActive = false;
    private bool m_startScreenPlaced = false;
    private const string KevinCivilianCharacterName = "KevinCivilian";
    private const string KevinArmyCharacterName = "KevinArmy";
    private const string ArianaCharacterName = "Ariana";
    private static readonly Color KevinCivilianButtonTint = Color.white;
    private static readonly Color KevinArmyButtonTint = new Color(0.82f, 0.94f, 0.82f, 1f);
    private Sprite m_kevinCivilianPortrait;
    private Sprite m_kevinCivilianSelectedPortrait;
    private GameObject m_startScreenInstance;
    private Button m_startScreenButton;
    private OVRCameraRig m_cameraRig;
    private void Start()
    {
        m_rightHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        m_leftHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceLeft);
        m_cameraRig = Camera.main ? Camera.main.GetComponentInParent<OVRCameraRig>() : FindAnyObjectByType<OVRCameraRig>();
        SetupLaserPointer();
        if (characterSelectionManager) characterSelectionManager.gameObject.SetActive(false);
        InstantiateDemoObjects();
        InitializeDemoController();
        ShowStartScreen();
    }

    private void OnDestroy()
    {
        UnsubscribeStartScreenPlacement();
    }

    private void Update()
    {
        if (!m_rightHandInputDevice.isValid)
            m_rightHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceRight);
        if (!m_leftHandInputDevice.isValid)
            m_leftHandInputDevice = InputDevices.GetDeviceAtXRNode(inputSourceLeft);

        UpdateKevinVariantState();
        UpdateCharacterSelectionInteractivity();

        if (m_startScreenActive)
            return;

        PerformSurfaceRaycast();
        HandlePlacementAndMove();
        UpdateVisualFeedback();

        // Push to talk
        bool isASRControllerButtonPressed = false;
        if (demoController != null &&
            m_leftHandInputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out isASRControllerButtonPressed))
        {
            if (isASRControllerButtonPressed && !m_asrButtonDownPrev)
                demoController.ToggleASR();
        }
        m_asrButtonDownPrev = isASRControllerButtonPressed;

        if (scrollView != null &&
            m_rightHandInputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stick) &&
            Mathf.Abs(stick.y) > 0.1f)
            Scroll(stick.y);
    }

    private void PerformSurfaceRaycast()
    {
        if (!rightHandController) { m_hasAnyHit = false; m_hasValidTarget = false; return; }

        Vector3 origin = rightHandController.position;
        Vector3 dir    = rightHandController.forward;

        m_hasAnyHit = Physics.Raycast(origin, dir, out m_lastHit, maxRaycastDistance, surfaceLayerMask);

        m_hasValidTarget = false;
        if (m_hasAnyHit)
        {
            Vector3 n = m_lastHit.normal.normalized;
            float cosMax = Mathf.Cos(maxSurfaceTiltDegrees * Mathf.Deg2Rad);

            bool upFacingFlat   = Vector3.Dot(n, Vector3.up)   >= cosMax; // floors/tables
            bool downFacingFlat = Vector3.Dot(n, Vector3.down) >= cosMax; // ceilings

            m_hasValidTarget = upFacingFlat || (allowCeilings && downFacingFlat);
        }

        if (showDebugRay)
        {
            float len = m_hasAnyHit ? m_lastHit.distance : maxRaycastDistance;
            Color c = !m_hasAnyHit ? Color.red : (m_hasValidTarget ? Color.green : Color.yellow);
            Debug.DrawRay(origin, dir * len, c);
        }
    }

    private void HandlePlacementAndMove()
    {
        bool triggerPressed = m_rightHandInputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool t) && t;
        m_isGripPressed    = m_rightHandInputDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool g) && g;

        if (triggerPressed && !m_triggerDownPrev && m_hasValidTarget && m_canSpawn)            
            SpawnPrefab(m_lastHit.point, m_lastHit.normal);

        //BOTH trigger+grip held and still valid = Reposition 
        if (m_isPlaced && m_placedObject && m_hasValidTarget && triggerPressed && m_isGripPressed)
            MovePlacedObject(m_lastHit.point, m_lastHit.normal);

        m_triggerDownPrev = triggerPressed;
    }

    private void SpawnPrefab(Vector3 pos, Vector3 normal)
    {
        if (!m_placedObject || demoController == null || m_placedExperienceRoot == null)
        {
            Debug.LogWarning($"[ScenePlacementController] SpawnPrefab aborted. placedObject={m_placedObject != null}, demoController={demoController != null}, placedExperienceRoot={m_placedExperienceRoot != null}");
            return;
        }

        m_placedObject.transform.position = pos + normal * surfaceOffset;
        FaceObjectTowardCamera(m_placedObject.transform.position);
        demoController.BindPlacement(m_placedExperienceRoot);
        m_isPlaced = true;
        m_canSpawn = false;
        InitializeCharactersAR();
    }

    private void InstantiateDemoObjects()
    {
        if (!objectToSpawn || m_placedObject)
            return;

        m_placedObject = Instantiate(objectToSpawn);
        m_placedExperienceRoot = m_placedObject.GetComponent<PlacedExperienceRoot>();
        demoController = m_placedObject.GetComponentInChildren<DemoControllerAR>(true);

        if (m_placedExperienceRoot != null)
            m_placedExperienceRoot.HidePlacementContent();
    }

    private void InitializeDemoController()
    {
        if (demoController == null && m_placedObject != null)
            demoController = m_placedObject.GetComponentInChildren<DemoControllerAR>(true);

        if (demoController)
            demoController.UI_Controller.HidePlacementUI();
        else
            Debug.LogWarning("[InitializeDemoController] DemoControllerAR not found under DemoObjects.");

        if (characterSelectionManager)
        {
            SetCharacterSelectionButtonAction(characterSelectionManager.button_Ellie, "Ellie");
            SetLegacyButtonVisible(characterSelectionManager.button_John, false);
            SetKevinSelectionButtonAction(characterSelectionManager.button_Kevin);
            SetCharacterSelectionButtonAction(characterSelectionManager.button_Ariana, ArianaCharacterName);
            SetCharacterSelectionButtonAction(characterSelectionManager.button_Aaron, "Aaron");
            UpdateKevinSelectionVisual();
        }
        scrollView = m_placedObject.GetComponentInChildren<ScrollRect>(true);
    }

    private void InitializeCharactersAR()
    {
        if (demoController)
        {
            demoController.SelectCharacter(GetSelectedKevinCharacterName());
            if (characterSelectionManager)
                characterSelectionManager.gameObject.SetActive(true);
        }
    }

    private void ShowStartScreen()
    {
        m_startScreenActive = true;
        m_startScreenPlaced = false;
        m_canSpawn = false;

        if (demoController != null)
            demoController.m_startButtonPressed = false;

        if (!startScreenPrefab)
        {
            HideStartScreen();
            return;
        }

        m_startScreenInstance = Instantiate(startScreenPrefab);
        ConfigureStartScreen(m_startScreenInstance);
    }

    private void ConfigureStartScreen(GameObject root)
    {
        if (!root)
            return;

        CurvedStartScreenLayout curvedLayout = root.GetComponent<CurvedStartScreenLayout>();
        if (curvedLayout)
        {
            curvedLayout.Configure(Camera.main, OnStartButtonPressed);
            m_startScreenButton = curvedLayout.StartButton;
            SubscribeStartScreenPlacement();
            return;
        }

        Canvas startScreenCanvas = root.GetComponentInChildren<Canvas>(true);
        if (!startScreenCanvas)
        {
            HideStartScreen();
            return;
        }

        root.transform.SetParent(null, false);
        SetLayerRecursively(startScreenCanvas.gameObject, 0);
        startScreenCanvas.renderMode = RenderMode.WorldSpace;
        startScreenCanvas.worldCamera = Camera.main;
        startScreenCanvas.overrideSorting = false;
        startScreenCanvas.gameObject.SetActive(true);

        m_startScreenButton = startScreenCanvas.GetComponentInChildren<Button>(true);
        if (m_startScreenButton)
        {
            m_startScreenButton.onClick.RemoveListener(OnStartButtonPressed);
            m_startScreenButton.onClick.AddListener(OnStartButtonPressed);
        }
        SubscribeStartScreenPlacement();
    }

    private void SubscribeStartScreenPlacement()
    {
        UnsubscribeStartScreenPlacement();
        if (!m_cameraRig)
            m_cameraRig = Camera.main ? Camera.main.GetComponentInParent<OVRCameraRig>() : FindAnyObjectByType<OVRCameraRig>();

        if (!m_cameraRig)
        {
            HideStartScreen();
            return;
        }

        m_cameraRig.UpdatedAnchors += OnCameraRigUpdatedAnchors;
    }

    private void UnsubscribeStartScreenPlacement()
    {
        if (m_cameraRig != null)
            m_cameraRig.UpdatedAnchors -= OnCameraRigUpdatedAnchors;
    }

    private void OnCameraRigUpdatedAnchors(OVRCameraRig rig)
    {
        if (!m_startScreenActive || m_startScreenPlaced || !m_startScreenInstance || !rig || !rig.centerEyeAnchor)
            return;

        Vector3 eyePosition = rig.centerEyeAnchor.position;
        if (eyePosition.y <= 0.5f)
            return;

        Vector3 forward = rig.centerEyeAnchor.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude < 1e-4f)
        {
            forward = rig.centerEyeAnchor.rotation * Vector3.forward;
            forward.y = 0f;
        }
        if (forward.sqrMagnitude < 1e-4f)
            return;

        forward.Normalize();

        Vector3 right = Vector3.Cross(Vector3.up, forward);
        if (right.sqrMagnitude < 1e-4f)
            right = Vector3.right;
        else
            right.Normalize();

        Vector3 worldPosition = eyePosition
            + (right * startScreenOffset.x)
            + (Vector3.up * startScreenOffset.y)
            + (forward * (startScreenDistance + startScreenOffset.z));
        Quaternion worldRotation = Quaternion.LookRotation(forward, Vector3.up);

        m_startScreenInstance.transform.SetParent(null, false);
        m_startScreenInstance.transform.SetPositionAndRotation(worldPosition, worldRotation);

        m_startScreenPlaced = true;
        UnsubscribeStartScreenPlacement();
    }

    private void OnStartButtonPressed()
    {
        HideStartScreen();
    }

    private void HideStartScreen()
    {
        UnsubscribeStartScreenPlacement();
        m_startScreenActive = false;
        m_startScreenPlaced = false;
        m_canSpawn = true;

        if (laserPointer)
            laserPointer.enabled = !m_isPlaced;

        if (demoController != null)
            demoController.m_startButtonPressed = true;

        CurvedStartScreenLayout curvedLayout = m_startScreenInstance ? m_startScreenInstance.GetComponent<CurvedStartScreenLayout>() : null;
        if (curvedLayout)
            curvedLayout.ClearButtonListener(OnStartButtonPressed);
        else if (m_startScreenButton)
            m_startScreenButton.onClick.RemoveListener(OnStartButtonPressed);

        if (m_startScreenInstance)
            Destroy(m_startScreenInstance);
        m_startScreenButton = null;
        m_startScreenInstance = null;
    }

    private static void SetLayerRecursively(GameObject target, int layer)
    {
        target.layer = layer;

        for (int i = 0; i < target.transform.childCount; i++)
            SetLayerRecursively(target.transform.GetChild(i).gameObject, layer);
    }

    private void MovePlacedObject(Vector3 pos, Vector3 normal)
    {
        if (!m_placedObject) return;
        m_placedObject.transform.position = pos + normal * surfaceOffset;
        FaceObjectTowardCamera(m_placedObject.transform.position);
    }

    private void FaceObjectTowardCamera(Vector3 atPosition)
    {
        if (!m_placedObject) return;
        var cam = Camera.main;
        Vector3 camPos = cam ? cam.transform.position : atPosition + Vector3.forward;

        Vector3 flatDir = camPos - atPosition;
        flatDir.y = 0f;
        if (flatDir.sqrMagnitude < 1e-6f) return;

        m_placedObject.transform.rotation = Quaternion.LookRotation(flatDir.normalized, Vector3.up);
    }

    private void Scroll(float thumbstickY)
    {
        if (!scrollView) return;
        const float scrollSpeed = 0.03f;
        scrollView.verticalNormalizedPosition = Mathf.Clamp01(
            scrollView.verticalNormalizedPosition + thumbstickY * scrollSpeed);
    }

    private void SetCharacterSelectionButtonAction(Button button, string characterName)
    {
        if (!button) return;
        button.onClick.RemoveAllListeners();
        void action() => CharacterSelectionAction(characterName);
        button.onClick.AddListener((UnityAction)action);
    }

    private void SetKevinSelectionButtonAction(Button button)
    {
        if (!button)
            return;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => CharacterSelectionAction(GetSelectedKevinCharacterName()));
    }

    private void SetLegacyButtonVisible(Button button, bool visible)
    {
        if (button)
            button.gameObject.SetActive(visible);
    }

    private void UpdateKevinVariantState()
    {
        bool useArmyKevin = false;
        m_leftHandInputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out useArmyKevin);

        if (m_useArmyKevin == useArmyKevin)
            return;

        m_useArmyKevin = useArmyKevin;
        UpdateKevinSelectionVisual();
    }

    private void UpdateKevinSelectionVisual()
    {
        if (!characterSelectionManager || !characterSelectionManager.button_Kevin)
            return;

        if (!characterSelectionManager.button_Kevin.interactable)
            return;

        Image kevinImage = characterSelectionManager.button_Kevin.targetGraphic as Image;
        if (!kevinImage)
            kevinImage = characterSelectionManager.button_Kevin.GetComponent<Image>();

        if (!kevinImage)
            return;

        if (m_kevinArmyPortrait)
        {
            // The civilian portraits are whatever the button starts with; remember them the
            // first time so the swap can be undone.
            if (!m_kevinCivilianPortrait)
                m_kevinCivilianPortrait = kevinImage.sprite;

            Sprite portrait = m_useArmyKevin ? m_kevinArmyPortrait : m_kevinCivilianPortrait;
            kevinImage.sprite = portrait;
            kevinImage.color = Color.white;

            // The button uses its "Selected" artwork for the pressed and highlighted states,
            // and the regular portrait for the selected state; keep all three in step.
            if (m_kevinArmySelectedPortrait)
            {
                Button kevinButton = characterSelectionManager.button_Kevin;
                if (!m_kevinCivilianSelectedPortrait)
                    m_kevinCivilianSelectedPortrait = kevinButton.spriteState.pressedSprite;

                Sprite selectedPortrait = m_useArmyKevin
                    ? m_kevinArmySelectedPortrait
                    : m_kevinCivilianSelectedPortrait;

                SpriteState spriteState = kevinButton.spriteState;
                spriteState.pressedSprite = selectedPortrait;
                spriteState.highlightedSprite = selectedPortrait;
                spriteState.selectedSprite = portrait;
                kevinButton.spriteState = spriteState;
            }
        }
        else
        {
            kevinImage.color = m_useArmyKevin ? KevinArmyButtonTint : KevinCivilianButtonTint;
        }
    }

    private string GetSelectedKevinCharacterName() => m_useArmyKevin ? KevinArmyCharacterName : KevinCivilianCharacterName;

    private void UpdateCharacterSelectionInteractivity()
    {
        if (!characterSelectionManager)
            return;

        bool interactable = demoController == null || demoController.CharacterSelectionEnabled;
        if (m_characterSelectionInteractable == interactable)
            return;

        m_characterSelectionInteractable = interactable;
        characterSelectionManager.SetInteractable(interactable);
        if (interactable)
            UpdateKevinSelectionVisual();
    }

    private void CharacterSelectionAction(string characterName)
    {
        if (demoController == null || !demoController.CharacterSelectionEnabled)
            return;

        demoController.SelectCharacter(characterName);
    }
    void SetupLaserPointer()
    {
        if (!laserPointer) return;
        laserPointer.positionCount = 2;
        laserPointer.startWidth = 0.005f;
        laserPointer.endWidth   = 0.002f;
        laserPointer.useWorldSpace = true;
        laserPointer.sortingOrder = 1000;
        if (laserPointer.material == null)
        {
            var mat = new Material(Shader.Find("Sprites/Default")) { color = Color.red };
            laserPointer.material = mat;
        }
        laserPointer.enabled = true;
    }

    void UpdateVisualFeedback()
    {
        if (!laserPointer || !rightHandController) return;

        // FYI When the laser is shown:
        // - Before first placement (aiming for initial spawn)
        // - After placement ONLY while grip is held (user intends to move)
        bool shouldShowLaser = !m_isPlaced || m_isGripPressed;
        laserPointer.enabled = shouldShowLaser;

        if (!shouldShowLaser) return;

        Vector3 start = rightHandController.position;
        Vector3 end = m_hasAnyHit ? m_lastHit.point : start + rightHandController.forward * maxRaycastDistance;

        laserPointer.SetPosition(0, start);
        laserPointer.SetPosition(1, end);

        // Red = no hit, Yellow = hit but not flat/up-facing, Green = valid
        Color c = !m_hasAnyHit ? Color.red : (m_hasValidTarget ? Color.green : Color.yellow);
        if (laserPointer.material) laserPointer.material.color = c;
        else
        {
            laserPointer.startColor = c;
            laserPointer.endColor = c;
        }
    }

    public void ResetSpawner()
    {
        if (demoController != null)
            demoController.UnbindPlacement();

        m_isPlaced = false;
        m_canSpawn = true;
        if (laserPointer) laserPointer.enabled = true;
        if (characterSelectionManager) characterSelectionManager.gameObject.SetActive(false);
    }
}
