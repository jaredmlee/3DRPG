using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text currentHp;
    public Text maxHp;

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        int number;
        if (currentHp == null)
        {
            return;
        }
        bool success = int.TryParse(currentHp.text, out number);
        if (success)
        {
            int newNum = health;
            currentHp.text = newNum.ToString();
        }
    }
    public void setMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        fill.color = gradient.Evaluate(1f);
        int number;
        if (maxHp == null)
        {
            return;
        }
        bool success = int.TryParse(maxHp.text, out number);
        if (success)
        {
            int newNum = maxHealth;
            maxHp.text = newNum.ToString();
        }
    }
}
