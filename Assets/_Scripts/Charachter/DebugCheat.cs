using UnityEngine;
using UnityEditor;
namespace BomberGame
{
    public class DebugCheat : MonoBehaviour
    {
        [SerializeField] private PlayerInventoryManager _inventory;
        [SerializeField] private string giveId;
        [SerializeField] private int giveCount = 1;
        public void GiveBomb()
        {
            if (_inventory == null)
            {
                Debug.Log("inventory is null");
                return;
            }
            _inventory.GetBomb(giveId, giveCount);
            _inventory.SetCurrentBomb(giveId);
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(DebugCheat))]
    public class DebugCheatEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DebugCheat me = target as DebugCheat;
            if(GUILayout.Button("Give Bomb"))
            {
                me.GiveBomb();
            }
        }
    }
#endif

}