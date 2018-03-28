using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsScreen : UIScreen
{
    public Button toggleTheme;
    public Button backButton;

    public override ScreenType screenType
    {
        get { return ScreenType.OPTIONS; }
    }
    protected override void LateAwake()
    {
        backButton.onClick.AddListener(OnBackButton);
        toggleTheme.onClick.AddListener(OnThemeToggle);
    }
    private void OnThemeToggle()
    {
        ScreenManager.Instance.options.ToggleTheme();
    }
    public override void OnBackButton()
    {
        ScreenManager.Instance.SwitchToMainMenu();
    }



}
