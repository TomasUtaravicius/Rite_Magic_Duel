using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PracticeModeUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    
    public VRInputModule vRInputModule;
    [SerializeField]
    private GestureController gc;


    // public SteamVR_Action_Boolean menuToggle;
    // public SteamVR_Input_Sources handType;
    public GameObject pointer;
    private bool menuValue;

    public void OnEnable()
    {
        
        EnableUI();
        DisableUI();
    }
    public void ResetMenu()
    {
        menuValue = false;
        DisableUI();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(vRInputModule.rightController.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            if (menuUI.activeSelf == true)
            {
               
                DisableUI();
                return;
            }
            else
            {
                EnableUI();
            }
        }

    }
    private void DisableUI()
    {
        menuUI.SetActive(false);
        gc.enabled = true;
        pointer.SetActive(false);
    }
    private void EnableUI()
    {
        menuUI.SetActive(true);
        gc.enabled = false;
        pointer.SetActive(true);
    }
}
