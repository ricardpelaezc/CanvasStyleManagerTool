using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageStyle : UIComponentStyle<Image>
{
    public OverrideableStyleProperty<Color> Color = new OverrideableStyleProperty<Color>();
    public ImageStyle(StyleData stylingData)
    {
        Color = new OverrideableStyleProperty<Color>();
    }

    public override void ApplyStyle(Image image, bool includeInactive)
    {
        base.ApplyStyle(image, includeInactive);

        //Debug.Log(image);
        image.color = Color.Value;
    }
}
