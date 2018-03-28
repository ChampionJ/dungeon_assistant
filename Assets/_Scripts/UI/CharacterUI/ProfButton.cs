using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfButton : CharacterUIButton
{


    protected override void Start()
    {
        base.Start();
        Debug.Log("FUCKING STARTED");
        updatingText = transform.Find("Num").GetComponent<TextMeshProUGUI>();
        
    }
    public override void UpdateText()
    {
        updatingText.text = CharacterData.GetModifierString(CharacterManager.Instance.GetActiveCharacter().proficiencyBonus);

    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.Show(OverlayMenuType.PROF);
    }
}
