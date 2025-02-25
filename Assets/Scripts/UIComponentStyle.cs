using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class OverrideableStyleProperty<T> 
{
    private T _defaultValue;
    public T _currentValue;
    private bool _isOverridden = false;

    public T Value
    {
        get => _currentValue;
        set
        {
            _currentValue = value;
            _isOverridden = !EqualityComparer<T>.Default.Equals(value, _defaultValue);
        }
    }

    public bool IsOverridden
    {
        get { return _isOverridden; }
        private set { _isOverridden = value; }
    }

    public void SetDefault(T defaultValue)
    {
        _defaultValue = defaultValue;
        if (!_isOverridden)
        {
            _currentValue = defaultValue;
        }
    }

    public void Revert()
    {
        StyleManager.Apply = true; //Careful here
        _currentValue = _defaultValue;
        _isOverridden = false;
    }
}
public abstract class UIComponentStyle<T>
{
    public virtual void ApplyStyle(T uIComponent, bool includeInactive)
    {

        if (uIComponent == null)
        {
            Debug.Log("Null UIComponent");
        }
        else if (uIComponent.GetType() != typeof(T))
        {
            Debug.Log("Invalid UIComponent");
            return;
        }
    }

    public virtual void OverrideAllComponentsOnPrefab(T uIComponent, bool includeInactive) 
    {
        StyleUtility.OverrideComponentOnPrefab(uIComponent as Component);
    }
}
