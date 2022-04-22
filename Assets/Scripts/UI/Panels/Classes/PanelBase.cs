using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SwordDefender.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class PanelBase : MonoBehaviour
    {
        private CanvasGroup m_canvasGroup = null;
        protected readonly UnityEvent<bool> m_onPanelActivated = new UnityEvent<bool>();
        
        private void Start()
        {
            m_canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }
        
        public void SetActive(bool flag, float transitionTime)
        {
            StartCoroutine(SetActiveRoutine(flag, transitionTime));
            m_onPanelActivated.Invoke(flag);
        }

        private IEnumerator SetActiveRoutine(bool flag, float transitionTime)
        {
            for (float time = 0f; time < transitionTime; time += Time.deltaTime / transitionTime)
            { 
                m_canvasGroup.alpha = flag ? Mathf.Lerp(0f, 1f, time) : Mathf.Lerp(1f, 0f, time);
                yield return null;
            }

            m_canvasGroup.alpha = flag ? 1f : 0f;
            m_canvasGroup.interactable = flag;
            m_canvasGroup.blocksRaycasts = flag;
        }
    }
}