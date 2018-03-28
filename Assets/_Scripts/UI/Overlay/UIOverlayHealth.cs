using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayHealth : UIOverlayMenu
{
    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.HEALTH; }
    }
    public TMP_InputField maxHealthInput;
    public TextMeshProUGUI currentHealth;
    public TMP_InputField mainInputField;
    public Button plusButton;
    public Button minusButton;
    public Button submitButton;
    public Toggle tempToggle;
    public Toggle healToggle;
    public Toggle damgToggle;
    public Button longRest;

    public ToggleGroup toggles;

    private int _inputNumberValue = 0;
    private int inputNumberValue { get { return _inputNumberValue; }
        set
        {
            if (value < 0) _inputNumberValue = 0;
            else _inputNumberValue = value;
        }
    }
    private int _inputNumberValueMaxHealth = 0;
    private int inputNumberValueMaxHealth
    {
        get { return _inputNumberValueMaxHealth; }
        set
        {
            if (value < 0) _inputNumberValueMaxHealth = 0;
            else _inputNumberValueMaxHealth = value;
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        //turn only damage on
        toggles.SetAllTogglesOff();
        damgToggle.isOn = true;

        inputNumberValue = 0;
        inputNumberValueMaxHealth = CharacterManager.Instance.GetActiveCharacter().maxHealth;
        ResetText();

    }
    private void OnMainInputFiendChange()
    {
        int newInputNumber;
        if (int.TryParse(mainInputField.text, out newInputNumber))
        {
            inputNumberValue = newInputNumber;
        }
        UpdateMainInputBoxText();
    }


    private void OnMaxHealthInputFiendChange()
    {
        int newInputNumber;
        if (int.TryParse(maxHealthInput.text, out newInputNumber))
        {
            
            inputNumberValueMaxHealth = newInputNumber;
        }
        UpdateMaxHealthBoxText();
    }
    private void OnLongRest()
    {
        CharacterManager.Instance.GetActiveCharacter().LongRest();
        ScreenManager.Instance.UpdateCharacterScreenText();
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
    private void UpdateMaxHealthBoxText()
    {
        maxHealthInput.text = inputNumberValueMaxHealth.ToString();
    }
    private void ResetText()
    {
        
        UpdateMainInputBoxText();
        UpdateMaxHealthBoxText();
        currentHealth.text = "Current Health: " + CharacterManager.Instance.GetActiveCharacter().currHealth.ToString();
    }
    private void Submit()
    {
        if (damgToggle.isOn && inputNumberValue > 0)
        {
            CharacterManager.Instance.GetActiveCharacter().DealDamage(inputNumberValue);
        }
        else if (healToggle.isOn && inputNumberValue > 0)
        {
            CharacterManager.Instance.GetActiveCharacter().currHealth += inputNumberValue;
        }
        else if (tempToggle.isOn)
        {
            CharacterManager.Instance.GetActiveCharacter().tempHealth = inputNumberValue;
        }

        //Update Character
        if (inputNumberValueMaxHealth != CharacterManager.Instance.GetActiveCharacter().maxHealth)
        {
            CharacterManager.Instance.GetActiveCharacter().maxHealth = inputNumberValueMaxHealth;

        }

        //Update Screen
        ScreenManager.Instance.UpdateCharacterScreenText();
        ScreenManager.Instance.overlay.Hide();
    }
    // Use this for initialization
    void Start () {
        submitButton.onClick.AddListener(Submit);
        plusButton.onClick.AddListener(OnPlusClick);
        minusButton.onClick.AddListener(OnMinusClick);
        mainInputField.onEndEdit.AddListener(delegate { OnMainInputFiendChange(); });
        maxHealthInput.onEndEdit.AddListener(delegate { OnMaxHealthInputFiendChange(); });
        longRest.onClick.AddListener(OnLongRest);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
