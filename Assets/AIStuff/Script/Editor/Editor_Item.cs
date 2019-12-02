using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
[CanEditMultipleObjects]
public class Editor_Item : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Item I = (Item)target;
        if (GUILayout.Button("Interact"))
            I.Interact();
    }
}
