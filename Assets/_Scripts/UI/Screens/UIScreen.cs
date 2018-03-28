using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIScreen : MonoBehaviour {
    public virtual ScreenType screenType
    {
        get { return ScreenType.MAIN; }
    }
    protected RectTransform rectTransform;
    private float tweenInOutTime = .5f;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOMoveX(Screen.width * 2, 0);
        //rectTransform.transform.position = new Vector3(Screen.width * 2, 0, 0);
        LateAwake();
    }
    /// <summary>
    /// Update at the end of Awake
    /// </summary>
    protected virtual void LateAwake() { }
    /// <summary>
    /// Back Button event
    /// </summary>
    public virtual void OnBackButton() { }
    /// <summary>
    /// Update before screen comes into view
    /// </summary>
    protected virtual void ScreenActivated() { }

    public virtual void UpdateText() { }

    /// <summary>
    /// Get tween in
    /// </summary>
    /// <returns>Tween</returns>
    public Tween TweenIn()
    {
        ScreenActivated();
        return rectTransform.DOMoveX(Screen.width / 2, tweenInOutTime).SetEase(Ease.OutCubic).Pause();
        
    }
    /// <summary>
    /// Get tween out
    /// </summary>
    /// <returns>Tween</returns>
    public Tween TweenOut()
    {
        return rectTransform.DOMoveX(Screen.width *2, tweenInOutTime).SetEase(Ease.OutCubic).Pause(); 
    }

}
