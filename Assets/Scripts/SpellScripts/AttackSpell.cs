using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpell : MonoBehaviour,ISpell
{
    public new string name;
    public float damage;
    public float feedback;
    public float speed;
    public bool hasHit;
    public float manaCost;
    public GameObject spellObject;
    
}
