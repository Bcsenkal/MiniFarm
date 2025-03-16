
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class ButtonAnimation : MonoBehaviour, IPointerDownHandler
    {
        private RectTransform _rect;
        private readonly Vector3 _defaultScale = new Vector3(1f, 1f, 1f);
        private readonly Vector3 _pressedDownScale = new Vector3(.8f, .8f, .8f);
        private Coroutine _ScalePopRoutine;

        private Button _button;
        private bool _disabled;

        public bool Disabled
        {
            get => _disabled;
            set => _disabled = value;
        }

        void Start()
        {
            _button = GetComponent<Button>();
            _rect = GetComponent<RectTransform>();
            
        }

        

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Disabled) return;
            _rect.localScale = _pressedDownScale;
            AudioManager.Instance.PlayButtonClick();
            _ScalePopRoutine = StartCoroutine(FramePopRoutine());
        }

        IEnumerator FramePopRoutine()
        {
            
            var counter = 0f;
            while (counter<=.5f)
            {
                var t = Easings.QuadEaseOut(counter, 0f, 1f, .5f);
                var s = Mathf.Lerp(.8f, 1f, t);
                _rect.localScale = _defaultScale * s;
                counter += Time.deltaTime;
                yield return null;
            }
            _rect.localScale = _defaultScale;

        }

        private void OnDisable()
        {
            if (_ScalePopRoutine != null)
            {
                StopCoroutine(_ScalePopRoutine);
            }
        }

        
    }
}
