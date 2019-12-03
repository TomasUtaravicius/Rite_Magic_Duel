using System;
using System.Collections.Generic;
using UnityEngine;

public class EditLoadoutUIPanel : MonoBehaviour
{
    SBLoadout currentSpellLoadout;

    [SerializeField] GameObject spellButtonPrefab = null;

    [Header("Spell Panel")]
    [SerializeField] GameObject spellPanel = null;
    [SerializeField] List<SpellUIButton> spellButtons = null;


    [Header("Loadout Panel")]
    [SerializeField] GameObject loadoutPanel = null;
    [SerializeField] UIStepper loadoutStepper = null;
    [SerializeField] List<SpellUIButton> loadoutSpellButtons = null;

    [SerializeField] ButtonTransitioner saveButton = null;
    [SerializeField] ButtonTransitioner resetButton = null;

    private void Start()
    {
        if(spellButtonPrefab)
        {
            //Spell panel
            SpellData[] sd = LoadoutManager.LoadAllSpellData();
            spellButtons = new List<SpellUIButton>(sd.Length);

            for (int i = 0; i < sd.Length; i++)
            {
                if (sd[i].buildReady)
                {
                    spellButtons.Add(Instantiate(spellButtonPrefab, spellPanel.transform).GetComponent<SpellUIButton>());
                    spellButtons[spellButtons.Count-1].SpellData = sd[i];
                    spellButtons[spellButtons.Count-1].buttonNumber = spellButtons.Count-1;
                    spellButtons[spellButtons.Count-1].OnSpellSelected += HandleSpellSelected;
                }
            }

            //Loadout panel
            GetLoadout(1);
        }
    }

    private void OnEnable()
    {
        if(loadoutStepper)
        {
            loadoutStepper.OnValueChanged += HandleStepperValueChanged;
            GetLoadout(loadoutStepper.CurrentValue);
        }

        if (saveButton) saveButton.buttonClick.AddListener(SaveLoadout);
        if (resetButton) resetButton.buttonClick.AddListener(ResetLoadout);
    }

    private void OnDisable()
    {
        if (loadoutStepper)
            loadoutStepper.OnValueChanged -= HandleStepperValueChanged;

        if (saveButton) saveButton.buttonClick.RemoveListener(SaveLoadout);
        if (resetButton) resetButton.buttonClick.RemoveListener(ResetLoadout);
    }

    private void GetLoadout(int index)
    {
        SBLoadout currentSpellLoadout = LoadoutManager.LoadLoadout(index);

        int newLoadoutSpellCount = 0;
        if(currentSpellLoadout != null) newLoadoutSpellCount = currentSpellLoadout.SpellCount;

        //Update loadout spell buttons count
        if (loadoutSpellButtons.Count > newLoadoutSpellCount)
        {
            SpellUIButton temp;
            for (int i = loadoutSpellButtons.Count - 1; i >= 0; i--)
            {
                temp = loadoutSpellButtons[i];
                temp.OnSpellSelected -= HandleSpellSelected;
                loadoutSpellButtons.RemoveAt(i);
                Destroy(temp.gameObject);
            }
        }
        else if (loadoutSpellButtons.Count < newLoadoutSpellCount)
        {
            for (int i = loadoutSpellButtons.Count; i < newLoadoutSpellCount; i++)
                loadoutSpellButtons.Add(Instantiate(spellButtonPrefab, spellPanel.transform).GetComponent<SpellUIButton>());
        }

        if(newLoadoutSpellCount > 0)
        {
            //Update loadout spell buttons spell data and number in list
            for (int i = 0; i < newLoadoutSpellCount; i++)
            {
                loadoutSpellButtons[i].SpellData = currentSpellLoadout.spells[i];
                loadoutSpellButtons[i].buttonNumber = i;
            }


            //Disable used spells on the spell panel
            bool isUsed;
            for (int i = 0; i < spellButtons.Count; i++)
            {
                isUsed = false;

                for (int j = 0; i < loadoutSpellButtons.Count; j++)
                    if (spellButtons[i].SpellData == loadoutSpellButtons[j].SpellData)
                        isUsed = true;

                spellButtons[i].enabled = !isUsed;
            }
        }
        else //enable all buttons
            for (int i = 0; i < spellButtons.Count; i++)
                spellButtons[i].enabled = true;
    }

    /// <summary> Enable selecting the removed spell and clear the spell slot at buttonNo index </summary>
    /// <param name="buttonNo"></param>
    private void HandleSpellSlotSelected(int buttonNo)
    {
        for (int i = 0; i < spellButtons.Count; i++)
            if (spellButtons[i].SpellData == loadoutSpellButtons[buttonNo].SpellData)
            { 
                spellButtons[i].enabled = true; 
                break; 
            }

        loadoutSpellButtons[buttonNo].SpellData = null;
    }

    private void HandleSpellSelected(int buttonNo)
    {
        //TODO add spell to first empty spell slot in loadout
        for (int i = 0; i < loadoutSpellButtons.Count; i++)
        {
            if(loadoutSpellButtons[i].SpellData == null)
            {
                loadoutSpellButtons[i].SpellData = spellButtons[buttonNo].SpellData;
                spellButtons[buttonNo].enabled = false;
            }
        }
    }


    private void HandleStepperValueChanged(int value)
    { GetLoadout(value); }

    private void SaveLoadout()
    {
        SBLoadout newLoadout = new SBLoadout();
        for (int i = 0; i < loadoutSpellButtons.Count; i++)
            newLoadout.spells.Add(loadoutSpellButtons[i].SpellData);

        LoadoutManager.SaveLoadout(newLoadout, loadoutStepper.CurrentValue);
        Debug.Log("Saved new loadout");
    }

    private void ResetLoadout()
    {
        GetLoadout(loadoutStepper.CurrentValue);
    }
}
