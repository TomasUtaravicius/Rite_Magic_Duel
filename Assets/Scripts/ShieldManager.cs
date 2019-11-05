using Photon.Pun;
using System.Collections;
using UnityEngine;

public class ShieldManager : MonoBehaviour, IDamagable
{
    public GameObject deflectedSpell;
    public PhotonView photonView;
    public GameObject ripplePrefab;
    public Material shieldMat;
    public float manaCost;
    public float health;
    private IEnumerator increaseCoroutine = null;
    private IEnumerator decreaseCoroutine = null;

    public void GetHit(float damage)
    {
        if(photonView.IsMine)
        {
            health -= damage;
            if(health<=0)
            {
                photonView.RPC("TurnOffShield", RpcTarget.AllViaServer);
            }
        }
    }

    /*[PunRPC]
    public void DestroyObject(int id)

    {
        Destroy(PhotonView.Find(id).gameObject);
    }*/

    private void Start()
    {
        StartCoroutine("IncreaseFresnel");
        Invoke("DisableShield", 2f);
    }

    public void DisableShield()
    {
        if (this.gameObject != null)
        {
            StartCoroutine("DecreaseFresnel");
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OffensiveSpell")
        {
            if (photonView.IsMine && !other.gameObject.GetComponent<Information>().hasHit)
            {
                other.gameObject.GetComponent<Information>().hasHit = true;
              
                other.gameObject.GetComponent<PhotonView>().RPC("DestroyItself", RpcTarget.AllViaServer);

                PhotonNetwork.Instantiate(deflectedSpell.name, other.gameObject.transform.position, photonView.gameObject.transform.rotation, 0);
                photonView.RPC("TurnOffShield", RpcTarget.AllViaServer);
            }
        }
    }*/

    [PunRPC]
    private void TurnOffShield()
    {
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        StartCoroutine("DecreaseFresnel");
    }

    private IEnumerator IncreaseFresnel()
    {
        if (increaseCoroutine == null)
        {
            float fresnelValue = 0f;
            increaseCoroutine = IncreaseFresnel();
            while (fresnelValue <= 2)
            {
                shieldMat.SetFloat("_FresnelWidth", fresnelValue);
                fresnelValue += 0.05f;

                yield return new WaitForSeconds(0.01f);
            }
            increaseCoroutine = null;
        }
    }

    private IEnumerator DecreaseFresnel()
    {
        if (decreaseCoroutine == null)
        {
            float fresnelValue = 2f;
            decreaseCoroutine = DecreaseFresnel();
            while (fresnelValue >= 0)
            {
                shieldMat.SetFloat("_FresnelWidth", fresnelValue);
                fresnelValue -= 0.05f;

                yield return new WaitForSeconds(0.01f);
            }
            DestroyShield();
            decreaseCoroutine = null;
        }
    }

    private void DestroyShield()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    /*[PunRPC]
    private void SetToHit(int id)
    {
        PhotonView pView = PhotonView.Find(id);

        if (pView != null && pView.IsMine)
        {
            //pView.gameObject.GetComponent<Information>().hasHit = true;
            PhotonNetwork.Destroy(pView.gameObject);
        }
    }*/
}