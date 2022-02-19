using System;
using System.Collections;
using Features.GameTagExtension;
using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
using SwordDefender.Game;
using UnityEngine;

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
        private bool m_canAttack = true;
        
        private float m_speed = 20f; //todo: реализовать preloader, после чего инициализировать в Awake
        private float m_distanceOffset = 4f; //to config
        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            m_rigidbody = gameObject.GetComponent<Rigidbody>();
            m_combatManager = gameObject.GetComponent<CombatManager>();
            m_animationsManager = gameObject.GetComponent<AnimationsManager>();
        }

        private void Start()
        {
            m_gameManager = GameManager.Instance;
            m_speed = m_gameManager.GameConfig.PlayerStats.Speed;
        }

        #endregion

        #region Public Methods
        public void StartMoving(Transform target)
        {
            m_target = target;
            m_combatManager.Refresh();
            m_canAttack = true;
            
            StartCoroutine(MoveRoutine());
        }
        
        public void StopAllActions(bool isDead = false)
        {
            m_canAttack = false;
            if (isDead) StartCoroutine(DeathRoutine());
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

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        #endregion
    }
    
    
}