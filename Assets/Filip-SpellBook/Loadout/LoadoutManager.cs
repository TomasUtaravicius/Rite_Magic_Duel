
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    /* debug variables for component */
    public bool saveTick = false;
    [Range(1, 5)] public int loadoutNo = 1;
    [SerializeField] SBLoadout currentlySelectedLoadout = null;
    
    private void OnValidate()
    {
        if (saveTick)
        {
            SaveLoadout(currentlySelectedLoadout, loadoutNo);
            SetPreferedLoadout(loadoutNo);
            saveTick = false;
        }
    }





    public static void SetPreferedLoadout(int i)
    {
        Debug.Log("Selected Loadout " + i);
        PlayerPrefs.SetInt("SelectedLoadout", i);
    }

    public static SBLoadout LoadSelectedLoadout()
    { return LoadLoadout(PlayerPrefs.GetInt("SelectedLoadout", 1)); }



    public static void SaveLoadout(SBLoadout loadout, int loadoutNumber)
    {
        loadout.loadoutNumber = loadoutNumber;

        Debug.Log("Saved spell loadout - " + loadout.name + " under name \"SpellLoadout-" + loadoutNumber + "\"");

        string bookJSON = JsonUtility.ToJson(loadout);
        PlayerPrefs.SetString("SpellLoadout-" + loadoutNumber, bookJSON);
    }

    public static SBLoadout LoadLoadout(int loadoutNumber)
    {
        string loadoutJSON = PlayerPrefs.GetString("SpellLoadout-" + loadoutNumber);
        SBLoadout loadout = JsonUtility.FromJson<SBLoadout>(loadoutJSON);
        if (loadout != null)
        {
            Debug.Log("Loaded spell loadout - " + loadout.name);
            return loadout;
        }
        else
        {
            Debug.LogError("Could not find SpellLoadout-" + loadoutNumber);
            return new SBLoadout(loadoutNumber);
        }
    }


    public static SpellData[] LoadAllSpellData()
    {
        return Resources.LoadAll<SpellData>("");
    }
}
