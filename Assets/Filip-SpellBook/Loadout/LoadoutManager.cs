
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public bool saveTick = false;

    [Range(1, 5)] public int loadoutNo = 1;
    [SerializeField] SpellLoadout currentlySelectedLoadout = null;

    public void SaveSelectedLoadout(int i)
    {
        Debug.Log("Selected Loadout " + i);
        PlayerPrefs.SetInt("SelectedLoadout", i);
    }

    public static void SaveLoadout(SpellLoadout loadout, int loadoutNumber)
    {
        Debug.Log("Saved spell loadout - " + loadout.name + " under name \"SpellLoadout-" + loadoutNumber + "\"");

        string bookJSON = JsonUtility.ToJson(loadout);
        PlayerPrefs.SetString("SpellLoadout-" + loadoutNumber, bookJSON);
    }

    public static SpellLoadout LoadSelectedLoadout()
    { return LoadLoadout(PlayerPrefs.GetInt("SelectedLoadout", 1)); }

    public static SpellLoadout LoadLoadout(int loadoutNumber)
    {
        string loadoutJSON = PlayerPrefs.GetString("SpellLoadout-" + loadoutNumber);
        SpellLoadout loadout = JsonUtility.FromJson<SpellLoadout>(loadoutJSON);
        if (loadout != null)
        {
            Debug.Log("Loaded spell loadout - " + loadout.name);
            return loadout;
        }
        else
        {
            Debug.LogError("Could not find SpellLoadout-" + loadoutNumber);
            return new SpellLoadout(loadoutNumber);
        }
    }


    private void OnValidate()
    {
        if (saveTick)
        {
            SaveLoadout(currentlySelectedLoadout, loadoutNo);
            SaveSelectedLoadout(loadoutNo);
            saveTick = false;
        }
    }
}
