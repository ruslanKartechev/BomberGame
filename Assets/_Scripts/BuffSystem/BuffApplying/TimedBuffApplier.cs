using System;
using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class TimedBuffApplier : ConstantBuffApplier
    {
        public class BuffTimer
        {
            private InteractableEntity _target;
            private BuffBase _buff;
            private float _duration;
            private float _elapsed;
            public Action<BuffTimer> OnFinish;
            public BuffTimer(InteractableEntity target, BuffBase appliedBuff, float duration)
            {
                _target = target;
                _buff = appliedBuff;
                _duration = duration;
            }

            public void Tick()
            {
                _elapsed += Time.deltaTime;
                if(_elapsed >= _duration)
                {
                    _buff.StopApply(_target);
                    OnFinish?.Invoke(this);
                }
            }
        }

        private List<BuffTimer> _activeBuffs = new List<BuffTimer>();

        public void ApplyTimed(BuffBase buff, InteractableEntity actor, float duration)
        {
            if(ApplyBuffToActor(buff, actor))
            {
                BuffTimer timer = new BuffTimer(actor, buff, duration);
                timer.OnFinish = OnTimerFinish;
                _activeBuffs.Add(timer);
            }
        }

        public void OnTimerFinish(BuffTimer timer)
        {
            _activeBuffs.Remove(timer);
        }

        private void Update()
        {
            for(int i=0; i<_activeBuffs.Count; i++)
            {
                _activeBuffs[i]?.Tick();
            }
        }
    }
}