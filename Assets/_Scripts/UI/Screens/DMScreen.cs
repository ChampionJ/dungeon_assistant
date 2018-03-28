using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DMScreen : UIScreen
{
    public override ScreenType screenType
    {
        get { return ScreenType.DM; }
    }
    public TextMeshProUGUI roomKeyText;
    public Button backButton;
    public Button addButton;
    private bool shouldSendKeepAlive = false; 

    protected override void LateAwake()
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(OnBackButton);
        addButton.onClick.RemoveAllListeners();
        addButton.onClick.AddListener(OnAddButton);
    }
    private void OnAddButton()
    {
        ScreenManager.Instance.overlay.Show(OverlayMenuType.INITNEWCHAR);
    }
    public override void OnBackButton()
    {
        shouldSendKeepAlive = false;
        SocketManager.Instance.StopSocket();
        ScreenManager.Instance.SwitchToMainMenu();
    }
    protected override void ScreenActivated()
    {
        roomKeyText.text = SocketManager.Instance.roomKey;
        shouldSendKeepAlive = true;
        StartCoroutine(KeepAlive());
    }
    private IEnumerator KeepAlive()
    {

        while (shouldSendKeepAlive)
        {
            Debug.Log("start of keep alive coroutine");
            
            SocketManager.Instance.SendKeepHostAlive();
            yield return new WaitForSecondsRealtime(600);
        }
        Debug.Log("end of keep alive coroutine");
        
    }
    private void OnApplicationFocus(bool focus)
    {
        if (shouldSendKeepAlive)
        {
            Debug.Log("Lost/Gained Focus, sending keepalive");

            SocketManager.Instance.SendKeepHostAlive();
        }
    }
    void Update()
    {
        
    }
}
