using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ACButton : CharacterUIButton {
    protected override void Start()
    {
        base.Start();
        updatingText = transform.Find("Num").GetComponent<TextMeshProUGUI>();

    }
    public override void UpdateText()
    {
        updatingText.text = CharacterManager.Instance.GetActiveCharacter().armorClass.ToString() ;
    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.Show(OverlayMenuType.AC);
    }
}
