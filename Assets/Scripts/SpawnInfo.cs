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
   
   
   
}
