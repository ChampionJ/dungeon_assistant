using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Champion;
using DG.Tweening;
public enum ScreenType
{
    MAIN, JOIN, HOST, DM, CHARSELECT, CHAR, OPTIONS
}
public class ScreenManager : MonoBehaviourSingleton<ScreenManager> {
    
    private Dictionary<ScreenType, UIScreen> screens;
    private UIScreen activeScreen;
    private UIScreen lastScreen = null;
    private bool switchingScene = false;
    public ScreenOptions options;
    public UIOverlay overlay
    {
        get;
        private set;
    }

    protected override void SingletonAwake()
    {
        overlay = FindObjectOfType<UIOverlay>();
        SetupScreenDic();
        
        options = new ScreenOptions();
    }
    
    // Use this for initialization
    void Start () {
        SwitchToScreen(ScreenType.MAIN);
        
    }
    void Update()
    {
        //Handle touching back button on android
        if (Input.GetKeyDown(KeyCode.Escape) && !overlay.isDisplayed)
        {
            activeScreen.OnBackButton();
        }
    }
    private void SetupScreenDic()
    {
        screens = new Dictionary<ScreenType, UIScreen>();
        foreach (UIScreen s in GameObject.FindObjectsOfType(typeof(UIScreen)))
        {
            screens.Add(s.screenType, s);

        }
    }

    /// <summary>
    /// Main screen switching function
    /// </summary>
    /// <param name="screen">Screen to switch to</param>
    private void SwitchToScreen(ScreenType screen)
    {
        if (switchingScene) return;
        //make sure screen exists in dictionary
        UIScreen screenHolder;
        screens.TryGetValue(screen, out screenHolder);

        if (screenHolder != null)
        {
            switchingScene = true;
            Sequence seq = DOTween.Sequence();
            if (activeScreen != null)
                seq.Append(activeScreen.TweenOut());
            seq.Append(screens[screen].TweenIn());

            seq.OnComplete(() => OnSwitchScreenEnd(screen));
            seq.Play();
        }

    }
    /// <summary>
    /// Screen Switch OnComplete Callback
    /// </summary>
    /// <param name="screen">Screen switched to</param>
    private void OnSwitchScreenEnd(ScreenType screen)
    {
        lastScreen = activeScreen;
        activeScreen = screens[screen];
        switchingScene = false;
    }
#region Scene Switching Functions
    /// <summary>
    /// Switch to the last screen that was up
    /// </summary>
    public void SwitchToLastScene()
    {
        SwitchToScreen(lastScreen.screenType);
    }
    public void SwitchToDMScreen()
    {
        SwitchToScreen(ScreenType.DM);
    }
    public void SwitchToCharacterSelectScreen()
    {
        SwitchToScreen(ScreenType.CHARSELECT);
    }
    public void SwitchToJoinScreen()
    {
        SwitchToScreen(ScreenType.JOIN);
    }
    public void SwitchToHostScreen()
    {
        SwitchToScreen(ScreenType.HOST);
    }
    public void SwitchToOptionsScreen()
    {
        SwitchToScreen(ScreenType.OPTIONS);
    }
    public void SwitchToMainMenu()
    {
        SwitchToScreen(ScreenType.MAIN);
    }
    public void SwitchToCharacterScreen()
    {
        SwitchToScreen(ScreenType.CHAR);
    }

    #endregion
    public void UpdateCharacterScreenText()
    {
        screens[ScreenType.CHAR].UpdateText();
    }
    public void UpdateStartHost()
    {
        StartHostScreen hs = screens[ScreenType.HOST] as StartHostScreen;
        hs.readyToSwitch = true;
    }
    public void UpdateJoinScreenText()
    {
        screens[ScreenType.JOIN].UpdateText();
    }
    public void UpdateJoinWithResponse(bool b)
    {
        JoinScreen js = screens[ScreenType.JOIN] as JoinScreen;
        js.roomKeyIsGood = b;
        js.OnGetRoomResponse(b);
        
    }
}
