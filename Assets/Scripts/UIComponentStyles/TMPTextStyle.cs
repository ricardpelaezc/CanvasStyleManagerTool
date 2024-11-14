using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TMPTextStyle : UIComponentStyle<TextMeshProUGUI>
{
    public TMP_FontAsset Font;
    public Color Color;
    public int FontSize;

    public override void ApplyStyle(TextMeshProUGUI text)
    {
        text.font = Font;
        text.color = Color;
        text.fontSize = FontSize;
    }

    public override void OverrideAllComponentsOnPrefab(TextMeshProUGUI text)
    {
        CanvasStyleUtility.OverrideComponentOnPrefab(text);
    }
}
