using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName = "SpellData", menuName = "Rite/Spell Data", order = 0)]
public class SpellData : ScriptableObject
{ 
    [Header("Spell")]
    public string spellName = "New Spell";

    public Sprite spellSprite = null;

    [Min(0)] public float manaCost = 10;

    /// <summary> Amount of health the spell has </summary>
    [Min(0)] public float health = 10;

    /// <summary> Amount of damage the spell deals when colliding a gameObject </summary>
    [Min(0)] public float damage = 10;

    /// <summary> Lifetime of the spell after being cast. Lifetime of 0 is infinite </summary>
    [Min(0)] public float lifetime = 0;

    /// <summary> Spell controller rumble feedback </summary>
    [Min(0)] public float feedback;

    //Spell visuals and audio
    [Space(10)]
    [Tooltip("Spell object. Requires Spell script in the parent object!")]
    public GameObject spellPrefab = null;

    [Tooltip("Spell object scaler")]
    public Vector3 spellScale = Vector3.one;

    [Tooltip("Should override the colors of the materials and particle effects of the Spell object")]
    public bool shouldTintSpell = true;

    [Tooltip("Multiply the color of the materials and particle effects of the Spell object")]
    public Color spellTint = Color.white;

    [Header("Projectile")]
    [Min(0)] public float spellSpeed = 10;

    [Header("Hit Effect"), Space(10)]
    [Tooltip("Hit Effect object. Requires SpellEffect script in the parent object!")]
    public GameObject hitEffectPrefab = null;

    [Tooltip("Hit Effect object scaler")]
    public Vector3 hitEffectScale = Vector3.one;

    [Tooltip("Multiplier that is applied to the SpellEffect particle systems")]
    public float timeMultiplier = 1f;

    [Tooltip("Should override the colors of the materials and particle effects of the Hit Effect object")]
    public bool overrideEffectColor = true;

    [Tooltip("Multiply the color of the materials and particle effects of the Hit Effect object")]
    public Color hitEffectColorMultiplier = Color.white;

    [Space(5)]
    [Tooltip("Sound used by the SpellEffect script")]
    public AudioClip hitEffectSound;
}