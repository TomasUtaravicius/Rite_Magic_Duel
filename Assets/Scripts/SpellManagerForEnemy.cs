using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManagerForEnemy : MonoBehaviour
{
    public GameObject spellCastingPoint;
    public GameObject ExpelliarmusPrefab;
    public GameObject StupefyPrefab;
    [HideInInspector]
    public Information Expelliarmus;
    [HideInInspector]
    public Information Stupefy;
    [HideInInspector]
    public bool castExpelliarmus;
    [HideInInspector]
    public bool castStupefy;

    private void Start()
    {
        Expelliarmus = ExpelliarmusPrefab.GetComponent<Information>();
        Stupefy = StupefyPrefab.GetComponent<Information>();
        castExpelliarmus = false;
        castStupefy = false;
    }
    private void Update()
    {
        if (castExpelliarmus == true)
        {
            CastExpelliarmus();
        }
        if (castStupefy == true)
        {
            CastStupefy();
        }

    }
    public void CastExpelliarmus()
    {
        castExpelliarmus = false;
        //Instantiate the spell
        var spell = (GameObject)Instantiate(
            ExpelliarmusPrefab,
            spellCastingPoint.transform.position,
            spellCastingPoint.transform.rotation);
        //Fire it
        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * Expelliarmus.speed;
        // Destroy
        Destroy(spell, 4.0f);
    }
    public void CastStupefy()
    {
        castStupefy = false;
        //Instantiate the spell
        var spell = (GameObject)Instantiate(
            StupefyPrefab,
            spellCastingPoint.transform.position,
            spellCastingPoint.transform.rotation);
        //Fire it
        spell.GetComponent<Rigidbody>().velocity = spell.transform.forward * Stupefy.speed;
        // Destroy
        Destroy(spell, 4.0f);
    }
}
