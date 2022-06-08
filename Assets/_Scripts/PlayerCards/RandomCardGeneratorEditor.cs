using UnityEngine;
using UnityEditor;
namespace BomberGame
{
#if UNITY_EDITOR
    [CustomEditor(typeof(CardsGenerator))]
    public class RandomCardGeneratorEditor : Editor
    {
        CardsGenerator me;
        private void OnEnable()
        {
            me = target as CardsGenerator;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("StartGenerator"))
            {
                me.EnableGenerator();
            }
        }
    }

#endif

}