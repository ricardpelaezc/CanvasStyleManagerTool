using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageStyle : UIComponentStyle<Image>
{
    public Color Color;

    public override void ApplyStyle(Image image)
    {
        image.color = Color;
    }
    public override void OverrideAllComponentsOnPrefab(Image image)
    {
        CanvasStyleUtility.OverrideComponentOnPrefab(image);
    }
}
