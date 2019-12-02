using UnityEngine;
using UnityEngine.UI;

public class SpellUIButton : ButtonTransitioner
{
    public delegate void SpellButtonCallback(int number);
    public event SpellButtonCallback OnSpellSelected;

    [HideInInspector] public int buttonNumber = -1;

    [Space(10)]
    [SerializeField] private SpellData spellData;
    [SerializeField] private Image spellImage;

    public void SetSpellData(SpellData spellData)
    {
        this.spellData = spellData;
        spellImage.sprite = spellData?.spellSprite;
    }

    protected override void HandleOnButtonClick()
    { OnSpellSelected?.Invoke(buttonNumber); }
}
