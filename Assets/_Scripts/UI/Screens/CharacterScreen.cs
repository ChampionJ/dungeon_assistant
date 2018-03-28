using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.UI.Extensions;

public class CharacterScreen : UIScreen
{
    public override ScreenType screenType
    {
        get { return ScreenType.CHAR; }
    }
    public Button backButton;
    public TMP_InputField notesField;
    private HorizontalScrollSnap scroller;

    private CharacterUIButton[] characterUIButtons;
    /// <summary>
    /// Update before screen comes into view
    /// </summary>
    protected override void ScreenActivated()
    {
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(OnBackButton);
        notesField.onEndEdit.RemoveAllListeners();
        notesField.onEndEdit.AddListener(delegate { OnEndEditNotes(); });


        UpdateStats();
        
    }
    public override void OnBackButton()
    {
        if (SocketManager.Instance.IsClientRunningAndConnectedToRoom())
        {
            SocketManager.Instance.StopSocket();
        }
        scroller.ChangePage(0);
        ScreenManager.Instance.SwitchToLastScene();
    }
    protected override void LateAwake()
    {
        base.LateAwake();

        //setup buttons
        characterUIButtons = transform.GetComponentsInChildren<CharacterUIButton>();
        scroller = transform.GetComponentInChildren<HorizontalScrollSnap>();
    }
    private void OnEndEditNotes()
    {
        CharacterManager.Instance.GetActiveCharacter().notes = notesField.text;
        CharacterManager.Instance.SaveActiveCharacterDataToFile();
    }
    public override void UpdateText()
    {
        base.UpdateText();
        UpdateStats();
    }
    public void UpdateStats()
    {
        foreach (CharacterUIButton b in characterUIButtons)
        {
            b.UpdateText();
        }
        notesField.text = CharacterManager.Instance.GetActiveCharacter().notes;

        //write data to file
        CharacterManager.Instance.SaveActiveCharacterDataToFile();

        //send update to server if connected
        if (SocketManager.Instance.IsClientRunningAndConnectedToRoom())
        {
            SocketManager.Instance.SendPlayerUpdate();
        }
    }
    void Update()
    {
        
    }

}
