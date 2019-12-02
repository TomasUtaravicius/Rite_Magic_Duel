using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SBLoadout
{
    public int loadoutNumber;
    public string name;
    public List<SpellData> spellSlots;

    public int SpellCount { get => 5; }

    public SBLoadout()
    {
        name = "SpellLoadout-New";
        spellSlots = new List<SpellData>(5);
    }

    public SBLoadout(int number)
    {
        name = "SpellLoadout" + number;
        spellSlots = new List<SpellData>(5);
    }

    public override string ToString()
    {
        string output = "Name: " + name;
        for (int i = 0; i < spellSlots.Count; i++)
            output += "\nSpellSlot1: " + spellSlots[i];

        return output;
    }    
}
