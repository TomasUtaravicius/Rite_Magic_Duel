using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    private SpellPool spellPool;
    [SerializeField] List<SpellData> spellSlots;


    // Start is called before the first frame update
    void Start()
    {
        List<string> book = LoadoutManager.LoadSelectedBook();

        for (int i = 0; i < book.Count; i++)
        {
            Debug.Log(book[i]);

            if(i > 1)
            {
                spellSlots.Add(Resources.Load<SpellData>(book[i]));
            }
        }

        for (int i = 0; i < spellSlots.Count; i++)
        {
            Debug.Log("Spell spawned");
            GameObject spellInstance = Instantiate(spellSlots[i].spellPrefab, transform.position, transform.rotation, null);


            SB_Spell spell = spellInstance.GetComponent<SB_Spell>();
            if (spell)
            {
                Debug.Log("Spell found");

                spell.SetSpellAttributes(spellSlots[i].spellName, spellSlots[i].hitEffectPrefab.name, spellSlots[i].health, spellSlots[i].damage, spellSlots[i].lifetime);
                spell.SetSpellVisuals(spellSlots[i].spellSound, spellSlots[i].shouldTintSpell, spellSlots[i].spellTint);

                switch (spell.SpellType)
                {
                    case SpellType.None:
                        break;

                    case SpellType.Projectile:
                        ((Projectile)spell).spellSpeed = spellSlots[i].spellSpeed;
                        break;

                    case SpellType.Shield:
                        break;
                }


                spell.FireSpell();
            }

        }
    }
}
