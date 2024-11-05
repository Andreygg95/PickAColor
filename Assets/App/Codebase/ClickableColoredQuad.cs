using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PickAColor
{
    public class ClickableColoredQuad : MonoBehaviour
    {
        public Color MyColor => _myColor;
        public event Action<ClickableColoredQuad, Color> Clicked;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        
        private Color _myColor;

        private Coroutine _blinkCoroutine;
        
        public void Init(Color color, Vector2 uvPos)
        {
            _image.color = color;
            _myColor = color;
            _button.onClick.AddListener(OnClick);
            _rectTransform.anchorMin = uvPos;
            _rectTransform.anchorMax = uvPos;
        }

        public void SetAlpha(float a)
        {
            var color = _image.color;
            color.a = a;
            _image.color = color;
        }

        private void OnClick()
        {
            Clicked?.Invoke(this, _myColor);
        }

        public void Blink()
        {
            if (_blinkCoroutine != null)
            {
                StopCoroutine(_blinkCoroutine);
                _image.color = _myColor;
            }

            _blinkCoroutine = StartCoroutine(BlinkAsync());
        }

        private IEnumerator BlinkAsync()
        {
            var targetColor = Color.black;
            float t = 0;
            float time = .5f;

            while (t<time)
            {
                _image.color = Color.Lerp(_myColor, targetColor, Mathf.Sin(t / time * Mathf.PI));
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime;
            }

            _image.color = _myColor;
        }
    }
}