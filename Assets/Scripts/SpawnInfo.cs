using Photon.Pun;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : MonoBehaviour {

    public PhotonView photonView;
    public GameObject playerReference;
    public VRIK avatarScript;
    HealthManager hManager;
    private PhotonView pViewToKill;
    public void AwakeAvatar()
    {
        
        hManager = playerReference.GetComponent<AvatarStateController>().hManager;

    }
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        // e.g. store this gameobject as this player's character in PhotonPlayer.TagObject
        //info.sender.TagObject = this.gameObject;
        
        playerReference = info.photonView.gameObject;
    }
    private void Update()
    {
        /*if(avatarScript.solver.spine.headTarget==null)
        {
            this.gameObject.SetActive(false);
        }*/
    }
   
    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "OffensiveSpell")
        {
            
            Debug.Log("Collided with offensive spell");
            if (photonView.IsMine && !other.gameObject.GetComponent<Information>().hasHit)
            {
                Debug.Log("Instantiating particles");
                Transform explosionTransform = gameObject.transform;
                GameObject explosionPrefabObject = PhotonNetwork.Instantiate("Expelliarmus_Explosion_Hit", gameObject.transform.position, gameObject.transform.rotation, 0);
                AudioSource.PlayClipAtPoint(other.gameObject.GetComponent<Information>().audioForExplosion.clip, gameObject.transform.position);
                hManager.photonView.RPC("TakeDamage", RpcTarget.AllViaServer, other.gameObject.GetComponent<Information>().damage);
                //other.gameObject.GetComponent<Information>().hasHit = true;



            }
            if (other.GetComponent<PhotonView>().IsMine)
            {
                //other.gameObject.GetComponent<Information>().hasHit = true;
                Debug.Log("Killing the spell");
                pViewToKill = other.GetComponent<PhotonView>();
                Invoke("KillTheSpell", 0.2f);
                
            }

        }
        
    }
    public void KillTheSpell()
    {
        if(pViewToKill!=null)
        PhotonNetwork.Destroy(pViewToKill.gameObject);
    }
   
}
