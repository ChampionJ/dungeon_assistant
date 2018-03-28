using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayStat : UIOverlayMenu
{

    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.STAT; }
    }
    public StatType type;
    public TMP_InputField field;
    public Button plusButton;
    public Button minusButton;
    public TextMeshProUGUI modifierText;
    public TextMeshProUGUI scoreText;

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
        
        scoreText.text = type.ToString() + " Score";
        UpdateText();

    }
    private void OnPlusClick()
    {
        CharacterManager.Instance.GetActiveCharacter().stats[type].score += 1;
        UpdateText();
    }
    private void OnMinusClick()
    {
        CharacterManager.Instance.GetActiveCharacter().stats[type].score -= 1;
        UpdateText();
    }
    private void UpdateScore()
    {
        int newScore;
        if (int.TryParse(field.text, out newScore))
        {
            CharacterManager.Instance.GetActiveCharacter().stats[type].score = newScore;
            
        }
        
        UpdateText();
    }
    private void UpdateText()
    {
        field.text = CharacterManager.Instance.GetActiveCharacter().stats[type].score.ToString();
        modifierText.text = "Modifier: " + CharacterData.GetModifierString(CharacterManager.Instance.GetActiveCharacter().stats[type].modifier);
        ScreenManager.Instance.UpdateCharacterScreenText();
    }
}
