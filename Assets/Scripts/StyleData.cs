using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StylesData", menuName = "Style Data", order = 1010)]
[System.Serializable]
public class StyleData : ScriptableObject
{
    //Default Styling
    public List<ImageStyle> _imageStyleList = new List<ImageStyle>();

    //Styles
    public List<TMPTextStyle> _tMPTextStyleList = new List<TMPTextStyle>();
    public List<ButtonStyle> _buttonStyleList = new List<ButtonStyle>();
    public List<SliderStyle> _sliderStyleList = new List<SliderStyle>();
    public List<ToggleStyle> _toggleStyleList = new List<ToggleStyle>();
    public List<TMPDropdownStyle> _tMPDropdownStyleList = new List<TMPDropdownStyle>();
    public List<TMPInputFieldStyle> _tMPInputFiledStyleList = new List<TMPInputFieldStyle>();

    private List<T> ReturnList<T>() // where T : UIComponentStyle<T>
    {
        if (typeof(T) == typeof(ImageStyle)) return _imageStyleList as List<T>;

        else if (typeof(T) == typeof(TMPTextStyle)) return _tMPTextStyleList as List<T>;
        else if (typeof(T) == typeof(ButtonStyle)) return _buttonStyleList as List<T>;
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

    public T GetIndex<T>(int index) // where T : UIComponentStyle
    {
        return ReturnList<T>()[index];

    }
    public void SetIndex<T>(int index, T style)
    {
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
