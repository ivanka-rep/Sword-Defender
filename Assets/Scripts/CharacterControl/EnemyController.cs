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
        #region Serizlized Fields
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private AnimationsManager animationsManager = null;
        [SerializeField] private CombatManager combatManager = null;
        // [SerializeField] private SkinnedMeshRenderer meshRenderer = null;
        #endregion

        #region Private
        private GameManager m_gameManager = null;
        private Transform m_target = null;
        private bool m_canMove = false;
        private bool m_canAttack = true;
        private float m_speedMultiply = 0;
        #endregion
        
        #region Unity Methods

        private void Start()
        {
            m_gameManager = GameManager.Instance;
            m_speedMultiply = m_gameManager.GameConfig.PlayerStats.Speed;
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
            rigidbody.velocity = Vector3.zero;
            animationsManager.SetSpeed(0);
            
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
            var t = rigidbody.transform;
            rigidbody.velocity = t.forward * (speed * m_speedMultiply);
            animationsManager.SetSpeed(speed);
        }

        private void Rotate() =>
            transform.LookAt(m_target);
        
        #endregion

        #region Coroutines

        private IEnumerator AttackRoutine()
        {
            if (!m_canAttack) yield break;
            var wfs = new WaitForSeconds(1f);
            
            combatManager.Attack();
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