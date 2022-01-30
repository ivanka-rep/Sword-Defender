using System;
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
                if(godMode) return;
                
                m_healthPoints = value;
                if (m_healthPoints <= 0) StartDeathAnim();
            }
        }
        #endregion
        
        #region Serialized Fields
        [SerializeField] private GameTag enemyTag = null;
        [SerializeField] private bool godMode = false;
        #endregion

        #region Private
        private IMovementController m_movementController = null;
        private AnimationsManager m_animationsManager = null;
        private GameManager m_gameManager = null;
        private int m_healthPoints = 100;
        private int m_damage = 0;
        private bool m_isAttack = false;

        private readonly int m_maxDistance = 5;
        private readonly Vector3 m_checkBoxHalfSize = new Vector3(3f, 0.25f, 2);
        #endregion

        #region Unity Methods

        private void Start()
        {
            m_gameManager = GameManager.Instance;
            m_damage = enemyTag.GameTagName == "Enemy"
                ? m_gameManager.GameConfig.PlayerStats.Damage
                : m_gameManager.GameConfig.EnemyStats.Damage;
            
            m_movementController = gameObject.GetComponent<IMovementController>();
            m_animationsManager = gameObject.GetComponent<AnimationsManager>();
        }
        #endregion

        #region Public Methods
        public void Attack()
        {
            if(m_isAttack) return;
            StartCoroutine(AttackRoutine());
        }

        #endregion
        
        #region Coroutines

        IEnumerator AttackRoutine()
        {
            var t = this.transform;
            var tPosition = t.position;
            var boxRotation = t.rotation;
            var boxCenter = new Vector3(tPosition.x, tPosition.y + 3, tPosition.z + 3);
            
            m_isAttack = true;
            m_animationsManager.SetAttackTrigger(true);

            var castResults = Physics.BoxCastAll(boxCenter, m_checkBoxHalfSize, t.forward, boxRotation, m_maxDistance);
            if (castResults.Length > 0)
            {
                foreach (var castResult in castResults)
                {
                    var col = castResult.collider;
                
                    if (!col.gameObject.TryGetComponent<GameTagReference>(out var gameTagRef))
                        gameTagRef = col.gameObject.GetComponentInParent<GameTagReference>();
            
                    if(gameTagRef == null || !gameTagRef.ExistsTagName(enemyTag.GameTagName)) continue;

                    var enemyCombatManager = col.gameObject.TryGetComponent<CombatManager>(out var combatManager) 
                        ? combatManager 
                        : col.gameObject.GetComponentInParent<CombatManager>();
            
                    enemyCombatManager.HealthPoints -= m_damage;   
                }
            }

            yield return new WaitForSeconds(0.5f);
            m_isAttack = false;
            m_animationsManager.SetAttackTrigger(false);
        }
        #endregion
        
        #region Private Methods

        private void StartDeathAnim()
        {
            m_animationsManager.StartDeathTrigger(true);
            m_movementController.StopAllActions(true);
            m_isAttack = false;
        }

        #endregion
    }
}