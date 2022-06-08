using UnityEngine;
using CommonGame.Controlls;
using CommonGame.Sound;
using Zenject;
namespace CommonGame
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
       // [SerializeField] LevelManager levelManager;
        [Header("General")]
        //[Inject] private ISoundSystem _sounds;
        [Inject] private IInputSystem _controlls;

        private void Start()
        {
            //if (levelManager == null) levelManager = FindObjectOfType<LevelManager>();
            //levelManager.LoadLast();
            _controlls.Init();
            //_sounds.Init();
        }
    }


}




public class SingletonMB<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<T>();
            return instance;
        }
        set
        {
            instance = value;
        }
    }



}
