using System;
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
    [SerializeField] private Text spellNameText;

    public SpellData SpellData 
    { 
        get => spellData;
        set
        {
            spellData = value;

            if (value)
            {
                spellImage.sprite = spellData.spellSprite;
                spellNameText.text = spellData.spellName;
            }
            else
            {
                spellImage.sprite = null;
                spellImage.color = new Color(1f, 0.4f, 0.4f, 1f);
                spellNameText.text = "None";
            }

        }
    }

    protected override void HandleOnButtonClick()
    { OnSpellSelected?.Invoke(buttonNumber); }


    public void EnableButton()
    {
        throw new NotImplementedException();
    }
    public void DisableButton()
    {
        throw new NotImplementedException();
    }

    
}
