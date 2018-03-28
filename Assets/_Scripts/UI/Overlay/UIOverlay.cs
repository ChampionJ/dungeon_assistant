using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public enum OverlayMenuType
{
    CHARSEL, ATTACK, NAME, HEALTH, SKILL, MESSAGE, SAVESTAT, STAT, AC, PROF, INITCHARACTER, INITNEWCHAR, INITCHARHEALTH
}
public class UIOverlay : MonoBehaviour {
    
    protected RectTransform rectTransform;
    private RectTransform originButton;
    private float tweenInOutTime = .5f;
    private bool _isDisplayed = false;
    public bool isDisplayed { get { return _isDisplayed; } }

    private Dictionary<OverlayMenuType, UIOverlayMenu> menus;
    private Transform fill;
    //private Transform fillButton;

    // Use this for initialization
    void Start () {
        rectTransform = GetComponent<RectTransform>();
        //rectTransform.DOMoveX(Screen.width * 2, 0);
        fill = transform.Find("Fill");
        fill.transform.Find("FillButton").GetComponent<Button>().onClick.AddListener(Hide);

        menus = new Dictionary<OverlayMenuType, UIOverlayMenu>();
        foreach (UIOverlayMenu m in fill.GetComponentsInChildren<UIOverlayMenu>())
        {
            menus.Add(m.menuType, m);
            //Debug.Log("added " + m.menuType + "to dic");
        }
        Hide();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }
    private void SetMenuToShow(OverlayMenuType type)
    {
        
        foreach(KeyValuePair<OverlayMenuType, UIOverlayMenu> menu in menus)
        {
            if(menu.Value.menuType != type)
                menu.Value.gameObject.SetActive(false);
            else menu.Value.gameObject.SetActive(true);
        }
    }

    public void Show(OverlayMenuType type)
    {
        _isDisplayed = true;
        fill.gameObject.SetActive(true);
        SetMenuToShow(type);
        menus[type].OnShow();

        //rectTransform.DOMoveX(Screen.width / 2, tweenInOutTime).SetEase(Ease.OutCubic).Pause();

    }
    public void ShowInitiativeCharacterOverlay(InitiativeCharacterButton initViewportButton)
    {
        UIOverlayInitiativeCharacter menu = menus[OverlayMenuType.INITCHARACTER] as UIOverlayInitiativeCharacter;
        menu.initCharButton = initViewportButton;
        Show(OverlayMenuType.INITCHARACTER);
    }
    public void ShowInitiativeCharacterHealthOverlay(InitiativeCharacterButton initViewportButton)
    {
        UIOverlayInitCharHealth menu = menus[OverlayMenuType.INITCHARHEALTH] as UIOverlayInitCharHealth;
        menu.initCharButton = initViewportButton;
        Show(OverlayMenuType.INITCHARHEALTH);
    }
    public void ShowStatOverlay(StatType statType)
    {
        
        UIOverlayStat menu = menus[OverlayMenuType.STAT] as UIOverlayStat;
        menu.type = statType;
        Show(OverlayMenuType.STAT);
        
    }
    public void ShowSaveStatOverlay(StatType statType)
    {
        UIOverlaySaveStat menu = menus[OverlayMenuType.SAVESTAT] as UIOverlaySaveStat;
        menu.type = statType;
        Show(OverlayMenuType.SAVESTAT);
    }
    public void ShowSkillOverlay(string skillname)
    {
        UIOverlaySkill menu = menus[OverlayMenuType.SKILL] as UIOverlaySkill;
        menu.skillName = skillname;
        Show(OverlayMenuType.SKILL);
    }
    public void ShowAttackOverlay(int attackNum)
    {
        UIOverlayAttack menu = menus[OverlayMenuType.ATTACK] as UIOverlayAttack;
        menu.attackNum = attackNum;
        Show(OverlayMenuType.ATTACK);
    }
    public void ShowMessageOverlay(string message)
    {
        UIOverlayMessage menu = menus[OverlayMenuType.MESSAGE] as UIOverlayMessage;
        menu.SetMessageText(message);
        Show(OverlayMenuType.MESSAGE);
    }

    public void Hide()
    {
        fill.gameObject.SetActive(false);
        _isDisplayed = false;
        //rectTransform.DOMoveX(Screen.width * 2, tweenInOutTime).SetEase(Ease.OutCubic).Pause();
    }
    


}
