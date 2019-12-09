using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    public VRInputModule vRInputModule;
    public GameObject pointer;
    public GestureController gestureController;
    private bool menuValue;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ResetMenu()
    {
        menuValue = false;
        DisableUI();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(vRInputModule && vRInputModule.rightController.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            menuValue = !menuValue;
            if (menuValue)
            {
                EnableUI();
            }
            else
            {
                DisableUI();
            }
        }
        
       
    }
    private void DisableUI()
    {
        menuUI.SetActive(false);

        pointer.SetActive(false);
        gestureController.enabled = true;
    }
    private void EnableUI()
    {
        menuUI.SetActive(true);
        
        pointer.SetActive(true);
        gestureController.enabled = false;
    }



}
