using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFill : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        RectTransform t = GetComponent<RectTransform>();
        t.offsetMax = new Vector2(Screen.width / 2, Screen.height / 2);
        t.offsetMin = new Vector2(-Screen.width / 2, -Screen.height / 2);
    }
}
