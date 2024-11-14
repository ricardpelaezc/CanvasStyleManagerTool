using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonStyle : UIComponentStyle<Button>
{
    //Other Settings
    public ImageStyle ImgStyle;
    public TMPTextStyle TMPTextStyle;

    public override void ApplyStyle(Button button)
    {
        ImgStyle.ApplyStyle(button.targetGraphic as Image);
        TextMeshProUGUI[] listText = button.transform.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var text in listText)
        {
            TMPTextStyle.ApplyStyle(text);
        }
    }

    public override void OverrideAllComponentsOnPrefab(Button button)
    {
        CanvasStyleUtility.OverrideComponentOnPrefab(button);
        ImgStyle.OverrideAllComponentsOnPrefab(button.targetGraphic as Image);
        TextMeshProUGUI[] listText = button.transform.GetComponentsInChildren<TextMeshProUGUI>(true);
        foreach (var text in listText)
        {
            TMPTextStyle.OverrideAllComponentsOnPrefab(text);
        }
        
    }
}
