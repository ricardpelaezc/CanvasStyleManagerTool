using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TMPTextStyle : UIComponentStyle<TextMeshProUGUI>
{
    public TMP_FontAsset FontAsset;
    public Color VertexColor;
    public int FontSize;

    public override void ApplyStyle(TextMeshProUGUI text, bool includeInactive)
    {
        base.ApplyStyle(text, includeInactive);

        text.font = FontAsset;
        text.color = VertexColor;
        text.fontSize = FontSize;
    }
}
