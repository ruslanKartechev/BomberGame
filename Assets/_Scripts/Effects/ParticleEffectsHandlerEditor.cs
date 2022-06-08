using UnityEngine;
using UnityEditor;
namespace CommonGame
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ParticlesLauncher))]
    public class ParticleEffectsHandlerEditor: Editor
    {
        ParticlesLauncher me;
        public void OnEnable()
        {
            me = target as ParticlesLauncher;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("PlayAll"))
            {
                me.Play();
            }
            if (GUILayout.Button("StopAll"))
            {
                me.Stop();
            }
        }
    }
#endif
}