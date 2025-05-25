using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIManager))]
public class AutoReferenceUIManager : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Auto-Assign UI References"))
        {
            UIManager ui = (UIManager)target;
            Undo.RecordObject(ui, "Auto-Assign UI References");

            var type = ui.GetType();
            type.GetField("mainMenuUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("MainMenuUI"));
            type.GetField("settingsUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("SettingsUI"));
            type.GetField("winCombatUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("WinCombatUI"));
            type.GetField("gameOverUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("GameOverUI"));
            type.GetField("inventoryUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("InventoryUI"));
            type.GetField("battleUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("BattleUI"));
            type.GetField("mapUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("MapUI"));
            type.GetField("shopUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("ShopUI"));
            type.GetField("eventUI", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("EventUI"));

            type.GetField("enemySprite", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("EnemySprite"));
            type.GetField("enemyHealthBarText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("EnemyHealthBarText"));
            type.GetField("playerHealthBarText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("PlayerHealthBarText"));
            type.GetField("combatText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.Find("CombatText"));

            // Arrays (by tag)
            type.GetField("attackButtons", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.FindGameObjectsWithTag("AttackButton"));
            type.GetField("menuButtons", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(ui, GameObject.FindGameObjectsWithTag("MenuButton"));

            EditorUtility.SetDirty(ui);
            Debug.Log("UIManager references auto-assigned!");
        }
    }
}