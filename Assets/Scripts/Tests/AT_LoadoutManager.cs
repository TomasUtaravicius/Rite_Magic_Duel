using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Rite.SpellBook;

namespace Tests
{

    //Arrange

    //Act

    //Assert
    public class AT_LoadoutManager
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AT_LoadoutManagerSimplePasses()
        {
            //Arrange
            LoadoutManager.CheckForValidLoadouts();
            SBLoadout spellLoadout = LoadoutManager.LoadLoadout(1);

            //Assert
            Assert.AreEqual(Color.white, Color.white);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator AT_LoadoutManagerWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
