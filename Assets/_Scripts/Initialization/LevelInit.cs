using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
namespace BomberGame
{

    public class LevelInit : MonoBehaviour
    {

        [SerializeField] private bool AutoStart = true;

        [SerializeField] private TrackInit _trackInit;
        [SerializeField] private BotsInit _botInit;
        private void Start()
        {
            if (AutoStart)
                InitLevel();
        }
        public void InitLevel()
        {
            StartCoroutine(InitProcess());
        }

        private IEnumerator InitProcess()
        {
            yield return null;
            yield return StartCoroutine(_trackInit.InitTrack());
            yield return StartCoroutine(_botInit.InitBots());
        }
    }


    [CustomEditor(typeof(LevelInit))]
    public class LevelInitEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            LevelInit me = target as LevelInit;
            if(GUILayout.Button("Full init"))
            {
                me.InitLevel();
            }
        }
    }
}