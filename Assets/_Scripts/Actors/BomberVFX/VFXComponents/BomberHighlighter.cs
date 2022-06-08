
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace BomberGame
{
    public class BomberHighlighter : ActorEffectBase, IHighlightable
    {
        [SerializeField] private Light2D _light;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _highlightColor;
        [SerializeField] private ParticleSystem _particles;
        private void Awake()
        {
            _entity.AddEntityComponent<IHighlightable>(this);
        }
        public void Highlight()
        {
            _particles.Play();
            //_light.color = _highlightColor;
        }

        public void StopHighlight()
        {
            _particles.Stop();
            _particles.Clear();
            //_light.color = _normalColor;
        }
    }
}
