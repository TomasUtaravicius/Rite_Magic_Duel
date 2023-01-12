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
    private ReferenceHolderForPracticeModeUI referenceHolder;
    // Start is called before the first frame update
    private void Start()
    {

       
    }
    private void OnEnable()
    {
        referenceHolder = GameObject.FindGameObjectWithTag("ReferenceHolder").GetComponent<ReferenceHolderForPracticeModeUI>();
        ResetPracticeModeSelection();
        //this.gameObject.transform.parent.gameObject.SetActive(false);
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

        referenceHolder.gestureTutorial.SetActive(true);
    }
    public void OnObstacleCourseButtonClicked()
    {
        ResetPracticeModeSelection();
        titleText.text = "Obstacle course";
        
        referenceHolder.obstacleCourse.SetActive(true);
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

        referenceHolder.baseTutorial.SetActive(true);
    }
    public void ResetPracticeModeSelection()
    {
        titleText.text = "";
        referenceHolder.obstacleCourse.SetActive(false);
        referenceHolder.gestureTutorial.SetActive(false);
        referenceHolder.baseTutorial.SetActive(false);
    }
}
