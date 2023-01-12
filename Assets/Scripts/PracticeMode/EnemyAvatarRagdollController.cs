using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvatarRagdollController : MonoBehaviour
{
    public List<GameObject> collisionParts;

 
    public void TurnOffRagdoll()
    {
        for (int i = 0; i < collisionParts.Count; i++)
        {
            collisionParts[i].GetComponent<Rigidbody>().detectCollisions = true;
            collisionParts[i].GetComponent<Rigidbody>().isKinematic = true;
            collisionParts[i].GetComponent<Rigidbody>().useGravity = false;
        }
      
    }
    public void TurnOnRagdoll()
    {
        for (int i = 0; i < collisionParts.Count; i++)
        {

            collisionParts[i].GetComponent<Rigidbody>().detectCollisions = true;
            collisionParts[i].GetComponent<Rigidbody>().isKinematic = false;
            collisionParts[i].GetComponent<Rigidbody>().useGravity = true;
        }
    }

}
