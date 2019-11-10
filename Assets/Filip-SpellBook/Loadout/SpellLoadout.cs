using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpellLoadout
{
    public string name;
    public SpellData spellSlot1;
    public SpellData spellSlot2;
    public SpellData spellSlot3;
    public SpellData spellSlot4;
    public SpellData spellSlot5;

    public int SpellCount { get => 5; }

    public SpellLoadout()
    {
        name = "SpellLoadout-New";
        spellSlot1 = null;
        spellSlot2 = null;
        spellSlot3 = null;
        spellSlot4 = null;
        spellSlot5 = null;
    }

    public SpellLoadout(int number)
    {
        name = "SpellLoadout" + number;
        spellSlot1 = null;
        spellSlot2 = null;
        spellSlot3 = null;
        spellSlot4 = null;
        spellSlot5 = null;
    }

    public override string ToString()
    {
        return "Name: " + name 
            + "\nSpellSlot1: " + spellSlot1 
            + "\nSpellSlot2: " + spellSlot2 
            + "\nSpellSlot3: " + spellSlot3 
            + "\nSpellSlot4: " + spellSlot4 
            + "\nSpellSlot5: " + spellSlot5;
    }  
    

    public SpellData[] GetSpellArray()
    { return new SpellData[] { spellSlot1, spellSlot2, spellSlot3, spellSlot4, spellSlot5 }; }
}
