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

    /// <summary> </summary>
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
            Debug.LogError("Could not find SpellLoadout-" + loadoutNumber + ". Creating a empty loadout");
            SaveLoadout(new SBLoadout(), loadoutNumber);
            return LoadLoadout(loadoutNumber);
        }
    }


    public static SpellData[] LoadAllSpellData()
    {
        return Resources.LoadAll<SpellData>("");
    }
}
