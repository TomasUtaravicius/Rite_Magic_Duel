using System;

namespace Rite.SpellBook
{
    [Serializable]
    public class SBLoadout
    {
        public static readonly int DEFAULT_LOADOUT_SPELL_COUNT = 5;

        public int loadoutNumber;
        public string name;
        public SpellData[] spells;

        public int SpellCount { get => spells.Length; }

        public SBLoadout()
        {
            name = "SpellLoadout-New";
            spells = new SpellData[DEFAULT_LOADOUT_SPELL_COUNT];
        }

        public SBLoadout(int number)
        {
            name = "SpellLoadout" + number;
            spells = new SpellData[DEFAULT_LOADOUT_SPELL_COUNT];

            /*
                spells = new SpellData[DEFAULT_LOADOUT_SPELL_COUNT];
                spells[0] = ScriptableObject.CreateInstance<SpellData>();
                for (int i = 0; i < spells.Length; i++)
                spells[i] = ScriptableObject.Instantiate<SpellData>(spells[0]);
            */
        }

        /// <summary> As scriptable objects are not allowed to be instantiated in the constructor. They have to be added post construction in runtime </summary>
        public void AddSpells(SpellData[] spells)
        {
            this.spells = spells;
        }

        public override string ToString()
        {
            string output = "Name: " + name;
            for (int i = 0; i < spells.Length; i++)
                output += "\nSpellSlot: " + spells[i];

            return output;
        }
    }
}


