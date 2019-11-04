using Photon.Pun;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInfo : MonoBehaviour {

    public PhotonView photonView;
    public GameObject playerReference;
    public VRIK avatarScript;
    ResourceManager hManager;
    private PhotonView pViewToKill;
    public void AwakeAvatar()
    {
        
        hManager = playerReference.GetComponent<AvatarStateController>().resourceManager;

    }
    void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        
        playerReference = info.photonView.gameObject;
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
