using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : UIScreen {
    public override ScreenType screenType
    {
        get { return ScreenType.MAIN; }
    }
    public Button characterSelect;
    public Button joinButton;
    public Button hostButton;
    public Button optionButton;
    public Button copyrightButton;
    
    protected override void LateAwake() {
        characterSelect.onClick.AddListener(ScreenManager.Instance.SwitchToCharacterSelectScreen);
        joinButton.onClick.AddListener(ScreenManager.Instance.SwitchToJoinScreen);
        hostButton.onClick.AddListener(ScreenManager.Instance.SwitchToHostScreen);
        copyrightButton.onClick.AddListener(() => { Application.OpenURL("http://championprogramming.com"); });
        optionButton.onClick.AddListener(ScreenManager.Instance.SwitchToOptionsScreen);
    }
}
