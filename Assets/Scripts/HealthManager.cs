using Com.UTYStudios.SpellArena;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class HealthManager : MonoBehaviourPun,IPunObservable {
    [SerializeField]
    public float health;
    private EnemyController eController;
    private AvatarStateController avatarStateController;
    public delegate void PlayerDeathEvent();
    public event PlayerDeathEvent OnPlayerDeath;
    public GameObject playerParentGO;
    [SerializeField]
    private NetworkedPlayer nPlayer;
    public void Start()
    {
        avatarStateController = this.gameObject.GetComponent<AvatarStateController>();
        health = 100f;
        if(gameObject.tag=="Enemy")
        {
            eController = GetComponent<EnemyController>();
        }
        if(photonView.IsMine)
        {
            photonView.RPC("AddPlayerRPC", Photon.Pun.RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber);
        }
        
        
    }
    [PunRPC]
    void AddPlayerRPC(int id)
    {
        Debug.Log("Add player to player list networked player test");
        nPlayer.photonID = id;
        foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if(player.ActorNumber==id)
            {
                nPlayer.name =player.NickName;
            }
        }
        
        RiteGameManager.Instance.AddPlayerToTheList(nPlayer);
    }
    [PunRPC]
    public void TakeDamage(float amount)
    {
        health -= amount;
        if(photonView.IsMine)
        {
            Debug.LogError("TakeDamage");
            if (health <= 0f)
            {

                Die();
            }
        }
        
    }
    
    [PunRPC]
	void Die()
    {
        if(photonView.IsMine)
        {
            if (gameObject.tag == "Enemy")
            {
                eController.Die();
            }
            else
            {

                avatarStateController.photonView.RPC("ChangeAvatarToDead", Photon.Pun.RpcTarget.AllBuffered);

                
                photonView.RPC("CallPlayerDeath", Photon.Pun.RpcTarget.AllBuffered, nPlayer.photonID);
                
                
            }
        }
        
    }
    [PunRPC]
    public void CallPlayerDeath(int id)
    {
        Debug.LogError("CallPlayerDeath");

        RiteGameManager.Instance.PlayerDeath(id);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*if(stream.IsWriting)
        {
            Debug.Log("Stream is writing");
            stream.SendNext(health);
        }
        else
        {
            Debug.Log("Stream is receiving");
            health = (float)stream.ReceiveNext();
        }*/
    }
}
