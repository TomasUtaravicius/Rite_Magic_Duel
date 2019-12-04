using System;
using Photon.Pun;
using UnityEngine;


public class ResourceManager : MonoBehaviourPun, IPunObservable
{

    [SerializeField]
    public float health;

    [SerializeField]
    public float mana;

    [SerializeField]
    private PlayerResourceUIController playerResourceUIController;

    [SerializeField]
    private NetworkedPlayer nPlayer;

    [SerializeField]
    private float interval;

    private float time;

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time >= interval)
        {
            time -= interval;

            if (health < 100f)
                health += 0.01f;
            if (mana < 100f)
                mana += 0.25f;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        playerResourceUIController.UpdateResourceBars(health, mana);
    }

    public void Start()
    {
        health = 100f;

        if (photonView.IsMine)
        {
            photonView.RPC("AddPlayer", Photon.Pun.RpcTarget.AllViaServer, PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }

    [PunRPC]
    private void AddPlayer(int id)
    {
        nPlayer.photonID = id;
        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == id)
            {
                nPlayer.name = player.NickName;
            }
        }

        RiteGameManager.Instance.AddPlayerToTheList(nPlayer);
    }

    [PunRPC]
    public void TakeDamage(float amount)
    {

            Debug.LogError("Take damage");
            health -= amount;
            UpdateUI();
            if (health <= 0f)
            {
                Die();
            }
 
    }

    [PunRPC]
    public void ReduceMana(float amount)
    {
        
        if (photonView.IsMine)
        {
            UpdateUI();
            mana -= amount;
        }
    }

    [PunRPC]
    private void Die()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("CallPlayerDeath", Photon.Pun.RpcTarget.AllViaServer, nPlayer.photonID);
        }
    }

    [PunRPC]
    public void CallPlayerDeath(int id)
    {
        RiteGameManager.Instance.PlayerDeath(id);
        //RiteGameManager.Instance.photonView.RPC("PlayerDeath", RpcTarget.AllViaServer, id);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}