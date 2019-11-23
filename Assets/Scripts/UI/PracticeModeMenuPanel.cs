using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeModeMenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacleCourse;
    [SerializeField]
    private GameObject enemyGroup;
    [SerializeField]
    private GameObject gestureTutorialProps;
    [SerializeField]
    private GameObject baseTutorialProps;
    public TextMeshPro titleText;
    // Start is called before the first frame update
    public void OnCombatPracticeButtonClicked()
    {
        ResetPracticeModeSelection();
        titleText.text = "Combat practice";
      
        enemyGroup.SetActive(true);
    }
    public void OnGestureTutorialButtonClicked()
    {
        ResetPracticeModeSelection();
        titleText.text = "Gesture tutorial";
        
        gestureTutorialProps.SetActive(true);
    }
    public void OnObstacleCourseButtonClicked()
    {
        ResetPracticeModeSelection();
        titleText.text = "Obstacle course";
        
        obstacleCourse.SetActive(true);
    }
    public void OnLeaveButtonPressed()
    {
     
        SceneManager.UnloadSceneAsync("PracticeScene");
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Lobby");
    }
    public void OnBaseTutorialButtonClicked()
    {
        ResetPracticeModeSelection();
        titleText.text = "Base tutorial";
      
        baseTutorialProps.SetActive(true);
    }
    public void ResetPracticeModeSelection()
    {
        titleText.text = "";
        obstacleCourse.SetActive(false);
        enemyGroup.SetActive(false);
        gestureTutorialProps.SetActive(false);
        baseTutorialProps.SetActive(false);
    }
}
