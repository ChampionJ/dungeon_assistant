using System;
using UnityEngine;

/// <summary>
/// A core Stat, this is used to generate all other stat and skills based around this StatType
/// </summary>
[Serializable]
public class Stat
{
    #region variables
    private int _score = 10;
    public int score
    {
        get { return _score; }
        set
        {
            if (value < 3) _score = 3;
            else if(value > 30) _score = 30;
            else _score = value;
            SetModifier();
        }
    }

    private int _modifier;
    public int modifier
    {
        get
        {
            return _modifier;
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
    /// Initializes a new instance of the <see cref="T:stat"/> class.
    /// </summary>
    /// <param name="character">Character</param>
    /// <param name="type">Stat Type</param>
    public Stat(CharacterData character, StatType type)
    {
        this.character = character;
        _type = type;
    }
    public Stat() { }
    private void SetModifier()
    {
        float value = score;
        //if (value < -5) value = 0;
        //if (value > 30) value = 30;
        value = (value - 10) / 2f;

        _modifier = (int)Math.Floor(value);

        
        if (character != null)
        {
            character.UpdateStatReliantFields();
        }
    }
    public void UpdateCharacter(CharacterData data)
    {
        character = data;
    }

}