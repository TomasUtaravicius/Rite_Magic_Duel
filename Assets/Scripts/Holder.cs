using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Holder : MonoBehaviour
    {

        public PhotonView photonView;
        [SerializeField]
        Transform _holderTarget;
        [SerializeField]
        public Vector3 offset;

        protected void Awake()
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

        void Update()
        {
            //Syncrhoize transform with target transform
            if (_holderTarget == null)
            {
                return;
            }

            transform.position = _holderTarget.position-offset;
            transform.rotation = _holderTarget.rotation;

        }
    }

