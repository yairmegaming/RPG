using UnityEngine;
using UnityEditor;

public class AddTagsEditor
{
    [MenuItem("Tools/Add Required Tags")]
    public static void AddTags()
    {
        AddTag("AttackButton");
        AddTag("MenuButton");
        AddTag("Untagged"); // Unity default, but included for completeness
        // Add any other tags your game may use:
        AddTag("Enemy");
        AddTag("Player");
        AddTag("InventoryItem");
        AddTag("ShopItem");
        AddTag("EventUI");
        AddTag("BattleUI");
        AddTag("MapUI");
        AddTag("MainMenuUI");
        AddTag("SettingsUI");
        AddTag("WinCombatUI");
        AddTag("GameOverUI");
        AddTag("InventoryUI");
        AddTag("ShopUI");
        AddTag("EnemySprite");
        AddTag("EnemyHealthBarText");
        AddTag("PlayerHealthBarText");
        AddTag("CombatText");
        Debug.Log("Tags added if they didn't exist.");
    }

    private static void AddTag(string tag)
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // Check if tag already exists
        for (int i = 0; i < tagsProp.arraySize; i++)
        {
            SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
            if (t.stringValue.Equals(tag)) return;
        }

        // Add new tag
        tagsProp.InsertArrayElementAtIndex(0);
        tagsProp.GetArrayElementAtIndex(0).stringValue = tag;
        tagManager.ApplyModifiedProperties();
    }
}