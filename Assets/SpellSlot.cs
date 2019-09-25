using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    public SpellManager spellManager;
    // Start is called before the first frame update

    private void Awake()
    {
        UpdateSpellSlot();
    }
    private void OnEnable()
    {
        spellManager.OnSpellValueChanged += UpdateSpellSlot;
    }
  
    void UpdateSpellSlot()
    {
        
        Debug.Log("Update spell slot");
    }
}
