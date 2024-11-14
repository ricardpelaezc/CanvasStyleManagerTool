using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanvasStyleUtility : MonoBehaviour
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
}
