using Core;
using UnityEngine;

namespace Systems
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _playerTransform;
        public float smoothSpeed = 0.125f;
        public Vector3 offset;



        private void Start()
        {
            _playerTransform = GameManager.instance.playerBehaviour.transform;
        }

        private void LateUpdate()
        {
            var desiredPosition = _playerTransform.position + offset;
            var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
