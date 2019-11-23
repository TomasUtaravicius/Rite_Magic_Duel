using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour {
    
    private TrailRenderer tRenderer;
    private GameObject trailToDestroy;
    public GameObject trail;
    private void Start()
    {
        
    }
    void OnEnable()
    {
        //trigger.AddOnStateDownListener(TurnOnTrail,handType);
       // trigger.AddOnStateUpListener(TurnOffTrail, handType);
      
    }
    


    void OnDisable()
    {
        //trigger.RemoveOnStateDownListener(TurnOnTrail, handType);
        //trigger.RemoveOnStateUpListener(TurnOffTrail, handType);
    }

    
    public void TurnOnTrail()
    {
        trailToDestroy=Instantiate(trail,gameObject.transform);
    }
    public void TurnOffTrail()
    {
        //if(!trailToDestroy)
        trailToDestroy.transform.parent = null;
    }
    /*publicvoid TurnOffTrail(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        trailToDestroy.transform.parent = null;
        
    }*/
}
