using System.Collections;
using System.Collections.Generic;
using System;
using TakeAsh;

[Serializable]
public enum StatType
{
    STR, DEX, CON, WIS, INT, CHA
}
/// <summary>
/// Main container for all data and stats of a player
/// </summary>
[Serializable]
public class CharacterData 
{
#region variables
    private int _maxHealth;
    /// <summary>
    /// Maximum Health
    /// </summary>
    public int maxHealth
    {
        get { return _maxHealth; }
        set {
            if (value < 0) _maxHealth = 0; else _maxHealth = value;
            if (maxHealth < _currHealth) _currHealth = maxHealth;
        }
    }

    private int _currHealth;
    /// <summary>
    /// Current Health
    /// </summary>
    public int currHealth
    {
        get {
            return _currHealth + tempHealth;
        }
        set {
            if (value < 0) _currHealth = 0;
            else if (value > maxHealth) _currHealth = maxHealth;
            else _currHealth = value;
        }
    }
    
    private int _tempHealth;
    /// <summary>
    /// temporary Health
    /// </summary>
    public int tempHealth
    {
        get { return _tempHealth; }
        set {
            if (value < 0) _tempHealth = 0;
            else _tempHealth = value;
        }
    }

    private int _armorClass;
    /// <summary>
    /// Armor Class
    /// </summary>
    public int armorClass
    {
        get { return _armorClass; }
        set {
            if (value < 0) _armorClass = 0;
            else if (value > 30) _armorClass = 30; //30 is a semi-arbitrary ceiling, based on RAW rules
            else _armorClass = value;
        }
    }

    private int _proficiencyBonus;
    /// <summary>
    /// Proficiency Bonus
    /// </summary>
    public int proficiencyBonus
    {
        get { return _proficiencyBonus; }
        set {
            if (value < 0) _proficiencyBonus = 0;
            else if (value > 12) _proficiencyBonus = 12; //12 is a semi-arbitrary ceiling, based on RAW rules
            else _proficiencyBonus = value;
            //Update fields that use proficiency
            UpdateSaveStats();
            UpdateSkills();
            UpdateAttacks();
        }
    }

    private string _name;
    /// <summary>
    /// Character Name
    /// </summary>
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    private string _notes = "";
    /// <summary>
    /// Character Notes
    /// </summary>
    public string notes
    {
        get { return _notes; }
        set { _notes = value; }
    }
    /// <summary>
    /// Core Stats
    /// </summary>
    public SerializableDictionary<StatType, Stat> stats;
    /// <summary>
    /// Saving Throw Stats
    /// </summary>
    public SerializableDictionary<StatType, SaveStat> saveStats;
    /// <summary>
    /// Skill Stats
    /// </summary>
    public SerializableDictionary<String, Skill> skills;
    /// <summary>
    /// Attacks
    /// </summary>
    public Attack[] attacks;

    #endregion
    /// <summary>
    /// Initializes a new instance of the <see cref="T:CharacterData"/> class.
    /// </summary>
    public CharacterData()
    {
        Setup();
    }
    /// <summary>
    /// assembles dictionaries, arrays, and sets default values
    /// </summary>
    private void Setup()
    {
        //setup dictionaries and arrays
        SetupStats();
        SetupSaveStats();
        SetupSkills();
        SetupAttacks();
        
        //set fields to default, this could be done inline when declared, but will be done here for ease of access. 
        name = "Unnamed Character";
        maxHealth = 6;
        tempHealth = 0;
        currHealth = 10;
        armorClass = 10;
        proficiencyBonus = 2;
    }

