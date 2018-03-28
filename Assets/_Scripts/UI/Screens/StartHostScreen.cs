using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StartHostScreen : UIScreen {
    public override ScreenType screenType
    {
        get { return ScreenType.HOST; }
    }
    public Button backButton;
    public TextMeshProUGUI statusText;
    public bool readyToSwitch = false;


    protected override void ScreenActivated()
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(OnBackButton);



        StartCoroutine(SendHost());
        
    }
    public override void OnBackButton()
    {
        
        SocketManager.Instance.StopSocket();
        ScreenManager.Instance.SwitchToMainMenu();
    }
    private IEnumerator SendHost()
    {
        SocketManager.Instance.StartSocket();
        yield return new WaitForSeconds(2f);
        SocketManager.Instance.SendHost();
        
    }
    void Update()
    {
        if (readyToSwitch)
        {
            readyToSwitch = false;
            ScreenManager.Instance.SwitchToDMScreen();
        }
        
    }
}
