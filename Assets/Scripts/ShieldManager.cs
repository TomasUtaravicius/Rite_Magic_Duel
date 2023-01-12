using Photon.Pun;
using UnityEngine;

public class ShieldManager : MonoBehaviour, IDamagable
{
    public PhotonView photonView;
    public float manaCost;
    public float health;

    [SerializeField]
    private RFX1_EffectSettingVisible visibilityScript;

    public void GetHit(float damage)
    {
        if (photonView.IsMine)
        {
            health -= damage;
            if (health <= 0)
            {
                TurnOffShield();
                photonView.RPC("TurnOffShield", RpcTarget.AllViaServer);
            }
        }
    }

    [PunRPC]
    public void TurnOffShield()
    {
        visibilityScript.IsActive = false;

        if (photonView.IsMine)
        {
            Invoke("DestroyShield", 0.7f);
        }
    }

    private void DestroyShield()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }
}