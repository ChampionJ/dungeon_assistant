using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class LaunchManager : MonoBehaviour {
    public Slider loadbar;
	// Use this for initialization
	void Start () {
        Sequence tweenSequence = DOTween.Sequence();
        tweenSequence.Insert(0, loadbar.DOValue(.2f, .5f));
        tweenSequence.InsertCallback(.2f, StartLoad);
        tweenSequence.Insert(0, loadbar.DOValue(1f, 1.5f));
        tweenSequence.OnComplete(OnLoaded);
        
    }
    private void StartLoad()
    {
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
    }
    private void OnLoaded()
    {
        SceneManager.UnloadSceneAsync("Launch");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
