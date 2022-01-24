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
        #endregion

        #region Public Methods
        
        public void SetSpeed(float speed) =>
            animator.SetFloat(Speed, speed);

        public void SetRotationSpeed(float speed) => 
            animator.SetFloat(RotationSpeed, speed);
        
        public void SetAttackTrigger(bool flag)
        {
            if (flag) animator.SetTrigger(Attack);
            else animator.ResetTrigger(Attack);
        }
        
        #endregion
    }
}