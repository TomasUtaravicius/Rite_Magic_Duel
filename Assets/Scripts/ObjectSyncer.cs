﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class ObjectSyncer : MonoBehaviour
{
    private Transform headTarget;
    private Transform leftHandTarget;
    private Transform rightHandTarget;
    public GameObject actualHead;
    public GameObject actualRightHand;
    public GameObject actualLeftHand;
    public PhotonView photonView;
    public VRInputModule vRInputModule;
    public GestureController gc;
    public SpellManager spellManager;
    public bool isPracticeMode;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        Setup();
    }
    private void Setup()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
        vRInputModule = GameObject.Find("PR_VRInputModule").GetComponent<VRInputModule>();
        if (isPracticeMode)
        {
           
            GetComponentInChildren<PracticeModeUIController>().vRInputModule = vRInputModule;
        }
        else
        {
            GetComponentInChildren<UIController>().vRInputModule = vRInputModule;
        }
        if(photonView.IsMine)
        {
            VRTK_SDKManager sdk = VRTK_SDKManager.instance;
           
            sdk.loadedSetup.actualBoundaries.transform.position = GameObject.Find("PlayerSpawnPoints").transform.GetChild(PhotonNetwork.LocalPlayer.ActorNumber).GetChild(0).gameObject.transform.position;
            sdk.loadedSetup.actualBoundaries.transform.rotation = GameObject.Find("PlayerSpawnPoints").transform.GetChild(PhotonNetwork.LocalPlayer.ActorNumber).GetChild(0).gameObject.transform.rotation;
            spellManager.vRInputModule = vRInputModule;
            gc.vRInputModule = vRInputModule;
            headTarget = GameObject.Find("LocalPlayerHead").transform;
            leftHandTarget = GameObject.Find("LocalPlayerLeftHand").transform;
            rightHandTarget = GameObject.Find("LocalPlayerRightHand").transform;
        }
       
    }

    void Awake()
    {
        VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
    }

    void OnDestroy()
    {
        VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            actualHead.transform.position = headTarget.position;
            actualHead.transform.rotation = headTarget.rotation;


            actualLeftHand.transform.position = leftHandTarget.position;
            actualLeftHand.transform.rotation = leftHandTarget.rotation;

            actualRightHand.transform.position = rightHandTarget.position;
            actualRightHand.transform.rotation = rightHandTarget.rotation;
        }
    }
}
