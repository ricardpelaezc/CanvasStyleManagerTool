using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultColorButton : Button
{
    [System.Serializable]
    public struct GraphicTransition
    {
        public Graphic TargetGraphic;
        public ColorBlock Color;
    }

    public GraphicTransition[] GraphicTransitions;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        if (GraphicTransitions == null) return;

        foreach (var gt in GraphicTransitions)
        {
            if (gt.TargetGraphic == null) continue;

            var targetColor =
                state == SelectionState.Disabled ? gt.Color.disabledColor :
                state == SelectionState.Highlighted ? gt.Color.highlightedColor :
                state == SelectionState.Normal ? gt.Color.normalColor :
                state == SelectionState.Pressed ? gt.Color.pressedColor :
                state == SelectionState.Selected ? gt.Color.selectedColor : Color.white;

            gt.TargetGraphic.CrossFadeColor(targetColor, instant ? 0 : colors.fadeDuration, true, true);
        }
    }
}
