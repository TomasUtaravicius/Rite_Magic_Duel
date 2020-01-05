using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    //Arrange

    //Act

    //Assert
    public class AT_LoadoutManager
    {
        [UnityTest]
        public IEnumerator LoadoutManager_SaveLoadoutTest()
        {
            //Arrange
            LoadoutManager.ClearAllData();

            SBLoadout spellLoadout = new SBLoadout();
            spellLoadout.name = "TestLoadout";
            LoadoutManager.SaveLoadout(spellLoadout, 3);

            //Act
            SBLoadout spellLoadout2 = LoadoutManager.LoadLoadout(3);
            yield return null;

            //Assert
            Assert.AreEqual(spellLoadout2, spellLoadout, spellLoadout2.name + " loadout is not equal to " + spellLoadout.name + " loadout!");

            LoadoutManager.ClearAllData();
        }
        
        [UnityTest]
        public IEnumerator LoadoutManager_LoadLoadoutTest()
        {
            //Arrange
            LoadoutManager.ClearAllData();

            SBLoadout newSpellLoadout = new SBLoadout();
            newSpellLoadout.spells[0] = ScriptableObject.CreateInstance<SpellData>();
            newSpellLoadout.spells[0].spellName = "TestSpell";
            LoadoutManager.SaveLoadout(newSpellLoadout, 3);

            //Act
            SBLoadout loadedLoadout = LoadoutManager.LoadLoadout(3);
            yield return null;

            //Assert
            Assert.AreEqual(newSpellLoadout, loadedLoadout, "The loaded spell was not the same as the one saved!");

            LoadoutManager.ClearAllData();
        }

        [UnityTest]
        public IEnumerator LoadoutManager_LoadSelectedLoadoutTest()
        {
            //Arrange
            LoadoutManager.ClearAllData();

            SBLoadout newSpellLoadout = new SBLoadout();
            newSpellLoadout.spells[0] = ScriptableObject.CreateInstance<SpellData>();
            newSpellLoadout.spells[0].spellName = "TestSpell";
            LoadoutManager.SaveLoadout(newSpellLoadout, 3);
            LoadoutManager.SelectedLoadoutNum = 3;

            //Act
            SBLoadout loadedLoadout = LoadoutManager.LoadLoadout(3);
            yield return null;

            //Assert
            Assert.AreEqual(newSpellLoadout, loadedLoadout, "The loaded spell was not the same as the one saved!");

            LoadoutManager.ClearAllData();
        }


        [UnityTest]
        public IEnumerator LoadoutManager_LoadAllSpellDataTest()
        {
            //Arrange
            LoadoutManager.ClearAllData();

            SpellData[] spelldata_manager = LoadoutManager.LoadAllSpellData();
            Debug.Log("Spell data from the Loadout Manager has " + spelldata_manager.Length + " elements");
            SpellData[] spelldata_resources = Resources.LoadAll<SpellData>("SpellData");
            Debug.Log("Spell data from the Resources class has " + spelldata_resources.Length + " elements");

            //Act
            yield return null;

            //Assert
            //lenght of arrays are the same.
            Assert.AreEqual(spelldata_manager.Length, spelldata_resources.Length 
                            ,"The loaded spelldata from the Loadout Manager and the Resources class are not the same lenght!");

            for (int i = 0; i < spelldata_manager.Length; i++)
                Assert.IsNotNull(spelldata_manager[i], "The loaded spell data array had a null element");

            //spell data at index i in each array are the same.
            for (int i = 0; i < Mathf.Min(spelldata_manager.Length, spelldata_resources.Length); i++)
                Assert.AreEqual(spelldata_resources[i], spelldata_manager[i], "The " + (i + 1)
                                    + " spell data from the Loadout Manager and the Resources class do not match");
        }
    }
}
