using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SBLoadout
{
    public int loadoutNumber;
    public string name;
    public List<SpellData> spells;

    public int SpellCount { get => 5; }

    public SBLoadout()
    {
        name = "SpellLoadout-New";
        spells = new List<SpellData>(5);
    }

    public SBLoadout(int number)
    {
        name = "SpellLoadout" + number;
        spells = new List<SpellData>(5);
    }

    public override string ToString()
    {
        string output = "Name: " + name;
        for (int i = 0; i < spells.Count; i++)
            output += "\nSpellSlot1: " + spells[i];

        return output;
    }    
}
