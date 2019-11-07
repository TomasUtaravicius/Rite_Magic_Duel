using UnityEngine;

public class Projectile : SB_Spell
{
    public override SpellType SpellType { get => SpellType.Projectile; }

    public float spellSpeed = 10;
}