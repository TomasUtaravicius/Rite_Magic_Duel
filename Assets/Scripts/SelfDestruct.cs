using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SelfDestruct : MonoBehaviourPun {
    
	// Use this for initialization
	void Start () {
        Invoke("SelfDestroy", 2f);

	}
    void SelfDestroy()
    {
        if(photonView.IsMine)
        {
            Debug.Log("Destroying myself");
            PhotonNetwork.Destroy(this.gameObject);
        }
       
    }
	
}
