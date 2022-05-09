using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class CharachterBuffer : MonoBehaviour, ICharachterBuffer
    {
        public void BuffCharachter(GameObject charachter, BuffBase buff)
        {
            buff.Apply(charachter);   
        }

    }
}