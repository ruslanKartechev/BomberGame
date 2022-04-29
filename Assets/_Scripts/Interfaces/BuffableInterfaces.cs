using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public interface ILengthBuffable
    {
        public void BuffLength(float length);
    }
    public interface IPierceBuffable
    {
        public void BuffPierce(int pierce);
    }
}