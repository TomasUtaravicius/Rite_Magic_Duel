using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Leaderboard : MonoBehaviour
{
    public GameObject playerScoreEntry;
    public GameObject playerNameEntry;
    public GameObject playerNameListCanvas;
    public GameObject playerScoreListCanvas;

    public void UpdateList(List<string> playerNameList,List<int> playerScoreList)
    {
        foreach (Transform child in playerNameListCanvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in playerScoreListCanvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        for(int i=0; i<playerNameList.Count;i++)
        {
            GameObject playerNameGo = Instantiate(playerNameEntry, playerNameListCanvas.transform);
            playerNameGo.GetComponent<TextMeshProUGUI>().text = playerNameList[i];
            GameObject playerScoreGo = Instantiate(playerScoreEntry, playerScoreListCanvas.transform);
            playerScoreGo.GetComponent<TextMeshProUGUI>().text = playerScoreList[i].ToString();

        }

    }
   
    
}
