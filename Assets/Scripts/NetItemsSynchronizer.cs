﻿using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Com.UTYStudios.SpellArena
{
    /// <summary>
    /// Syncrhonize states of child objects
    /// </summary>
    public class NetItemsSynchronizer : MonoBehaviourPun, IPunObservable
    {
        [SerializeField]
        GameObject[] _syncState;
        [SerializeField]
        PhotonView _view;

        void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                for (int i = 0; i < _syncState.Length; i++)
                {
                    stream.SendNext(_syncState[i].activeSelf);
                }
            }
            else
            {
                for (int i = 0; i < _syncState.Length; i++)
                {
                    bool isActive = (bool)stream.ReceiveNext();
                    if (isActive != _syncState[i].activeSelf)
                    {
                        _syncState[i].SetActive(isActive);
                    }
                }
            }
        }

        public void ForceUpdate()
        {
            bool[] parameters = new bool[_syncState.Length];
            for (int i = 0; i < _syncState.Length; i++)
            {
                parameters[i] = _syncState[i].activeSelf;
            }

            photonView.RPC("ForceUpdateItem", Photon.Pun.RpcTarget.Others, parameters);
        }

        [PunRPC]
        void ForceUpdateItem(bool[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                _syncState[i].SetActive(parameters[i]);
            }
        }
    }
}
