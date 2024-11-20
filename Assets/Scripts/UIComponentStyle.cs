using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentStyle<T>
{
    public virtual void ApplyStyle (T uIComponent, bool includeInactive) 
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
        StylingUtility.OverrideComponentOnPrefab(uIComponent as Component);
    }
}
