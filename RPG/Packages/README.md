# RPG Project Documentation

## Overview

This is a Unity-based RPG project featuring:

- Modular UI management
- Automated Editor tools to speed up development
- Minimal manual Inspector work

---

## Folder Structure

```plaintext
Assets/
  Editor/           # Custom Editor scripts and tools
  Prefabs/          # Prefab assets
  Scenes/           # Unity scenes
  Scripts/          # Game logic scripts
  Sprites/          # Art assets
  TextMesh Pro/     # Fonts and TMP assets
```

---

## Custom Editor Tools

### 1. AddTagEditor

**Location:** `Assets/Editor/AddTagEditor.cs`  
**Purpose:** Adds all required tags to your project automatically.

**How to use:**

- In Unity, go to **Tools > Add Required Tags**.
- All tags needed by your scripts and UI will be added if they don’t exist.

---

### 2. AutoReferenceUIManager

**Location:** `Assets/Editor/AutoReferenceUIManager.cs`  
**Purpose:** Automatically assigns UI GameObjects to the fields in your `UIManager` script by name or tag.

**How to use:**

- Select your `UIManager` GameObject in the scene.
- In the Inspector, click **Auto-Assign UI References**.

---

### 3. BatchTagLayerAssigner

**Location:** `Assets/Editor/BatchTagLayerAssigner.cs`  
**Purpose:** Assign a tag or layer to all selected GameObjects at once.

**How to use:**

- Select GameObjects in the scene.
- Go to **Tools > Batch Tag/Layer Assigner**.
- Choose tag/layer and click **Assign to Selected**.

---

### 4. PrefabReplacer

**Location:** `Assets/Editor/PrefabReplacer.cs`  
**Purpose:** Replace selected GameObjects with a chosen prefab.

**How to use:**

- Select GameObjects in the scene.
- Go to **Tools > Prefab Replacer**.
- Choose a prefab and click **Replace Selected With Prefab**.

---

### 5. SceneObjectFinder

**Location:** `Assets/Editor/SceneObjectFinder.cs`  
**Purpose:** Find all objects in the scene with a specific tag.

**How to use:**

- Go to **Tools > Scene Object Finder**.
- Enter a tag and click **Find Objects**.

---

### 6. BulkRenameTool

**Location:** `Assets/Editor/BulkRenameTool.cs`  
**Purpose:** Rename selected GameObjects in bulk with a pattern.

**How to use:**

- Select GameObjects in the scene.
- Go to **Tools > Bulk Rename Tool**.
- Set base name and start index, then click **Rename Selected**.

---

### 7. AutoConnectUIButtonEvents

**Location:** `Assets/Editor/AutoConnectUIButtonEvents.cs`  
**Purpose:** Automatically connects all UI Buttons’ `onClick` events to methods in your `UIManager` based on naming conventions.

**How to use:**

- Go to **Tools > Auto-Connect UI Button Events**.
- Click **Connect All Buttons in Scene to UIManager**.

---

## UIManager Script

- Handles all UI panels and references.
- Uses dictionaries for fast lookup and management.
- Can be auto-assigned using the Editor tool above.

---

## Adding New Tags or Tools

- Add new tags to `AddTagEditor.cs` as needed.
- For new Editor tools, place scripts in the `Assets/Editor` folder.

---

## Best Practices

- Use the provided Editor tools to minimize manual Inspector work.
- Name and tag your GameObjects consistently for automation.
- Keep your Editor scripts organized in the `Editor` folder.

---

## Expanding Documentation

- Add sections for gameplay, systems, and asset guidelines as your project grows.
- Document any new tools or workflows you introduce.

---

**Tip:**  
Keep this documentation in your project root as `README.md` for easy access and updates!
