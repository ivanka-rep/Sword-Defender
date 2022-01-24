using Features.GameTagExtension;
using SwordDefender.Animations;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class EnemyController : MonoBehaviour
    {
        #region Serizlized Fields
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private AnimationsManager animationsManager = null;
        #endregion

        #region Private
        private bool m_canMove = false;
        private float m_speedMultiply = 20; //Задавать значение из конфига.
        private Transform m_target = null;
        #endregion
        
        #region Unity Methods

        private void Update()
        {
            if (!m_canMove) return;
            
            Move();
            Rotate();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!col.gameObject.TryGetComponent<GameTagReference>(out var gameTagRef)) return;
            if (!gameTagRef.ExistsTagName("Player")) return;
            
            m_canMove = false;
            rigidbody.velocity = Vector3.zero;
            animationsManager.SetSpeed(0);
            //Attack   
        }

        #endregion

        #region Public Methods
        public void StartMoving(Transform target)
        {
            m_target = target;
            m_canMove = true;
            Rotate();
        }
        #endregion
        
        #region Private
        private void Move()
        {
            var speed = m_canMove ? 1f : 0;
            var t = rigidbody.transform;
            rigidbody.velocity = t.forward * (speed * m_speedMultiply);
            animationsManager.SetSpeed(speed);
        }

        private void Rotate()
        {
            if (m_target == null)
            {
                Debug.Log("Target is null");
                return;
            }
            transform.LookAt(m_target);
        }
        #endregion
    }
    
    
}