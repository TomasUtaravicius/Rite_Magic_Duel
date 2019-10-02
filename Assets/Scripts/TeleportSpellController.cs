using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSpellController : MonoBehaviour {

    private float animation=0;
    public Vector3 startingPosition;
    public Vector3 endingPosition;
    [SerializeField]
    private bool shouldMoveInParabola;
    private bool shouldMoveTowardsThePlayer;
    public TeleportVive teleporter;
    private Vector3 playerPosition;
    //public SteamVR_TrackedObject device;
    private float timer;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(shouldMoveInParabola)
        {
            
            //Debug.Log(animation);
            animation += 0.045f;
            if (animation >= 5f)
            {
                Debug.Log("Calling move towards the player");
                shouldMoveInParabola = false;
                MoveTowardsThePlayer();
                
            }
           
            else
            {
                transform.position = MathParabola.Parabola(startingPosition, endingPosition, 5f, animation / 5f);
            }
 
        }
        if(shouldMoveTowardsThePlayer)
        {
            
           animation = animation +0.07f;
            // Debug.Log(startingPosition);
            //Debug.Log(animation);
            transform.position += transform.forward * Time.deltaTime * animation;
        }
       
	}
    public void Teleport(Vector3 sPosition, Vector3 ePosition)
    {
        shouldMoveInParabola = true;
        animation = 0;
        
        startingPosition = sPosition;
        playerPosition = sPosition;
        endingPosition = ePosition;
        Invoke("TurnOnTrail", 0.2f);
    }
    void TurnOnTrail()
    {
        GetComponent<TrailRenderer>().enabled = true;
    }
    public void MoveTowardsThePlayer()
    {
        animation = 0.2f;
        timer = 0f;
        this.gameObject.transform.LookAt(playerPosition);
        shouldMoveTowardsThePlayer = true;
        Destroy(this.gameObject, 5f);
        
    }
    /*void OnTriggerStay(Collider other)
    {
        //Debug.LogError(other.gameObject.name);
       
        if (other.gameObject.GetComponent<SteamVR_TrackedObject>()!=null && other.gameObject.GetComponent<SteamVR_TrackedObject>().Equals(device))
        {
            var device = SteamVR_Controller.Input((int)other.gameObject.GetComponent<SteamVR_TrackedObject>().index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.LogError("Success");
                teleporter.TeleportTimeMarker = Time.time;
                teleporter.CurrentTeleportState=TeleportState.Teleporting;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.LogError(other.gameObject.name);

        if (other.gameObject.GetComponent<SteamVR_TrackedObject>() != null && other.gameObject.GetComponent<SteamVR_TrackedObject>().Equals(device))
        {
            var device = SteamVR_Controller.Input((int)other.gameObject.GetComponent<SteamVR_TrackedObject>().index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.LogError("Success");
                teleporter.TeleportTimeMarker = Time.time;
                teleporter.CurrentTeleportState = TeleportState.Teleporting;
            }
        }
    }*/
}
