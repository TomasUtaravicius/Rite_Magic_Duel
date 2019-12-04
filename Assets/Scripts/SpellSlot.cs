using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GestureController;

public class SpellSlot : MonoBehaviour
{
    public SpellManager spellManager;
    [SerializeField]
    private GameObject animatedSpellSlot;
    private Animator spellSlotAnimator;
    // Start is called before the first frame update

    private void Start()
    {
        spellSlotAnimator = animatedSpellSlot.GetComponent<Animator>();
    }
    private void Awake()
    {
        //UpdateSpellSlot();
    }
    private void OnEnable()
    {
        spellManager.OnSpellValueChanged += UpdateSpellSlot;
    }
  
    void UpdateSpellSlot()
    {
        if(spellManager.bufferedGesture == Gesture.NONE)
        {
            spellSlotAnimator.SetInteger("AnimationNumber", 0);
            Debug.Log("Turning the animation off");
        }
        else if(spellManager.bufferedGesture == Gesture.Sandtimer)
        {
            spellSlotAnimator.SetInteger("AnimationNumber",1);
            Debug.Log("Turning the animation to sandclock)");
        }
        else if (spellManager.bufferedGesture == Gesture.Circle)
        {
            spellSlotAnimator.SetInteger("AnimationNumber", 2);
            Debug.Log("Turning the animation to Circle)");
        }
        Debug.Log("Update spell slot");
    }
}
