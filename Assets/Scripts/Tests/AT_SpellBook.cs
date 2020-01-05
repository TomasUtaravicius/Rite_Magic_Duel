using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    //Arrange

    //Act

    //Assert
    public class AT_SpellBook
    {
        private SpellBook InstantiateSpellBook()
        {
            GameObject obj = new GameObject();
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;

            return obj.AddComponent<SpellBook>();            
        }



        [UnityTest]
        public IEnumerator SpellBook_SpawnSpellBookTest()
        {
            //Arrange
            GameObject obj = new GameObject();
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;

            SBLoadout testLoadout = new SBLoadout();
            testLoadout.name = "TestLoadout";
            testLoadout.loadoutNumber = 4;
            LoadoutManager.SaveLoadout(testLoadout, 4);

            LoadoutManager.SelectedLoadoutNum = 4; 

            //Act
            SpellBook spellBook = obj.AddComponent<SpellBook>();
            yield return new WaitForFixedUpdate();

            //Assert
            Assert.AreEqual(testLoadout, spellBook.Loadout, "The spell book component did not load the correct loadout from the loadout manager");


            Object.Destroy(obj);
            LoadoutManager.ClearAllData();
        }

        [UnityTest]
        public IEnumerator SpellBook_SpawnSpellTest()
        {
            //Arrange
            LoadoutManager.ClearAllData();
            //Audio listener added to scene suppress audio source warning logs
            GameObject temp = new GameObject().AddComponent<AudioListener>().gameObject;
            SpellData[] spells = LoadoutManager.LoadAllSpellData();

            yield return new WaitForFixedUpdate();

            //Act
            if(spells[0])
            {
                Spell spawnedSpell = SpellBook.SpawnSpell(spells[0], Vector3.zero, Quaternion.identity, true);
                Debug.Log(spells[0].name + " spell spawned!");

                yield return new WaitForSeconds(2f);

                //Assert
                Assert.IsNotNull(spawnedSpell, spells[0].name + " Did not spawn");

                Object.Destroy(spawnedSpell.gameObject);
                Object.Destroy(temp);
                LoadoutManager.ClearAllData();
            }
            else
                Assert.IsNotNull(spells[0], "A recieved spell from the loadout manager was null");
        }


        [UnityTest]
        public IEnumerator SpellBook_SpawnSpellTest_AllSpells()
        {
            //Arrange
            LoadoutManager.ClearAllData();
            //Audio listener added to scene suppress audio source warning logs
            GameObject temp = new GameObject().AddComponent<AudioListener>().gameObject;
            SpellData[] spells = LoadoutManager.LoadAllSpellData();

            yield return new WaitForFixedUpdate();

            //Act
            //Test for any spells with spellSpeed
            for (int i = 0; i < spells.Length; i++)
                if (spells[i])
                {
                    Debug.Log(spells[i].name + " spell spawned!");
                    Spell spawnedSpell = SpellBook.SpawnSpell(spells[i], Vector3.zero, Quaternion.identity, true);

                    yield return new WaitForSeconds(2f);

                    //Assert
                    Assert.IsNotNull(spawnedSpell, spells[i].name + " Did not spawn");

                    Object.Destroy(spawnedSpell.gameObject);
                }
                else
                    Assert.IsNotNull(spells[i], "The spell data is null");

            Object.Destroy(temp);
            LoadoutManager.ClearAllData();
        }

        [UnityTest]
        public IEnumerator SpellBook_SpawnSpellTest_AllProjectiles()
        {
            //Arrange
            LoadoutManager.ClearAllData();
            //Audio listener added to scene suppress audio source warning logs
            GameObject temp = new GameObject().AddComponent<AudioListener>().gameObject;
            SpellData[] spells = LoadoutManager.LoadAllSpellData();

            yield return new WaitForFixedUpdate();

            //Act
            //Test for any spells with spellSpeed
            for (int i = 0; i < spells.Length; i++)
                if (spells[i])
                {
                    if (spells[i].spellSpeed != 0)
                    {
                        Debug.Log(spells[i].name + " spell spawned!");
                        Spell spawnedSpell = SpellBook.SpawnSpell(spells[i], Vector3.zero, Quaternion.identity, true);

                        yield return new WaitForSeconds(2f);

                        //Assert
                        Assert.AreNotEqual(Vector3.zero, spawnedSpell.GetComponentInChildren<RFX1_TransformMotion>().transform.position, spells[i].name + " projectile spell did not move!");

                        Object.Destroy(spawnedSpell.gameObject);
                    }
                }
                else
                    Assert.IsNotNull(spells[i], "The spell data is null");

            Object.Destroy(temp);
            LoadoutManager.ClearAllData();
        }

        [UnityTest]
        public IEnumerator SpellBook_GetSpellDataTest()
        {
            //Arrange
            LoadoutManager.ClearAllData();
            SBLoadout loadout = new SBLoadout();
            SpellData[] allSDs = LoadoutManager.LoadAllSpellData();

            SpellData[] sd = new SpellData[3];
            for (int i = 0; i < sd.Length; i++)
                sd[i] = allSDs[i + 2];

            //Act
            loadout.spells = sd;
            LoadoutManager.SaveLoadout(loadout, 2);
            LoadoutManager.SelectedLoadoutNum = 2;

            SpellBook sb = InstantiateSpellBook();
            yield return null;

            //Assert
            Assert.AreEqual(loadout.spells[2], sb.GetSpellData(2), "The GetSpellData method does not return the correct spellData that was stored");

            Object.Destroy(sb);
            LoadoutManager.ClearAllData();
        }

        [UnityTest]
        public IEnumerator SpellBook_GetSpellDataTest_UpperOutOfBounds()
        {
            //Arrange
            LoadoutManager.ClearAllData();
            SBLoadout loadout = new SBLoadout();
            SpellData[] allSDs = LoadoutManager.LoadAllSpellData();

            SpellData[] sd = new SpellData[SBLoadout.DEFAULT_LOADOUT_SPELL_COUNT];
            for (int i = 0; i < sd.Length; i++)
                sd[i] = allSDs[i];

            //Act
            loadout.spells = sd;
            LoadoutManager.SaveLoadout(loadout, 2);
            LoadoutManager.SelectedLoadoutNum = 2;

            SpellBook sb = InstantiateSpellBook();
            yield return null;

            //Assert
            Assert.IsNull(sb.GetSpellData(sb.Loadout.spells.Length));

            Object.Destroy(sb);
            LoadoutManager.ClearAllData();
        }

        [UnityTest]
        public IEnumerator SpellBook_GetSpellDataTest_LowerOutOfBounds()
        {
            //Arrange
            LoadoutManager.ClearAllData();
            SBLoadout loadout = new SBLoadout();
            SpellData[] allSDs = LoadoutManager.LoadAllSpellData();

            SpellData[] sd = new SpellData[3];
            for (int i = 0; i < sd.Length; i++)
                sd[i] = allSDs[i + 2];

            //Act
            loadout.spells = sd;
            LoadoutManager.SaveLoadout(loadout, 2);
            LoadoutManager.SelectedLoadoutNum = 2;

            SpellBook sb = InstantiateSpellBook();
            yield return null;

            //Assert
            Assert.IsNull(sb.GetSpellData(-1));

            Object.Destroy(sb);
            LoadoutManager.ClearAllData();
        }
    }
}
