using System.Collections;
using Features.GameTagExtension;
using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
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
        [SerializeField] private int damage = 100; //Todo: устанавливать значение через конфиг 
        #endregion

        #region Private
        private IMovementController m_movementController = null;
        private int m_healthPoints = 100;
        private bool m_isAttack = false;
        #endregion
        
        #region Public Methods

        public void Attack()
        {
            m_isAttack = true;
            animationsManager.SetAttackTrigger(true);
            StartCoroutine(StopAttackRoutine());
        }
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_movementController = gameObject.GetComponent<IMovementController>();
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
            
            enemyCombatManager.HealthPoints -= damage;
            m_isAttack = false;
            animationsManager.SetAttackTrigger(false);
        }

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

        IEnumerator StopAttackRoutine()
        {
            yield return new WaitForSeconds(1f);
            m_isAttack = false;
        }
        #endregion
    }
}