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

    public ToggleStyle(StylingData stylingData)
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
