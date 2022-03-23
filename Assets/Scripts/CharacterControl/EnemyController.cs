using System.Collections;
using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
using SwordDefender.Game;
using UnityEngine;
using UnityEngine.Events;

namespace SwordDefender.CharacterControl
{
    public class EnemyController : MonoBehaviour, IMovementController
    {
        #region Private
        private GameManager m_gameManager = null;
        private Rigidbody m_rigidbody = null;
        private CombatManager m_combatManager = null;
        private AnimationsManager m_animationsManager = null;
        private Transform m_target = null;
        private bool m_canAttack = false;
        
        private float m_speed = 0;
        private float m_distanceOffset = 0;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_gameManager = GameManager.Instance;
            m_speed = m_gameManager.Config.EnemyParams.Speed;
            m_distanceOffset = m_gameManager.Config.EnemyParams.DistanceToPlayerOffset;
            
            m_rigidbody = gameObject.GetComponent<Rigidbody>();
            m_combatManager = gameObject.GetComponent<CombatManager>();
            m_animationsManager = gameObject.GetComponent<AnimationsManager>();
            
            GameEventManager.OnGameProcessStarted.AddListener(() => m_canAttack = true);
            GameEventManager.OnGameProcessEnded.AddListener(() => m_canAttack = false);
        }

        #endregion

        #region Public Methods
        public void StartMoving(Transform target)
        {
            m_target = target;
            m_combatManager.Refresh();

            StartCoroutine(MoveRoutine());
        }
        
        public void StopAction()
        {
            StopAllCoroutines();
            StartCoroutine(DisableObjectRoutine());
            GameEventManager.SendEnemyKilled();
        }

        #endregion
        
        #region Coroutines

        private IEnumerator MoveRoutine()
        {
            var distance = Vector3.Distance(transform.position, m_target.position) - m_distanceOffset;
            var time = distance / m_speed;
            var timeElapsed = 0f;
            
            transform.LookAt(m_target);
            m_animationsManager.SetSpeed(1f);

            while (timeElapsed < time)
            {
                m_rigidbody.velocity = transform.forward * m_speed;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            m_rigidbody.velocity = Vector3.zero;
            m_animationsManager.SetSpeed(0);
            
            StartCoroutine(AttackRoutine());
        }
        
        private IEnumerator AttackRoutine()
        {
            while (m_canAttack)
            {
                m_combatManager.Attack();
                transform.LookAt(m_target);
                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator DisableObjectRoutine()
        {
            m_rigidbody.velocity = Vector3.zero;
            
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        #endregion
    }
    
    
}