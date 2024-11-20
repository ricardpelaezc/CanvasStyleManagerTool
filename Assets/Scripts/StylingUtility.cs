using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StylingUtility : MonoBehaviour
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
    public static void ApplyStyleOnTexts(TMPTextStyle tMPTextStyle, Transform transform, bool includeInactive)
    {
        TextMeshProUGUI[] listText = transform.GetComponentsInChildren<TextMeshProUGUI>(includeInactive);
        foreach (var text in listText)
        {
            tMPTextStyle.ApplyStyle(text, includeInactive);
        }
    }
    public static void OverrideAllTextsComponentsOnPrefab(TMPTextStyle tMPTextStyle, Transform transform, bool includeInactive)
    {
        TextMeshProUGUI[] listText = transform.GetComponentsInChildren<TextMeshProUGUI>(includeInactive);
        foreach (var text in listText)
        {
            tMPTextStyle.OverrideAllComponentsOnPrefab(text, includeInactive);
        }
    }

    public static void RevertStyleOverride(ImageStyle imageStyle)
    {

    }
    public static void RevertStyleOverride(TMPTextStyle tMPTextStyle)
    {

    }
    public static void RevertPropertyOverride(SerializedProperty instanceProperty)
    {

    }
}
