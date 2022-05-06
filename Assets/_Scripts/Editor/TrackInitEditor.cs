using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BomberGame {
    [CustomEditor(typeof(TrackInit))]
    public class TrackInitEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TrackInit me = target as TrackInit;
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Show"))
            {
                me.ShowLevel();
            }
            if (GUILayout.Button("Hide"))
            {
                me.HideLevel();
            }
            GUILayout.EndHorizontal();

        }
    }
}