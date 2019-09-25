using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public List<InputField> iFields;
    private float delay = 0.5f;
    private float timer;
    private InputField currentInputField;
    private void Start()
    {
        timer = 0f;
    }
    public void ButtonRegular(string letter)
    {
        if(Time.time>=timer)
        {
            currentInputField.text += letter;
            timer = Time.time + delay;
        }
        
    }
    public void Clear()
    {
        currentInputField.text = "";
    }
    public void ToggleKeyboard()
    {

    }
    public void TurnOffGroup(GameObject group)
    {

    }
    public void ResetInputFieldReference()
    {
        currentInputField = null;
    }
    public void SetInputFieldReference(InputField iField)
    {
        currentInputField = iField;
    }
}