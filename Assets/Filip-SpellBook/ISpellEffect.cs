
 public delegate void SpellEffectDelegate();

public interface ISpellEffect
{
    event SpellEffectDelegate OnSpellHidden;

    /// <summary> Return the length of spell effect in seconds </summary>
    float Lenght { get; }
    /// <summary> Color of the Spell Effect </summary>
    float EffectColor { get; }
    /// <summary> Set Color of Spell Effect</summary>
    void SetEffectColor();

    /// <summary> Make the spell effect visible. </summary>
    void Show(bool shouldReset);
    /// <summary> Reset the spell effect. </summary>
    void Reset();
    /// <summary> Hides the spell effect. Sets it ready for pooling. Resets the spell effect. </summary>
    void Hide();
}
