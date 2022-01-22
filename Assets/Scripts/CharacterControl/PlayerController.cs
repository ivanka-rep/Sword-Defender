using System;
using System.Collections;
using System.Collections.Generic;
using SwordDefender.CharacterControl.Interfaces;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class PlayerController : MonoBehaviour, IMovementController
    {
        #region Serialized Fields
        [SerializeField] private bool canMove = true;
        #endregion

        #region Implementation
        public float Speed { get => m_speed; set => m_speed = value; }
        public float RotationSpeed { get => m_rotationSpeed; set => m_rotationSpeed = value; }
        #endregion

        #region Private
        private float m_speedMultiply = 20; //Задавать значение из конфига.
        private float m_sensitivity = 0.3f; //Задавать как параметр.
        private Rigidbody m_rigidbody = new Rigidbody();
        private float m_speed = 0;
        private float m_rotationSpeed = 0;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            m_rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Move();
            Rotate();
        }
        #endregion

        #region Private Methods
        private void Move()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var t = m_rigidbody.transform;
            m_rigidbody.velocity = (t.forward * vertical + t.right * horizontal) * m_speedMultiply;
            
            m_speed = horizontal > 0 ? horizontal : vertical;
        }

        private void Rotate()
        {
            var yRotation = m_rigidbody.rotation.eulerAngles.y;

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    m_rigidbody.rotation = Quaternion.Euler(0f,yRotation + touch.deltaPosition.x * m_sensitivity, 0f);
                    m_rotationSpeed = 1;
                }
            }
            else m_rotationSpeed = 0;

#if UNITY_EDITOR
            var mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0) 
            {
                m_rigidbody.rotation = Quaternion.Euler(0f,yRotation + mouseX , 0f);
            }
            m_rotationSpeed = mouseX;
#endif
        }
        #endregion
    }
}