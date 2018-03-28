using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIOverlaySaveStat : UIOverlayMenu
{
    
    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.SAVESTAT; }
        
    }
    public StatType type;
    public Toggle normalProficiency;
    public Toggle doubleProficiency;
    public Toggle halfProficiency;
    public TextMeshProUGUI text;

    
    private void OnToggleNormal(bool val)
    {
        if (!val)
        {
            Debug.Log("Turned Off Normal");
            CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier = 0;
        }
        else
        {
            CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier = 1;
        }
        
        UpdateText();
    }
    private void OnToggleDouble(bool val)
    {
        if (!val)
        {
            Debug.Log("Turned Off Double");
            CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier = 0;
        }
        else
        {
            Debug.Log("Turned On Double");
            CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier = 2;
        }
        UpdateText();
    }
    private void OnToggleHalf(bool val)
    {
        if (!val)
        {
            Debug.Log("Turned Off Half");
            CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier = 0;
        }
        else
        {
            CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier = 0.5f;
        }
        UpdateText();
    }
    public override void OnShow()
    {
        base.OnShow();

        text.text = type.ToString() + " Save Proficiency";

        normalProficiency.onValueChanged.RemoveAllListeners();
        doubleProficiency.onValueChanged.RemoveAllListeners();
        halfProficiency.onValueChanged.RemoveAllListeners();

        normalProficiency.isOn = false;
        halfProficiency.isOn = false;
        doubleProficiency.isOn = false;

        normalProficiency.onValueChanged.AddListener(delegate { OnToggleNormal(normalProficiency.isOn); });
        doubleProficiency.onValueChanged.AddListener(delegate { OnToggleDouble(doubleProficiency.isOn); });
        halfProficiency.onValueChanged.AddListener(delegate { OnToggleHalf(halfProficiency.isOn); });

        float profMult = CharacterManager.Instance.GetActiveCharacter().saveStats[type].proficiencyMultiplier;
        if(profMult != 0)
        {
            if(profMult == 1)
            {
                normalProficiency.isOn = true;
            }
            if (profMult == 0.5f)
            {
                halfProficiency.isOn = true;
            }
            if (profMult == 2)
            {
                doubleProficiency.isOn = true;
            }
        }
        
        UpdateText();

    }
    private void UpdateText()
    {
        
        ScreenManager.Instance.UpdateCharacterScreenText();
    }
}
