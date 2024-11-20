using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TMPDropdownStyle : UIComponentStyle<TMP_Dropdown>
{
    public ImageStyle BackgroundStyle = new ImageStyle();
    public ImageStyle TargetGraphicStyle = new ImageStyle();
    public TMPTextStyle TextStyle = new TMPTextStyle();
    public override void ApplyStyle(TMP_Dropdown dropdown, bool includeInactive)
    {
        base.ApplyStyle(dropdown, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, dropdown.targetGraphic as Image, dropdown.transform, includeInactive);
        StylingUtility.ApplyStyleOnTexts(TextStyle, dropdown.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(TMP_Dropdown dropdown, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(dropdown, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, dropdown.targetGraphic as Image, dropdown.transform, includeInactive);
        StylingUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, dropdown.transform, includeInactive);
    }
}
