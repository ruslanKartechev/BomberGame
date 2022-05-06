using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BomberGame.UI
{
    [CustomEditor(typeof(BombMenuManager))]
    public class BombMenuManagerEditor : Editor
    {
        private int b01Count = 3;
        private void OnEnable()
        {
            b01Count = 3;
        }
        public override void OnInspectorGUI()
        {

            base.OnInspectorGUI();
            BombMenuManager me = target as BombMenuManager;
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Show"))
            {
                me.ShowMenu();
            }
            if (GUILayout.Button("Hide"))
            {
                me.HideMenu();
            }
            GUILayout.EndHorizontal();

           

        }
    }
}