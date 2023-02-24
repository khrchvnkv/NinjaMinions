using NM.Services;
using NM.Services.Input;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionMove : MonoBehaviour
    {
        private const float _movementSpeed = 2.0f;

        [SerializeField] private CharacterController _characterController;

        private Transform _camera;

        private void Start()
        {
            _camera = Camera.main.transform;
        }
        public void Move(Vector3 axis)
        {
            var movementVector = _camera.TransformDirection(axis);
            movementVector.y = 0.0f;
            movementVector.Normalize();

            movementVector += Physics.gravity;
            _characterController.Move(movementVector * _movementSpeed * Time.deltaTime);
        }
    }
}