using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatButton : CharacterUIButton {
    public StatType type = StatType.STR;

    protected override void Start()
    {
        base.Start();
        updatingText = transform.Find("Num").GetComponent<TextMeshProUGUI>();

    }
    public override void UpdateText()
    {
        updatingText.text = CharacterData.GetModifierString(CharacterManager.Instance.GetActiveCharacter().stats[type].modifier);
    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.ShowStatOverlay(type);
    }
}
