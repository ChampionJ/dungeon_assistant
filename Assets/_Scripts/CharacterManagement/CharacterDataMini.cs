using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterDataMini {
    private int _currentHealth;
    public int currentHealth { get { return _currentHealth + tempHealth; } set { _currentHealth = value; } }
    public int tempHealth = 0;
    public int maxHealth;
    public int ac;
    public int passivePerception;
    public string name;
    public string uniqueID;
    public CharacterDataMini()
    {

    }
    public CharacterDataMini(CharacterData data)
    {
        currentHealth = data.currHealth;
        maxHealth = data.maxHealth;
        ac = data.armorClass;
        passivePerception = 10 + data.skills["Perception"].modifier;
        name = data.name;
        uniqueID = SystemInfo.deviceUniqueIdentifier;
    }
    public void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
        
    public void DealDamage(int damage)
    {
        for (int i = damage; i > 0; i--)
        {
            if (tempHealth > 0)
                tempHealth--;
            else {
                currentHealth--;
                if (currentHealth < 0)
                    currentHealth = 0;
            }
        }
    }
}
