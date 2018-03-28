using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System;

public class UIOverlayAttack : UIOverlayMenu
{
#region variables
    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.ATTACK; }
    }
    //name
    public TMP_InputField nameInput;
    //stat
    public Toggle strToggle;
    public Toggle dexToggle;
    public Toggle conToggle;
    public Toggle intToggle;
    public Toggle wisToggle;
    public Toggle chaToggle;
    public ToggleGroup statGroup;
    StatType activeStatType;


    //prof
    public Toggle normProfToggle;
    public Toggle doubProfToggle;
    public Toggle halfProfToggle;
    public ToggleGroup profGroup;

    //dicenum
    public Button diceMinusButton;
    public Button dicePlusButton;
    public TMP_InputField diceNumInput;
    int _diceNum = 1;
    int diceNum { get { return _diceNum; } set {
            if (value < -20) _diceNum = -20;
            else if (value > 20) _diceNum = 20;
            else _diceNum = value;
        }
    }

    //dicetype
    public Toggle d2;
    public Toggle d4;
    public Toggle d6;
    public Toggle d8;
    public Toggle d10;
    public Toggle d12;
    public ToggleGroup diceGroup;
    Dice activeDiceType;


    //extradmg
    public Button extraMinusButton;
    public Button extraPlusButton;
    public TMP_InputField extraNumInput;
    
    int _extraDamageNum = 0;
    int extraDamageNum
    {
        get { return _extraDamageNum; }
        set
        {
            if (value < -20) _extraDamageNum = -20;
            else if (value > 20) _extraDamageNum = 20;
            else _extraDamageNum = value;
        }
    }

    //submit
    public Button submitButton;


    public int attackNum = 0;

