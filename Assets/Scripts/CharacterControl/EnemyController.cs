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
        private bool m_canMove = false;
        private bool m_canAttack = true;
        private float m_speedMultiply = 0;
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
            m_speedMultiply = m_gameManager.GameConfig.PlayerStats.Speed;
        }
        
        private void FixedUpdate()
        {
            if (!m_canMove) return;
            
            Move();
            Rotate();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!col.gameObject.TryGetComponent<GameTagReference>(out var gameTagRef)) return;
            if (!gameTagRef.ExistsTagName("StopTrigger")) return;
            
            m_canMove = false;
            m_rigidbody.velocity = Vector3.zero;
            m_animationsManager.SetSpeed(0);
            
            StartCoroutine(AttackRoutine());
        }

        #endregion

        #region Public Methods
        public void StartMoving(Transform target)
        {
            if (target == null)
            {
                Debug.LogError("Target is null");
                return;
            }
            
            m_target = target;
            m_combatManager.Refresh();
            Rotate();
            m_canMove = true;
            m_canAttack = true;
        }
        
        public void StopAllActions(bool isDead = false)
        {
            m_canAttack = false;
            if (isDead) StartCoroutine(DeathRoutine());
        }
        #endregion
        
        #region Private
        private void Move()
        {
            var speed = m_canMove ? 1f : 0;
            var t = m_rigidbody.transform;
            m_rigidbody.velocity = t.forward * (speed * m_speedMultiply);
            m_animationsManager.SetSpeed(speed);
        }

        private void Rotate() =>
            transform.LookAt(m_target);
        
        #endregion

        #region Coroutines

        private IEnumerator AttackRoutine()
        {
            if (!m_canAttack) yield break;
            var wfs = new WaitForSeconds(1f);
            
            m_combatManager.Attack();
            yield return wfs;
            yield return AttackRoutine();
        }

        private IEnumerator DeathRoutine()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        #endregion
    }
    
    
}