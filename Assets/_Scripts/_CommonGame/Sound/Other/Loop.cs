using UnityEngine;


namespace CommonGame.Sound
{
    [System.Serializable]
    public struct Loop
    {
        public string Name;
        public Coroutine LoopRoutine;
        public AudioSource Source;
        public Loop(string _name, Coroutine _myLoop, AudioSource _source)
        {
            Name = _name;
            LoopRoutine = _myLoop;
            Source = _source;
        }
    }

}