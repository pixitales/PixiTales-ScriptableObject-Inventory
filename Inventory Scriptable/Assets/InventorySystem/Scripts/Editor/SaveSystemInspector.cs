using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveSystemInspector : Editor
{
    public override void OnInspectorGUI()
    {
        var saveManager = (SaveManager)target;

        base.OnInspectorGUI();

        EditorGUI.indentLevel++;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Save Game", GUILayout.Width(100), GUILayout.Height(25)))
        {
            saveManager.Save();
        }

        if (GUILayout.Button("Load Game", GUILayout.Width(100), GUILayout.Height(25)))
        {
            saveManager.Load();
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Go to persistent data folder", GUILayout.Height(25)))
        {
            EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
        }
    }
}
