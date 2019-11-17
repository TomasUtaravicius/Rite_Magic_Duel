using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PracticeModeUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    public SteamVR_Action_Boolean menuToggle;
    public SteamVR_Input_Sources handType;
    public GameObject pointer;
    private bool menuValue;

    public void ResetMenu()
    {
        menuValue = false;
        DisableUI();
    }
    // Update is called once per frame
    void Update()
    {

        if (menuToggle.GetStateDown(handType))
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
    }
    private void EnableUI()
    {
        menuUI.SetActive(true);

        pointer.SetActive(true);
    }
}
