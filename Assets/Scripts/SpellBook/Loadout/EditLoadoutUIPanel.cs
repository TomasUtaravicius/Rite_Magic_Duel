using System.Collections.Generic;
using UnityEngine;

namespace Rite.SpellBook
{
    public class EditLoadoutUIPanel : MonoBehaviour
    {
        SBLoadout currentSpellLoadout;

        [SerializeField] GameObject spellButtonPrefab = null;

        [Header("Spell Panel")]
        [SerializeField] GameObject spellPanel = null;
        [SerializeField] List<SpellUIButton> spellButtons = null;


        [Header("Loadout Panel")]
        [SerializeField] UIStepper loadoutStepper = null;
        [SerializeField] Transform loadoutSpellSlotParent = null;
        [SerializeField] SpellUIButton[] loadoutSpellButtons = null;

        [SerializeField] ButtonTransitioner saveButton = null;
        [SerializeField] ButtonTransitioner resetButton = null;



        private void Awake()
        {
            loadoutSpellButtons = new SpellUIButton[SBLoadout.DEFAULT_LOADOUT_SPELL_COUNT];
            for (int i = 0; i < loadoutSpellButtons.Length; i++)
                loadoutSpellButtons[i] = Instantiate(spellButtonPrefab, loadoutSpellSlotParent).GetComponent<SpellUIButton>();


            SpellData[] spellArray = LoadoutManager.LoadAllSpellData();
            spellButtons = new List<SpellUIButton>(spellArray.Length);

            for (int i = 0; i < spellArray.Length; i++)
                if (spellArray[i].buildReady)
                {
                    spellButtons.Add(Instantiate(spellButtonPrefab, spellPanel.transform).GetComponent<SpellUIButton>());
                    spellButtons[spellButtons.Count - 1].SpellData = spellArray[i];
                    spellButtons[spellButtons.Count - 1].buttonNumber = spellButtons.Count - 1;
                }
        }

        private void OnEnable()
        {
            if (loadoutStepper)
            {
                loadoutStepper.OnValueChanged += HandleStepperValueChanged;
                GetLoadout(loadoutStepper.CurrentValue);
            }

            if (saveButton) saveButton.buttonClick.AddListener(SaveLoadout);
            if (resetButton) resetButton.buttonClick.AddListener(ResetLoadout);


            for (int i = 0; i < loadoutSpellButtons.Length; i++)
                loadoutSpellButtons[i].OnSpellSelected += HandleSpellSlotSelected;

            for (int i = 0; i < spellButtons.Count; i++)
                spellButtons[i].OnSpellSelected += HandleSpellSelected;
        }

        private void OnDisable()
        {
            if (loadoutStepper)
                loadoutStepper.OnValueChanged -= HandleStepperValueChanged;

            if (saveButton) saveButton.buttonClick.RemoveListener(SaveLoadout);
            if (resetButton) resetButton.buttonClick.RemoveListener(ResetLoadout);

            for (int i = 0; i < loadoutSpellButtons.Length; i++)
                loadoutSpellButtons[i].OnSpellSelected -= HandleSpellSlotSelected;

            for (int i = 0; i < spellButtons.Count; i++)
                spellButtons[i].OnSpellSelected -= HandleSpellSelected;
        }

        private void GetLoadout(int index)
        {
            currentSpellLoadout = LoadoutManager.LoadLoadout(index);

            //Update loadout spell buttons spell data and number in list
            for (int i = 0; i < currentSpellLoadout.SpellCount; i++)
            {
                loadoutSpellButtons[i].SpellData = currentSpellLoadout.spells[i];
                loadoutSpellButtons[i].buttonNumber = i;
            }


            //Disable used spells on the spell panel
            bool isUsed;
            for (int i = 0; i < spellButtons.Count; i++)
            {
                isUsed = false;

                for (int j = 0; j < loadoutSpellButtons.Length; j++)
                    if (spellButtons[i].SpellData == loadoutSpellButtons[j].SpellData)
                        isUsed = true;

                spellButtons[i].enabled = !isUsed;
            }
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
            for (int i = 0; i < loadoutSpellButtons.Length; i++)
            {
                if (loadoutSpellButtons[i].SpellData == null)
                {
                    loadoutSpellButtons[i].SpellData = spellButtons[buttonNo].SpellData;
                    spellButtons[buttonNo].enabled = false;
                    return;
                }
            }
        }


        private void HandleStepperValueChanged(int value)
        { GetLoadout(value); }

        private void SaveLoadout()
        {
            SBLoadout newLoadout = new SBLoadout();
            for (int i = 0; i < newLoadout.spells.Length; i++)
                newLoadout.spells[i] = loadoutSpellButtons[i].SpellData;

            LoadoutManager.SaveLoadout(newLoadout, loadoutStepper.CurrentValue);
            Debug.Log("Saved new loadout");
        }

        private void ResetLoadout()
        {
            GetLoadout(loadoutStepper.CurrentValue);
        }
    }
}