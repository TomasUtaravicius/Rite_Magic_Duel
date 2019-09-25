using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class TrailController : MonoBehaviour {
    public SteamVR_Action_Boolean trigger;
    public SteamVR_Input_Sources handType;
    private TrailRenderer tRenderer;
    private GameObject trailToDestroy;
    public GameObject trail;
    private void Start()
    {
        
    }
    void OnEnable()
    {
        trigger.AddOnStateDownListener(TurnOnTrail,handType);
        trigger.AddOnStateUpListener(TurnOffTrail, handType);
      
    }
    


    void OnDisable()
    {
        trigger.RemoveOnStateDownListener(TurnOnTrail, handType);
        trigger.RemoveOnStateUpListener(TurnOffTrail, handType);
    }

    
    void TurnOnTrail(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        trailToDestroy=Instantiate(trail,gameObject.transform);
    }
    void TurnOffTrail(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        trailToDestroy.transform.parent = null;
        
    }
}
