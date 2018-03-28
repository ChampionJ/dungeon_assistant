using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayInitNewCharacter : UIOverlayMenu
{

    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.INITNEWCHAR; }
    }
    public TMP_InputField nameField;
    public TMP_InputField healthField;
    public TMP_InputField acField;
    public TMP_InputField initiative;
    public Button submit;

    private void CheckEverythingFilledOut()
    {
        if (nameField.text != "" &&
            healthField.text != "" &&
            acField.text != "" &&
            initiative.text != "")
        {
            submit.interactable = true;
        }
    }

    void Start()
    {
        submit.onClick.AddListener(OnSubmit);
        nameField.onEndEdit.AddListener(delegate { CheckEverythingFilledOut(); });
        healthField.onEndEdit.AddListener(delegate { CheckEverythingFilledOut(); });
        acField.onEndEdit.AddListener(delegate { CheckEverythingFilledOut(); });
        initiative.onEndEdit.AddListener(delegate { CheckEverythingFilledOut(); });
    }
    private void OnSubmit()
    {
        CharacterDataMini newCharDat = new CharacterDataMini();
        newCharDat.maxHealth = Mathf.Abs(int.Parse(healthField.text));
        newCharDat.ac = Mathf.Abs(int.Parse(acField.text));
        newCharDat.name = nameField.text;
        newCharDat.currentHealth = newCharDat.maxHealth;
        newCharDat.uniqueID = "host";
        newCharDat.tempHealth = 0;

        InitiativeCharacter newInitChar = new InitiativeCharacter(newCharDat);
        newInitChar.initiativeNumber = Mathf.Abs(int.Parse(initiative.text));

        newInitChar.isClientCharacter = false;


        InitiativeManager.Instance.AddCharacter(newInitChar);
        ScreenManager.Instance.overlay.Hide();
    }
    public override void OnShow()
    {
        base.OnShow();
        submit.interactable = false;
        nameField.text = "";
        healthField.text = "";
        acField.text = "";
        initiative.text = "";

    }

}
