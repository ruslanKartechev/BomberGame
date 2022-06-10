using System;
namespace BomberGame
{
    public interface IBombMap<T>
    {
        public event Action<T> OnBombPlaced;
        void PlaceBomb(T position);
        void RemoveBomb(T position);
    }


}