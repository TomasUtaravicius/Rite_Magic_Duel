using UnityEngine;

public class Projectile : Spell
{
    public override SpellType SpellType { get => SpellType.Projectile; }

    public float spellSpeed = 10;

    public Light light;


}