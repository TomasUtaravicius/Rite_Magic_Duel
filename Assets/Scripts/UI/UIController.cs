using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject menuUI;
    //public SteamVR_Action_Boolean menuToggle;
    //public SteamVR_Input_Sources handType;
    public GameObject pointer;
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
        
        /*if(menuToggle.GetStateDown(handType))
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
        /*if (menuToggle.GetState(handType)==true)
        {
            
            
        }
        if (menuToggle.GetState(handType)==false)
        {
            menuValue = false;
            
        }*/
       
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
