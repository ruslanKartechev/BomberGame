using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame;
using Zenject;
namespace BomberGame
{
    public class BombParticleCoundownEffect : BombCountDown
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _countDownParticlesPosition;
        [SerializeField] private IBombView _view;
        [Inject] private IBombParticlesPool _particlePool;

        public override void StartCountdown(float time)
        {
            _view = gameObject.GetComponent<IBombView>();
            var p = _particlePool.GetCoundownParticles();
            p.transform.position = _countDownParticlesPosition.position;
            StartCoroutine(CountdownParticles(p, time));
            _anim.Play("Idle");
        }

        private IEnumerator CountdownParticles(ParticlesLauncher launcher, float time)
        {
            launcher.Play();
            yield return new WaitForSeconds(time);
            launcher.Stop();
        }

        public override void OnExplode()
        {
            _view?.Hide();
            var p = _particlePool.GetExplosionParticles();
            p.gameObject.transform.position = transform.position;
            p.Play();
        }

    }
}