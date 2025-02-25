using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderStyle : UIComponentStyle<Slider>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;

    public SliderStyle(StyleData styleData)
    {
        BackgroundStyle = new ImageStyle(styleData);
        TargetGraphicStyle = new ImageStyle(styleData);

        StyleUtility.SliderStyleSetDefault(styleData);
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

    public override void ApplyStyle(Slider slider, bool includeInactive)
    {
        base.ApplyStyle(slider, includeInactive);
        StyleUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, slider.targetGraphic as Image, slider.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Slider slider, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(slider, includeInactive);
        StyleUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, slider.targetGraphic as Image, slider.transform, includeInactive);
    }
}
