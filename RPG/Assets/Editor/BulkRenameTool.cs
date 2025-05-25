using UnityEngine;
using UnityEditor;

public class BulkRenameTool : EditorWindow
{
    private string baseName = "GameObject";
    private int startIndex = 1;

    [MenuItem("Tools/Bulk Rename Tool")]
    public static void ShowWindow()
    {
        GetWindow<BulkRenameTool>("Bulk Rename Tool");
    }

    void OnGUI()
    {
        baseName = EditorGUILayout.TextField("Base Name", baseName);
        startIndex = EditorGUILayout.IntField("Start Index", startIndex);

        if (GUILayout.Button("Rename Selected"))
        {
            int index = startIndex;
            foreach (GameObject go in Selection.gameObjects)
            {
                Undo.RecordObject(go, "Bulk Rename");
                go.name = $"{baseName}_{index++}";
                EditorUtility.SetDirty(go);
            }
            Debug.Log("Renamed selected objects.");
        }
    }
}