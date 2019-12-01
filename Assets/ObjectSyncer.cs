using Photon.Pun;
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
    public GestureController gc;
    public SpellManager spellManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnEnable()
    {
        if (!photonView.IsMine)
        {
            Destroy(this);
        }
        VRTK_SDKManager sdk = VRTK_SDKManager.instance;
        sdk.loadedSetup.actualBoundaries.transform.position = transform.position;
        sdk.loadedSetup.actualBoundaries.transform.rotation = transform.rotation;
        spellManager.vRInputModule = GameObject.Find("PR_VRInputModule").GetComponent<VRInputModule>();
        gc.vRInputModule = GameObject.Find("PR_VRInputModule").GetComponent<VRInputModule>();
        headTarget = GameObject.Find("LocalPlayerHead").transform;
        leftHandTarget = GameObject.Find("LocalPlayerLeftHand").transform;
        rightHandTarget = GameObject.Find("LocalPlayerRightHand").transform;
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
