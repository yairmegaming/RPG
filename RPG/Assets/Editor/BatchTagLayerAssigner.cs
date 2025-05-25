using UnityEngine;
using UnityEditor;

public class BatchTagLayerAssigner : EditorWindow
{
    private string tagToAssign = "Untagged";
    private int layerToAssign = 0;

    [MenuItem("Tools/Batch Tag/Layer Assigner")]
    public static void ShowWindow()
    {
        GetWindow<BatchTagLayerAssigner>("Batch Tag/Layer Assigner");
    }

    void OnGUI()
    {
        tagToAssign = EditorGUILayout.TagField("Tag", tagToAssign);
        layerToAssign = EditorGUILayout.LayerField("Layer", layerToAssign);

        if (GUILayout.Button("Assign to Selected"))
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                Undo.RecordObject(go, "Batch Tag/Layer Assign");
                go.tag = tagToAssign;
                go.layer = layerToAssign;
                EditorUtility.SetDirty(go);
            }
            Debug.Log("Assigned tag and layer to selected objects.");
        }
    }
}