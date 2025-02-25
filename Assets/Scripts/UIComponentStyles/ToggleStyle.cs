using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ToggleStyle : UIComponentStyle<Toggle>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;

    public ToggleStyle(StyleData styleData)
    {
        BackgroundStyle = new ImageStyle(styleData);
        TargetGraphicStyle = new ImageStyle(styleData);

        StyleUtility.ToggleStyleSetDefault(styleData);
    }

    public override void ApplyStyle(Toggle toggle, bool includeInactive)
    {
        base.ApplyStyle(toggle, includeInactive);
        StyleUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, toggle.targetGraphic as Image, toggle.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Toggle toggle, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(toggle, includeInactive);
        StyleUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, toggle.targetGraphic as Image, toggle.transform, includeInactive);
    }
}
