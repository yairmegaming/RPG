using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SceneObjectFinder : EditorWindow
{
    private string tagToFind = "Untagged";
    private Vector2 scroll;

    private List<GameObject> foundObjects = new List<GameObject>();

    [MenuItem("Tools/Scene Object Finder")]
    public static void ShowWindow()
    {
        GetWindow<SceneObjectFinder>("Scene Object Finder");
    }

    void OnGUI()
    {
        tagToFind = EditorGUILayout.TagField("Tag to Find", tagToFind);

        if (GUILayout.Button("Find Objects"))
        {
            foundObjects.Clear();
            foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
            {
                if (go.CompareTag(tagToFind))
                    foundObjects.Add(go);
            }
        }

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var go in foundObjects)
        {
            EditorGUILayout.ObjectField(go, typeof(GameObject), true);
        }
        EditorGUILayout.EndScrollView();
    }
}