using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AirSig;
using Photon.Pun;

public class AvatarStateController : MonoBehaviour {

    int photonId;
    public Transform head;
    public Transform leftArm;
    public Transform rightArm;
    public PhotonView photonView;
    public HealthManager hManager;
    public GameObject aliveAvatar;
    public GameObject deadAvatar;
    public SpellManager sManager;
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private GameObject aliveAvatarBody;
    [SerializeField]
    private GestureController gestureController;
    [SerializeField]
    private List<GameObject> listGO;
    private int positionIndex;

    VRIK avatarScript;
    VRIK deadAvatarScript;
    
    // Use this for initialization
    [PunRPC]
    void Start () {

       
        if(photonView.IsMine)
        {
           
            positionIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["position"];
            photonView.RPC("SpawnAvatarBody", RpcTarget.AllViaServer);
            

        }
        


    }
    [PunRPC]
    public void SpawnAvatarBody()
    {

        if(!photonView.IsMine)
        {
            playerCamera.enabled = false;
            Debug.Log("PhotonView is not mine");
            gestureController.enabled = false;
        }
        else
        {
            aliveAvatarBody.SetActive(false);
        }
        aliveAvatar.SetActive(true);
        deadAvatar.SetActive(false);

        aliveAvatar.GetComponent<SpawnInfo>().playerReference = this.gameObject;
        aliveAvatar.GetComponent<SpawnInfo>().AwakeAvatar();
        sManager.canCastSpells = true;
        hManager.health = 100f;
        avatarScript = aliveAvatar.GetComponent<VRIK>();
        avatarScript.solver.spine.headTarget = head;
        avatarScript.solver.leftArm.target = leftArm;
        avatarScript.solver.rightArm.target = rightArm;
        

    }
    [PunRPC]
    public void ChangeAvatarToDead()
    {
        if(photonView.IsMine)
        {
            aliveAvatarBody.SetActive(true);
        }
        sManager.canCastSpells = false;
        aliveAvatar.SetActive(false);
        deadAvatar.SetActive(true);
        deadAvatar.GetComponent<SpawnInfo>().playerReference = this.gameObject;
        

        deadAvatarScript = deadAvatar.GetComponent<VRIK>();
        deadAvatarScript.solver.spine.headTarget = head;
        deadAvatarScript.solver.leftArm.target = leftArm;
        deadAvatarScript.solver.rightArm.target = rightArm;
        
    }
   


}
