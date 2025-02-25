using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StyleDataEditor : EditorWindow
{
    [MenuItem("Tools/Style Data Editor")]
    public static void ShowExample()
    {
        GetWindow<StyleDataEditor>("Canvas Style");
    }

    private const string _keyEditorPrefsPath = "SelectedStylingDataPath";
    private static StyleData _editedSD;

    private bool _collapseUndoNeeded;
    private float _timeCollapseUndo = 2f;
    private float _timerCollapseUndo;


    private static int _tabIndex;
    private static string[] _tabOptions = new string[]
{
                    "Default", "Button", "Scrollbar", "Scroll Rect",
                    "Slider", "Toggle", "Dropdown", "Input Field"
};

    private void OnEnable()
    {
        LoadData();
    }

    private void OnDisable()
    {
    }

    void LoadData()
    {
        string selectedStylingDataPath = EditorPrefs.GetString(_keyEditorPrefsPath, "");
        if (!string.IsNullOrEmpty(selectedStylingDataPath))
        {
            _editedSD = AssetDatabase.LoadAssetAtPath<StyleData>(selectedStylingDataPath);
        }

        if (_editedSD == null)
        {
            Debug.LogWarning("No StylingData found. Create one and assign it.");
            return;
        }
    }
    private void Update()
    {
        if (_collapseUndoNeeded) _timerCollapseUndo += Time.deltaTime;
        if (_collapseUndoNeeded && _timerCollapseUndo >= _timeCollapseUndo)
        {
            StyleManager.CollapseObjectUndo();
            _timerCollapseUndo = 0;
            _collapseUndoNeeded = false;
        }

    }
    public void OnGUI()
    {
        if (!_editedSD)
        {
            _editedSD = (StyleData) EditorGUILayout.ObjectField("Style Data", _editedSD, typeof(StyleData), false);
            if (_editedSD)
            {
                string valueEdtiorPrefsPath = AssetDatabase.GetAssetPath(_editedSD);
                EditorPrefs.SetString(_keyEditorPrefsPath, valueEdtiorPrefsPath);
            }
        }
        else
        {
            EditorGUI.BeginChangeCheck();
            GUILayout.Space(10);
            _editedSD = (StyleData)EditorGUILayout.ObjectField("Style Data", _editedSD, typeof(StyleData), false);
            if (EditorGUI.EndChangeCheck())
            {
                string valueEdtiorPrefsPath = AssetDatabase.GetAssetPath(_editedSD);
                EditorPrefs.SetString(_keyEditorPrefsPath, valueEdtiorPrefsPath);
            }
            GUILayout.Space(10);

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            _tabIndex = GUILayout.SelectionGrid(_tabIndex, _tabOptions, 4, GUILayout.ExpandWidth(true));
            GUILayout.EndHorizontal();
            GUILayout.Space(10);

            EditorGUI.BeginChangeCheck();
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
                // Set prefab
                _timerCollapseUndo = 0;
                _collapseUndoNeeded = true;

                StyleManager.Apply = true; //Applying everything to much too fast

                EditorUtility.SetDirty(_editedSD);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
    private void RenderDefaultStyle()
    {
        GUILayout.Label("Default Background Graphic Style");
        _editedSD.DefaultImageBackgroundColor = EditorGUILayout.ColorField("Color", _editedSD.DefaultImageBackgroundColor);

        GUILayout.Label("Default Target Graphic Style");
        _editedSD.DefaultImageTargetColor = EditorGUILayout.ColorField("Color", _editedSD.DefaultImageTargetColor);

        GUILayout.Label("Default Text Style");
        _editedSD.DefaultTextFontAsset = 
            (TMP_FontAsset)EditorGUILayout.ObjectField("Font Asset", _editedSD.DefaultTextFontAsset, typeof(TMP_FontAsset), false);

        _editedSD.DefaultTextFontSize = 
            EditorGUILayout.IntField("Font Size", _editedSD.DefaultTextFontSize);

        _editedSD.DefaultTextVertexColor = 
            EditorGUILayout.ColorField("Vertex Color", _editedSD.DefaultTextVertexColor);
    }
    private void RenderImageStyle(ImageStyle imageStyle)
    {
        Rect fieldRect0 = EditorGUILayout.GetControlRect();

        EditorStyles.label.fontStyle = (imageStyle.Color.IsOverridden) ? FontStyle.Bold : FontStyle.Normal;
        imageStyle.Color.Value = EditorGUI.ColorField(fieldRect0, "Color", imageStyle.Color.Value);

        EditorStyles.label.fontStyle = FontStyle.Normal;

        if (imageStyle.Color.IsOverridden) HandleRightClick(fieldRect0, () => { imageStyle.Color.Revert(); });
    }

    private void RenderTMPTextStyle(TextStyle tMPTextStyle)
    {
        Rect fieldRect0 = EditorGUILayout.GetControlRect();
        Rect fieldRect1 = EditorGUILayout.GetControlRect();
        Rect fieldRect2 = EditorGUILayout.GetControlRect();


        EditorStyles.label.fontStyle = (tMPTextStyle.FontAsset.IsOverridden) ? FontStyle.Bold : FontStyle.Normal;
        EditorStyles.objectField.fontStyle = (tMPTextStyle.FontAsset.IsOverridden) ? FontStyle.Bold : FontStyle.Normal;
        tMPTextStyle.FontAsset.Value = (TMP_FontAsset)EditorGUI.ObjectField(fieldRect0, "Font Asset", tMPTextStyle.FontAsset.Value, typeof(TMP_FontAsset), false);

        EditorStyles.label.fontStyle = (tMPTextStyle.FontSize.IsOverridden) ? FontStyle.Bold : FontStyle.Normal;
        EditorStyles.textField.fontStyle = (tMPTextStyle.FontSize.IsOverridden) ? FontStyle.Bold : FontStyle.Normal;
        tMPTextStyle.FontSize.Value = EditorGUI.IntField(fieldRect1, "Font Size", tMPTextStyle.FontSize.Value);

        EditorStyles.label.fontStyle = (tMPTextStyle.VertexColor.IsOverridden) ? FontStyle.Bold : FontStyle.Normal;
        tMPTextStyle.VertexColor.Value = EditorGUI.ColorField(fieldRect2, "Vertex Color", tMPTextStyle.VertexColor.Value);

        EditorStyles.label.fontStyle = FontStyle.Normal;
        EditorStyles.textField.fontStyle = FontStyle.Normal;
        EditorStyles.objectField.fontStyle = FontStyle.Normal;

        if (tMPTextStyle.FontAsset.IsOverridden) HandleRightClick(fieldRect0, () => { tMPTextStyle.FontAsset.Revert(); });
        if (tMPTextStyle.FontSize.IsOverridden) HandleRightClick(fieldRect1, () => { tMPTextStyle.FontSize.Revert(); });
        if (tMPTextStyle.VertexColor.IsOverridden) HandleRightClick(fieldRect2, () => { tMPTextStyle.VertexColor.Revert(); });
    }
    private void RenderButtonStyles()
    {
        ButtonStyle tempButtonStyle = _editedSD.Return<ButtonStyle>();
        GUILayout.Label("Background Style");
        RenderImageStyle(tempButtonStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempButtonStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Text Style");
        RenderTMPTextStyle(tempButtonStyle.TextStyle);
        _editedSD.Set(tempButtonStyle);
        //if (GUI.changed) {} Maybe
    }
    private void RenderScrollbarStyles()
    {
        ScrollbarStyle tempScrollbarStyle = _editedSD.Return<ScrollbarStyle>();
        GUILayout.Label("Background Style");
        RenderImageStyle(tempScrollbarStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempScrollbarStyle.TargetGraphicStyle);
        _editedSD.Set(tempScrollbarStyle);
    }
    private void RenderScrollRectStyles()
    {
        ScrollRectStyle tempScrollRectStyle = _editedSD.Return<ScrollRectStyle>();
        GUILayout.Label("Content Style");
        RenderImageStyle(tempScrollRectStyle.ContentStyle);
        _editedSD.Set(tempScrollRectStyle);
    }
    private void RenderSliderStyles()
    {
        SliderStyle tempSliderStyle = _editedSD.Return<SliderStyle>();
        GUILayout.Label("Background Style");
        RenderImageStyle(tempSliderStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempSliderStyle.TargetGraphicStyle);
        _editedSD.Set(tempSliderStyle);
    }
    private void RenderToggleStyles()
    {
        ToggleStyle tempToggleStyle = _editedSD.Return<ToggleStyle>();
        GUILayout.Label("Background Style");
        RenderImageStyle(tempToggleStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempToggleStyle.TargetGraphicStyle);
        _editedSD.Set(tempToggleStyle);
    }
    private void RenderTMPDropdownStyles()
    {
        DropdownStyle tempDropdownStyle = _editedSD.Return<DropdownStyle>();
        GUILayout.Label("Background Style");
        RenderImageStyle(tempDropdownStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempDropdownStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Text Style");
        RenderTMPTextStyle(tempDropdownStyle.TextStyle);
        _editedSD.Set(tempDropdownStyle);
    }
    private void RenderTMPInputFieldStyles()
    {
        InputFieldStyle tempInputFieldStyle = _editedSD.Return<InputFieldStyle>();
        GUILayout.Label("Background Style");
        RenderImageStyle(tempInputFieldStyle.BackgroundStyle);
        GUILayout.Space(10);
        GUILayout.Label("Target Graphic Style");
        RenderImageStyle(tempInputFieldStyle.TargetGraphicStyle);
        GUILayout.Space(10);
        GUILayout.Label("Text Style");
        RenderTMPTextStyle(tempInputFieldStyle.TextStyle);
        _editedSD.Set(tempInputFieldStyle);
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
