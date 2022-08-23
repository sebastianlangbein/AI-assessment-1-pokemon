using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalAi : MonoBehaviour
{
    //Text and float fields for boss stats
    public Text healthText, stateText, attackText, defenceText, speedText, 
        dodgeText, physResText, fireResText, elecResText, descText;
    public float health, maxHealth, attack, defence, speed,
        dodgeChance, physRes, fireRes, elecRes;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        //Change the text box to show actual boss health
        healthText.text = health.ToString();
    }

    public void Damage(int damageAmount)
    {
        //if health is negative after an attack from player
        if (health - damageAmount < 0)
        {
            //set health to 0 to avoid negative health
            health = 0;
        }
        else
        {
            //health equals health minus damage
            health -= damageAmount;
        }
        UpdateUI();
    }

    public void Heal(int healAmount)
    {
        //if health is greater than max health after a heal
        if (health + healAmount > maxHealth)
        {
            //set to max health
            health = maxHealth;
        }
        else
        {
            //health equals health plus heal
            health += healAmount;
        }
        UpdateUI();
    }
}