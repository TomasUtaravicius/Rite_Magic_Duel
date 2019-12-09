using Photon.Pun;
using UnityEngine;


namespace Rite.SpellBook
{
    public delegate void SpellEffectDelegate();

    public class SpellEffect : MonoBehaviourPun, IPunObservable
    {
        private event SpellEffectDelegate OnSpellHidden;

        [SerializeField, Tooltip("Multiplier that is applied to the SpellEffect particle systems")]
        private float timeMultiplier = 1f;

        [SerializeField] private ParticleSystem[] particleSystems = null;
        [SerializeField] private Material[] materials = null;
        [SerializeField] private AudioSource audioSource = null;

        /// <summary> Make the spell effect visible. </summary>
        private void Show(bool shouldReset)
        { }

        /// <summary> Reset the spell effect. </summary>
        private void Reset()
        { }

        /// <summary> Hides the spell effect. Sets it ready for pooling. Resets the spell effect. </summary>
        private void Hide()
        { }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }

        public void SetSpellEffect(AudioClip audio, float timeMultiplier = 1f)
        {
            if (audioSource) audioSource.clip = audio;
        }

        public void SetSpellEffect(AudioClip audio, bool shouldOverrideColor, Color colorMultiplier, float timeMultiplier = 1f)
        {
            if (audioSource) audioSource.clip = audio;
            if (shouldOverrideColor) SetEffectColor(colorMultiplier);
        }

        /// <summary> Set Color of the Spell Effect </summary>
        private void SetEffectColor(Color newColor)
        {
            //TODO set color of materials and particle effects
        }
    }
}