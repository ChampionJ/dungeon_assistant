using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameButton : CharacterUIButton
{
    public override void UpdateText()
    {
        updatingText.text = CharacterManager.Instance.GetActiveCharacter().name;
    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.Show(OverlayMenuType.NAME);
    }
}
