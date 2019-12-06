using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PracticeModeMenuPanel : MonoBehaviour
{
    
    public GameObject obstacleCourse;
   

    
    public GameObject gestureTutorialProps;
   
    public GameObject baseTutorialProps;
    public TextMeshPro titleText;
    // Start is called before the first frame update
    private void Start()
    {
        obstacleCourse = GameObject.Find("ObstacleCourse");
        gestureTutorialProps = GameObject.Find("GestureTutorialProps");
        baseTutorialProps = GameObject.Find("BaseTutorialProps");
    }
    public void OnCombatPracticeButtonClicked()
    {
        ResetPracticeModeSelection();
        titleText.text = "Combat practice";
        
        //enemyGroup.SetActive(true);
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
        gestureTutorialProps.SetActive(false);
        baseTutorialProps.SetActive(false);
    }
}
