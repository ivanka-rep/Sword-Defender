using System.Collections;
using Features.GameTagExtension;
using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
using SwordDefender.Game;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class CombatManager : MonoBehaviour
    {
        #region Public
        public int HealthPoints
        {
            get => m_healthPoints;
            set
            {
                m_healthPoints = value;
                if (m_healthPoints <= 0) StartDeathAnim();
            }
        }
        #endregion
        
        #region Serialized Fields
        [SerializeField] private AnimationsManager animationsManager = null;
        [SerializeField] private GameTag enemyTag;
        #endregion

        #region Private
        private IMovementController m_movementController = null;
        private GameManager m_gameManager = null;
        private int m_healthPoints = 100;
        private int m_damage = 0;
        private bool m_isAttack = false;
        #endregion

        #region Unity Methods

        private void Start()
        {
            m_movementController = gameObject.GetComponent<IMovementController>();
            m_gameManager = GameManager.Instance;
            m_damage = enemyTag.GameTagName == "Enemy"
                ? m_gameManager.GameConfig.PlayerStats.Damage
                : m_gameManager.GameConfig.EnemyStats.Damage;
        }

        private void OnTriggerEnter(Collider col)
        {
            if (!m_isAttack) return;
            if (!col.gameObject.TryGetComponent<GameTagReference>(out var gameTagRef))
                gameTagRef = col.gameObject.GetComponentInParent<GameTagReference>();
            
            if(gameTagRef == null || !gameTagRef.ExistsTagName(enemyTag.GameTagName)) return;

            var enemyCombatManager = col.gameObject.TryGetComponent<CombatManager>(out var combatManager) 
                ? combatManager 
                : col.gameObject.GetComponentInParent<CombatManager>();
            
            enemyCombatManager.HealthPoints -= m_damage;
            m_isAttack = false;
            animationsManager.SetAttackTrigger(false);
        }

        #endregion

        #region Public Methods
        public void Attack() =>
            StartCoroutine(AttackRoutine());

        #endregion
        
        #region Private Methods

        private void StartDeathAnim()
        {
            animationsManager.StartDeathTrigger(true);
            m_movementController.StopAllActions(true);
            m_isAttack = false;
        }

        #endregion

        #region Coroutines

        IEnumerator AttackRoutine()
        {
            m_isAttack = true;
            animationsManager.SetAttackTrigger(true);
            yield return new WaitForSeconds(1f);
            m_isAttack = false;
        }
        #endregion
    }
}