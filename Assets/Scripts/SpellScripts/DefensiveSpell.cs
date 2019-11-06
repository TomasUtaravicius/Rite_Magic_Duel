using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveSpell : MonoBehaviour,ISpell,IDamagable
{
    public string name;
    public float health;
    public float manaCost;
    public GameObject spellObject;

    public void GetHit(float damage)
    {
        health -= damage;
    }
}
