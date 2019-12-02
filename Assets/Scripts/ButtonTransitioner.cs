using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonTransitioner : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IPointerClickHandler
{
    public Color32 m_NormalColor;
    public Color32 m_HoverColor;
    public Color32 m_DownColor;
    public UnityEvent buttonClick;
    [SerializeField]
    private bool isToggleButton;
    [SerializeField]
    private bool isInputField;
    private bool toggleState;
    private Image m_Image = null;
    [SerializeField]
    private Keyboard keyboard;
    private void Awake()
    {
        buttonClick.AddListener(HandleOnButtonClick);
        m_Image = GetComponent<Image>();
    }
    public void OnInputFieldToggled()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer Click");
        m_Image.color = m_HoverColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogError("Pointer Down");
        m_Image.color = m_DownColor;
        buttonClick.Invoke();
        
        if (isToggleButton)
        {
           
            toggleState = !toggleState;
            if(toggleState)
            {
                keyboard.gameObject.SetActive(true);
            }
            else
            {
                keyboard.gameObject.SetActive(false);
            }
            
        }
        if (!isInputField && keyboard!=null)
        {
            keyboard.ResetInputFieldReference();
        }
        if (isInputField)
        {
            keyboard.SetInputFieldReference(GetComponent<InputField>());
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter");
        m_Image.color = m_HoverColor;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer Exit");
        m_Image.color = m_NormalColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Pointer Up");
        m_Image.color = m_NormalColor;



    }

    protected virtual void HandleOnButtonClick() { }
}
