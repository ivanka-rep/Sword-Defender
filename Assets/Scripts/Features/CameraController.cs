using UnityEngine;

namespace Features
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        #region Serialized Fields

        [Header("Player To Follow")]
        //Player root
        [SerializeField] private Transform root;

        [Header("Follow Properties")]
        //Follow values
        [SerializeField] private float distance = 10.0f;
        [SerializeField] private float height = 10.0f;
        [SerializeField] private float smoothness = 0.15f;

        [Header("Rotation Properties")]
        //Rotate with input
        [SerializeField] private bool rotateCamera = false;

        #endregion

        #region Private
        private Vector3 m_offset;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            
            m_offset = transform.position * 0.7f;
            m_offset.x = 0f;
        }

        
        //Camera follow and rotation
        private void FixedUpdate()
        {
            var rootPos = root.position;
            var cameraT = transform;
            var cameraPos = cameraT.position;
            
            if (rotateCamera)
            {
                var requiredPos = root.forward * -distance + root.up * height + rootPos;
                transform.position = Vector3.Lerp(cameraPos, requiredPos, smoothness);
                transform.LookAt(rootPos);
                return;
            }

            //if camera rotation is disabled
            var targetRotation = Quaternion.LookRotation(rootPos - cameraPos);
            transform.position = Vector3.Lerp(cameraPos, rootPos + m_offset, smoothness);
            transform.rotation = Quaternion.Slerp(cameraT.rotation, targetRotation, smoothness);
        }

        #endregion
    }
}