using Photon.Pun;
using System;
using UnityEngine;

public enum SpellType { None, Projectile, Shield }

public class SB_Spell : MonoBehaviourPun, IPunObservable
{


    public virtual SpellType SpellType { get => SpellType.None; }

    protected string spellName = "";

    /// <summary> Amount of health the spell has </summary>
    private float health = 10;

    /// <summary> Amount of damage the spell deals when touching a overlapped gameObject </summary>
    private float damage = 10;

    /// <summary> Lifetime of the spell after being cast. Lifetime of 0 is infinite </summary>
    private float lifeTime = 0;


    [SerializeField] protected string hitEffectPrefabName = "";
    [SerializeField] protected AudioSource audioSource = null;


    public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        throw new System.NotImplementedException();
    }



    public virtual void FireSpell()
    { }


    public void SetSpellAttributes(string spellName, string hitEffectPrefabName, float health = 10, float damage = 10, float lifeTime = 0)
    {
        gameObject.name = spellName;
        this.spellName = spellName;
        this.hitEffectPrefabName = hitEffectPrefabName;
        this.health = health;
        this.damage = damage;
        this.lifeTime = lifeTime;
    }

    public virtual void SetSpellVisuals(AudioClip audio)
    {
        if (audioSource) audioSource.clip = audio;
    }

    public virtual void SetSpellVisuals(AudioClip audio, bool shouldTintColor, Color tintColor)
    {
        if (audioSource) audioSource.clip = audio;
        if (shouldTintColor) TintSpellColors(tintColor);
    }

    protected virtual void TintSpellColors(Color tintColor)
    {
        throw new NotImplementedException();
    }
}