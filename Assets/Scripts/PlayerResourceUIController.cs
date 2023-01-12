using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResourceUIController : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Image manaBar;

    public void UpdateResourceBars(float health,float mana)
    {
        healthBar.fillAmount= health/100 * 0.4f;
        manaBar.fillAmount = mana/100* 0.4f;
    }
}
