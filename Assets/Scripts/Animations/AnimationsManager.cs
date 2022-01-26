using System.Collections;
using UnityEngine;

namespace SwordDefender.Animations
{
    public class AnimationsManager : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private Animator animator = null;
        #endregion
        
        #region Indexes
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int RotationSpeed = Animator.StringToHash("RotationSpeed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int IsTurning = Animator.StringToHash("IsTurning");
        private static readonly int Dead = Animator.StringToHash("Dead");
        #endregion

        #region Private
        private float m_rotationSpeed = 0f;
        private bool m_isTurning = false;
        #endregion
        
        #region Public Methods
        
        public void SetSpeed(float speed) =>
            animator.SetFloat(Speed, speed);

        public void SetRotationSpeed(float speed)
        {
            m_rotationSpeed = speed;
            animator.SetFloat(RotationSpeed, speed);
            animator.SetBool(IsTurning, true);
            
            if (speed == 0)
                StartCoroutine(CheckForTurningRoutine());
            
        }

        public void SetAttackTrigger(bool flag)
        {
            SetTrigger(flag, Attack);
            if (flag) StartCoroutine(StopAttackRoutine());
        }

        public void StartDeathTrigger(bool flag) =>
            SetTrigger(flag, Dead);

        #endregion

        #region Private Methods

        private void SetTrigger(bool flag, int id)
        {
            if (flag) animator.SetTrigger(id);
            else animator.ResetTrigger(id);
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
                animator.SetBool(IsTurning, false);
            }
            
        }
        #endregion
    }
}