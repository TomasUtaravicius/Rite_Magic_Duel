using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(SpellPool))]
public class SpellBook : MonoBehaviour
{
    private SpellPool spellPool;
    [SerializeField] bool usePool = true;
    [SerializeField] SBLoadout loadout = null;


    // Start is called before the first frame update
    void Start()
    {
        spellPool = GetComponent<SpellPool>();
        loadout = LoadoutManager.LoadSelectedLoadout();
        Debug.Log(loadout.ToString());

        //TestLoadout();
        if(usePool) PoolSpells();
    }

    private void TestLoadout()
    {
        SpellData[] spells = loadout.spells;

        for (int i = 0; i < spells.Length; i++)
            if (spells[i])
            {
                Debug.Log("Spell spawned");
                SpawnSpell(spells[i], transform.position, transform.rotation).FireSpell();
            }
            else
                Debug.LogWarning("Spell " + (i + 1) + " is null");
    }

    private void PoolSpells()
    {
        SpellData[] spelldata = loadout.spells;


        for (int i = 0; i < spelldata.Length; i++)
            if (spelldata[i])
            {
                GameObject[] spawnedSpells = new GameObject[5];
                for (int j = 0; j < 5; j++)
                    spawnedSpells[i] = SpawnSpell(spelldata[i], transform.position, transform.rotation).gameObject;

                spellPool.DestroyGroup(spawnedSpells);
                Debug.Log("Spell pooled");
            }
            else
                Debug.LogWarning("Spell " + (i + 1) + " is null");
    }

    private Spell SpawnSpell(SpellData spellData, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject spellInstance;
        spellInstance = PhotonNetwork.Instantiate(spellData.spellPrefab.name, spawnPosition, spawnRotation, 0);
        spellInstance.name = spellData.spellName;

        Spell spell = spellInstance.GetComponent<Spell>();
        if (spell)
        {
            Debug.Log("Spell found");

            spell.SetSpellAttributes(spellData.spellName, spellData.health, spellData.damage, spellData.lifetime, spellData.spellSpeed);
            spell.SetSpellVisuals(spellData.shouldTintSpell, spellData.spellTint);
        }


        return spell;
    }

    public SpellData GetSpellData(int gestureIdx)
    {
        //try getting spell data
        Debug.Log("Spell book - spawning spell at index " + gestureIdx);
        SpellData data = null;
        try { data = loadout.spells[gestureIdx]; }
        catch (System.ArgumentOutOfRangeException) { }

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
        SpellData spellData = GetSpellData(gestureIdx);

        if (spellData)
        {
            if (usePool)
                return spellPool.Instantiate(spellData.spellName, position, rotation);
            else
                return SpawnSpell(spellData, position, rotation).gameObject;
        }
        else
            return null;
    }
}
