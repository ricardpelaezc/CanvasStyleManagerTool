using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InputFieldStyle : UIComponentStyle<TMP_InputField>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;
    public TextStyle TextStyle;

    public InputFieldStyle(StyleData styleData)
    {
        BackgroundStyle = new ImageStyle(styleData);
        TargetGraphicStyle = new ImageStyle(styleData);
        TextStyle = new TextStyle(styleData);

        StyleUtility.InputFieldStyleSetDefault(styleData);
    }
   
    public override void ApplyStyle(TMP_InputField inputField, bool includeInactive)
    {
        base.ApplyStyle(inputField, includeInactive);
        StyleUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, inputField.targetGraphic as Image, inputField.transform, includeInactive);
        StyleUtility.ApplyStyleOnTexts(TextStyle, inputField.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(TMP_InputField inputField, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(inputField, includeInactive);
        StyleUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, inputField.targetGraphic as Image, inputField.transform, includeInactive);
        StyleUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, inputField.transform, includeInactive);
    }
}

