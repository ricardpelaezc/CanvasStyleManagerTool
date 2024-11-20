using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderStyle : UIComponentStyle<Slider>
{
    public ImageStyle BackgroundStyle = new ImageStyle();
    public ImageStyle TargetGraphicStyle = new ImageStyle();
    public override void ApplyStyle(Slider slider, bool includeInactive)
    {
        base.ApplyStyle(slider, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, slider.targetGraphic as Image, slider.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Slider slider, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(slider, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, slider.targetGraphic as Image, slider.transform, includeInactive);
    }
}
