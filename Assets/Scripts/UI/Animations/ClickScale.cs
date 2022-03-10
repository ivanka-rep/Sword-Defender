using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

namespace SwordDefender.UI.Animations
{
    public class ClickScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region SerializeFields
        [SerializeField] private Selectable button = null;
        [SerializeField] private Ease ease = Ease.OutBack;
        
        [SerializeField] private float scale = 0.95f;
        [SerializeField] private float timeUp = 0.1f;
        [SerializeField] private float timeDown = 0.1f;
        #endregion

        #region Private
        private Tween tweenScale = null;
        #endregion

        #region Unity methods
        private void Start()
        {
            if (!button)
                button = GetComponent<Selectable>();
        }
        #endregion

        #region Public methods
        public void OnPointerDown(PointerEventData _eventData)
        {
            if (button && button.interactable)
            {
                tweenScale?.Complete();
                tweenScale = transform.DOScale(scale, timeDown).SetEase(ease).SetUpdate(true);
            }
        }

        public void OnPointerUp(PointerEventData _eventData)
        {
            if (button && button.interactable)
            {
                tweenScale?.Complete();
                tweenScale = transform.DOScale(1.0f, timeUp).SetEase(ease).SetUpdate(true);
            }
        }
        #endregion
    }
}
