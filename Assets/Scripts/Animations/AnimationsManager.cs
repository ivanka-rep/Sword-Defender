using System;
using System.Collections;
using UnityEngine;

namespace SwordDefender.Animations
{
    public class AnimationsManager : MonoBehaviour
    {
        #region Indexes
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int RotationSpeed = Animator.StringToHash("RotationSpeed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");
        private static readonly int Dead = Animator.StringToHash("Dead");
        #endregion

        #region Private
        private Animator m_animator = null;
        private float m_rotationSpeed = 0f;
        #endregion

        #region Unity Methods

        private void Awake() =>
            m_animator = gameObject.GetComponent<Animator>();

        #endregion
        
        #region Public Methods
        
        public void SetSpeed(float speed) =>
            m_animator.SetFloat(Speed, speed);

        public void SetRotationSpeed(float speed)
        {
            m_rotationSpeed = speed;
            m_animator.SetFloat(RotationSpeed, speed);
            m_animator.SetBool(IsTurning, true);
            
            if (speed == 0)
                StartCoroutine(CheckForTurningRoutine());
            
        }

        public void SetAttackTrigger(bool flag)
        {
            SetTrigger(flag, Attack);
            //if (flag) StartCoroutine(StopAttackRoutine());
        }

        public void StartDeathTrigger(bool flag) =>
            SetTrigger(flag, Dead);

        #endregion

        #region Private Methods

        private void SetTrigger(bool flag, int id)
        {
            if (flag) m_animator.SetTrigger(id);
            else m_animator.ResetTrigger(id);
        }
        #endregion
        
        #region Coroutines

        private IEnumerator StopAttackRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            SetAttackTrigger(false);
        }

        private IEnumerator CheckForTurningRoutine()
        {
            var elapsedTime = 0f;
            var waitTime = 0.25f;
            var rotationSpeed = 0f;

            while (elapsedTime < waitTime)
            {
                rotationSpeed += m_rotationSpeed;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            if (rotationSpeed < 0.1f && rotationSpeed > -0.1)
            {
                m_animator.SetBool(IsTurning, false);
            }
            
        }
        #endregion
    }
}