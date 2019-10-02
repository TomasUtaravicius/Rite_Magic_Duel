using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
public class Information : MonoBehaviourPun, IPunObservable
{

    
    public AudioSource audioForExplosion;
    public AudioSource audioForFlight;
    public GameObject explosionPrefab;
    public new string name;
    public float damage;
    public float feedback;
    public float speed;
    public float explosionPower;
    public GameObject spellModel;
    public bool isLethal;
    public GameObject ripplePrefab;
    public bool hasHit;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        //gameObject.GetComponent<PhotonTransformView>().SetSynchronizedValues(transform.forward * speed, 0f);
        audioForFlight.Play();
    }
    
    public void OnTriggerEnter(Collider other)
    {
            if(photonView.IsMine)
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
                Debug.LogError("Collision with environment");
                Transform explosionTransform = gameObject.transform;
                GameObject explosionPrefabObject = PhotonNetwork.Instantiate(explosionPrefab.name, gameObject.transform.position, gameObject.transform.rotation, 0);

                AudioSource.PlayClipAtPoint(audioForExplosion.clip, gameObject.transform.position);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
            
        
       
      
       
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
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
        if(photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

}