using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour, IDamagable
{
    public PhotonView photonView;
    public ResourceManager resourceManager;
    int count = 0;
    bool allowDamage = true;
    private PhotonView pViewToKill;
    

    public void GetHit(float damage)
    {
        if(photonView.IsMine)
        {
            Debug.LogWarning("PHOTON VIEW IS MINE AND IM ABOUT TO CALL TAKE DAMAGE");
            resourceManager.photonView.RPC("TakeDamage", RpcTarget.AllViaServer, damage);
        }
    }
}
