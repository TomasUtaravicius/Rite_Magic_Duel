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
        public void LoadoutManager_SaveLoadoutTest()
        {
            //Arrange
            LoadoutManager.CheckForValidLoadouts();
            SBLoadout spellLoadout = LoadoutManager.LoadLoadout(1);
            SBLoadout spellLoadout2 = spellLoadout;

            spellLoadout2.name += " delta";
            LoadoutManager.SaveLoadout(spellLoadout2, 1);
            spellLoadout = LoadoutManager.LoadLoadout(1);

            //Assert
            Assert.AreEqual(spellLoadout2, spellLoadout, spellLoadout2.name + " is not equal " + spellLoadout.name + "!");
        }

        

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LoadoutManager_LoadLoadoutTest()
        {
            //Arrange
            SBLoadout newSpellLoadout = new SBLoadout();
            newSpellLoadout.spells[0] = ScriptableObject.CreateInstance<SpellData>();
            newSpellLoadout.spells[0].spellName = "TestSpell";
            LoadoutManager.SaveLoadout(newSpellLoadout, 3);

            SBLoadout loadedLoadout = LoadoutManager.LoadLoadout(3);

            yield return new WaitForFixedUpdate();

            //Assert
            //Assert.AreEqual(newSpellLoadout, loadedLoadout, "The loaded spell was not the same as the one saved!");
        }
    }
}