#endregion
    private void Start()
    {
        //Set listeners to everything
        d2.onValueChanged.AddListener(delegate { OnDiceToggle(d2.isOn, d2.transform); });
        d4.onValueChanged.AddListener(delegate { OnDiceToggle(d4.isOn, d4.transform); });
        d6.onValueChanged.AddListener(delegate { OnDiceToggle(d6.isOn, d6.transform); });
        d8.onValueChanged.AddListener(delegate { OnDiceToggle(d8.isOn, d8.transform); });
        d10.onValueChanged.AddListener(delegate { OnDiceToggle(d10.isOn, d10.transform); });
        d12.onValueChanged.AddListener(delegate { OnDiceToggle(d12.isOn, d12.transform); });
        

        conToggle.onValueChanged.AddListener(delegate { OnStatToggle(conToggle.isOn, conToggle.transform); });
        strToggle.onValueChanged.AddListener(delegate { OnStatToggle(strToggle.isOn, strToggle.transform); });
        dexToggle.onValueChanged.AddListener(delegate { OnStatToggle(dexToggle.isOn, dexToggle.transform); });
        wisToggle.onValueChanged.AddListener(delegate { OnStatToggle(wisToggle.isOn, wisToggle.transform); });
        intToggle.onValueChanged.AddListener(delegate { OnStatToggle(intToggle.isOn, intToggle.transform); });
        chaToggle.onValueChanged.AddListener(delegate { OnStatToggle(chaToggle.isOn, chaToggle.transform); });

        diceNumInput.onValueChanged.AddListener(delegate { OnEndEditDiceNum(); });
        dicePlusButton.onClick.AddListener(OnDiceNumberPlusClick);
        diceMinusButton.onClick.AddListener(OnDiceNumberMinusClick);

        extraNumInput.onValueChanged.AddListener(delegate { OnEndEditExtra(); });
        extraPlusButton.onClick.AddListener(OnExtraPlusClick);
        extraMinusButton.onClick.AddListener(OnExtraMinusClick);

        submitButton.onClick.AddListener(OnSubmit);
    }
    public override void OnShow()
    {
        base.OnShow();
        UpdateTextFromCharacterManager();
    }
    private void UpdateTextFromCharacterManager()
    {
        Attack attack = CharacterManager.Instance.GetActiveCharacter().attacks[attackNum];
        SetStatToggles(attack.type);
        SetDiceToggles(attack.dice);
        SetProficiencyToggles(attack.proficiencyMultiplier);
        
        SetName(attack.attackName);
        extraDamageNum = attack.extraDamageModifier;
        diceNum = attack.dice.numberOfDice;

        RefreshExtraDamageText();
        RefreshDiceNumText();
    }
    /// <summary>
    /// Update dice number text
    /// </summary>
    private void RefreshDiceNumText()
    {
        diceNumInput.text = diceNum.ToString();
    }
    /// <summary>
    /// Set name input text
    /// </summary>
    /// <param name="name">new name</param>
    private void SetName(string name)
    {
        nameInput.text = name;
    }
    /// <summary>
    /// Update extra damage text
    /// </summary>
    private void RefreshExtraDamageText()
    {
        extraNumInput.text = extraDamageNum.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="profMultiplier"></param>
    private void SetProficiencyToggles(float profMultiplier)
    {
        profGroup.SetAllTogglesOff();
        if (profMultiplier == .5f) halfProfToggle.isOn = true;
        else if (profMultiplier == 1) normProfToggle.isOn = true;
        else if (profMultiplier == 2) doubProfToggle.isOn = true;
    }
    private void SetStatToggles(StatType statType)
    {
        statGroup.SetAllTogglesOff();
        switch (statType)
        {
            case StatType.STR:
                strToggle.isOn = true;
                break;
            case StatType.DEX:
                dexToggle.isOn = true;
                break;
            case StatType.CON:
                conToggle.isOn = true;
                break;
            case StatType.INT:
                intToggle.isOn = true;
                break;
            case StatType.WIS:
                wisToggle.isOn = true;
                break;
            case StatType.CHA:
                chaToggle.isOn = true;
                break;
        }
        activeStatType = statType;
    }
    private void SetDiceToggles(DiceRoll dr)
    {
        diceGroup.SetAllTogglesOff();

        switch (dr.dice)
        {
            case Dice.d2:
                d2.isOn = true;
                break;
            case Dice.d4:
                d4.isOn = true;
                break;
            case Dice.d6:
                d6.isOn = true;
                break;
            case Dice.d8:
                d8.isOn = true;
                break;
            case Dice.d10:
                d10.isOn = true;
                break;
            case Dice.d12:
                d12.isOn = true;
                break;
        }
        activeDiceType = dr.dice;
    }
    private void OnDiceNumberPlusClick()
    {
        diceNum++;
        RefreshDiceNumText();
    }
    private void OnDiceNumberMinusClick()
    {
        diceNum--;
        RefreshDiceNumText();
    }
    private void OnExtraPlusClick()
    {
        extraDamageNum++;
        RefreshExtraDamageText();
    }
    private void OnExtraMinusClick()
    {
        extraDamageNum--;
        RefreshExtraDamageText();
    }
    private void OnEndEditExtra()
    {
        int newNum;
        if (int.TryParse(extraNumInput.text, out newNum))
        {
            extraDamageNum = newNum;
        }
        RefreshExtraDamageText();
    }
    private void OnEndEditDiceNum()
    {
        int newNum;
        if (int.TryParse(diceNumInput.text, out newNum))
        {
            diceNum = newNum;
        }
        RefreshDiceNumText();
    }
    private void OnStatToggle(bool val, Transform t) {
        if (!val) return;
        activeStatType = (StatType)Enum.Parse(typeof(StatType), t.parent.name.ToUpper());
    }
    private void OnDiceToggle(bool val, Transform t)
    {
        if (!val) return;
        activeDiceType = (Dice)Enum.Parse(typeof(Dice), t.parent.name.ToLower());
    }
    private void OnSubmit()
    {
        float profMult = 0;
        if (doubProfToggle.isOn) profMult = 2;
        else if (halfProfToggle.isOn) profMult = .5f;
        else if (normProfToggle.isOn) profMult = 1;
        Debug.Log("name: " + nameInput.text);

        CharacterManager.Instance.GetActiveCharacter().attacks[attackNum].attackName = nameInput.text;
        CharacterManager.Instance.GetActiveCharacter().attacks[attackNum].dice = new DiceRoll(activeDiceType, diceNum);
        CharacterManager.Instance.GetActiveCharacter().attacks[attackNum].proficiencyMultiplier = profMult;
        CharacterManager.Instance.GetActiveCharacter().attacks[attackNum].type = activeStatType;


        CharacterManager.Instance.GetActiveCharacter().attacks[attackNum].extraDamageModifier = extraDamageNum;

        ScreenManager.Instance.UpdateCharacterScreenText();
        ScreenManager.Instance.overlay.Hide();
    }
}
