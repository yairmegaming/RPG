using UnityEngine;
using UnityEditor;

public class PrefabReplacer : EditorWindow
{
    private GameObject prefab;

    [MenuItem("Tools/Prefab Replacer")]
    public static void ShowWindow()
    {
        GetWindow<PrefabReplacer>("Prefab Replacer");
    }

    void OnGUI()
    {
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Replace Selected With Prefab") && prefab != null)
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefab, go.transform.parent);
                newObj.transform.position = go.transform.position;
                newObj.transform.rotation = go.transform.rotation;
                newObj.transform.localScale = go.transform.localScale;
                Undo.DestroyObjectImmediate(go);
                Undo.RegisterCreatedObjectUndo(newObj, "Replace with Prefab");
            }
            Debug.Log("Replaced selected objects with prefab.");
        }
    }
}