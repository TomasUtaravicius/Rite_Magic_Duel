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
    public ResourceManager resourceManager;
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
    public AvatarRagdollController avatarRagdollController;
    [SerializeField]
    bool isLobbyMode;
    VRIK avatarScript;
    VRIK deadAvatarScript;
    
    // Use this for initialization
    [PunRPC]
    void Start () {


        if(isLobbyMode)
        {
            SpawnAvatarBody();
        }
        if (photonView.IsMine)
        {
            positionIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["position"];
            photonView.RPC("SpawnAvatarBody", RpcTarget.AllViaServer);
        }
        


    }
    [PunRPC]
    public void SpawnAvatarBody()
    {
        if(isLobbyMode)
        {
            TurnOffAvatarBodyForLocalPlayer();
 
        }
        else
        {
            if (!photonView.IsMine)
            {
                playerCamera.enabled = false;
                gestureController.enabled = false;

            }
            else
            {

                TurnOffAvatarBodyForLocalPlayer();
            }
        }
        
        aliveAvatar.SetActive(true);
        //deadAvatar.SetActive(false);
        avatarRagdollController.TurnOffRagdoll();
        sManager.canCastSpells = true;
        resourceManager.health = 100f;
        resourceManager.mana = 100f;
        avatarScript = aliveAvatar.GetComponent<VRIK>();
        avatarScript.solver.spine.headTarget = head;
        avatarScript.solver.leftArm.target = leftArm;
        avatarScript.solver.rightArm.target = rightArm;
        

    }
    private void TurnOffAvatarBodyForLocalPlayer()
    {
        foreach(SkinnedMeshRenderer meshRenderer in aliveAvatarBody.transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
    private void TurnOnAvatarBodyForLocalPlayer()
    {
        foreach (SkinnedMeshRenderer meshRenderer in aliveAvatarBody.transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            Debug.LogError("Turning on the avatar for local player");
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
    [PunRPC]
    public void ChangeAvatarToDead()
    {
        if(photonView.IsMine)
        {
            TurnOnAvatarBodyForLocalPlayer();
        }
        avatarRagdollController.TurnOnRagdoll();
        sManager.canCastSpells = false;
        //aliveAvatar.SetActive(false);
        //deadAvatar.SetActive(true);

       /* deadAvatarScript = deadAvatar.GetComponent<VRIK>();
        deadAvatarScript.solver.spine.headTarget = head;
        deadAvatarScript.solver.leftArm.target = leftArm;
        deadAvatarScript.solver.rightArm.target = rightArm;*/
        
    }
   


}
