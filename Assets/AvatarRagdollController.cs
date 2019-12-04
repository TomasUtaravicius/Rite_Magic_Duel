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
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TurnOffRagdoll()
    {
       
        for (int i = 0; i < collisionParts.Count; i++)
            {
                collisionParts[i].GetComponent<Rigidbody>().detectCollisions = true;
                collisionParts[i].GetComponent<Rigidbody>().isKinematic = true;
                collisionParts[i].GetComponent<Rigidbody>().useGravity = false;
            }
        animator.enabled = true;
        vrIK.enabled = true;
        GetComponent<VRIKAnimatedLocomotion>().enabled = true;


    }
    public void TurnOnRagdoll()
    {
      
        for (int i = 0; i < collisionParts.Count; i++)
        {

            collisionParts[i].GetComponent<Rigidbody>().detectCollisions = true;
            collisionParts[i].GetComponent<Rigidbody>().isKinematic = false;
            collisionParts[i].GetComponent<Rigidbody>().useGravity = true;
        }
        animator.enabled = false;
        vrIK.enabled = false;
        GetComponent<VRIKAnimatedLocomotion>().enabled = false;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
