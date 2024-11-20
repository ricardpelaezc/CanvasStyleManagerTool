using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageStyle : UIComponentStyle<Image>
{
    public Color Color;

    public override void ApplyStyle(Image image, bool includeInactive)
    {
        base.ApplyStyle(image, includeInactive);

        image.color = Color;
    }
}
