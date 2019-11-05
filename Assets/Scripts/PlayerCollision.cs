using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PhotonView photonView;
    public ResourceManager resourceManager;
    int count = 0;
    bool allowDamage = true;
    private PhotonView pViewToKill;
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "OffensiveSpell")
        {

            Debug.Log("Collided with offensive spell");
            if (photonView.IsMine && !other.gameObject.GetComponent<Information>().hasHit)
            {

                other.gameObject.GetComponent<Information>().hasHit = true;
                    Transform explosionTransform = gameObject.transform;
                    GameObject explosionPrefabObject = PhotonNetwork.Instantiate("Expelliarmus_Explosion_Hit", gameObject.transform.position, gameObject.transform.rotation, 0);
                    AudioSource.PlayClipAtPoint(other.gameObject.GetComponent<Information>().audioForExplosion.clip, gameObject.transform.position);
                    resourceManager.photonView.RPC("TakeDamage", RpcTarget.AllViaServer, other.gameObject.GetComponent<Information>().damage);
               



            }
            if (other.GetComponent<PhotonView>().IsMine)
            {
                //other.gameObject.GetComponent<Information>().hasHit = true;
                Debug.Log("Killing the spell");
                pViewToKill = other.GetComponent<PhotonView>();
                Invoke("KillTheSpell", 0.1f);

            }
           

        }

    }
    public void KillTheSpell()
    {
        if (pViewToKill != null)
            PhotonNetwork.Destroy(pViewToKill.gameObject);
        allowDamage = true;
    }
}
