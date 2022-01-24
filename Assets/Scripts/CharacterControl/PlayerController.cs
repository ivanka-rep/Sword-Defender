using SwordDefender.Animations;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class PlayerController : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private bool canMove = true;
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private AnimationsManager animationsManager = null;
        #endregion

        #region Private
        private float m_speedMultiply = 20; //Задавать значение из конфига.
        private float m_sensitivity = 0.3f; //Задавать как параметр.
        #endregion

        #region Unity Methods
        private void Update()
        {
            Move();
            Rotate();
        }
        #endregion

        #region Private Methods
        private void Move()
        {
            if (!canMove) return;
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var t = rigidbody.transform;
            rigidbody.velocity = (t.forward * vertical + t.right * horizontal) * m_speedMultiply;
            
            animationsManager.SetSpeed(vertical);
        }

        private void Rotate()
        {
            var yRotation = rigidbody.rotation.eulerAngles.y;
            var rotationSpeed = 0f;
            
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    rigidbody.rotation = Quaternion.Euler(0f,yRotation + touch.deltaPosition.x * m_sensitivity, 0f);
                    rotationSpeed = touch.deltaPosition.x > 0 ? 1 : -1;
                }
            }

#if UNITY_EDITOR
            var mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0) 
            {
                rigidbody.rotation = Quaternion.Euler(0f,yRotation + mouseX , 0f);
                rotationSpeed = mouseX;
            }
#endif
            
            animationsManager.SetRotationSpeed(rotationSpeed);
        }
        #endregion
    }
}