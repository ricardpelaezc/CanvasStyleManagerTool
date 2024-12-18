using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;

public enum SaveMode
{
    SCENE,
    OUTERMOST_PREFAB
}

[RequireComponent(typeof(Canvas))]
[ExecuteInEditMode]
public class StylingManager : MonoBehaviour
{
    [Header("Style Data")]
    public StylingData StyleData;

    [Header("To Apply")]
    public bool IncludeInactive;
    public SaveMode Mode;
    public static bool Apply;

    /*
    Apply All Style Setting
    bool includeInactive (UIComponents of the scene) = true/false
    ApplyStyleMode notOverridePrefabs/overridePrefabs
    */
    private void ApplyStyleToAllUIComponents<TStyle, TUIComponent>(TStyle style) 
        where TStyle : UIComponentStyle<TUIComponent>
        where TUIComponent : MonoBehaviour
    {
        TUIComponent[] listComponent = transform.GetComponentsInChildren<TUIComponent>(IncludeInactive);

        Undo.SetCurrentGroupName("Apply Style to All Components");
        int undoGroup = Undo.GetCurrentGroup();

        foreach (var component in listComponent)
        {
            Undo.RecordObject(component, "Apply Style");

            style.ApplyStyle(component, IncludeInactive);

            Debug.Log(Mode);
            if (Mode == SaveMode.OUTERMOST_PREFAB)
            {
                if (PrefabUtility.IsPartOfAnyPrefab(component))
                {
                    style.OverrideAllComponentsOnPrefab(component, IncludeInactive);
                }
            }
        }

        Undo.CollapseUndoOperations(undoGroup);
    }

    private void Update()
    {
        if (Apply)
        {
            ApplyStyleToAllUIComponents <ImageStyle, Image>(StyleData.GetIndex<ImageStyle>(0));
            ApplyStyleToAllUIComponents<TMPTextStyle, TextMeshProUGUI>(StyleData.GetIndex<TMPTextStyle>(0));
            ApplyStyleToAllUIComponents<ButtonStyle, Button>(StyleData.GetIndex<ButtonStyle>(0));
            ApplyStyleToAllUIComponents<SliderStyle, Slider>(StyleData.GetIndex<SliderStyle>(0));
            ApplyStyleToAllUIComponents<ScrollbarStyle, Scrollbar>(StyleData.GetIndex<ScrollbarStyle>(0));
            ApplyStyleToAllUIComponents<ScrollRectStyle, ScrollRect>(StyleData.GetIndex<ScrollRectStyle>(0));
            ApplyStyleToAllUIComponents<ToggleStyle, Toggle>(StyleData.GetIndex<ToggleStyle>(0));
            ApplyStyleToAllUIComponents<TMPDropdownStyle, TMP_Dropdown>(StyleData.GetIndex<TMPDropdownStyle>(0));
            ApplyStyleToAllUIComponents<TMPInputFieldStyle, TMP_InputField>(StyleData.GetIndex<TMPInputFieldStyle>(0));
            Apply = false;
        }
    }
}
