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

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Give B01"))
            {
                me.AddItem("b01", 3);
            }
            if (GUILayout.Button("Give B02"))
            {
                me.AddItem("b02", 5);

            }
            if (GUILayout.Button("Give B03"))
            {
                me.AddItem("b03", 3);

            }
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Remove B01"))
            {
                me.RemoveItem("b01");
            }
            if (GUILayout.Button("Remove B02"))
            {
                me.RemoveItem("b02");

            }
            if (GUILayout.Button("Remove B03"))
            {
                me.RemoveItem("b03");
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Update count b01"))
            {
                b01Count--;
                me.UpdateCount("b01", b01Count);
            }

        }
    }
}