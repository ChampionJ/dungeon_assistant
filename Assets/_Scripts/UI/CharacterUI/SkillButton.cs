using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillButton : CharacterUIButton
{
    private string _skillName;
    public string skillName { get { return _skillName; } set {
            _skillName = value;
            transform.Find("Title").GetComponent<TextMeshProUGUI>().text = _skillName;
        }
    }
    protected override void Start()
    {
        base.Start();
        skillName = transform.Find("Title").GetComponent<TextMeshProUGUI>().text;
        //string title = skillName + " (" + CharacterManager.Instance.GetActiveCharacter().skills[skillName].type.ToString() + ")";
        //transform.Find("Num (1)").GetComponent<TextMeshProUGUI>().text = title;
        updatingText = transform.Find("Num").GetComponent<TextMeshProUGUI>();

        
    }

    public override void UpdateText()
    {
        updatingText.text = CharacterData.GetModifierString(CharacterManager.Instance.GetActiveCharacter().skills[skillName].modifier);
        
    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.ShowSkillOverlay(skillName);
    }
}
