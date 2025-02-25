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

    public ScrollbarStyle(StyleData styleData)
    {
        BackgroundStyle = new ImageStyle(styleData);
        TargetGraphicStyle = new ImageStyle(styleData);

        StyleUtility.ScrollbarStyleSetDefault(styleData);
    }

    public void InitDelegates(StyleData stylingData)
    {
        stylingData.onDefaultImageBackgroundColorChanged -= BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageBackgroundColorChanged += BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged -= TargetGraphicStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged += TargetGraphicStyle.Color.SetDefault;

        BackgroundStyle.Color.SetDefault(stylingData.DefaultImageBackgroundColor);
        TargetGraphicStyle.Color.SetDefault(stylingData.DefaultImageTargetColor);
    }

    public void ClearDelegates(StyleData stylingData)
    {
        stylingData.onDefaultImageBackgroundColorChanged -= BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged -= TargetGraphicStyle.Color.SetDefault;
    }

    public override void ApplyStyle(Scrollbar scrollbar, bool includeInactive)
    {
        base.ApplyStyle(scrollbar, includeInactive);
        StyleUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, scrollbar.targetGraphic as Image, scrollbar.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Scrollbar scrollbar, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(scrollbar, includeInactive);
        StyleUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, scrollbar.targetGraphic as Image, scrollbar.transform, includeInactive);
    }
}
