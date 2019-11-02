using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Z
{
    [CustomEditor(typeof(PathObstacle))]
    [CanEditMultipleObjects]
    public class Editor_PathObstacle : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            PathObstacle PO = (PathObstacle)target;
            if (GUILayout.Button("Apply"))
            {
                Undo.RegisterFullObjectHierarchyUndo(PO.gameObject, "FindPoints");
                PO.EditorApply();
            }
        }
    }
}