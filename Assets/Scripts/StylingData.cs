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
public class StylingData : ScriptableObject
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
    public DefaultTMPTextStyle DefaultTMPTextStyle;

    //Default Image Styling (Hide in inspector)
    public Color DefaultImageBackgroundColor
    {
        get { return DefaultImageStyle.color; }
        set
        {
            if (DefaultImageStyle.color != value)
            {
                DefaultImageStyle.color = value;
                DefaultImageBackgroundChanged();
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
                DefaultImageTargetColorChanged();
            }
        }
    }

    //Default TMP Text Styling (Hide in inspector)
    public TMP_FontAsset DefaultTMPTextFontAsset
    {
        get { return DefaultTMPTextStyle.FontAsset; }
        set
        {
            if (DefaultTMPTextStyle.FontAsset != value)
            {
                DefaultTMPTextStyle.FontAsset = value;
                DefaultTMPTextFontAssetChanged();
            }
        }
    }

    public int DefaultTMPTextFontSize
    {
        get { return DefaultTMPTextStyle.FontSize; }
        set
        {
            if (DefaultTMPTextStyle.FontSize != value)
            {
                DefaultTMPTextStyle.FontSize = value;
                DefaultTMPTextFontSizeChanged();
            }
        }
    }

    public Color DefaultTMPTextVertexColor
    {
        get { return DefaultTMPTextStyle.VertexColor; }
        set
        {
            if (DefaultTMPTextStyle.VertexColor != value)
            {
                DefaultTMPTextStyle.VertexColor = value;
                DefaultTMPTextVertexColorChanged();
            }
        }
    }
    #endregion

    #region DEFAULT_FUNTIONS
    public void DefaultImageBackgroundChanged()
    {
        onDefaultImageBackgroundColorChanged?.Invoke(DefaultImageBackgroundColor);
    }
    public void DefaultImageTargetColorChanged()
    {
        onDefaultImageTargetColorChanged?.Invoke(DefaultImageTargetColor);
    }
    public void DefaultTMPTextFontAssetChanged()
    {
        onDefaultTMPTextFontAssetChanged?.Invoke(DefaultTMPTextFontAsset);
    }
    public void DefaultTMPTextFontSizeChanged()
    {
        onDefaultTMPTextFontSizeChanged?.Invoke(DefaultTMPTextFontSize);
    }
    public void DefaultTMPTextVertexColorChanged()
    {
        onDefaultTMPTextVertexColorChanged?.Invoke(DefaultTMPTextVertexColor);
    }
    #endregion

    //Styles
    [SerializeField] private List<ButtonStyle> _buttonStyleList;
    [SerializeField] private List<ScrollbarStyle> _scrollbarStyles;
    [SerializeField] private List<ScrollRectStyle> _scrollRectStyles;
    [SerializeField] private List<SliderStyle> _sliderStyleList;
    [SerializeField] private List<ToggleStyle> _toggleStyleList;
    [SerializeField] private List<TMPDropdownStyle> _tMPDropdownStyleList;
    [SerializeField] private List<TMPInputFieldStyle> _tMPInputFiledStyleList;

    private void OnEnable()
    {
        if (Empty<ButtonStyle>())
            SetIndex(0, new ButtonStyle(this));

        if (Empty<ScrollbarStyle>())
            SetIndex(0, new ScrollbarStyle(this));

        if (Empty<ScrollRectStyle>())
            SetIndex(0, new ScrollRectStyle(this));

        if (Empty<SliderStyle>())
            SetIndex(0, new SliderStyle(this));

        if (Empty<ToggleStyle>())
            SetIndex(0, new ToggleStyle(this));

        if (Empty<TMPDropdownStyle>())
            SetIndex(0, new TMPDropdownStyle(this));

        if (Empty<TMPInputFieldStyle>())
            SetIndex(0, new TMPInputFieldStyle(this));

        GetIndex<ButtonStyle>(0).InitDelegates(this);
        GetIndex<ScrollbarStyle>(0).InitDelegates(this);
        GetIndex<ScrollRectStyle>(0).InitDelegates(this);
        GetIndex<SliderStyle>(0).InitDelegates(this);
        GetIndex<ToggleStyle>(0).InitDelegates(this);
        GetIndex<TMPDropdownStyle>(0).InitDelegates(this);
        GetIndex<TMPInputFieldStyle>(0).InitDelegates(this);

        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
    }

    private void OnDisable()
    {
        GetIndex<ButtonStyle>(0).ClearDelegates(this);
        GetIndex<ScrollbarStyle>(0).ClearDelegates(this);
        GetIndex<ScrollRectStyle>(0).ClearDelegates(this);
        GetIndex<SliderStyle>(0).ClearDelegates(this);
        GetIndex<ToggleStyle>(0).ClearDelegates(this);
        GetIndex<TMPDropdownStyle>(0).ClearDelegates(this);
        GetIndex<TMPInputFieldStyle>(0).ClearDelegates(this);

    }

    private List<T> ReturnList<T>() // where T : UIComponentStyle<T>
    {
        if (typeof(T) == typeof(ButtonStyle))
        {
            if (_buttonStyleList == null)
            {
                _buttonStyleList = new List<ButtonStyle>();
            }
            return _buttonStyleList as List<T>;
        }
        else if (typeof(T) == typeof(ScrollbarStyle))
        {
            if (_scrollbarStyles == null)
            {
                _scrollbarStyles = new List<ScrollbarStyle>();
            }
            return _scrollbarStyles as List<T>;
        }
        else if (typeof(T) == typeof(ScrollRectStyle))
        {
            if (_scrollRectStyles == null)
            {
                _scrollRectStyles = new List<ScrollRectStyle>();
            }
            return _scrollRectStyles as List<T>;
        }
        else if (typeof(T) == typeof(SliderStyle))
        {
            if (_sliderStyleList == null)
            {
                _sliderStyleList = new List<SliderStyle>();
            }
            return _sliderStyleList as List<T>;
        }
        else if (typeof(T) == typeof(ToggleStyle))
        {
            if (_toggleStyleList == null)
            {
                _toggleStyleList = new List<ToggleStyle>();
            }
            return _toggleStyleList as List<T>;
        }
        else if (typeof(T) == typeof(TMPDropdownStyle))
        {
            if (_tMPDropdownStyleList == null)
            {
                _tMPDropdownStyleList = new List<TMPDropdownStyle>();
            }
            return _tMPDropdownStyleList as List<T>;
        }
        else if (typeof(T) == typeof(TMPInputFieldStyle))
        {
            if (_tMPInputFiledStyleList == null)
            {
                _tMPInputFiledStyleList = new List<TMPInputFieldStyle>();
            }
            return _tMPInputFiledStyleList as List<T>;
        }

        else
        {
            Debug.Log("Aura Style Data of " + typeof(T) + " not found");
            return null;
        }
    }
    public T GetIndex<T>(int index)
    {
        var list = ReturnList<T>();
        if (index >= list.Count)
        {
            T empty = default(T); ;
            SetIndex(index, empty);
            return empty;
        }
        return ReturnList<T>()[index];
    }
    public void SetIndex<T>(int index, T style)
    {
        var list = ReturnList<T>();
        if (index >= list.Count)
        {
            for (int i = list.Count; i < index + 1; i++)
            {
                list.Add(default(T));
            }
        }
        ReturnList<T>()[index] = style;
    }
    public void Add<T>(T style) // where T : UIComponentStyle
    {
        ReturnList<T>().Add(style);
    }
    public void Remove<T>(T style) // where T : UIComponentStyle
    {
        ReturnList<T>().Remove(style);
    }
    public bool Empty<T>()
    {
        return ReturnList<T>().Count == 0;
    }
}
