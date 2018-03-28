using System;
using UnityEngine;

/// <summary>
/// Skill Data
/// </summary>
[Serializable]
public class Skill
{
    #region variables
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
            UpdateSkill();
        }
    }
    private CharacterData character;

    private StatType _type;
    public StatType type
    {
        get { return _type; }
        set { _type = value; }
    }


    private int _modifier;
    public int modifier
    {
        get
        {
            return _modifier;
        }

    }
    #endregion
    /// <summary>
    /// Updates the skill modifier
    /// </summary>
    public void UpdateSkill()
    {
        if (character == null) return;

        _modifier = character.stats[type].modifier;
        _modifier += Mathf.FloorToInt(proficiencyMultiplier * character.proficiencyBonus);
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="T:skill"/> class.
    /// </summary>
    /// <param name="character">Character</param>
    /// <param name="type">Skill Type</param>
    public Skill(CharacterData character, StatType type)
    {
        this.character = character;


        _type = type;
        UpdateSkill();
    }
    public Skill()
    {

    }
    /// <summary>
    /// Update the reference to the Character data. Link seems to break when importing
    /// </summary>
    /// <param name="data"></param>
    public void UpdateCharacter(CharacterData data)
    {
        character = data;
    }
}