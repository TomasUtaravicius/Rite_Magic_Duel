using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expelliarmus : Projectile
{
    [SerializeField] private Light spellLight;
    [SerializeField] private new ParticleSystem particleSystem;
    public bool isLethal;
    public bool hasHit;

    private void OnDisable()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        audioSource.Stop();
    }

    public override void FireSpell()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * spellSpeed;
        audioSource.Play();
    }

    protected override void TintSpellColors(Color tintColor)
    {
        spellLight.color *= tintColor;

        ParticleSystem.MainModule main = particleSystem.main;
        main.startColor = tintColor;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "SimpleDefensiveSpell" && !isLethal)
            {
                Debug.Log("non lethal spell collision with simple defensive shield");
                //PhotonNetwork.Destroy(gameObject);

            }
            if (other.gameObject.tag == "LethalDefensiveSpell" && !isLethal)
            {
                Debug.Log("non lethal spell collision with Lethal defensive shield");
                //PhotonNetwork.Destroy(gameObject);

            }
            if (isLethal && other.gameObject.tag == "LethalDefensiveSpell")
            {
                Debug.Log("lethal spell collision with lethal defensive spell");
                //PhotonNetwork.Destroy(gameObject);


            }
            if (isLethal && other.gameObject.tag == "SimpleDefensiveSpell")
            {
                Debug.Log("Lethal spell collision with simple defensive shield");

            }
            if (other.gameObject.tag == "Player")
            {
                //Vector3 positionForExplosion = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                Debug.Log(gameObject.transform);

                Debug.Log("Collision with player");




            }
            if (other.gameObject.tag == "Environment")
            {

                Transform explosionTransform = gameObject.transform;
                GameObject explosionPrefabObject = PhotonNetwork.Instantiate(hitEffectPrefabName, gameObject.transform.position, gameObject.transform.rotation, 0);

                AudioSource.PlayClipAtPoint(audioSource.clip, gameObject.transform.position);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }





    }
    public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("Stream is writing");
            stream.SendNext(hasHit);
        }
        else
        {
            Debug.Log("Stream is receiving");
            hasHit = (bool)stream.ReceiveNext();

        }
    }

    [PunRPC]
    public void DestroyItself()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}
