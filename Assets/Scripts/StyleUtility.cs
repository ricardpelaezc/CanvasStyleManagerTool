using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StyleUtility : MonoBehaviour
{
    public static void OverrideComponentOnPrefab(Component component)
    {
        // Get the outermost prefab root
        GameObject outermostPrefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(component);
        if (outermostPrefabRoot == null)
        {
            Debug.LogWarning("Failed to find the outermost prefab root.");
            return;
        }

        // Apply changes to the outermost prefab
        string assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(outermostPrefabRoot);
        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogError("Failed to get the prefab asset path.");
            return;
        }

        // Apply the override to the prefab
        PrefabUtility.ApplyObjectOverride(component, assetPath, InteractionMode.AutomatedAction);

        // Mark as dirty and save the changes
        EditorUtility.SetDirty(outermostPrefabRoot);

        AssetDatabase.SaveAssets(); // Might Cause Problems
        AssetDatabase.Refresh();
    }
    public static void ApplyStyleSelectable(ImageStyle targetGraphicStyle, ImageStyle backgroundStyle, Image targetGraphic, Transform transform, bool includeInactive)
    {
        Image[] listImage = transform.GetComponentsInChildren<Image>(includeInactive);
        foreach (var image in listImage)
        {
            backgroundStyle.ApplyStyle(image, includeInactive);
        }

        targetGraphicStyle.ApplyStyle(targetGraphic, includeInactive);
    }
    public static void OverrideAllSelectableComponentsOnPrefab(ImageStyle targetGraphicStyle, ImageStyle backgroundStyle, Image targetGraphic, Transform transform, bool includeInactive)
    {
        Image[] listImage = transform.GetComponentsInChildren<Image>(includeInactive);
        foreach (var image in listImage)
        {
            backgroundStyle.OverrideAllComponentsOnPrefab(image, includeInactive);
        }
    }
    public static void ApplyStyleOnTexts(TextStyle tMPTextStyle, Transform transform, bool includeInactive)
    {
        TextMeshProUGUI[] listText = transform.GetComponentsInChildren<TextMeshProUGUI>(includeInactive);
        foreach (var text in listText)
        {
            tMPTextStyle.ApplyStyle(text, includeInactive);
        }
    }
    public static void OverrideAllTextsComponentsOnPrefab(TextStyle tMPTextStyle, Transform transform, bool includeInactive)
    {
        TextMeshProUGUI[] listText = transform.GetComponentsInChildren<TextMeshProUGUI>(includeInactive);
        foreach (var text in listText)
        {
            tMPTextStyle.OverrideAllComponentsOnPrefab(text, includeInactive);
        }
    }
    public static void ButtonSetDefault(StyleData styleData)
    {
        styleData.Return<ButtonStyle>().BackgroundStyle.Color.SetDefault(styleData.DefaultImageBackgroundColor);
        styleData.Return<ButtonStyle>().TargetGraphicStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
        styleData.Return<ButtonStyle>().TextStyle.FontAsset.SetDefault(styleData.DefaultTextStyle.FontAsset);
        styleData.Return<ButtonStyle>().TextStyle.FontSize.SetDefault(styleData.DefaultTextStyle.FontSize);
        styleData.Return<ButtonStyle>().TextStyle.VertexColor.SetDefault(styleData.DefaultTextStyle.VertexColor);
    }
    public static void ScrollbarStyleSetDefault(StyleData styleData)
    {
        styleData.Return<ScrollbarStyle>().BackgroundStyle.Color.SetDefault(styleData.DefaultImageBackgroundColor);
        styleData.Return<ScrollbarStyle>().TargetGraphicStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
    }
    public static void ScrollRectStyleSetDefault(StyleData styleData)
    {
        styleData.Return<ScrollRectStyle>().ContentStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
    }
    public static void SliderStyleSetDefault(StyleData styleData)
    {
        styleData.Return<SliderStyle>().BackgroundStyle.Color.SetDefault(styleData.DefaultImageBackgroundColor);
        styleData.Return<SliderStyle>().TargetGraphicStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
    }
    public static void DropdownStyleSetDefault(StyleData styleData)
    {
        styleData.Return<DropdownStyle>().BackgroundStyle.Color.SetDefault(styleData.DefaultImageBackgroundColor);
        styleData.Return<DropdownStyle>().TargetGraphicStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
        styleData.Return<DropdownStyle>().TextStyle.FontAsset.SetDefault(styleData.DefaultTextStyle.FontAsset);
        styleData.Return<DropdownStyle>().TextStyle.FontSize.SetDefault(styleData.DefaultTextStyle.FontSize);
        styleData.Return<DropdownStyle>().TextStyle.VertexColor.SetDefault(styleData.DefaultTextStyle.VertexColor);
    }
    public static void InputFieldStyleSetDefault(StyleData styleData)
    {
        styleData.Return<InputFieldStyle>().BackgroundStyle.Color.SetDefault(styleData.DefaultImageBackgroundColor);
        styleData.Return<InputFieldStyle>().TargetGraphicStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
        styleData.Return<InputFieldStyle>().TextStyle.FontAsset.SetDefault(styleData.DefaultTextStyle.FontAsset);
        styleData.Return<InputFieldStyle>().TextStyle.FontSize.SetDefault(styleData.DefaultTextStyle.FontSize);
        styleData.Return<InputFieldStyle>().TextStyle.VertexColor.SetDefault(styleData.DefaultTextStyle.VertexColor);
    }
    public static void ToggleStyleSetDefault(StyleData styleData)
    {
        styleData.Return<ToggleStyle>().BackgroundStyle.Color.SetDefault(styleData.DefaultImageBackgroundColor);
        styleData.Return<ToggleStyle>().TargetGraphicStyle.Color.SetDefault(styleData.DefaultImageTargetColor);
    }
    public static void StyleSetDefault(StyleData styleData)
    {
        ButtonSetDefault(styleData);
        ScrollbarStyleSetDefault(styleData);
        ScrollRectStyleSetDefault(styleData);
        SliderStyleSetDefault(styleData);
        DropdownStyleSetDefault(styleData);
        InputFieldStyleSetDefault(styleData);
        ToggleStyleSetDefault(styleData);
    }
    public static void DefaultImageBackgroundChanged(StyleData styleData, Color color)
    {
        styleData.Return<ButtonStyle>().BackgroundStyle.Color.SetDefault(color);
        styleData.Return<ScrollbarStyle>().BackgroundStyle.Color.SetDefault(color);
        styleData.Return<SliderStyle>().BackgroundStyle.Color.SetDefault(color);
        styleData.Return<DropdownStyle>().BackgroundStyle.Color.SetDefault(color);
        styleData.Return<InputFieldStyle>().BackgroundStyle.Color.SetDefault(color);
        styleData.Return<ToggleStyle>().TargetGraphicStyle.Color.SetDefault(color);
    }
    public static void DefaultImageTargetColorChanged(StyleData styleData, Color color)
    {
        styleData.Return<ButtonStyle>().TargetGraphicStyle.Color.SetDefault(color);
        styleData.Return<ScrollbarStyle>().TargetGraphicStyle.Color.SetDefault(color);
        styleData.Return<ScrollRectStyle>().ContentStyle.Color.SetDefault(color);
        styleData.Return<SliderStyle>().TargetGraphicStyle.Color.SetDefault(color);
        styleData.Return<DropdownStyle>().TargetGraphicStyle.Color.SetDefault(color);
        styleData.Return<InputFieldStyle>().TargetGraphicStyle.Color.SetDefault(color);
        styleData.Return<ToggleStyle>().TargetGraphicStyle.Color.SetDefault(color);
    }
    public static void DefaultTextFontAssetChanged(StyleData styleData, TMP_FontAsset fontAsset)
    {
        styleData.Return<ButtonStyle>().TextStyle.FontAsset.SetDefault(fontAsset);
        styleData.Return<DropdownStyle>().TextStyle.FontAsset.SetDefault(fontAsset);
        styleData.Return<InputFieldStyle>().TextStyle.FontAsset.SetDefault(fontAsset);
    }
    public static void DefaultTextFontSizeChanged(StyleData styleData, int fontSize)
    {
        styleData.Return<ButtonStyle>().TextStyle.FontSize.SetDefault(fontSize);
        styleData.Return<DropdownStyle>().TextStyle.FontSize.SetDefault(fontSize);
        styleData.Return<InputFieldStyle>().TextStyle.FontSize.SetDefault(fontSize);
    }
    public static void DefaultTextVertexColorChanged(StyleData styleData, Color vertexColor)
    {
        styleData.Return<ButtonStyle>().TextStyle.VertexColor.SetDefault(vertexColor);
        styleData.Return<DropdownStyle>().TextStyle.VertexColor.SetDefault(vertexColor);
        styleData.Return<InputFieldStyle>().TextStyle.VertexColor.SetDefault(vertexColor);
    }

    public static void RevertStyleOverride(ImageStyle imageStyle)
    {

    }
    public static void RevertStyleOverride(TextStyle tMPTextStyle)
    {

    }
    public static void RevertPropertyOverride(SerializedProperty instanceProperty)
    {

    }
}
