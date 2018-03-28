using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIOverlayName : UIOverlayMenu
{
    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.NAME; }
    }
    public TMP_InputField field;
    
    void Start()
    {
        
        field.onEndEdit.AddListener(delegate { UpdateName(); });
        field.onSubmit.AddListener(delegate { UpdateName(); ScreenManager.Instance.overlay.Hide(); });
    }
    public override void OnShow()
    {
        base.OnShow();
        field.text = CharacterManager.Instance.GetActiveCharacter().name;
    }
    private void UpdateName()
    {
        if (field.text != "")
        {
            
            CharacterManager.Instance.GetActiveCharacter().name = field.text;
            //CharacterManager.Instance.SaveActiveCharacterDataToFile();
            ScreenManager.Instance.UpdateCharacterScreenText();
        }
    }
    
}
