using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    /* debug variables for component */
    public bool saveTick = false;
    [Range(1, 5)] public int loadoutNo = 1;
    [SerializeField] SBLoadout currentlySelectedLoadout = null;

    private void Awake()
    { CheckForValidLoadouts(); }


    private void OnValidate()
    {
        if (saveTick)
        {
            SaveLoadout(currentlySelectedLoadout, loadoutNo);
            SetPreferedLoadout(loadoutNo);
            saveTick = false;
        }
    }


    public static void CheckForValidLoadouts()
    {
        //Try loading each loadout. If it is not valid, it will be created, filled out or resized by the LoadLoadout function
        for (int i = 0; i < 5; i++)
            LoadLoadout(i + 1);
    }


    public static void SetPreferedLoadout(int i)
    {
        DebugLog(LogState.Log, "Selected Loadout " + i);
        PlayerPrefs.SetInt("SelectedLoadout", i);
    }

    public static SBLoadout LoadSelectedLoadout()
    { return LoadLoadout(PlayerPrefs.GetInt("SelectedLoadout", 1)); }



    public static void SaveLoadout(SBLoadout loadout, int loadoutNumber)
    {
        loadout.loadoutNumber = loadoutNumber;

        DebugLog(LogState.Log, "Saved spell loadout - " + loadout.name + " under name \"SpellLoadout-" + loadoutNumber + "\"");

        string bookJSON = JsonUtility.ToJson(loadout);
        PlayerPrefs.SetString("SpellLoadout-" + loadoutNumber, bookJSON);
    }

    /// <summary> Loads a loadout saved under passed number. If the loadout does not exist, a blank one will be created with no spell data. If the size of the spell array is not equal to the </summary>
    public static SBLoadout LoadLoadout(int loadoutNumber)
    {
        string loadoutJSON = PlayerPrefs.GetString("SpellLoadout-" + loadoutNumber);
        SBLoadout loadout = JsonUtility.FromJson<SBLoadout>(loadoutJSON);
        if (loadout != null)
        {
            if(loadout.spells.Length != SBLoadout.DEFAULT_LOADOUT_SPELL_COUNT)
            {
                SpellData[] newSpellsArray = new SpellData[SBLoadout.DEFAULT_LOADOUT_SPELL_COUNT];
                for (int i = 0; i < newSpellsArray.Length; i++)
                    if (loadout.spells[i] != null)
                        newSpellsArray[i] = loadout.spells[i];
                    else
                        newSpellsArray[i] = new SpellData();

                loadout.spells = newSpellsArray;
                SaveLoadout(loadout, loadoutNumber);
            }
            
            Debug.Log("Loaded spell loadout - " + loadout.name);
            return loadout;
        }
        else
        {
            DebugLog(LogState.Error, "Could not find SpellLoadout-" + loadoutNumber + ". Creating a empty loadout");
            SaveLoadout(new SBLoadout(), loadoutNumber);
            return LoadLoadout(loadoutNumber);
        }
    }


    public static SpellData[] LoadAllSpellData()
    { return Resources.FindObjectsOfTypeAll<SpellData>(); }

    private enum LogState { Log, Warning, Error}
    private static void DebugLog(LogState state, string log)
    {
        string color = "";
        switch (state)
        {
            case LogState.Log:     color = "green";     break;
            case LogState.Warning: color = "orange";    break;
            case LogState.Error:   color = "red";       break;
        }
        string output = "<color=" + color + ">[]</color> - " + log;

        switch (state)
        {
            case LogState.Log:      Debug.Log(output);          break;
            case LogState.Warning:  Debug.LogWarning(output);   break;
            case LogState.Error:    Debug.LogError(output);     break;
        }
    }
}
