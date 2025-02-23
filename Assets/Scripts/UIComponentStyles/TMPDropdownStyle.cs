using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TMPDropdownStyle : UIComponentStyle<TMP_Dropdown>
{
    public ImageStyle BackgroundStyle;
    public ImageStyle TargetGraphicStyle;
    public TMPTextStyle TextStyle;

    public TMPDropdownStyle(StylingData stylingData)
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
