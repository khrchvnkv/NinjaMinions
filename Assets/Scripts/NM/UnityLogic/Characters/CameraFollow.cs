using UnityEngine;

namespace NM.UnityLogic.Characters
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _movingSpeed;
        [SerializeField] private Vector3 _offset;

        private Transform _target;

        public void SetTarget(Transform target) => _target = target;
        private void LateUpdate()
        {
            if (_target)
            {
                var newPosition = _target.position + _offset;
                _transform.position = Vector3.Lerp(_transform.position, newPosition, _movingSpeed * Time.deltaTime);
            }
        }
    }
}