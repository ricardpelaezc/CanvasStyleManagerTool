using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIComponentStyle<T>
{
    [SerializeField]
    public string StyleName;

    public virtual void ApplyStyle (T uIComponent) 
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

    public virtual void OverrideAllComponentsOnPrefab(T uIComponent) { }
}
