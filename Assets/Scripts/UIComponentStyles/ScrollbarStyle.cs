using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScrollbarStyle : UIComponentStyle<Scrollbar>
{
    public ImageStyle BackgroundStyle = new ImageStyle();
    public ImageStyle TargetGraphicStyle = new ImageStyle();
    public override void ApplyStyle(Scrollbar scrollbar, bool includeInactive)
    {
        base.ApplyStyle(scrollbar, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, scrollbar.targetGraphic as Image, scrollbar.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Scrollbar scrollbar, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(scrollbar, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, scrollbar.targetGraphic as Image, scrollbar.transform, includeInactive);
    }
}
