using Photon.Pun;
using UnityEngine;

public class Holder : MonoBehaviour
{
    public PhotonView photonView;

    [SerializeField]
    private Transform _holderTarget;

    [SerializeField]
    public Vector3 offset;

    [SerializeField]
    private bool recordMode;

    protected void Awake()
    {
        if (!recordMode)
        {
            if (!photonView.IsMine)
            {
                Destroy(this);
            }
            if (_holderTarget == null)
            {
                Debug.Log("Destroying target holder target null");
                Destroy(this);
            }
        }
    }

    private void Update()
    {
        //Syncrhoize transform with target transform
        if (_holderTarget == null)
        {
            return;
        }

        transform.position = _holderTarget.position - offset;
        transform.rotation = _holderTarget.rotation;
    }
}