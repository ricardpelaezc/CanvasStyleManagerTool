using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MultColorButton), true)]
public class MultColorButtonEditor : Editor
{
    private SerializedProperty graphicTransitions;

    private void OnEnable()
    {
        // Cache the serialized property
        graphicTransitions = serializedObject.FindProperty("GraphicTransitions");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Display the list of GraphicTransition structs
        EditorGUILayout.LabelField("Graphic Transitions", EditorStyles.boldLabel);

        for (int i = 0; i < graphicTransitions.arraySize; i++)
        {
            SerializedProperty element = graphicTransitions.GetArrayElementAtIndex(i);

            // Display each field of the struct
            SerializedProperty targetGraphic = element.FindPropertyRelative("TargetGraphic");
            SerializedProperty color = element.FindPropertyRelative("Color");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.PropertyField(targetGraphic);
            EditorGUILayout.PropertyField(color);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
        }

        // Add and Remove Buttons
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add"))
        {
            graphicTransitions.arraySize++;
        }
        if (GUILayout.Button("Remove") && graphicTransitions.arraySize > 0)
        {
            graphicTransitions.arraySize--;
        }
        EditorGUILayout.EndHorizontal();

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}
