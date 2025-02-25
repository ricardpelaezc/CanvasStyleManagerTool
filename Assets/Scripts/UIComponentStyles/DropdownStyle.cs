using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DropdownStyle : UIComponentStyle<TMP_Dropdown>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;
    public TextStyle TextStyle;

    public DropdownStyle(StyleData styleData)
    {
        BackgroundStyle = new ImageStyle(styleData);
        TargetGraphicStyle = new ImageStyle(styleData);
        TextStyle = new TextStyle(styleData);

        StyleUtility.DropdownStyleSetDefault(styleData);
    }
    public override void ApplyStyle(TMP_Dropdown dropdown, bool includeInactive)
    {
        base.ApplyStyle(dropdown, includeInactive);
        StyleUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, dropdown.targetGraphic as Image, dropdown.transform, includeInactive);
        StyleUtility.ApplyStyleOnTexts(TextStyle, dropdown.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(TMP_Dropdown dropdown, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(dropdown, includeInactive);
        StyleUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, dropdown.targetGraphic as Image, dropdown.transform, includeInactive);
        StyleUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, dropdown.transform, includeInactive);
    }
}
