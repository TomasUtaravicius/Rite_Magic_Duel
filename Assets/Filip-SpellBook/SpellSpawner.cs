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