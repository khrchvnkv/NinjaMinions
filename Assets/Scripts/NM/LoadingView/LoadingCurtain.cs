using System.Collections;
using UnityEngine;

namespace NM.LoadingView
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1.0f;
        }
        public void Hide() => StartCoroutine(FadeIn());
        private IEnumerator FadeIn()
        {
            float t = 1.0f;
            while (t > 0.0f)
            {
                t -= Time.deltaTime;
                t = Mathf.Clamp01(t);
                _canvasGroup.alpha = t;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}
