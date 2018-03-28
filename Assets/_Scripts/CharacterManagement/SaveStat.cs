using System;
using UnityEngine;


[Serializable]
public class SaveStat
{
    #region variables
    private int _modifier;
    public int modifier
    {
        get
        {
            return _modifier;
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
            UpdateSkillSave();
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
    /// <summary>
    /// Initializes a new instance of the <see cref="T:saveStat"/> class.
    /// </summary>
    /// <param name="character">Character</param>
    /// <param name="type">Stat Type</param>
    public SaveStat(CharacterData character, StatType type)
    {
        this.character = character;
        this._type = type;
        UpdateSkillSave();
    }
    public SaveStat() { }
    /// <summary>
    /// Updates the Skill Save Modifier
    /// </summary>
    public void UpdateSkillSave()
    {
        if (character == null) return;
        _modifier = character.stats[type].modifier;

        _modifier += Mathf.FloorToInt(proficiencyMultiplier * character.proficiencyBonus);
    }
    public void UpdateCharacter(CharacterData data)
    {
        character = data;
    }
}