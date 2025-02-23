using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScrollbarStyle : UIComponentStyle<Scrollbar>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;

    public ScrollbarStyle(StylingData stylingData)
    {
        BackgroundStyle = new ImageStyle(stylingData);
        TargetGraphicStyle = new ImageStyle(stylingData);

        InitDelegates(stylingData);
    }

    public void InitDelegates(StylingData stylingData)
    {
        stylingData.onDefaultImageBackgroundColorChanged -= BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageBackgroundColorChanged += BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged -= TargetGraphicStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged += TargetGraphicStyle.Color.SetDefault;

        BackgroundStyle.Color.SetDefault(stylingData.DefaultImageBackgroundColor);
        TargetGraphicStyle.Color.SetDefault(stylingData.DefaultImageTargetColor);
    }

    public void ClearDelegates(StylingData stylingData)
    {
        stylingData.onDefaultImageBackgroundColorChanged -= BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged -= TargetGraphicStyle.Color.SetDefault;
    }

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
