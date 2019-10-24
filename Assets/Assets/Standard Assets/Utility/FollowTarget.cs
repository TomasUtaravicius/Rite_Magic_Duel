using Photon.Pun;
using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : Holder
    {
        [SerializeField]
        Transform _holderTarget;
        [SerializeField]


        protected void Awake()
        {
            base.Awake();
            if (!photonView.IsMine)
            {
                Destroy(this);
            }
            if (_holderTarget == null)
            {
                Destroy(this);
            }
        }

        void Update()
        {
            //Syncrhoize transform with target transform
            if (_holderTarget == null)
            {
                return;
            }

            transform.position = _holderTarget.position;
            transform.rotation = _holderTarget.rotation;

        }
    }
}
