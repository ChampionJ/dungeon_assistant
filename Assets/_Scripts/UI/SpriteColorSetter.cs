using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SpriteColorSetter : MonoBehaviour
{
    public SpriteType spriteType;
    private Image sprite;
    // Use this for initialization
    void Start()
    {
        sprite = transform.GetComponent<Image>();
        UpdateColor();
        ScreenManager.Instance.options.themeChanged.AddListener(UpdateColor);
    }

    public void UpdateColor()
    {
        sprite.color = ScreenManager.Instance.options.spriteColors[spriteType];
    }
    public void UpdateType(SpriteType newType)
    {
        spriteType = newType;
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
