using UnityEngine;

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
        SpellData[] spells = loadout.GetSpellArray();

        for (int i = 0; i < spells.Length; i++)
            if (spells[i])
            {
                Debug.Log("Spell spawned");
                SpawnSpell(spells[i], transform, false).FireSpell();
            }
            else
                Debug.LogWarning("Spell " + (i + 1) + " is null");
    }

    private void PoolSpells()
    {
        SpellData[] spells = loadout.GetSpellArray();


        for (int i = 0; i < spells.Length; i++)
            if (spells[i])
            {
                GameObject[] spawnedSpells = new GameObject[5];
                for (int j = 0; j < 5; j++)
                    spawnedSpells[i] = SpawnSpell(spells[i], transform, false).gameObject;

                spellPool.DestroyGroup(spawnedSpells);
                Debug.Log("Spell pooled");
            }
            else
                Debug.LogWarning("Spell " + (i + 1) + " is null");
    }

    

    private SB_Spell SpawnSpell(SpellData spellData, Transform spawnTransform, bool spawnFromPool)
    {
        GameObject spellInstance;
        if (spawnFromPool)
            spellInstance = spellPool.Instantiate(spellData.spellName, spawnTransform.position, spawnTransform.rotation);
        else
            spellInstance = Instantiate(spellData.spellPrefab, spawnTransform.position, spawnTransform.rotation, null);

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
}
