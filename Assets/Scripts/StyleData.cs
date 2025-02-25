using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#region DEFAULT_IMAGE_TEXT_CLASSES

[System.Serializable]
public class DefaultImageStyle : UIComponentStyle<Image>
{
    public Color color;

    public override void ApplyStyle(Image image, bool includeInactive)
    {
        base.ApplyStyle(image, includeInactive);

        image.color = color;
    }
}
[System.Serializable]
public class DefaultTMPTextStyle : UIComponentStyle<TextMeshProUGUI>
{
    public TMP_FontAsset FontAsset;
    public int FontSize;
    public Color VertexColor;

    public override void ApplyStyle(TextMeshProUGUI text, bool includeInactive)
    {
        base.ApplyStyle(text, includeInactive);

        text.font = FontAsset;
        text.color = VertexColor;
        text.fontSize = FontSize;
    }
}
#endregion

[CreateAssetMenu(fileName = "StylesData", menuName = "Style Data", order = 1010)]
[System.Serializable]
public class StyleData : ScriptableObject
{
    #region DELEGATES
    //Default Image Styling Changed Delegates
    public delegate void OnDefaultImageBackgroundColorChanged(Color color);
    public OnDefaultImageBackgroundColorChanged onDefaultImageBackgroundColorChanged;
    public delegate void OnDefaultImageTargetColorChanged(Color color);
    public OnDefaultImageTargetColorChanged onDefaultImageTargetColorChanged;

    //Default TMPText Styling Changed Delegates
    public delegate void OnDefaultTMPTextFontAssetChanged(TMP_FontAsset fontAsset);
    public OnDefaultTMPTextFontAssetChanged onDefaultTMPTextFontAssetChanged;
    public delegate void OnDefaultTMPTextFontSizeChanged(int fontSize);
    public OnDefaultTMPTextFontSizeChanged onDefaultTMPTextFontSizeChanged;
    public delegate void OnDefaultTMPTextVertexColorChanged(Color color);
    public OnDefaultTMPTextVertexColorChanged onDefaultTMPTextVertexColorChanged;
    #endregion

    #region DEFAULT VARIABLES

    public DefaultImageStyle DefaultImageStyle;
    [SerializeField] private Color _defaultImageTargetColor;
    public DefaultTMPTextStyle DefaultTextStyle;

    //Default Image Styling (Hide in inspector)
    public Color DefaultImageBackgroundColor
    {
        get { return DefaultImageStyle.color; }
        set
        {
            if (DefaultImageStyle.color != value)
            {
                DefaultImageStyle.color = value;
                StyleUtility.DefaultImageBackgroundChanged(this, value);
            }
        }
    }

    public Color DefaultImageTargetColor
    {
        get { return _defaultImageTargetColor; }
        set
        {
            if (_defaultImageTargetColor != value)
            {
                _defaultImageTargetColor = value;
                StyleUtility.DefaultImageTargetColorChanged(this, value);
            }
        }
    }

    //Default TMP Text Styling (Hide in inspector)
    public TMP_FontAsset DefaultTextFontAsset
    {
        get { return DefaultTextStyle.FontAsset; }
        set
        {
            if (DefaultTextStyle.FontAsset != value)
            {
                DefaultTextStyle.FontAsset = value;
                StyleUtility.DefaultTextFontAssetChanged(this, value);
            }
        }
    }

    public int DefaultTextFontSize
    {
        get { return DefaultTextStyle.FontSize; }
        set
        {
            if (DefaultTextStyle.FontSize != value)
            {
                DefaultTextStyle.FontSize = value;
                StyleUtility.DefaultTextFontSizeChanged(this, value);
            }
        }
    }

    public Color DefaultTextVertexColor
    {
        get { return DefaultTextStyle.VertexColor; }
        set
        {
            if (DefaultTextStyle.VertexColor != value)
            {
                DefaultTextStyle.VertexColor = value;
                StyleUtility.DefaultTextVertexColorChanged(this, value);
            }
        }
    }
    #endregion

    //Styles
    [SerializeField] private ButtonStyle _buttonStyle;
    [SerializeField] private ScrollbarStyle _scrollbarStyle;
    [SerializeField] private ScrollRectStyle _scrollRectStyle;
    [SerializeField] private SliderStyle _sliderStyle;
    [SerializeField] private ToggleStyle _toggleStyle;
    [SerializeField] private DropdownStyle _dropdownStyle;
    [SerializeField] private InputFieldStyle _inputFiledStyle;



    private void Awake()
    {
        //Only if just created
        if (_buttonStyle == null) 
        {
            _buttonStyle = new ButtonStyle(this);
            _scrollbarStyle = new ScrollbarStyle(this);
            _scrollRectStyle = new ScrollRectStyle(this);
            _sliderStyle = new SliderStyle(this);
            _toggleStyle = new ToggleStyle(this);
            _dropdownStyle = new DropdownStyle(this);
            _inputFiledStyle = new InputFieldStyle(this);

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }

    private void OnEnable()
    {
        StyleUtility.StyleSetDefault(this);
    }

    private void OnDisable()
    {
        //...
    }

    public T Return<T>() where T : class
    {
        if (typeof(T) == typeof(ButtonStyle))
        {
            return _buttonStyle as T;
        }
        else if (typeof(T) == typeof(ScrollbarStyle))
        {
            return _scrollbarStyle as T;
        }
        else if (typeof(T) == typeof(ScrollRectStyle))
        {
            return _scrollRectStyle as T;
        }
        else if (typeof(T) == typeof(SliderStyle))
        {
            return _sliderStyle as T;
        }
        else if (typeof(T) == typeof(ToggleStyle))
        {
            return _toggleStyle as T;
        }
        else if (typeof(T) == typeof(DropdownStyle))
        {
            return _dropdownStyle as T;
        }
        else if (typeof(T) == typeof(InputFieldStyle))
        {
            return _inputFiledStyle as T;
        }

        else
        {
            Debug.Log("Style Data of " + typeof(T) + " not found");
            return null;
        }
    }
    public void Set<T>(T style) where T : class
    {
        if (typeof(T) == typeof(ButtonStyle))
        {
            _buttonStyle = style as ButtonStyle;
        }
        else if (typeof(T) == typeof(ScrollbarStyle))
        {
            _scrollbarStyle = style as ScrollbarStyle;
        }
        else if (typeof(T) == typeof(ScrollRectStyle))
        {
            _scrollRectStyle = style as ScrollRectStyle;
        }
        else if (typeof(T) == typeof(SliderStyle))
        {
            _sliderStyle = style as SliderStyle;
        }
        else if (typeof(T) == typeof(ToggleStyle))
        {
            _toggleStyle = style as ToggleStyle;
        }
        else if (typeof(T) == typeof(DropdownStyle))
        {
            _dropdownStyle = style as DropdownStyle;
        }
        else if (typeof(T) == typeof(InputFieldStyle))
        {
            _inputFiledStyle = style as InputFieldStyle;
        }
        else
        {
            Debug.Log("Style Data of " + typeof(T) + " not found");
        }
    }
}