    #region setup
    /// <summary>
    /// Assembles the stats dictionary
    /// </summary>
    private void SetupStats()
    {
        stats = new SerializableDictionary<StatType, Stat>();
        stats.Add(StatType.STR, new Stat(this, StatType.STR));
        stats.Add(StatType.DEX, new Stat(this, StatType.DEX));
        stats.Add(StatType.CON, new Stat(this, StatType.CON));
        stats.Add(StatType.INT, new Stat(this, StatType.INT));
        stats.Add(StatType.WIS, new Stat(this, StatType.WIS));
        stats.Add(StatType.CHA, new Stat(this, StatType.CHA));
    }
    /// <summary>
    /// Assembles the save stats dictionary
    /// </summary>
    private void SetupSaveStats()
    {
        saveStats = new SerializableDictionary<StatType, SaveStat>();
        saveStats.Add(StatType.STR, new SaveStat(this, StatType.STR));
        saveStats.Add(StatType.DEX, new SaveStat(this, StatType.DEX));
        saveStats.Add(StatType.CON, new SaveStat(this, StatType.CON));
        saveStats.Add(StatType.INT, new SaveStat(this, StatType.INT));
        saveStats.Add(StatType.WIS, new SaveStat(this, StatType.WIS));
        saveStats.Add(StatType.CHA, new SaveStat(this, StatType.CHA));
    }
    /// <summary>
    /// Assembles the skills dictionary
    /// </summary>
    private void SetupSkills()
    {
        skills = new SerializableDictionary<string, Skill>();
        skills.Add("Acrobatics", new Skill(this, StatType.DEX));
        skills.Add("Animal Handling", new Skill(this, StatType.WIS));
        skills.Add("Arcana", new Skill(this, StatType.INT));
        skills.Add("Athletics", new Skill(this, StatType.STR));
        skills.Add("Deception", new Skill(this, StatType.CHA));
        skills.Add("History", new Skill(this, StatType.INT));
        skills.Add("Insight", new Skill(this, StatType.WIS));
        skills.Add("Intimidation", new Skill(this, StatType.CHA));
        skills.Add("Investigation", new Skill(this, StatType.INT));
        skills.Add("Medicine", new Skill(this, StatType.WIS));
        skills.Add("Nature", new Skill(this, StatType.INT));
        skills.Add("Perception", new Skill(this, StatType.WIS));
        skills.Add("Performance", new Skill(this, StatType.CHA));
        skills.Add("Persuasion", new Skill(this, StatType.CHA));
        skills.Add("Religion", new Skill(this, StatType.INT));
        skills.Add("Sleight of Hand", new Skill(this, StatType.DEX));
        skills.Add("Stealth", new Skill(this, StatType.DEX));
        skills.Add("Survival", new Skill(this, StatType.WIS));
    }
    /// <summary>
    /// Assembles the attacks array
    /// </summary>
    private void SetupAttacks()
    {
        attacks = new Attack[4];
        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i] = new Attack(this, StatType.STR, "Attack " + (i + 1));
        }
    }
    #endregion
    #region updates
    /// <summary>
    /// update all save stats
    /// </summary>
    /// <param name="updateCharacter">should update character?</param>
    private void UpdateSaveStats(bool updateCharacter = false)
    {
        foreach (KeyValuePair<StatType, SaveStat> entry in saveStats)
        {
            if (updateCharacter) entry.Value.UpdateCharacter(this);
            entry.Value.UpdateSkillSave();
            
        }
    }
    /// <summary>
    /// update all attacks
    /// </summary>
    /// <param name="updateCharacter">should update character?</param>
    private void UpdateAttacks(bool updateCharacter = false)
    {
        foreach (Attack attack in attacks)
        {
            if (updateCharacter) attack.UpdateCharacter(this);
            attack.UpdateAttack();
            
        }
    }
    /// <summary>
    /// Update all skills
    /// </summary>
    /// <param name="updateCharacter">should update character?</param>
    private void UpdateSkills(bool updateCharacter = false)
    {
        foreach (KeyValuePair<String, Skill> entry in skills)
        {
            if (updateCharacter) entry.Value.UpdateCharacter(this);
            entry.Value.UpdateSkill();
            
        }
    }
    /// <summary>
    /// Update fields that use core stat modifiers
    /// This should be called whenever a core stat is changed
    /// </summary>
    public void UpdateStatReliantFields(bool updateCharacter = false)
    {
        UpdateSaveStats(updateCharacter);
        UpdateSkills(updateCharacter);
        UpdateAttacks(updateCharacter);
    }
    /// <summary>
    /// Set stat character, update and set character for all fields
    /// </summary>
    public void ImportUpdate()
    {
        foreach (KeyValuePair<StatType, Stat> entry in stats)
        {
            entry.Value.UpdateCharacter(this);
        }
        UpdateStatReliantFields(true);
    }
    #endregion

    /// <summary>
    /// Deal damage to the player
    /// </summary>
    /// <param name="damage">amount of damage to deal</param>
    public void DealDamage(int damage)
    {
        //Take damage to temp health before current health
        for (int i = damage; i > 0; i--)
        {
            if (tempHealth > 0)
                tempHealth--;
            else currHealth--;
        }
    }
    /// <summary>
    /// simulates a long rest, sets temporary Health to 0 and current health to the max
    /// </summary>
    public void LongRest()
    {
        tempHealth = 0;
        currHealth = maxHealth;
    }
    /// <summary>
    /// returns string value of modifier int with positive or negative sign.
    /// </summary>
    /// <param name="modifier"></param>
    /// <returns></returns>
    public static String GetModifierString(int modifier)
    {
        return modifier.ToString("+0;-#");
    }
}


