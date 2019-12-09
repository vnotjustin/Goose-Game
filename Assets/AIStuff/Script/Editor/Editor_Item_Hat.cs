using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item_Hat))]
[CanEditMultipleObjects]
public class Editor_Item_Hat : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Item I = (Item)target;
        if (GUILayout.Button("Interact"))
            I.Interact();
    }
}
