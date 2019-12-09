using UnityEngine;


namespace Rite.SpellBook
{
    [System.Serializable, CreateAssetMenu(fileName = "SpellDataAsset", menuName = "Rite/Spell Data Asset", order = 0)]
    public class SpellData : ScriptableObject
    {
        //#if UNITY_EDITOR
        /// <summary> Will the spell be used in build or hidden in game </summary>
        public bool buildReady;
        //#endif

        [Header("Spell")]
        public string spellName = "New Spell";

        public Sprite spellSprite = null;

        public bool requiresHeldCast = false;
        public bool canChargeOnCast = false;

        [Min(0)] public float manaCost = 10;

        /// <summary> Amount of health the spell has </summary>
        [Min(0)] public float health = 10;

        /// <summary> Amount of damage the spell deals when colliding a gameObject </summary>
        [Min(0)] public float damage = 10;

        /// <summary> Lifetime of the spell after being cast. Lifetime of 0 is infinite </summary>
        [Min(0)] public float lifetime = 0;

        [Min(0)] public float spellSpeed = 10;

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

        [Header("Hit Effect"), Space(10)]
        [Tooltip("Hit Effect object. Requires SpellEffect script in the parent object!")]
        public GameObject hitEffectPrefab = null;

        [Tooltip("Hit Effect object scaler")]
        public Vector3 hitEffectScale = Vector3.one;

        [Tooltip("Should override the colors of the materials and particle effects of the Hit Effect object")]
        public bool overrideEffectColor = true;

        [Tooltip("Multiply the color of the materials and particle effects of the Hit Effect object")]
        public Color hitEffectColorMultiplier = Color.white;
    }
}