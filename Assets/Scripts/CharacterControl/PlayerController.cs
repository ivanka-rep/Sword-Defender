using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
using SwordDefender.Game;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour, IMovementController
    {
        #region Serialized Fields
        [SerializeField] private bool canMove = true;
        #endregion

        #region Private
        private Rigidbody m_rigidbody = null;
        private AnimationsManager m_animationsManager = null;
        private CombatManager m_combatManager = null;
        private float m_speed = 0;
        [Min(0.1f)] private float m_sensitivity = 0.5f;
        private bool m_canControl = false;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_rigidbody = gameObject.GetComponent<Rigidbody>();
            m_animationsManager = gameObject.GetComponent<AnimationsManager>();
            m_combatManager = gameObject.GetComponent<CombatManager>();
            m_speed = GameManager.Instance.Config.PlayerParams.Speed;
            m_sensitivity = PlayerPrefs.GetFloat("SENSITIVITY", 0.5f);
            
            GameEventManager.OnGameProcessStarted.AddListener(StartAction);
            GameEventManager.OnGameProcessEnded.AddListener(() => m_canControl = false);
        }

        private void Update()
        {
            if (!m_canControl) return;
            
            Attack();
            Move();
            Rotate();
        }
        #endregion

        #region Public Methods
        public void StopAction()
        {
            GameEventManager.SendGameProcessEnded();
            GameEventManager.SendPlayerGameOver(false);
        }

        #endregion
        
        #region Private Methods

        private void StartAction()
        {
            m_combatManager.Refresh();
            m_canControl = true;
        }
        
        private void Attack()
        {
#if UNITY_EDITOR
            if (Input.GetAxis("Fire1") > 0) m_combatManager.Attack();
#endif
        }

        private void Move()
        {
            if (!canMove) return;
            
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var t = m_rigidbody.transform;
            m_rigidbody.velocity = (t.forward * vertical + t.right * horizontal) * m_speed;
            
            m_animationsManager.SetSpeed(vertical);
        }

        private void Rotate()
        {
            var yRotation = m_rigidbody.rotation.eulerAngles.y;
            var rotationSpeed = 0f;
            
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    m_rigidbody.rotation = Quaternion.Euler(0f,yRotation + touch.deltaPosition.x * m_sensitivity, 0f);
                    rotationSpeed = touch.deltaPosition.x > 0 ? 1 : -1;
                }
            }

#if UNITY_EDITOR
            var mouseX = Input.GetAxis("Mouse X");
            if (mouseX != 0) 
            {
                m_rigidbody.rotation = Quaternion.Euler(0f,yRotation + mouseX , 0f);
                rotationSpeed = mouseX;
            }
#endif
            
            m_animationsManager.SetRotationSpeed(rotationSpeed);
        }
        #endregion
    }
}