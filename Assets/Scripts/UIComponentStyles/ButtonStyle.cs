using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonStyle : UIComponentStyle<Button>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;
    public TMPTextStyle TextStyle;

    public ButtonStyle(StylingData stylingData)
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

    public override void ApplyStyle(Button button, bool includeInactive)
    {
        base.ApplyStyle(button, includeInactive);
        StylingUtility.ApplyStyleSelectable(TargetGraphicStyle, BackgroundStyle, button.targetGraphic as Image, button.transform, includeInactive);
        StylingUtility.ApplyStyleOnTexts(TextStyle, button.transform, includeInactive);
    }

    public override void OverrideAllComponentsOnPrefab(Button button, bool includeInactive)
    {
        base.OverrideAllComponentsOnPrefab(button, includeInactive);
        StylingUtility.OverrideAllSelectableComponentsOnPrefab(TargetGraphicStyle, BackgroundStyle, button.targetGraphic as Image, button.transform, includeInactive);
        StylingUtility.OverrideAllTextsComponentsOnPrefab(TextStyle, button.transform, includeInactive);
    }
}
