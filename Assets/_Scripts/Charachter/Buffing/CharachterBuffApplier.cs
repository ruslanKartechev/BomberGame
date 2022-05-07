using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{


    public class CharachterBuffApplier : MonoBehaviour, ICharachterBuffer
    {
        public void BuffCharachter(GameObject charachter, BuffBase buff)
        {
            buff.Apply(charachter);   
            
        }

    }
}