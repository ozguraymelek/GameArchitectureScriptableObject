using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Unity.VisualScripting.ReorderableList.Internal;
using UnityEditor;
using UnityEngine;
using GUIHelper = Sirenix.Utilities.Editor.GUIHelper;

public class EnemyEditor : OdinMenuEditorWindow
{
    [MenuItem("Utils/Enemy/Editor Panel")]
    private static void Open()
    {
        var window = GetWindow<EnemyEditor>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
    }
    
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree(true);

        tree.DefaultMenuStyle.IconSize = 25.00f;
        tree.Config.DrawSearchToolbar = true;
        
        //character overview table

        return tree;
    }
}
