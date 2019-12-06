using Photon.Pun;
using UnityEngine;

public enum SpellType { None, Projectile, Shield }

public class Spell : MonoBehaviourPun, IPunObservable
{
    protected string spellName = "";

    [SerializeField] private bool requiresHeldCast = false;
    [SerializeField] private bool canChargeOnCast = false;

    /// <summary> Amount of health the spell has </summary>
    private float health = 10;

    /// <summary> Amount of damage the spell deals when touching a overlapped gameObject </summary>
    public float damage = 10;

    /// <summary> Lifetime of the spell after being cast. Lifetime of 0 is infinite </summary>
    private float lifeTime = 0;

    public float manaCost = 0;

    public float spellSpeed = 10;

    /// <summary> A held cast spell requires the caster to hold the spell to keep it active and release to deactivate it </summary>
    public bool RequiresHeldCast { get => requiresHeldCast; }
    public bool CanChargeOnCast { get => canChargeOnCast; }



    //Spell effect references
    [SerializeField] RFX1_EffectSettingVisible visibilityScript;

    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }



    public virtual void FireSpell()
    { }

    [PunRPC]
    public void OnSpellReleased()
    {
        visibilityScript.IsActive = false;

        if (photonView.IsMine)
        {
            Invoke("DestroySpell", 0.7f);
        }
    }

    private void DestroySpell()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }


    public void SetSpellAttributes(string spellName, bool requiresHeldCast = false, bool canChargeOnCast = false, float health = 10, float damage = 10, float lifeTime = 0, float spellSpeed = 0)
    {
        gameObject.name = spellName;
        this.spellName = spellName;

        this.requiresHeldCast = requiresHeldCast;
        this.canChargeOnCast = canChargeOnCast;

        this.health = health;
        this.damage = damage;
        this.lifeTime = lifeTime;
        this.spellSpeed = spellSpeed;
    }

    
    public virtual void SetSpellVisuals(bool shouldTintColor, Color tintColor)
    {
        if (shouldTintColor) TintSpellColors(tintColor);
    }

    protected virtual void TintSpellColors(Color tintColor)
    {
        var spellColor = GetComponent<RFX1_EffectSettingColor>();
        if(spellColor) spellColor.Color = tintColor;
    }
}