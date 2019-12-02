using System.Collections.Generic;
using UnityEngine;

public class EditLoadoutUIPanel : MonoBehaviour
{
    int currentLoadoutNo = 1;
    SBLoadout currentSpellLoadout;

    [Header("Spell Panel")]
    [SerializeField] GameObject spellPanel = null;

    [SerializeField] GameObject spellButtonPrefab = null;
    [SerializeField] List<SpellUIButton> spellButtons = null;


    [Header("Loadout Panel")]
    [SerializeField] GameObject loadoutPanel = null;
    [SerializeField] UIStepper loadoutStepper = null;

    private void Start()
    {
        if(spellButtonPrefab)
        {
            spellButtons = new List<SpellUIButton>();
            SpellData[] sd = LoadoutManager.LoadAllSpellData();

            for (int i = 0; i < sd.Length; i++)
            {
                spellButtons.Add(Instantiate(spellButtonPrefab, spellPanel.transform).GetComponent<SpellUIButton>());
                spellButtons[i].SetSpellData(sd[i]);
            }
        }
    }

    private void OnEnable()
    {
        if(loadoutStepper)
            GetLoadout(loadoutStepper.CurrentValue);
    }


    private void GetLoadout(int i)
    {
        SBLoadout currentSpellLoadout = LoadoutManager.LoadLoadout(i);
        
        //TODO update loadoutPanelUI
        //TODO enable and disable spell buttons based on the loadout spell selection
    }

    private void SaveLoadout()
    {

    }

    private void ResetLoadout()
    {

    }
}
