using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StylingDataEditor : EditorWindow
{
    [MenuItem("Tools/Canvas Style Manager")]
    public static void ShowExample()
    {
        GetWindow<StylingDataEditor>("Canvas Style");
    }

    private const string _keyEditorPrefsPath = "SelectedStylingDataPath";
    private static StylingData _selectedStylingData;

    private static int _tabIndex;
    private static string[] _tabOptions = new string[]
{
                    "Default", "Button", "Scrollbar", "Scroll Rect",
                    "Slider", "Toggle", "Dropdown", "Input Field"
};

    private void OnEnable()
    {
        //Load the style data
    }

    private void OnDisable()
    {
        //...
    }

    void LoadData()
    {
        string selectedStylingDataPath = EditorPrefs.GetString(_keyEditorPrefsPath, null);
        bool isStylingDataSelected = selectedStylingDataPath == null ? false : true;
        _selectedStylingData = isStylingDataSelected ?
            _selectedStylingData = AssetDatabase.LoadAssetAtPath(selectedStylingDataPath, typeof(StylingData)) as StylingData :
            null;
    }

    public void OnGUI()
    {
        if (!_selectedStylingData)
        {
            _selectedStylingData = (StylingData) EditorGUILayout.ObjectField("Style Data", _selectedStylingData, typeof(StylingData), false);
            if (_selectedStylingData)
            {
                string valueEdtiorPrefsPath = AssetDatabase.GetAssetPath(_selectedStylingData);
                EditorPrefs.SetString(_keyEditorPrefsPath, valueEdtiorPrefsPath);
            }
        }
        else
        {
            EditorGUI.BeginChangeCheck();

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            _tabIndex = GUILayout.SelectionGrid(_tabIndex, _tabOptions, 4, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            switch (_tabIndex)
            {
                case 0: RenderDefaultStyle(); break;
                case 1: RenderButtonStyles(); break;
                case 2: RenderScrollbarStyles(); break;
                case 3: RenderScrollRectStyles(); break;
                case 4: RenderSliderStyles(); break;
                case 5: RenderToggleStyles(); break;
                case 6: RenderTMPDropdownStyles(); break;
                case 7: RenderTMPInputFieldStyles(); break;
            }
            GUILayout.Space(10);
            GUILayout.EndVertical();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_selectedStylingData);
                AssetDatabase.SaveAssets();
                StylingManager.Apply = true; //Applying everything to much too fast
            }
        }
    }
    private void RenderDefaultStyle()
    {
        //Create a new fance defaultStyle with all the necessary
        ImageStyle targetGraphicStyle = _selectedStylingData.GetIndex<ImageStyle>(0);
        TMPTextStyle textStyle = _selectedStylingData.GetIndex<TMPTextStyle>(0);

        GUILayout.Label("Default Target Graphic Style");
        targetGraphicStyle.Color = EditorGUILayout.ColorField("Color", targetGraphicStyle.Color);
        //When default changed all the nonOverriden element inside the style data with the same property change

        GUILayout.Label("Default Text Style");
        textStyle.FontAsset = (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", textStyle.FontAsset, typeof(TMP_FontAsset), false);
        textStyle.FontSize = EditorGUILayout.IntField("Font Size", textStyle.FontSize);
        textStyle.VertexColor = EditorGUILayout.ColorField("Vertex Color", textStyle.VertexColor);
    }
    private void RenderImageStyle(ImageStyle imageStyle)
    {
        ImageStyle defaultImageStyle = _selectedStylingData.GetIndex<ImageStyle>(0);

        Rect fieldRect0 = EditorGUILayout.GetControlRect();

        bool isFieldDefault0 = imageStyle.Color == defaultImageStyle.Color;

        EditorStyles.label.fontStyle = (isFieldDefault0) ? FontStyle.Normal : FontStyle.Bold;
        imageStyle.Color = EditorGUI.ColorField(fieldRect0, "Color", imageStyle.Color);

        EditorStyles.label.fontStyle = FontStyle.Normal;

        if (isFieldDefault0) HandleRightClick(fieldRect0, () => { imageStyle.Color = defaultImageStyle.Color; });
    }

    private void RenderTMPTextStyle(TMPTextStyle tMPTextStyle)
    {
        TMPTextStyle defaultTMPTextStyle = _selectedStylingData.GetIndex<TMPTextStyle>(0);

        Rect fieldRect0 = EditorGUILayout.GetControlRect();
        Rect fieldRect1 = EditorGUILayout.GetControlRect();
        Rect fieldRect2 = EditorGUILayout.GetControlRect();

        bool isOverriden0 = false;
        bool isOverriden1 = false;
        bool isOverriden2 = false;

        EditorStyles.label.fontStyle = (isOverriden0) ? FontStyle.Bold : FontStyle.Normal;
        EditorStyles.objectField.fontStyle = (isOverriden0) ? FontStyle.Bold : FontStyle.Normal;
        tMPTextStyle.FontAsset = (TMP_FontAsset)EditorGUI.ObjectField(fieldRect0, "Font Asset", tMPTextStyle.FontAsset, typeof(TMP_FontAsset), false);

        EditorStyles.label.fontStyle = (isOverriden1) ? FontStyle.Bold : FontStyle.Normal;
        EditorStyles.textField.fontStyle = (isOverriden1) ? FontStyle.Bold : FontStyle.Normal;
        tMPTextStyle.FontSize = EditorGUI.IntField(fieldRect1, "Font Size", tMPTextStyle.FontSize);

        EditorStyles.label.fontStyle = (isOverriden2) ? FontStyle.Bold : FontStyle.Normal;
        tMPTextStyle.VertexColor = EditorGUI.ColorField(fieldRect2, "Vertex Color", tMPTextStyle.VertexColor);

        EditorStyles.label.fontStyle = FontStyle.Normal;
        EditorStyles.textField.fontStyle = FontStyle.Normal;
        EditorStyles.objectField.fontStyle = FontStyle.Normal;

        if (isOverriden0) HandleRightClick(fieldRect0, () => { tMPTextStyle.FontAsset = defaultTMPTextStyle.FontAsset; });
        if (isOverriden1) HandleRightClick(fieldRect1, () => { tMPTextStyle.FontSize = defaultTMPTextStyle.FontSize; });
        if (isOverriden2) HandleRightClick(fieldRect2, () => { tMPTextStyle.VertexColor = defaultTMPTextStyle.VertexColor; });
    }
    private void RenderButtonStyles()
    {
        ButtonStyle tempButtonStyle = _selectedStylingData.GetIndex<ButtonStyle>(0);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempButtonStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Background Style");
        RenderImageStyle(tempButtonStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Text Style");
        RenderTMPTextStyle(tempButtonStyle.TextStyle);
        _selectedStylingData.SetIndex(0, tempButtonStyle);
        //if (GUI.changed) {} Maybe
    }
    private void RenderScrollbarStyles()
    {
        ScrollbarStyle tempScrollbarStyle = _selectedStylingData.GetIndex<ScrollbarStyle>(0);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempScrollbarStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Background Style");
        RenderImageStyle(tempScrollbarStyle.BackgroundStyle);
        _selectedStylingData.SetIndex(0, tempScrollbarStyle);
    }
    private void RenderScrollRectStyles()
    {
        ScrollRectStyle tempScrollRectStyle = _selectedStylingData.GetIndex<ScrollRectStyle>(0);
        GUILayout.Label("Content Style");
        RenderImageStyle(tempScrollRectStyle.ContentStyle);
        _selectedStylingData.SetIndex(0, tempScrollRectStyle);
    }
    private void RenderSliderStyles()
    {
        SliderStyle tempSliderStyle = _selectedStylingData.GetIndex<SliderStyle>(0);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempSliderStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Background Style");
        RenderImageStyle(tempSliderStyle.BackgroundStyle);
        _selectedStylingData.SetIndex(0, tempSliderStyle);
    }
    private void RenderToggleStyles()
    {
        ToggleStyle tempToggleStyle = _selectedStylingData.GetIndex<ToggleStyle>(0);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempToggleStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Background Style");
        RenderImageStyle(tempToggleStyle.BackgroundStyle);
        _selectedStylingData.SetIndex(0, tempToggleStyle);
    }
    private void RenderTMPDropdownStyles()
    {
        TMPDropdownStyle tempDropdownStyle = _selectedStylingData.GetIndex<TMPDropdownStyle>(0);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempDropdownStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Background Style");
        RenderImageStyle(tempDropdownStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Text Style");
        RenderTMPTextStyle(tempDropdownStyle.TextStyle);
        _selectedStylingData.SetIndex(0, tempDropdownStyle);
    }
    private void RenderTMPInputFieldStyles()
    {
        TMPInputFieldStyle tempInputFieldStyle = _selectedStylingData.GetIndex<TMPInputFieldStyle>(0);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempInputFieldStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Background Style");
        RenderImageStyle(tempInputFieldStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Text Style");
        RenderTMPTextStyle(tempInputFieldStyle.TextStyle);
        _selectedStylingData.SetIndex(0, tempInputFieldStyle);
    }

    private void HandleRightClick(Rect rect, System.Action onRevert)
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.ContextClick && rect.Contains(currentEvent.mousePosition))
        {
            // Add a custom context menu option while keeping Unity's default behavior
            GenericMenu menu = new GenericMenu();

            // Add the custom "Revert" option
            menu.AddItem(new GUIContent("Revert"), false, () =>
            {
                onRevert?.Invoke();
            });

            // Show the custom menu
            menu.ShowAsContext();
        }
    }
}
