using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUIButton : Button {
    protected TextMeshProUGUI updatingText;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        updatingText = transform.GetComponent<TextMeshProUGUI>();
        
        onClick.AddListener(OnClickEvent);
    }
    /// <summary>
    /// Callback for onclick event
    /// </summary>
    protected virtual void OnClickEvent()
    {
        
    }
    /// <summary>
    /// Callback to update text contents
    /// </summary>
    public virtual void UpdateText() { }

}
