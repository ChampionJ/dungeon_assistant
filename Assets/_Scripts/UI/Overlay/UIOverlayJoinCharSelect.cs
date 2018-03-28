using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIOverlayJoinCharSelect : UIOverlayMenu
{

    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.CHARSEL; }
    }
    public Button[] charButtons;

    void Start()
    {
        for(int i = 0; i < charButtons.Length; i++)
        {
            charButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = CharacterManager.Instance.GetCharacter(i).name;
            int a = i;
            charButtons[i].onClick.AddListener(() => SelectChar(a));
        }
        
    }
    public override void OnShow()
    {
        base.OnShow();
        for (int i = 0; i < charButtons.Length; i++)
        {
            charButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = CharacterManager.Instance.GetCharacter(i).name;

        }
    }
    
    private void SelectChar(int i)
    {
        CharacterManager.Instance.SetActiveCharacter(i);
        Debug.Log("Selected Character: " + i);
        ScreenManager.Instance.UpdateJoinScreenText();
        ScreenManager.Instance.overlay.Hide();
    }
}
