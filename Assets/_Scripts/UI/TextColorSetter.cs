using UnityEngine;
using System.Collections;
using TMPro;

public class TextColorSetter : MonoBehaviour
{
    public TextType textType;
    private TextMeshProUGUI text;
    // Use this for initialization
    void Start()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        UpdateColor();
        ScreenManager.Instance.options.themeChanged.AddListener(UpdateColor);
    }

    public void UpdateColor()
    {
        text.color = ScreenManager.Instance.options.textColors[textType];
    }
}
