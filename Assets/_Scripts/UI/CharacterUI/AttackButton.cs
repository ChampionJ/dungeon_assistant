using UnityEngine;
using System.Collections;
using TMPro;

public class AttackButton : CharacterUIButton
{
    public int attackNumber;
    
    private TextMeshProUGUI attackNameText;
    private TextMeshProUGUI hitText;
    private TextMeshProUGUI damageText;
    
    protected override void Start()
    {
        base.Start();
        attackNumber = transform.GetSiblingIndex();
        attackNameText = transform.Find("Name").Find("MainText").GetComponent<TextMeshProUGUI>();
        hitText = transform.Find("Hit/MainText").GetComponent<TextMeshProUGUI>();
        damageText = transform.Find("Damage/MainText").GetComponent<TextMeshProUGUI>();

    }

    public override void UpdateText()
    {
        hitText.text = CharacterData.GetModifierString(CharacterManager.Instance.GetActiveCharacter().attacks[attackNumber].hitModifier);
        damageText.text = CharacterManager.Instance.GetActiveCharacter().attacks[attackNumber].DamageText();
        attackNameText.text = CharacterManager.Instance.GetActiveCharacter().attacks[attackNumber].attackName;
    }
    protected override void OnClickEvent()
    {
        base.OnClickEvent();
        ScreenManager.Instance.overlay.ShowAttackOverlay(attackNumber);
    }
}
