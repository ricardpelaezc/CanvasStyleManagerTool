using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TMPInputFieldStyle : UIComponentStyle<TMP_InputField>
{
    public ImageStyle BackgroundStyle = new ImageStyle();
    public ImageStyle TargetGraphicStyle = new ImageStyle();
    public TMPTextStyle TextStyle = new TMPTextStyle();
    public override void ApplyStyle(TMP_InputField inputField, bool includeInactive)
    {
        base.ApplyStyle(inputField, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, inputField.targetGraphic as Image, inputField.transform, includeInactive);
        StylingUtility.ApplyStyleOnTexts(TextStyle, inputField.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(TMP_InputField inputField, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(inputField, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, inputField.targetGraphic as Image, inputField.transform, includeInactive);
        StylingUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, inputField.transform, includeInactive);
    }
}

