using System.Collections;
using Features.GameTagExtension;
using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class EnemyController : MonoBehaviour, IMovementController
    {
        #region Serizlized Fields
        [SerializeField] private new Rigidbody rigidbody = null;
        [SerializeField] private AnimationsManager animationsManager = null;
        [SerializeField] private CombatManager combatManager = null;
        [SerializeField] private SkinnedMeshRenderer meshRenderer = null;
        #endregion

        #region Private
        private bool m_canMove = false;
        private bool m_canAttack = true;
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
            // var oldColor = meshRenderer.sharedMaterial.color;
            // var newColor = new Color(oldColor.r, oldColor.g, oldColor.b, 0f);
            //
            // var elapsedTime = 0f;
            // var waitTime = 2f;
            //
            // while (elapsedTime < waitTime)
            // {
            //     meshRenderer.sharedMaterial.color = Color.Lerp(oldColor, newColor, (elapsedTime / waitTime));
            //     elapsedTime += Time.deltaTime;
            //     yield return null;
            // }
            
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        #endregion
    }
    
    
}