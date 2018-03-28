using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

public class UIOverlayMessage : UIOverlayMenu {

    public override OverlayMenuType menuType
    {
        get { return OverlayMenuType.MESSAGE; }
    }
    public TextMeshProUGUI messageText;
    public Button submit;

    private void Start()
    {
        submit.onClick.AddListener(OnSubmit);
    }
    private void OnSubmit()
    {
        ScreenManager.Instance.overlay.Hide();
    }
    public override void OnShow()
    {
        base.OnShow();
       
    }
    public void SetMessageText(string text)
    {
        messageText.text = text;
    }

}
