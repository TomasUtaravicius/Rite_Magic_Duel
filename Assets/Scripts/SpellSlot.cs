using UnityEngine;
using static GestureController;

public class SpellSlot : MonoBehaviour
{
    public SpellManager spellManager;
    [SerializeField]
    private GameObject NoAnimation;
    [SerializeField]
    private GameObject SandClockAnimation;
    [SerializeField]
    private GameObject CircleAnimation;
    [SerializeField]
    private GameObject ThunderboltAnimation;
    [SerializeField]
    private GameObject SwishAndFlickAnimation;
    [SerializeField]
    private GameObject FifthGestureAnimation;

    private Animator spellSlotAnimator;
    // Start is called before the first frame update

    private void Start()
    {

    }
    private void OnEnable()
    {
        spellManager.OnSpellValueChanged += UpdateSpellSlot;
    }
    private void OnDisable()
    {
        spellManager.OnSpellValueChanged -= UpdateSpellSlot;
    }

    void UpdateSpellSlot()
    {
        TurnOffSpellAnimations();
        if (spellManager.bufferedGesture == Gesture.NONE)
        {
            TurnOffSpellAnimations();
            Debug.Log("Turning the animation off");
        }
        else if (spellManager.bufferedGesture == Gesture.Sandtimer)
        {

            SandClockAnimation.SetActive(true);
            Debug.Log("Turning the animation to sandclock)");
        }
        else if (spellManager.bufferedGesture == Gesture.Circle)
        {
            CircleAnimation.SetActive(true);
            Debug.Log("Turning the animation to Circle)");
        }
        else if (spellManager.bufferedGesture == Gesture.SwishAndFlick)
        {
            SwishAndFlickAnimation.SetActive(true);
            Debug.Log("Turning the animation to Circle)");
        }
        else if (spellManager.bufferedGesture == Gesture.ThunderBolt)
        {
            ThunderboltAnimation.SetActive(true);
            Debug.Log("Turning the animation to Circle)");
        }


        Debug.Log("Update spell slot");
    }
    void TurnOffSpellAnimations()
    {
        SandClockAnimation.SetActive(false);
        CircleAnimation.SetActive(false);
        ThunderboltAnimation.SetActive(false);
        SwishAndFlickAnimation.SetActive(false);
    }
}
