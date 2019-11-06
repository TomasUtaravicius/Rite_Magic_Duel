
using System.Collections.Generic;
using UnityEngine;

public class LoadoutManager : MonoBehaviour
{
    public bool saveTick = false;

    [SerializeField] int bookNo = 1;
    [SerializeField] string bookName; 
    [SerializeField] string spellSlot1;
    [SerializeField] string spellSlot2;
    [SerializeField] string spellSlot3;
    [SerializeField] string spellSlot4;
    [SerializeField] string spellSlot5;

    private void OnValidate()
    {
        if (saveTick) SaveSpellBook();
    }

    private void SaveSpellBook()
    {
        Debug.Log("Saved book " + bookName);
        
        PlayerPrefs.SetInt("SelectedBookNo", bookNo);
        PlayerPrefs.SetString("BookName", bookName);
        PlayerPrefs.SetString("SelectedLoadoutSpell1", spellSlot1);
        PlayerPrefs.SetString("SelectedLoadoutSpell2", spellSlot2);
        PlayerPrefs.SetString("SelectedLoadoutSpell3", spellSlot3);
        PlayerPrefs.SetString("SelectedLoadoutSpell4", spellSlot4);
        PlayerPrefs.SetString("SelectedLoadoutSpell5", spellSlot5);
    }

    public static List<string> LoadSelectedBook()
    {
        List<string> book = new List<string>();

        book.Add(PlayerPrefs.GetInt("SelectedLoadoutNo").ToString());
        book.Add(PlayerPrefs.GetString("BookName"));
        book.Add(PlayerPrefs.GetString("SelectedLoadoutSpell1"));
        book.Add(PlayerPrefs.GetString("SelectedLoadoutSpell2"));
        book.Add(PlayerPrefs.GetString("SelectedLoadoutSpell3"));
        book.Add(PlayerPrefs.GetString("SelectedLoadoutSpell4"));
        book.Add(PlayerPrefs.GetString("SelectedLoadoutSpell5"));

        return book;
    }
}
