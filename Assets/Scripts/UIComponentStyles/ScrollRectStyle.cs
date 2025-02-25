using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScrollRectStyle : UIComponentStyle<ScrollRect>
{
    public ImageStyle ContentStyle;

    public ScrollRectStyle(StyleData styleData)
    {
        ContentStyle = new ImageStyle(styleData);

        StyleUtility.ScrollRectStyleSetDefault(styleData);
    }

    public override void ApplyStyle(ScrollRect scrollRect, bool includeInactive)
    {
        base.ApplyStyle(scrollRect, includeInactive);

        ContentStyle.ApplyStyle(scrollRect.viewport.GetComponent<Image>(), includeInactive); //not ideal
    }

    public override void OverrideAllComponentsOnPrefab(ScrollRect scrollRect, bool includeInactive)
    {
        base.ApplyStyle(scrollRect, includeInactive);

        StyleUtility.OverrideComponentOnPrefab(scrollRect.viewport.GetComponent<Image>());
    }
}
