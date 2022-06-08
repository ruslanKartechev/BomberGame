using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{

    public class LevelInit : MonoBehaviour
    {

        [SerializeField] private bool AutoStart = true;

        [SerializeField] private TrackInit _trackInit;
        [SerializeField] private BombersInitializer _actorsInit;
        [Space(10)]
        [SerializeField] private LevelFlowManager _levelFlowManager;
        private CancellationTokenSource _tokenSource;
        private void Start()
        {
            if (AutoStart)
                InitLevel();
        }
        public void InitLevel()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            InitProcess(_tokenSource.Token);
        }

        private async void InitProcess(CancellationToken token)
        {
            await _actorsInit.InitBombers(token);
            _levelFlowManager.Init();
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