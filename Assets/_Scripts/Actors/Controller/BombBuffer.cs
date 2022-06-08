using BomberGame.Bombs;
using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class BombBuffer : IBombBuffer
    {
        private List<BombBuffBase> _givenBuffs;
        public BombBuffer()
        {
            _givenBuffs = new List<BombBuffBase>(10);
        }

        public void AddBuff(BombBuffBase buff)
        {
            if(_givenBuffs.Contains(buff) == false)
                _givenBuffs.Add(buff);
        }

        public void RemoveBuff(BombBuffBase buff)
        {
            _givenBuffs.Remove(buff);
        }

        public void ApplyBuffs(Bomb target)
        {
            foreach(BombBuffBase b in _givenBuffs)
            {
                b.ApplyToBomb(target);
            }
        }

        

    }
}