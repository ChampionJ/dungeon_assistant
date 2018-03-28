using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempHealthButton : CharacterUIButton {


    public override void UpdateText()
    {
        updatingText.text = CharacterManager.Instance.GetActiveCharacter().tempHealth.ToString();
    }
}
