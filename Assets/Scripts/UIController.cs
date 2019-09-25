using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private GestureController gestureController;
    public SteamVR_Action_Boolean menuToggle;
    public SteamVR_Input_Sources handType;
    public GameObject pointer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (menuToggle.GetState(handType)==true)
        {
            EnableUI();
        }
        if (menuToggle.GetState(handType)==false)
        {
            DisableUI();
        }
    }
    private void DisableUI()
    {
        menuUI.SetActive(false);
        gestureController.enabled = true;
        pointer.SetActive(false);
    }
    private void EnableUI()
    {
        menuUI.SetActive(true);
        gestureController.enabled = false;
        pointer.SetActive(true);
    }



}
