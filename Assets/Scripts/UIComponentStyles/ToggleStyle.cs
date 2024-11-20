using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ToggleStyle : UIComponentStyle<Toggle>
{
    public ImageStyle BackgroundStyle = new ImageStyle();
    public ImageStyle TargetGraphicStyle = new ImageStyle();
    public override void ApplyStyle(Toggle toggle, bool includeInactive)
    {
        base.ApplyStyle(toggle, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, toggle.targetGraphic as Image, toggle.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Toggle toggle, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(toggle, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, toggle.targetGraphic as Image, toggle.transform, includeInactive);
    }
}
