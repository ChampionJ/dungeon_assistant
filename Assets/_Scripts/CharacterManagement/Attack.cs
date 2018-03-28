using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Attack
{
#region variables
    private int _hitModifier;
    public int hitModifier
    {
        get
        {
            return _hitModifier;
        }

    }

    private DiceRoll _dice;
    public DiceRoll dice
    {
        get
        {
            return _dice;
        }
        set
        {
            _dice = value;
            UpdateAttack();
        }

    }

    private string _attackName;
    public string attackName
    {
        get
        {
            return _attackName;
        }
        set
        {
            if (value != "") _attackName = value;
            else _attackName = "Attack";
            UpdateAttack();
        }

    }

    private int _extraDamageModifier;
    public int extraDamageModifier
    {
        get
        {
            return _extraDamageModifier;
        }
        set
        {
            if (value < 0) _extraDamageModifier = 0;
            else _extraDamageModifier = value;
            UpdateAttack();
        }

    }
    private int _damageModifier = 0;
    public int damageModifier
    {
        get
        {
            return _damageModifier;
        }

    }
    private float _proficiencyMultiplier = 0;
    public float proficiencyMultiplier
    {
        get
        {
            return _proficiencyMultiplier;
        }
        set
        {
            if (value < 0) _proficiencyMultiplier = 0;
            else _proficiencyMultiplier = value;
            UpdateAttack();
        }

    }
    private StatType _type;
    public StatType type
    {
        get { return _type; }
        set { _type = value; }
    }
    private CharacterData character;
#endregion
    public Attack(CharacterData character, StatType type, string attackName, float proficiencyMultiplier = 0, int extraDamageModifier = 0)
    {
        this.character = character;
        this.attackName = attackName;
        this.proficiencyMultiplier = proficiencyMultiplier;
        this.extraDamageModifier = extraDamageModifier;
        dice = new DiceRoll(Dice.d6, 1);
        this._type = type;
        UpdateAttack();
    }
    public Attack(){ }
    public void UpdateAttack()
    {
        if (character == null) return;
        _hitModifier = character.stats[type].modifier + Mathf.FloorToInt(proficiencyMultiplier * character.proficiencyBonus);
        _damageModifier = character.stats[type].modifier + _extraDamageModifier;
    }
    public void UpdateCharacter(CharacterData data)
    {
        character = data;
    }
    public string DamageText()
    {
        return dice.ToString() + " +" + _damageModifier.ToString();
    }


}
