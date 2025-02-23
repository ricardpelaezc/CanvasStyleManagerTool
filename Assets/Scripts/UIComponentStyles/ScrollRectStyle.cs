using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScrollRectStyle : UIComponentStyle<ScrollRect>
{
    public ImageStyle ContentStyle;

    public ScrollRectStyle(StylingData stylingData)
    {
        ContentStyle = new ImageStyle(stylingData);

        stylingData.onDefaultImageTargetColorChanged += ContentStyle.Color.SetDefault;
    }

    public void InitDelegates(StylingData stylingData)
    {
        stylingData.onDefaultImageTargetColorChanged -= ContentStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged += ContentStyle.Color.SetDefault;

        ContentStyle.Color.SetDefault(stylingData.DefaultImageTargetColor);
    }

    public void ClearDelegates(StylingData stylingData)
    {
        stylingData.onDefaultImageTargetColorChanged -= ContentStyle.Color.SetDefault;
    }

    public override void ApplyStyle(ScrollRect scrollRect, bool includeInactive)
    {
        base.ApplyStyle(scrollRect, includeInactive);

        ContentStyle.ApplyStyle(scrollRect.viewport.GetComponent<Image>(), includeInactive); //not ideal
    }

    public override void OverrideAllComponentsOnPrefab(ScrollRect scrollRect, bool includeInactive)
    {
        base.ApplyStyle(scrollRect, includeInactive);

        StylingUtility.OverrideComponentOnPrefab(scrollRect.viewport.GetComponent<Image>());
    }
}
