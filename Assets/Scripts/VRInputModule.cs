using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Camera m_Camera;
    [SerializeField]
    SteamVR_TrackedObject rightControllerTracked;
    [SerializeField]
    SteamVR_TrackedObject leftControllerTracked;
    // public SteamVR_Controller.Device rightController;
    //public SteamVR_Controller.Device leftController;
    public SteamVR_Controller.Device rightController { get { return SteamVR_Controller.Input((int)rightControllerTracked.index); } }
    public SteamVR_Controller.Device leftController{ get { return SteamVR_Controller.Input((int)leftControllerTracked.index); } }
    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;
    public override void Process()
    {
        // Reset data, set camera
        //m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth / 2, m_Camera.pixelHeight / 2);
        // Raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;
        // Clear
        m_RaycastResultCache.Clear();
        // Hover 
        HandlePointerExitAndEnter(m_Data,m_CurrentObject);
        // Press
        if(rightController!=null)
        {
            if (rightController.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Press");
                ProcessPress(m_Data);

            }
            // Release
            if (rightController.GetPressUp(EVRButtonId.k_EButton_SteamVR_Trigger))
            {
                ProcessRelease(m_Data);
            }
        }
        
    }
    private void Update()
    {
        if(rightController!=null)
        {
            if (rightController.GetHairTriggerUp())
            {
                Debug.LogError("Right controller trigger press up");
            }
        }
       
      
    }
    protected override void Awake()
    {
        base.Awake();
        m_Data = new PointerEventData(eventSystem);
    }
    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        // Set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        // Check for object hit, get the down handler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);
        // if no down handler, try to get click handler
        if(newPointerPress == null)
        {
            
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);
           
        }
        // Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;

    }
    private void ProcessRelease(PointerEventData data)
    {
        // Execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        // Check for click handler

        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerUpHandler>(m_CurrentObject);
        // Check if actual
        if(data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }
        // Clear selected gameobject
        eventSystem.SetSelectedGameObject(null);
        // Reset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }


}
