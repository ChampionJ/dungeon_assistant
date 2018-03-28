using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveStatButton : CharacterUIButton {
    public StatType type = StatType.STR;
    protected override void Start()
    {
        base.Start();
        updatingText = transform.Find("Num").GetComponent<TextMeshProUGUI>();

    }
    public override void UpdateText()
    {
        updatingText.text = "Save " + CharacterData.GetModifierString(CharacterManager.Instance.GetActiveCharacter().saveStats[type].modifier);

    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.ShowSaveStatOverlay(type);
    }
}