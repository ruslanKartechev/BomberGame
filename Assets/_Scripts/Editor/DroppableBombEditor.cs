using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace BomberGame
{
    [CustomEditor(typeof(DroppableBomb))]
    public class DroppableBombEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DroppableBomb me = target as DroppableBomb;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("SetHidden"))
            {
                me.SetHiddenView();
            }
            if (GUILayout.Button("SetDropped"))
            {
                me.SetDroppedView();
            }
            GUILayout.EndHorizontal();
        }
    }
}
#endif