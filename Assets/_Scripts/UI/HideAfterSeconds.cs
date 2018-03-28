using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class HideAfterSeconds : MonoBehaviour {
    public float timeToHide = 1;
	// Use this for initialization
	void Awake () {
        GetComponent<Image>().DOFade(0, timeToHide).OnComplete(Kill).SetEase(Ease.InCubic);
	}
    private void Kill()
    {
        GameObject.Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
