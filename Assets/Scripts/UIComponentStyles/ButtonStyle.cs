using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonStyle : UIComponentStyle<Button>
{
    public ImageStyle BackgroundStyle = new ImageStyle();
    public ImageStyle TargetGraphicStyle = new ImageStyle();
    public TMPTextStyle TextStyle = new TMPTextStyle();
    public override void ApplyStyle(Button button, bool includeInactive)
    {
        base.ApplyStyle(button, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, button.targetGraphic as Image, button.transform, includeInactive);
        StylingUtility.ApplyStyleOnTexts(TextStyle, button.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Button button, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(button, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, button.targetGraphic as Image, button.transform, includeInactive);
        StylingUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, button.transform, includeInactive);
    }
}
