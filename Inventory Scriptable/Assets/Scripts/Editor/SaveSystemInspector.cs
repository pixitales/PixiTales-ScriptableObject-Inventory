using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveManager))]
public class SaveSystemInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Go to persistent data folder"))
        {
            EditorUtility.OpenWithDefaultApp(Application.persistentDataPath);
        }
    }
}
