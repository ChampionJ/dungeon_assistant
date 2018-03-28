using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayInitCharHealth : UIOverlayMenu
{

    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.INITCHARHEALTH; }
    }
    public TMP_InputField initiative;
    public TMP_InputField mainInputField;
    public Button plusButton;
    public Button minusButton;
    public Button submitButton;
    public Toggle tempToggle;
    public Toggle healToggle;
    public Toggle damgToggle;
    public Button longRest;
    public Button removeButton;

    public InitiativeCharacterButton initCharButton;

    public ToggleGroup toggles;

    private int _inputNumberValue = 0;
    private int inputNumberValue
    {
        get { return _inputNumberValue; }
        set
        {
            if (value < 0) _inputNumberValue = 0;
            else _inputNumberValue = value;
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        //turn only damage on
        toggles.SetAllTogglesOff();
        damgToggle.isOn = true;

        inputNumberValue = 0;
        ResetText();

    }
    private void OnMainInputFiendChange()
    {
        int newInputNumber;
        if (int.TryParse(mainInputField.text, out newInputNumber))
        {
            inputNumberValue = newInputNumber;
        }
    }
    private void OnLongRest()
    {
        initCharButton.initCharacter.characterDataMini.currentHealth = initCharButton.initCharacter.characterDataMini.maxHealth;

        InitiativeManager.Instance.RefreshButtons();
        //initCharButton.UpdateText();
        ScreenManager.Instance.overlay.Hide();
    }
    private void OnPlusClick()
    {
        inputNumberValue += 1;
        UpdateMainInputBoxText();
    }
    private void OnMinusClick()
    {

        inputNumberValue -= 1;
        UpdateMainInputBoxText();
    }
    private void UpdateMainInputBoxText()
    {
        mainInputField.text = inputNumberValue.ToString();
    }
    private void ResetText()
    {

        UpdateMainInputBoxText();
        initiative.text = initCharButton.initCharacter.initiativeNumber.ToString();

    }
    private void Submit()
    {
        if (damgToggle.isOn && inputNumberValue > 0)
        {
            initCharButton.initCharacter.characterDataMini.DealDamage(inputNumberValue);
            
        }
        else if (healToggle.isOn && inputNumberValue > 0)
        {
            initCharButton.initCharacter.characterDataMini.Heal(inputNumberValue);
            
        }
        else if (tempToggle.isOn)
        {
            initCharButton.initCharacter.characterDataMini.tempHealth = inputNumberValue;
        }

        //Update Character
        if (initiative.text != initCharButton.initCharacter.characterDataMini.maxHealth.ToString())
        {
            initCharButton.initCharacter.initiativeNumber = int.Parse(initiative.text);
        }

        //Update Screen
        InitiativeManager.Instance.RefreshButtons();
        ScreenManager.Instance.overlay.Hide();
    }
    private void OnRemove()
    {
        InitiativeManager.Instance.RemoveCharButton(initCharButton);
        ScreenManager.Instance.overlay.Hide();
    }
    // Use this for initialization
    void Start()
    {
        removeButton.onClick.AddListener(OnRemove);
        submitButton.onClick.AddListener(Submit);
        plusButton.onClick.AddListener(OnPlusClick);
        minusButton.onClick.AddListener(OnMinusClick);
        mainInputField.onEndEdit.AddListener(delegate { OnMainInputFiendChange(); });
        longRest.onClick.AddListener(OnLongRest);
    }
}
