using System.Collections;
using SwordDefender.Animations;
using SwordDefender.CharacterControl.Interfaces;
using SwordDefender.Game;
using UnityEngine;

namespace SwordDefender.CharacterControl
{
    public class CombatManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private bool isPlayer = false;
        [SerializeField] private bool godMode = false;
        [SerializeField] private LayerMask layerMask = 0;
        #endregion

        #region Private
        private int HealthPoints //Available only for this class instances
        {
            get => m_healthPoints;
            set
            {
                if(godMode || m_healthPoints <= 0) return;
                m_healthPoints = value;
                if (isPlayer) GameEventManager.SendPlayerHealthChanged(m_healthPoints);
                if (m_healthPoints <= 0) StartDeathAnim();
            }
        }
        
        private IMovementController m_movementController = null;
        private AnimationsManager m_animationsManager = null;
        private ParticlesManager m_particlesManager = null;
        private GameManager m_gameManager = null;
        private int m_healthPoints = 100;
        private int m_damage = 0;
        private bool m_isAttack = false;

        private int m_castMaxDistance = 0;
        private Vector3 m_checkBoxHalfSize = Vector3.zero;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_gameManager = GameManager.Instance;
            m_castMaxDistance = m_gameManager.Config.CombatParams.BoxCastMaxDistance;
            m_checkBoxHalfSize = m_gameManager.Config.CombatParams.CheckBoxHalfSize;
            m_damage = isPlayer
                ? m_gameManager.Config.PlayerParams.Damage
                : m_gameManager.Config.EnemyParams.Damage;

            m_movementController = gameObject.GetComponent<IMovementController>();
            m_animationsManager = gameObject.GetComponent<AnimationsManager>();
            m_particlesManager = isPlayer ? gameObject.GetComponent<ParticlesManager>() : default;
        }
        #endregion

        #region Public Methods

        public void Refresh() =>
            m_healthPoints = 100;

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

            var castResults = new RaycastHit[10];
            int hits = Physics.BoxCastNonAlloc(boxCenter, m_checkBoxHalfSize, t.forward, castResults, boxRotation, m_castMaxDistance, layerMask);

            for (int i = 0; i < hits; i++)
            {
                var col = castResults[i].collider;
                var enemyCombatManager = col.gameObject.GetComponent<CombatManager>();
                enemyCombatManager.HealthPoints -= m_damage;   
            }

            yield return new WaitForSeconds(0.5f);
            m_isAttack = false;
            m_animationsManager.SetAttackTrigger(false);
            if (isPlayer) m_particlesManager.PlayAttackParticle();
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