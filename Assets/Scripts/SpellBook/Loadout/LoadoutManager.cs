using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    /* debug variables for component */
    public bool saveTick = false;
    public bool loadTick = false;
    [Range(1, 5)] public int loadoutNo = 1;
    [SerializeField] SBLoadout currentlySelectedLoadout = null;

    private void Awake()
    { CheckForValidLoadouts(); }


    private void OnValidate()
    {
        if (saveTick)
        {
            SaveLoadout(currentlySelectedLoadout, loadoutNo);
            SelectedLoadoutNum = loadoutNo;
            saveTick = false;
        }
        if (loadTick)
        {
            currentlySelectedLoadout = LoadLoadout(loadoutNo);
            loadTick = false;
        }
    }


    /////////////-Static implementation-////////////////

    public static int SelectedLoadoutNum
    {
        get => PlayerPrefs.GetInt("SelectedLoadout", 1);
        set
        {
            DebugLog(LogState.Log, "Selected Loadout " + value);
            PlayerPrefs.SetInt("SelectedLoadout", value);
        }
    }


    public static void CheckForValidLoadouts()
    {
        //Try loading each loadout. If it is not valid, it will be created, filled out or resized by the LoadLoadout function
        for (int i = 0; i < 5; i++)
            LoadLoadout(i + 1);
    }

    public static void ClearAllData()
    {
        PlayerPrefs.DeleteKey("SelectedLoadout");

        for (int i = 1; i <= 20; i++)
        {
            PlayerPrefs.DeleteKey("SpellLoadout-" + i);
        }

        DebugLog(LogState.Log, "Cleared all player loadout preferences");
    }

    public static void SaveLoadout(SBLoadout loadout, int loadoutNumber)
    {
        loadout.loadoutNumber = loadoutNumber;

        DebugLog(LogState.Log, "Saved spell loadout - " 
                              + loadout.name 
                              + " under name \"SpellLoadout-" 
                              + loadoutNumber + "\"");

        string bookJSON = JsonUtility.ToJson(loadout);
        PlayerPrefs.SetString("SpellLoadout-" + loadoutNumber, bookJSON);
    }

    public static SBLoadout LoadSelectedLoadout()
    { return LoadLoadout(PlayerPrefs.GetInt("SelectedLoadout", 1)); }



    /// <summary> 
    /// Loads a loadout saved under passed number. 
    /// If the loadout does not exist, a blank one will be created with no spell data. 
    /// If the size of the spell array is not equal to the 
    /// </summary>
    public static SBLoadout LoadLoadout(int loadoutNumber)
    {
        string loadoutJSON = PlayerPrefs.GetString("SpellLoadout-" + loadoutNumber);
        SBLoadout loadout = JsonUtility.FromJson<SBLoadout>(loadoutJSON);
        if (loadout != null)
        {
            if (loadout.spells.Length != SBLoadout.DEFAULT_LOADOUT_SPELL_COUNT)
            {
                SpellData[] newSpellsArray = new SpellData[SBLoadout.DEFAULT_LOADOUT_SPELL_COUNT];
                for (int i = 0; i < newSpellsArray.Length; i++)
                    if (loadout.spells.Length > i && loadout.spells[i] != null)
                        newSpellsArray[i] = loadout.spells[i];
                    else
                        newSpellsArray[i] = ScriptableObject.CreateInstance<SpellData>();

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
    {
        SpellData[] allSpells = Resources.LoadAll<SpellData>("SpellData") as SpellData[];

        Debug.LogWarning("Found " + allSpells.Length + " spells in Resources folder!");
        return allSpells;
    }

    private enum LogState { Log, Warning, Error }
    private static void DebugLog(LogState state, string log)
    {
        string color = "";
        switch (state)
        {
            case LogState.Log: color = "green"; break;
            case LogState.Warning: color = "orange"; break;
            case LogState.Error: color = "red"; break;
        }
        string output = "<color=" + color + ">[LoadoutManager]</color> - " + log;

        switch (state)
        {
            case LogState.Log: Debug.Log(output); break;
            case LogState.Warning: Debug.LogWarning(output); break;
            case LogState.Error: Debug.LogError(output); break;
        }
    }
}
