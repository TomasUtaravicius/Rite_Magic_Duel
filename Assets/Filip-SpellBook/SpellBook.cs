using System;
using System.Collections.Generic;
using UnityEngine;
using static GestureController;

[RequireComponent(typeof(SpellPool))]
public class SpellBook : MonoBehaviour
{
    private SpellPool spellPool;
    [SerializeField] SpellLoadout loadout = null;


    // Start is called before the first frame update
    void Start()
    {
        spellPool = GetComponent<SpellPool>();
        loadout = LoadoutManager.LoadSelectedLoadout();
        Debug.Log(loadout.ToString());

        //TestLoadout();
        PoolSpells();
    }

    private void TestLoadout()
    {
        List<SpellData> spells = loadout.spellSlots;

        for (int i = 0; i < spells.Count; i++)
            if (spells[i])
            {
                Debug.Log("Spell spawned");
                SpawnSpell(spells[i], transform).FireSpell();
            }
            else
                Debug.LogWarning("Spell " + (i + 1) + " is null");
    }

    private void PoolSpells()
    {
        List<SpellData> spells = loadout.spellSlots;


        for (int i = 0; i < spells.Count; i++)
            if (spells[i])
            {
                GameObject[] spawnedSpells = new GameObject[5];
                for (int j = 0; j < 5; j++)
                    spawnedSpells[i] = SpawnSpell(spells[i], transform).gameObject;

                spellPool.DestroyGroup(spawnedSpells);
                Debug.Log("Spell pooled");
            }
            else
                Debug.LogWarning("Spell " + (i + 1) + " is null");
    }

    private SB_Spell SpawnSpell(SpellData spellData, Transform spawnTransform)
    {
        GameObject spellInstance;
        spellInstance = Instantiate(spellData.spellPrefab, spawnTransform.position, spawnTransform.rotation, null);
        spellInstance.name = spellData.spellName;

        SB_Spell spell = spellInstance.GetComponent<SB_Spell>();
        if (spell)
        {
            Debug.Log("Spell found");

            spell.SetSpellAttributes(spellData.spellName, spellData.hitEffectPrefab.name, spellData.health, spellData.damage, spellData.lifetime);
            spell.SetSpellVisuals(spellData.shouldTintSpell, spellData.spellTint);

            switch (spell.SpellType)
            {
                case SpellType.None:
                    break;

                case SpellType.Projectile:
                    ((Projectile)spell).spellSpeed = spellData.spellSpeed;
                    break;

                case SpellType.Shield:
                    break;
            }
        }


        return spell;
    }

    public SpellData GetSpellData(int gestureIdx)
    {
        //try getting spell data
        SpellData data = null;
        try { data = loadout.spellSlots[gestureIdx]; }
        catch (System.IndexOutOfRangeException) { }

        return data;
    }

    public bool CanCastSpell(int gestureIdx, float manaAmount)
    {
        SpellData data = GetSpellData(gestureIdx);

        if (data)
            return manaAmount > data.manaCost;
        else
            return false;
    }

    public GameObject CastSpell(int gestureIdx, Vector3 position, Quaternion rotation)
    {
        SpellData data = GetSpellData(gestureIdx);

        if (data)
            return spellPool.Instantiate(data.name, position, rotation);
        else
            return null;
    }
}
