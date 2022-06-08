using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BomberGame.UI
{
    public class ActorUIHighlighter : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _highlighterColor;
        private Coroutine _highlightRoutine;

        public void Init()
        {
            try
            {
                _image.color = _normalColor;
            }
            catch
            {
                Debug.LogError("Image for highlight not assigned");
            }
        }

        public void Highlight()
        {
            _image.color = _highlighterColor;
        }

        public void Highlight(float duration)
        {
            if (_highlightRoutine != null)
                StopCoroutine(_highlightRoutine);
            _highlightRoutine = StartCoroutine(Highlighting(duration));
        }

        public void Hightlight()
        {
            _image.color = _highlighterColor;
        }

        public void StopHighlight()
        {
            if (_highlightRoutine != null)
                StopCoroutine(_highlightRoutine);
            _image.color = _normalColor;
        }

        private IEnumerator Highlighting(float duration)
        {
            _image.color = _highlighterColor;
            yield return new WaitForSeconds(duration);
            _image.color = _normalColor;
            _highlightRoutine = null;
        }

        
    }

}