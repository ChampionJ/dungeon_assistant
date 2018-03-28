using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayAC : UIOverlayMenu
{


    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.AC; }
    }
    
    public TMP_InputField field;
    public Button plusButton;
    public Button minusButton;
    public TextMeshProUGUI text;

    void Start()
    {

        field.onEndEdit.AddListener(delegate { UpdateScore(); });
        //field.onSubmit.AddListener(delegate { UpdateScore(); });
        plusButton.onClick.AddListener(OnPlusClick);
        minusButton.onClick.AddListener(OnMinusClick);
    }
    public override void OnShow()
    {
        base.OnShow();

        text.text = "Armor Class";
        UpdateText();

    }
    private void OnPlusClick()
    {
        CharacterManager.Instance.GetActiveCharacter().armorClass += 1;
        UpdateText();
    }
    private void OnMinusClick()
    {
        CharacterManager.Instance.GetActiveCharacter().armorClass -= 1;
        UpdateText();
    }
    private void UpdateScore()
    {
        int newScore;
        if (int.TryParse(field.text, out newScore))
        {
            CharacterManager.Instance.GetActiveCharacter().armorClass = newScore;
            UpdateText();
        }
    }
    private void UpdateText()
    {
        field.text = CharacterManager.Instance.GetActiveCharacter().armorClass.ToString();
        
        ScreenManager.Instance.UpdateCharacterScreenText();
    }
}
