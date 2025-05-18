using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(GameDatabase))]
public class GameDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameDatabase db = (GameDatabase)target;

        if (GUILayout.Button("Auto-Fill Items and Cards"))
        {
            string[] itemGuids = AssetDatabase.FindAssets("t:Item", new[] { "Assets/Items/Prefabs/Items" });
            db.allItems = itemGuids
                .Select(guid => AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(item => item != null)
                .ToList();

            string[] cardGuids = AssetDatabase.FindAssets("t:Card", new[] { "Assets/Items/Prefabs/Cards" });
            db.allCards = cardGuids
                .Select(guid => AssetDatabase.LoadAssetAtPath<Card>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(card => card != null)
                .ToList();

            EditorUtility.SetDirty(db);
            Debug.Log("GameDatabase auto-filled!");
        }
    }
}