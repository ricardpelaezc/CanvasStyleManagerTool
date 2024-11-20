using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StylesData", menuName = "Style Data", order = 1010)]
[System.Serializable]
public class StylingData : ScriptableObject
{
    //Default Styling
    [SerializeField] private List<ImageStyle> _imageStyleList = new List<ImageStyle>();
    [SerializeField] private List<TMPTextStyle> _tMPTextStyleList = new List<TMPTextStyle>();

    //Styles
    [SerializeField] private List<ButtonStyle> _buttonStyleList = new List<ButtonStyle>();
    [SerializeField] private List<ScrollbarStyle> _scrollbarStyles = new List<ScrollbarStyle>();
    [SerializeField] private List<ScrollRectStyle> _scrollRectStyles = new List<ScrollRectStyle>();
    [SerializeField] private List<SliderStyle> _sliderStyleList = new List<SliderStyle>();
    [SerializeField] private List<ToggleStyle> _toggleStyleList = new List<ToggleStyle>();
    [SerializeField] private List<TMPDropdownStyle> _tMPDropdownStyleList = new List<TMPDropdownStyle>();
    [SerializeField] private List<TMPInputFieldStyle> _tMPInputFiledStyleList = new List<TMPInputFieldStyle>();

    private List<T> ReturnList<T>() // where T : UIComponentStyle<T>
    {
        if (typeof(T) == typeof(ImageStyle)) return _imageStyleList as List<T>;
        else if (typeof(T) == typeof(TMPTextStyle)) return _tMPTextStyleList as List<T>;

        else if (typeof(T) == typeof(ButtonStyle)) return _buttonStyleList as List<T>;
        else if (typeof(T) == typeof(ScrollbarStyle)) return _scrollbarStyles as List<T>;
        else if (typeof(T) == typeof(ScrollRectStyle)) return _scrollRectStyles as List<T>;
        else if (typeof(T) == typeof(SliderStyle)) return _sliderStyleList as List<T>;
        else if (typeof(T) == typeof(ToggleStyle)) return _toggleStyleList as List<T>;
        else if (typeof(T) == typeof(TMPDropdownStyle)) return _tMPDropdownStyleList as List<T>;
        else if (typeof(T) == typeof(TMPInputFieldStyle)) return _tMPInputFiledStyleList as List<T>;

        else
        {
            Debug.Log("Aura Style Data of " + typeof(T) + " not found");
            return null;
        }
    }

    public T GetIndex<T>(int index) where T : new() // where T : UIComponentStyle
    {
        var list = ReturnList<T>();
        if (index >= list.Count)
        {
            T empty = new T();
            SetIndex(index, empty);
            return empty;
}
        return ReturnList<T>()[index];
    }
    public void SetIndex<T>(int index, T style) where T : new()
    {
        var list = ReturnList<T>();
        if (index >= list.Count)
        {
            for (int i = list.Count; i < index + 1; i++)
            {
                list.Add(new T());
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
}
