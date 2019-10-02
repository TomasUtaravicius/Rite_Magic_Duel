using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class PointerRotate : MonoBehaviour {
    public GameObject goToRotate;
    SteamVR_TrackedObject trackedobj;
    
    public Transform cameraRotation;
    // Use this for initialization
    void Start () {

        trackedobj = GetComponent<SteamVR_TrackedObject>();
       
    }
	
	// Update is called once per frame
	void Update () {

        float tiltAroundX = SteamVR_Actions.default_TouchpadValue.GetAxis(SteamVR_Input_Sources.LeftHand).x;

        float tiltAroundY = SteamVR_Actions.default_TouchpadValue.GetAxis(SteamVR_Input_Sources.LeftHand).y; 

        if (goToRotate != null)
        {
            
            if (SteamVR_Actions.default_TouchpadTouch.GetState(SteamVR_Input_Sources.LeftHand))
            {
                goToRotate.transform.eulerAngles = new Vector3(goToRotate.transform.eulerAngles.x, (Mathf.Atan2(tiltAroundX, tiltAroundY) * Mathf.Rad2Deg)+cameraRotation.eulerAngles.y, goToRotate.transform.eulerAngles.z);
                
                //goToRotate.transform.localRotation = Quaternion.Euler(0, tiltAroundX*60, 0);
            }

            if (SteamVR_Actions.default_TouchpadTouch.GetStateUp(SteamVR_Input_Sources.LeftHand))
            {
                goToRotate.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
