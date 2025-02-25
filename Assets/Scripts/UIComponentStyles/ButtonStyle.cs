using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonStyle : UIComponentStyle<Button>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;
    public TextStyle TextStyle;

    public ButtonStyle(StyleData styleData)
    {
        BackgroundStyle = new ImageStyle(styleData);
        TargetGraphicStyle = new ImageStyle(styleData);
        TextStyle = new TextStyle(styleData);

        StyleUtility.ButtonSetDefault(styleData);
    }

    public override void ApplyStyle(Button button, bool includeInactive)
    {
        base.ApplyStyle(button, includeInactive);
        StyleUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, button.targetGraphic as Image, button.transform, includeInactive);
        StyleUtility.ApplyStyleOnTexts(TextStyle, button.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Button button, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(button, includeInactive);
        StyleUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, button.targetGraphic as Image, button.transform, includeInactive);
        StyleUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, button.transform, includeInactive);
    }
}
