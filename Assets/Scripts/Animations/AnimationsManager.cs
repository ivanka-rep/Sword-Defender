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

        #region Public Methods
        
        public void SetSpeed(float speed) =>
            animator.SetFloat(Speed, speed);

        public void SetRotationSpeed(float speed)
        {
            animator.SetFloat(RotationSpeed, speed);
            animator.SetBool(IsTurning, speed != 0);
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
        #endregion
    }
}