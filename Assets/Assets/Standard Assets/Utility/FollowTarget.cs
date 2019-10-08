using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
        


        private void Update()
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
