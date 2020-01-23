using Photon.Pun;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(SpellPool))]
public class SpellBook : MonoBehaviour
{
    [SerializeField] public static bool USE_POOLING = false;

    private SpellPool spellPool;
    [SerializeField] SBLoadout loadout = null;

    public SBLoadout Loadout { get => loadout;}


    // Start is called before the first frame updatess
    void Start()
    {
        spellPool = GetComponent<SpellPool>();
        loadout = LoadoutManager.LoadSelectedLoadout();
        Debug.Log(loadout.ToString());
        
        if (USE_POOLING) PoolSpells();
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

    public static Spell SpawnSpell(SpellData spellData, Vector3 spawnPosition, Quaternion spawnRotation, bool isOffline = false)
    {
        GameObject spellInstance;
        //spawn spell offline or online through PhotonNetwork
        spellInstance = isOffline ? Instantiate(spellData.spellPrefab, spawnPosition, spawnRotation)
                                  : PhotonNetwork.Instantiate(spellData.spellPrefab.name, spawnPosition, spawnRotation, 0);

        spellInstance.name = spellData.spellName;

        Spell spell = spellInstance.GetComponent<Spell>();
        if (spell)
        {
            Debug.Log("Spell found");

            spell.SetSpellAttributes(spellData.spellName, spellData.requiresHeldCast, spellData.canChargeOnCast, spellData.health, spellData.damage, spellData.lifeTime, spellData.spellSpeed);
            spell.SetSpellVisuals(spellData.shouldTintSpell, spellData.spellTint);
        }


        return spell;
    }

    public SpellData GetSpellData(int gestureIdx)
    {
        //try getting spell data
        Debug.Log("Spell book - spawning spell at index " + gestureIdx);
        if (loadout == null 
            || loadout.spells.Length <= gestureIdx 
            || 0 > gestureIdx
            || loadout.spells[gestureIdx].IsDefaultSpellData())
            return null;
        else
            return loadout.spells[gestureIdx];
    }

    public bool CanCastSpell(int gestureIdx, float manaAmount)
    {
        SpellData data = GetSpellData(gestureIdx);

        if (data)
            return manaAmount > data.manaCost;
        else
            return false;
    }

    public GameObject ConstructSpell(int gestureIdx, Vector3 position, Quaternion rotation)
    {
        SpellData spellData = GetSpellData(gestureIdx);

        if (spellData)
        {
            if (USE_POOLING)
                return spellPool.Instantiate(spellData.spellName, position, rotation);
            else
                return SpawnSpell(spellData, position, rotation).gameObject;
        }
        else
            return null;
    }
}