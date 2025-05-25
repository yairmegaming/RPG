using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Reflection;

public class AutoConnectUIButtonEvents : EditorWindow
{
    [MenuItem("Tools/Auto-Connect UI Button Events")]
    public static void ShowWindow()
    {
        GetWindow<AutoConnectUIButtonEvents>("Auto-Connect UI Button Events");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Connect All Buttons in Scene to UIManager"))
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null)
            {
                Debug.LogWarning("No UIManager found in scene.");
                return;
            }

            Button[] buttons = GameObject.FindObjectsOfType<Button>();
            foreach (Button btn in buttons)
            {
                string methodName = "On" + btn.gameObject.name + "Clicked";
                MethodInfo method = typeof(UIManager).GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (method != null)
                {
                    // Remove all persistent listeners
                    int count = btn.onClick.GetPersistentEventCount();
                    for (int i = count - 1; i >= 0; i--)
                    {
                        UnityEditor.Events.UnityEventTools.RemovePersistentListener(btn.onClick, i);
                    }
                    UnityEditor.Events.UnityEventTools.AddPersistentListener(btn.onClick, () => method.Invoke(uiManager, null));
                    Debug.Log($"Connected {btn.gameObject.name} to {methodName} in UIManager.");
                }
            }
        }
    }
}