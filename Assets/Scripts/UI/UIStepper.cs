
using UnityEngine;
using UnityEngine.UI;

public class UIStepper : MonoBehaviour
{
    public delegate void StepperCallback(int value);
    public event StepperCallback OnValueChanged;

    int currentValue = 1;

    [SerializeField] int minValue = 1;
    [SerializeField] int maxValue = 5;
    [SerializeField] Text valueText;

    [SerializeField] ButtonTransitioner decrementButton;
    [SerializeField] ButtonTransitioner incrementButton;

    public int CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = Mathf.Clamp(value, minValue, maxValue);
            valueText.text = currentValue.ToString();
            OnValueChanged?.Invoke(currentValue);
        }
    }

    private void OnEnable()
    {
        if (incrementButton) incrementButton.buttonClick.AddListener(Increment);
        if (decrementButton) decrementButton.buttonClick.AddListener(Decrement);
    }

    private void Increment() { CurrentValue++; }
    private void Decrement() { CurrentValue--; }

    

}
