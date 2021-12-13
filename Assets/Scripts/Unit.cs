using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public int damage = 10;
    public int maxHP;
    public int currentHP;

    public Slider hpSlider;
    public Text valueText;

    void Update()
    {
        valueText.text = currentHP.ToString();

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        } 
        
        if(currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public void SetHUD() //set HP slider
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = currentHP;
    }
    
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
    
    public bool TakeDamage(int dmg) //damage teken from either sides attacks
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }
}
