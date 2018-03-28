using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JoinScreen : UIScreen {
    public override ScreenType screenType
    {
        get { return ScreenType.JOIN; }
    }
    public Button backButton;
    public Button joinButton;
    public Button charSelectButton;
    private TextMeshProUGUI charSelectButtonText;
    public TMP_InputField roomKeyInput;
    public bool roomKeyIsGood = false;
    private bool readyToSwitch = false;
    private bool roomKeyError = false;

    protected override void LateAwake()
    {
        charSelectButtonText = charSelectButton.GetComponentInChildren<TextMeshProUGUI>();
        backButton.onClick.AddListener(OnBackButton);
        joinButton.onClick.AddListener(OnJoinButton);
        charSelectButton.onClick.AddListener(() => ScreenManager.Instance.overlay.Show(OverlayMenuType.CHARSEL));
        roomKeyInput.onSubmit.AddListener(delegate { CheckIfFieldsEntered(); });
    }
    public override void OnBackButton()
    {
        ScreenManager.Instance.SwitchToMainMenu();
    }
    private void OnJoinButton()
    {
        SocketManager.Instance.StartSocket();
        SocketManager.Instance.roomKey = roomKeyInput.text.ToLower();
        SocketManager.Instance.SendRoomQuery();

        
    }
    public void OnGetRoomResponse(bool b)
    {
        if (b)
        {
            readyToSwitch = true;
        }
        else
        {
            roomKeyError = true;
        }
    }
    protected override void ScreenActivated()
    {
     
        Reset();

    }
    private void Reset()
    {
        charSelectButtonText.text = "Select a Character";
        joinButton.interactable = false;
        readyToSwitch = false;
        roomKeyIsGood = false;
        roomKeyInput.text = "";

    }
    public override void UpdateText()
    {
        charSelectButtonText.text = CharacterManager.Instance.GetActiveCharacter().name;
        CheckIfFieldsEntered();
    }
    private void CheckIfFieldsEntered()
    {
        if(charSelectButtonText.text != "Select a Character" && roomKeyInput.text.Length == 4)
        {
            joinButton.interactable = true;
        }
    }
    
    // Update is called once per frame
    void Update () {
        if (readyToSwitch)
        {
            ScreenManager.Instance.SwitchToCharacterScreen();
            Reset();
        }
        if (roomKeyError)
        {
            ScreenManager.Instance.overlay.ShowMessageOverlay("No dungeon found with that code");
            roomKeyError = false;
        }


    }
}
