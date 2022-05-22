namespace CommonGame.Sound
{
    [System.Serializable]
    public abstract class AudioPlayer
    {
        protected bool IsActive = true;
        protected float _volumeFactor = 1;
        public abstract void ResumeVolume();
        public abstract void Silence();
        public abstract void Enable();
        public abstract void Disable();
    }
}