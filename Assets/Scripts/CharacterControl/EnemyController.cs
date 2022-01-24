using System;
using Features.GameTagExtension;
using SwordDefender.CharacterControl.Interfaces;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class EnemyController : MonoBehaviour, IMovementController
    {
        #region Public
        public bool CanMove { get => m_canMove; set => m_canMove = value; }
        public float Speed { get => m_speed; set => m_speed = value; }
        public float RotationSpeed { get; set; } = 0;
        #endregion

        #region Private
        private bool m_canMove = false;
        private float m_speed = 0;
        private float m_speedMultiply = 20; //Задавать значение из конфига.
        private Rigidbody m_rigidbody = new Rigidbody();
        private Transform m_target = null;
        #endregion
        
        #region Unity Methods
        private void Awake()
        {
            m_rigidbody = gameObject.GetComponent<Rigidbody>();
        }

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
            m_rigidbody.velocity = Vector3.zero;
            gameObject.GetComponent<Animator>().enabled = false; // temporarly
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
            var t = m_rigidbody.transform;
            m_rigidbody.velocity = t.forward * (speed * m_speedMultiply);

            m_speed = speed;
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