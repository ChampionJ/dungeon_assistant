using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

[Serializable]
public enum SpriteType
{
    Background, Header, Element, Invisible, Element2, Element3, PageMarker, PageMarkerActive, GlobalBackground, OverlayBackground, OverlayHeader, OverlayElement, OverlayElement2, OverlayToggle
}
[Serializable]
public enum TextType
{
    Header, Element, ElementHeader, Field, FieldTemp
}

public class ScreenOptions {
#region variables
    private UIScreen[] screens;
    private List<TextMeshProUGUI> screenTexts;
    public UnityEvent themeChanged = new UnityEvent();
    private bool usingDarkTheme = true;

    private static Dictionary<TextType, Color32> darkTextColors = new Dictionary<TextType, Color32>
    {
        { TextType.Header,              new Color32(255,255,255,255) },
        { TextType.Element,             new Color32(255, 255, 255, 255) },
        { TextType.ElementHeader,       new Color32(255, 255, 255, 255) },
        { TextType.Field,               new Color32(255, 255, 255, 255) },
        { TextType.FieldTemp,           new Color32(255, 255, 255, 122) },
    };
    private static Dictionary<TextType, Color32> lightTextColors = new Dictionary<TextType, Color32>
    {
        { TextType.Header,              new Color32(0,0,0, 255) },
        { TextType.Element,             new Color32(0, 0, 0, 255) },
        { TextType.ElementHeader,       new Color32(0, 0, 0, 255) },
        { TextType.Field,               new Color32(0, 0, 0, 255) },
        { TextType.FieldTemp,           new Color32(0, 0, 0, 255) },
    };

    private static Dictionary<SpriteType, Color32> darkSpriteColors = new Dictionary<SpriteType, Color32>
    {
        { SpriteType.Background,        new Color32(30,30,33,255) },
        { SpriteType.GlobalBackground,  new Color32(20,20,23,255) },
        { SpriteType.Header,            new Color32(50, 50, 53, 255) },
        { SpriteType.Invisible,         new Color32(50, 50, 50, 0) },
        { SpriteType.Element,           new Color32(50, 50, 53, 255) },

        { SpriteType.OverlayBackground, new Color32(30, 30, 33, 255) },
        { SpriteType.OverlayElement,    new Color32(50, 50, 53, 255) },
        { SpriteType.OverlayElement2,   new Color32(60, 60, 63, 255) },
        { SpriteType.OverlayToggle,   new Color32(50, 50, 53, 255) },
        { SpriteType.OverlayHeader,     new Color32(50, 50, 53, 255) },

        { SpriteType.Element2,          new Color32(60, 60, 63, 255) },
        { SpriteType.Element3,          new Color32(70, 70, 73, 255) },
        { SpriteType.PageMarker,        new Color32(255, 255, 255, 100) },
        { SpriteType.PageMarkerActive,  new Color32(255, 255, 255, 255) }
    };

    private static Dictionary<SpriteType, Color32> lightSpriteColors = new Dictionary<SpriteType, Color32>
    {
        { SpriteType.Background,        new Color32(230,230,230,255) },
        { SpriteType.GlobalBackground,  new Color32(210,210,210,255) },
        { SpriteType.Header,            new Color32(255, 255, 255, 255) },
        { SpriteType.Invisible,         new Color32(50, 50, 50, 0) },
        { SpriteType.Element,           new Color32(255, 255, 255, 255) },

        { SpriteType.OverlayBackground, new Color32(230, 230, 230, 255) },
        { SpriteType.OverlayElement,    new Color32(255, 255, 255, 255) },
        { SpriteType.OverlayElement2,   new Color32(200, 200, 200, 255) },
        { SpriteType.OverlayToggle,   new Color32(255, 255, 255, 150) },
        { SpriteType.OverlayHeader,     new Color32(255, 255, 255, 255) },

        { SpriteType.Element2,          new Color32(210, 210, 210, 255) },
        { SpriteType.Element3,          new Color32(230, 230, 230, 255) },
        { SpriteType.PageMarker,        new Color32(150, 150, 150, 100) },
        { SpriteType.PageMarkerActive,  new Color32(150, 150, 150, 255) }
    };

    public Dictionary<SpriteType, Color32> spriteColors = darkSpriteColors;

    public Dictionary<TextType, Color32> textColors = darkTextColors;
#endregion
    private void SwitchToLight()
    {
        usingDarkTheme = false;
        spriteColors = lightSpriteColors;
        textColors = lightTextColors;
        themeChanged.Invoke();
    }
    private void SwitchToDark()
    {
        usingDarkTheme = true;
        spriteColors = darkSpriteColors;
        textColors = darkTextColors;
        themeChanged.Invoke();
    }
    /// <summary>
    /// Switch between light and dark theme
    /// </summary>
    public void ToggleTheme()
    {
        if (usingDarkTheme)
        {
            SwitchToLight();
        }
        else
        {
            SwitchToDark();
        }
    }
    
	public ScreenOptions()
    {
        //set phone to never sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
