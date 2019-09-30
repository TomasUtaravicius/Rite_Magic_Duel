using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RiteGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform playerSpawnPoints;

    [SerializeField]
    private GameObject steamVrPlayerPrefab;
    private GameObject spawnPoints;
    public List<NetworkedPlayer> playerList;
    public List<NetworkedPlayer> alivePlayerList;

    public delegate void UpdateScoreEvent(int playerID);

    public event UpdateScoreEvent UpdateScore;

    public Leaderboard leaderboard;
    public static RiteGameManager Instance { get; private set; }

    //public Text InfoText;

    #region UNITY

    public override void OnEnable()
    {
        base.OnEnable();

        //CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }

    public void Start()
    {
        //InfoText.text = "Waiting for other players...";
        Instance = this;
        Hashtable props = new Hashtable
            {
                {RiteGame.PLAYER_LOADED_LEVEL, true}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        PhotonNetwork.ConnectUsingSettings();
        
        
        StartGame();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }

    #endregion UNITY

    #region COROUTINES

    /*private IEnumerator EndOfGame(string winner, int score)
    {
        float timer = 5.0f;

        while (timer > 0.0f)
        {
            //InfoText.text = string.Format("Player {0} won with {1} points.\n\n\nReturning to login screen in {2} seconds.", winner, score, timer.ToString("n2"));

            //yield return new WaitForEndOfFrame();

            //timer -= Time.deltaTime;
        }

        PhotonNetwork.LeaveRoom();
    }*/

    #endregion COROUTINES

    #region PUN CALLBACKS

    public override void OnDisconnected(DisconnectCause cause)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
        
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber == newMasterClient.ActorNumber)
        {
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        CheckEndOfGame();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        /*if (changedProps.ContainsKey(AsteroidsGame.PLAYER_LIVES))
        {
            CheckEndOfGame();
            return;
        }*/

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (changedProps.ContainsKey(RiteGame.PLAYER_LOADED_LEVEL))
        {
            if (CheckAllPlayerLoadedLevel())
            {
                Hashtable props = new Hashtable
                    {
                        {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time}
                    };
                PhotonNetwork.CurrentRoom.SetCustomProperties(props);
            }
        }
    }

    #endregion PUN CALLBACKS

    private void StartGame()
    {
        
        Hashtable props = new Hashtable
            {
                {"position", PhotonNetwork.LocalPlayer.ActorNumber}
            };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        int number = PhotonNetwork.LocalPlayer.ActorNumber;
        int positionIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["position"];

        GameObject spawnPoint = playerSpawnPoints.GetChild(positionIndex).GetChild(0).gameObject;
        spawnPoint.GetComponent<MeshRenderer>().material.color = RiteGame.GetColor(positionIndex);
        photonView.RPC("UpdateSpawnPointColor", RpcTarget.Others, positionIndex);
        spawnPoint.transform.GetChild(0).gameObject.SetActive(enabled);

       
        //GameObject go = PhotonNetwork.Instantiate(steamVrPlayerPrefab.name, spawnPoint.transform.position, Quaternion.identity, 0) as GameObject;

        if (PhotonNetwork.IsMasterClient)
        {
        }
    }
    [PunRPC]
    void UpdateSpawnPointColor(int positionIndex)
    {
        GameObject spawnPoint = playerSpawnPoints.GetChild(positionIndex).GetChild(0).gameObject;
        spawnPoint.transform.GetChild(0).gameObject.SetActive(enabled);
        spawnPoint.GetComponent<MeshRenderer>().material.color = RiteGame.GetColor(positionIndex);
    }

    private bool CheckAllPlayerLoadedLevel()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object playerLoadedLevel;

            if (p.CustomProperties.TryGetValue(RiteGame.PLAYER_LOADED_LEVEL, out playerLoadedLevel))
            {
                if ((bool)playerLoadedLevel)
                {
                    continue;
                }
            }

            return false;
        }

        return true;
    }

    private void CheckEndOfGame()
    {
        /*bool allDestroyed = true;

        foreach (Player p in PhotonNetwork.PlayerList)
        {
            object lives;
            /*if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_LIVES, out lives))
            {
                if ((int)lives > 0)
                {
                    allDestroyed = false;
                    break;
                }
            }*
        }

        if (allDestroyed)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StopAllCoroutines();
            }

            string winner = "";
            int score = -1;

            foreach (Player p in PhotonNetwork.PlayerList)
            {
                if (p.GetScore() > score)
                {
                    winner = p.NickName;
                    score = p.GetScore();
                }
            }

            StartCoroutine(EndOfGame(winner, score));
        }*/
    }

    private void OnCountdownTimerIsExpired()
    {
        StartGame();
    }

   

    public void Awake()
    {
        Instance = this;
    }

    [PunRPC]
    public void RoundStart()
    {
        alivePlayerList.Clear();
        for (int i = 0; i < playerList.Count; i++)
        {
            NetworkedPlayer dummyPlayer = playerList[i];
            alivePlayerList.Add(dummyPlayer);
        }

        ReturnPlayersToPositions();
    }

    [PunRPC]
    public void AddPlayerToTheList(NetworkedPlayer nPlayer)
    {
        playerList.Add(nPlayer);
        RoundStart();
        UpdateLeaderboard();
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

        //Debug.Log("Player death " + alivePlayerList);

        if (alivePlayerList.Count == 1)
        {
            Debug.LogError("Player " + alivePlayerList[0].photonID + " won");
            PlayerWinRound(alivePlayerList[0].photonID);
        }
    }

    

    [PunRPC]
    public void PlayerWinRound(int playerID)
    {
        Debug.LogError("PLAYER WON ROUND");

        IncrementPlayerScore(playerID);
        Invoke("RoundStart", 3f);

    }
    void IncrementPlayerScore(int playerID)
    {
        foreach(NetworkedPlayer nPlayer in playerList)
        {
            if(nPlayer.photonID==playerID)
            {
                nPlayer.score++;
            }
        }
        UpdateLeaderboard();
    }
    void UpdateLeaderboard()
    {
        List<string> playerNameList = new List<string>();
        List<int> playerScoreList = new List<int>();

        foreach (NetworkedPlayer nPlayer in playerList)
        {
            playerNameList.Add(nPlayer.name);
            playerScoreList.Add(nPlayer.score);
        }
        leaderboard.UpdateList(playerNameList, playerScoreList);
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
                if (playerList[n].photonID == id)
                {
                    Debug.LogError("Moving player with ID:" + playerList[n].photonID + "to spawn point with index: " + positionIndex);
                    playerList[n].gameObject.transform.parent.transform.position = playerSpawnPoints.GetChild(positionIndex).position;
                }
            }
        }
    }

    [PunRPC]
    public void ReviveAllPlayers()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            Debug.LogError("Calling Revive on all players");
            playerList[i].avatarStateController.SpawnAvatarBody();
        }
    }
}