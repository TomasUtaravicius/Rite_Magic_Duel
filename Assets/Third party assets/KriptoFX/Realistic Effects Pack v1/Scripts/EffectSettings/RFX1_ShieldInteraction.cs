using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFX1_ShieldInteraction : MonoBehaviour
{

    RFX1_TransformMotion transformMotion;
    SphereCollider coll;
    // Use this for initialization
    void Start()
    {
        transformMotion = GetComponentInChildren<RFX1_TransformMotion>();
        if (transformMotion != null)
        {
            transformMotion.CollisionEnter += TransformMotion_CollisionEnter;
            coll = transformMotion.gameObject.AddComponent<SphereCollider>();
            coll.radius = 0.3f;
            coll.isTrigger = true;
        }

        var physxMotion = GetComponentInChildren<RFX4_PhysicsMotion>();
        if (physxMotion != null)
        {
            physxMotion.CollisionEnter += PhysxMotion_CollisionEnter;
            coll = physxMotion.gameObject.AddComponent<SphereCollider>();
            coll.radius = 0.1f;
            coll.isTrigger = true;
        }
    }

    private void PhysxMotion_CollisionEnter(object sender, RFX4_PhysicsMotion.RFX4_CollisionInfo e)
    {
        var shielCT = e.HitCollider.transform.GetComponentInChildren<RFX1_ShieldCollisionTrigger>();
        if (shielCT == null) return;
        RaycastHit hit = new RaycastHit();
        hit.point = e.HitPoint;

        shielCT.OnCollision(hit, gameObject);
        coll.enabled = false;
    }

    private void TransformMotion_CollisionEnter(object sender, RFX1_TransformMotion.RFX1_CollisionInfo e)
    {
        var shielCT = e.Hit.transform.GetComponentInChildren<RFX1_ShieldCollisionTrigger>();
        if (shielCT == null) return;
        shielCT.OnCollision(e.Hit, gameObject);
        coll.enabled = false;
    }

    void OnEnable()
    {
        if (coll != null)
            coll.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}