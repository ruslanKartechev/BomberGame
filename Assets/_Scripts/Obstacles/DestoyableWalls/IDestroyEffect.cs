using UnityEngine;
using System;
namespace BomberGame
{
    public interface IDestroyEffect
    {
       void PlayDestroyEffect(Action onDone);
    }
}