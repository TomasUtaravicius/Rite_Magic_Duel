using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AT_LoadoutManager
    {
        // A Test behaves as an ordinary method
        [Test]
        public void AT_LoadoutManagerSimplePasses()
        {
            // Use the Assert class to test conditions

            Debug.Log("Penis!");
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
