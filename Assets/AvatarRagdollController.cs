using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarRagdollController : MonoBehaviour
{
    public List<GameObject> collisionParts;
    private List<CharacterJoint> savedCJoints = new List<CharacterJoint>();
    private List<CharacterJoint> cJoints = new List<CharacterJoint>();
    public ResourceManager resourceManager;
    public VRIK vrIK;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TakeDamage()
    {

    }
    public void TurnOffRagdoll()
    {
        Debug.LogError("TurnOffRagdoll called");
        for (int i = 0; i < collisionParts.Count; i++)
        {
            //savedCJoints.Add(collisionParts[i].gameObject.GetComponent<CharacterJoint>());
            //Destroy(collisionParts[i].gameObject.GetComponent<CharacterJoint>());
            // Destroy(collisionParts[i].GetComponent<Rigidbody>());

            collisionParts[i].GetComponent<Rigidbody>().detectCollisions = true;
            collisionParts[i].GetComponent<Rigidbody>().isKinematic = true;
            collisionParts[i].GetComponent<Rigidbody>().useGravity = false;
            
        }



        vrIK.enabled = true;
        
    }
    public void TurnOnRagdoll()
    {
        for (int i = 0; i < collisionParts.Count; i++)
        {
            //collisionParts[i].AddComponent<Rigidbody>();
            //collisionParts[i].gameObject.AddComponent<CharacterJoint>();
            //cJoints.Add(collisionParts[i].gameObject.AddComponent<CharacterJoint>());
            //cJoints[i] = savedCJoints[i];
            collisionParts[i].GetComponent<Rigidbody>().detectCollisions = true;
            collisionParts[i].GetComponent<Rigidbody>().isKinematic = false;
            collisionParts[i].GetComponent<Rigidbody>().useGravity = true;
        }
            
            

            // rb.detectCollisions = false;
            // rb.isKinematic = false;
            // rb.useGravity = false;
            vrIK.enabled = false;
        
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
            TurnOnRagdoll();
        }
        if (Input.GetKeyDown("k"))
        {
            TurnOffRagdoll();
        }
    }
}
