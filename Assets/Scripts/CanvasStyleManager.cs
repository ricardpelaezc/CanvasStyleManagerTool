using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasStyleManager : MonoBehaviour
{
    [Header("Style Data")]
    public StyleData StyleData;

    [Header("To Apply")]
    public bool Apply;

    /*
    Apply All Style Setting
    bool includeInactive (UIComponents of the scene) = true/false
    ApplyStyleMode notOverridePrefabs/overridePrefabs
    */
    private void ApplyStyleToAllUIComponents<TStyle, TUIComponent>(TStyle style) 
        where TStyle : UIComponentStyle<TUIComponent>
        where TUIComponent : MonoBehaviour
    {
        TUIComponent[] listComponent = transform.GetComponentsInChildren<TUIComponent>(true); //Include inactive Yes/No

        Undo.SetCurrentGroupName("Apply Style to All Components");
        int undoGroup = Undo.GetCurrentGroup();

        foreach (var component in listComponent)
        {
            Undo.RecordObject(component, "Apply Style");

            style.ApplyStyle(component);

            if (PrefabUtility.IsPartOfAnyPrefab(component))
            {
                style.OverrideAllComponentsOnPrefab(component);
            }
        }

        Undo.CollapseUndoOperations(undoGroup);
    }

    private void Update()
    {
        if (Apply)
        {
            ApplyStyleToAllUIComponents<TMPTextStyle, TextMeshProUGUI>(StyleData.GetIndex<TMPTextStyle>(0));
            ApplyStyleToAllUIComponents<ButtonStyle, Button>(StyleData.GetIndex<ButtonStyle>(0));
            Apply = false;
        }
    }
}
