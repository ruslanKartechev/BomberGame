using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BomberGame
{
    public class BuffSystemDebugger : MonoBehaviour
    {
        public BuffManager _buffSystem;

        public ActorsContainer _actors;
        public string _debugTargetID;
        [Space(5)]
        public BuffBase _buff;
        [Space(5)]
        public BuffBase _bombBuff;
        [Space(5)]
        public float Duration;

        public void ApplyDebugBuff()
        {
            if (_buff != null)
                _buffSystem.ApplyTimedBuff(_buff.TypeID, _buff.VersionID, _debugTargetID, Duration);
        }


        public void ApplyBombBuff()
        {
            if(_bombBuff != null)
                _buffSystem.ApplyTimedBuff(_bombBuff.TypeID, _bombBuff.VersionID, _debugTargetID, Duration);
        }

    }


    [CustomEditor(typeof(BuffSystemDebugger))]
    public class BuffSystemDebuggerEditor : Editor
    {
        BuffSystemDebugger me;
        private void OnEnable()
        {
            me = target as BuffSystemDebugger;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Apply"))
            {
                me.ApplyDebugBuff();
            }

            if (GUILayout.Button("ApplyBombBuff"))
            {
                me.ApplyBombBuff();
            }
        }


    }
}