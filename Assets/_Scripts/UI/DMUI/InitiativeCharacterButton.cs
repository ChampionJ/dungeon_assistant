using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeCharacterButton : Button
{
    [SerializeField]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI initiativeNumText;
    public TextMeshProUGUI acText;
    public TextMeshProUGUI currHealthText;
    //public CharacterDataMini characterDataMini;
    public InitiativeCharacter initCharacter;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        nameText = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        initiativeNumText = transform.Find("Init/Init").GetComponent<TextMeshProUGUI>();
        acText = transform.Find("ACImg/text").GetComponent<TextMeshProUGUI>();
        currHealthText = transform.Find("HealthImg/CurrHealth").GetComponent<TextMeshProUGUI>();
    }
    protected override void Start()
    {
        base.Start();
        
        onClick.AddListener(OnClickEvent);
        
    }
    protected virtual void OnClickEvent()
    {
        //Debug.Log("Clicked: " + transform.parent.name + "/" + gameObject.name);
        if (initCharacter.isClientCharacter)
        {
            ScreenManager.Instance.overlay.ShowInitiativeCharacterOverlay(this);
        }
        else
        {
            ScreenManager.Instance.overlay.ShowInitiativeCharacterHealthOverlay(this);
        }
    }
    public virtual void UpdateText()
    {
        nameText.text = initCharacter.characterDataMini.name;
        initiativeNumText.text = initCharacter.initiativeNumber.ToString();
        acText.text = initCharacter.characterDataMini.ac.ToString();
        currHealthText.text = initCharacter.characterDataMini.currentHealth.ToString();
    }
}
