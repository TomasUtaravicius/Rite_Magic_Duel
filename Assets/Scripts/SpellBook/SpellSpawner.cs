using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    public bool testFire = false;

    public SpellData spellData;

    public void Fire()
    {
        if (spellData)
        {
            Debug.Log("Fire!");

            GameObject spellInstance = Instantiate(spellData.spellPrefab, transform.position, transform.rotation, null);

            Spell spell = spellInstance.GetComponent<Spell>();
            if (spell)
            {
                Debug.Log("Spell found");

                spell.SetSpellAttributes(spellData.spellName, spellData.requiresHeldCast, spellData.canChargeOnCast ,spellData.health, spellData.damage, spellData.lifetime, spellData.spellSpeed);
                spell.SetSpellVisuals(spellData.shouldTintSpell, spellData.spellTint);
            }
        }
        else
        {
            Debug.LogWarning("No spell data found!");
        }
    }

    private void OnValidate()
    {
        if (testFire)
        {
            Fire();
            testFire = false;
        }
    }
}