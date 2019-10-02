using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

namespace Com.UTYStudios.SpellArena
{
    
    public class GameManager : MonoBehaviourPun
    {
        public List<NetworkedPlayer> playerList;
        public List<NetworkedPlayer> alivePlayerList;
        public delegate void UpdateScoreEvent(int playerID);
        public event UpdateScoreEvent UpdateScore;
        [SerializeField]
        Transform playerSpawnPoints;
        public static GameManager Instance { get; private set; }
        public void Awake()
        {
            Instance = this;
        }
        public void OnEnable()
        {
            
        }
        public void OnDisable()
        {
            
        }
        public void Start()
        {
            
        }
        [PunRPC]
        public void RoundStart()
        {
            alivePlayerList.Clear();
            for(int i =0; i<playerList.Count;i++)
            {
                NetworkedPlayer dummyPlayer = playerList[i];
                alivePlayerList.Add(dummyPlayer);
            }
                 
                //ReturnPlayersToPositions();
      
            
        }
        [PunRPC]
        public void AddPlayerToTheList(NetworkedPlayer nPlayer)
        {

            playerList.Add(nPlayer);
            RoundStart();
            
        }
        [PunRPC]
        public void RemovePlayerFromTheList(NetworkedPlayer nPlayer)
        {
            playerList.Remove(nPlayer);
            alivePlayerList.Remove(nPlayer);
        }
        [PunRPC]
        public void PlayerDeath(int id)
        {
            for (int n = 0; n < alivePlayerList.Count; n++)
            {


                if (alivePlayerList[n].photonID == id)
                {
                    alivePlayerList.Remove(alivePlayerList[n]);
                    break;
                }
            }
        
       
            Debug.Log("Player death " + alivePlayerList);
         
            if(alivePlayerList.Count==1)
            {
                PlayerWinRound(alivePlayerList[0].photonID);
                
            }
        }
        [PunRPC]
        public void RemovePlayerFromAliveList(NetworkedPlayer nPlayer)
        {
            //alivePlayerList.Remove(nPlayer);
            Debug.LogError("RemovePlayerFromAliveList " + alivePlayerList);
        }
       
        [PunRPC]
        public void PlayerWinRound(int playerID)
        {
            

            //GameModel.Instance.IncrementPlayerScore(playerID);
            Invoke("RoundStart", 3f);
            
            //photonView.RPC("RoundStart", PhotonTargets.AllBuffered);



        }
        [PunRPC]
        public void ReturnPlayersToPositions()
        {
            photonView.RPC("ReviveAllPlayers", Photon.Pun.RpcTarget.AllBuffered);
            Debug.LogError("Returning players to their original positions");
                for (int j = 0; j < PhotonNetwork.PlayerList.Length; j++)
                {
                    int positionIndex = (int)PhotonNetwork.PlayerList[j].CustomProperties["position"];
                    int id = (int)PhotonNetwork.PlayerList[j].ActorNumber;
                    for (int n = 0; n < alivePlayerList.Count; n++)
                    {
                    
                   
                        if(playerList[n].photonID==id)
                        {
                            Debug.LogError("Moving player with ID:" + playerList[n].photonID + "to spawn point with index: " + positionIndex);
                            playerList[n].gameObject.transform.parent.transform.position =  playerSpawnPoints.GetChild(positionIndex).position;
                        }
                    }
                    }
                   
            
        }
        [PunRPC]
        public void ReviveAllPlayers()
        {
            
            for(int i = 0; i< playerList.Count;i++)
            {
                Debug.LogError("Calling Revive on all players");
                playerList[i].avatarStateController.SpawnAvatarBody();
            }
        }
       
    }

}

