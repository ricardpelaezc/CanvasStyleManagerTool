using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TMPInputFieldStyle : UIComponentStyle<TMP_InputField>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;
    public TMPTextStyle TextStyle;

    public TMPInputFieldStyle(StylingData stylingData)
    {
        BackgroundStyle = new ImageStyle(stylingData);
        TargetGraphicStyle = new ImageStyle(stylingData);
        TextStyle = new TMPTextStyle(stylingData);

        InitDelegates(stylingData);
    }
    public void InitDelegates(StylingData stylingData)
    {
        stylingData.onDefaultImageBackgroundColorChanged -= BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageBackgroundColorChanged += BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged -= TargetGraphicStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged += TargetGraphicStyle.Color.SetDefault;

        stylingData.onDefaultTMPTextFontAssetChanged -= TextStyle.FontAsset.SetDefault;
        stylingData.onDefaultTMPTextFontAssetChanged += TextStyle.FontAsset.SetDefault;
        stylingData.onDefaultTMPTextFontSizeChanged -= TextStyle.FontSize.SetDefault;
        stylingData.onDefaultTMPTextFontSizeChanged += TextStyle.FontSize.SetDefault;
        stylingData.onDefaultTMPTextVertexColorChanged -= TextStyle.VertexColor.SetDefault;
        stylingData.onDefaultTMPTextVertexColorChanged += TextStyle.VertexColor.SetDefault;

        BackgroundStyle.Color.SetDefault(stylingData.DefaultImageBackgroundColor);
        TargetGraphicStyle.Color.SetDefault(stylingData.DefaultImageTargetColor);

        TextStyle.FontAsset.SetDefault(stylingData.DefaultTMPTextStyle.FontAsset);
        TextStyle.FontSize.SetDefault(stylingData.DefaultTMPTextStyle.FontSize);
        TextStyle.VertexColor.SetDefault(stylingData.DefaultTMPTextStyle.VertexColor);
    }

    public void ClearDelegates(StylingData stylingData)
    {
        stylingData.onDefaultImageBackgroundColorChanged -= BackgroundStyle.Color.SetDefault;
        stylingData.onDefaultImageTargetColorChanged -= TargetGraphicStyle.Color.SetDefault;

        stylingData.onDefaultTMPTextFontAssetChanged -= TextStyle.FontAsset.SetDefault;
        stylingData.onDefaultTMPTextFontSizeChanged -= TextStyle.FontSize.SetDefault;
        stylingData.onDefaultTMPTextVertexColorChanged -= TextStyle.VertexColor.SetDefault;
    }

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

