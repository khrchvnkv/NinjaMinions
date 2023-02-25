using NM.Data;
using NM.Services.PersistentProgress;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionMove : MonoBehaviour, ISavedProgressWriter
    {
        [SerializeField] private CharacterController _characterController;

        private Transform _camera;
        
        private string _id;
        private float _movementSpeed;

        private void Start()
        {
            _camera = Camera.main.transform;
        }
        public void Construct(string id, float movementSpeed)
        {
            _id = id;
            _movementSpeed = movementSpeed;
        }
        public void Move(Vector3 axis)
        {
            var movementVector = _camera.TransformDirection(axis);
            movementVector.y = 0.0f;
            movementVector.Normalize();

            movementVector += Physics.gravity;
            _characterController.Move(movementVector * _movementSpeed * Time.deltaTime);
        }
        public void LoadProgress(SaveSlotData slot)
        {
            foreach (var minion in slot.MinionsData)
            {
                if (minion.Id == _id)
                {
                    _characterController.enabled = false;
                    _characterController.transform.position = minion.Position.AsUnityVector();
                    _characterController.enabled = true;
                    return;
                }
            }
        }
        public void SaveProgress(SaveSlotData slot)
        {
            var newPositionData = _characterController.transform.position.AsVector3Data();
            foreach (var minion in slot.MinionsData)
            {
                if (minion.Id == _id)
                {
                    minion.Position = newPositionData;
                    return;
                }
            }

            var newMinionData = new HealthCharacterData();
            newMinionData.Position = newPositionData;
            slot.MinionsData.Add(newMinionData);
        }
    }
}