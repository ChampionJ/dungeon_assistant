using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayInitiativeCharacter : UIOverlayMenu
{
    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.INITCHARACTER; }
    }
    public InitiativeCharacterButton initCharButton;


    public TMP_InputField field;
    public Button plusButton;
    public Button minusButton;
    public TextMeshProUGUI text;
    public Button removeButton;

    void Start()
    {

        field.onEndEdit.AddListener(delegate { UpdateScore(); });
        //field.onSubmit.AddListener(delegate { UpdateScore(); });
        plusButton.onClick.AddListener(OnPlusClick);
        minusButton.onClick.AddListener(OnMinusClick);
        removeButton.onClick.AddListener(OnRemove);
    }
    public override void OnShow()
    {
        base.OnShow();

        text.text = "Initiative";
        UpdateText();

    }
    private void OnPlusClick()
    {
        initCharButton.initCharacter.initiativeNumber += 1;
        
        UpdateText();
    }
    private void OnMinusClick()
    {
        initCharButton.initCharacter.initiativeNumber -= 1;
        UpdateText();
    }
    private void UpdateScore()
    {
        int newScore;
        if (int.TryParse(field.text, out newScore))
        {
            initCharButton.initCharacter.initiativeNumber = newScore;
            
        }
        UpdateText();
    }
    private void UpdateText()
    {
        field.text = initCharButton.initCharacter.initiativeNumber.ToString();

        initCharButton.UpdateText();
        InitiativeManager.Instance.RefreshButtons();
    }
    private void OnRemove()
    {
        InitiativeManager.Instance.RemoveCharButton(initCharButton);
        ScreenManager.Instance.overlay.Hide();
    }

}
