using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectScreen : UIScreen {
    public override ScreenType screenType
    {
        get { return ScreenType.CHARSELECT; }
    }
    public Button[] charButtons;
    public Button backButton;

    protected override void LateAwake()
    {
        backButton.onClick.AddListener(OnBackButton);
        for (int i = 0; i < charButtons.Length; i++)
        {
            int a = i;
            charButtons[i].onClick.AddListener(() => SelectChar(a));
            
        }



    }
    public override void OnBackButton()
    {
        ScreenManager.Instance.SwitchToMainMenu();
    }
    protected override void ScreenActivated()
    {
        for (int i = 0; i < charButtons.Length; i++)
        {
            
            charButtons[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = CharacterManager.Instance.GetCharacter(i).name;
        }
    }
    private void SelectChar(int i)
    {
        CharacterManager.Instance.SetActiveCharacter(i);
        Debug.Log("Selected Character: " + i);
        ScreenManager.Instance.SwitchToCharacterScreen();

    }

	
	// Update is called once per frame
	void Update () {
        
    }
}
