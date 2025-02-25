using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class TextStyle : UIComponentStyle<TextMeshProUGUI> //Base Class
{
    public OverrideableStyleProperty<TMP_FontAsset> FontAsset = new OverrideableStyleProperty<TMP_FontAsset>();
    public OverrideableStyleProperty<int> FontSize = new OverrideableStyleProperty<int>();
    public OverrideableStyleProperty<Color> VertexColor = new OverrideableStyleProperty<Color>();

    public TextStyle(StyleData stylingData)
    {
        FontAsset = new OverrideableStyleProperty<TMP_FontAsset>();
        FontSize = new OverrideableStyleProperty<int>();
        VertexColor = new OverrideableStyleProperty<Color>();
    }

    public override void ApplyStyle(TextMeshProUGUI text, bool includeInactive)
    {
        base.ApplyStyle(text, includeInactive);

        text.font = FontAsset.Value;
        text.color = VertexColor.Value;
        text.fontSize = FontSize.Value;
    }
}