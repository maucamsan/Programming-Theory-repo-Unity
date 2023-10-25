using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Pool;

public class DropBehavior : MonoBehaviour, IGenericPoolObject
{
        private XPManager _xpManager; 
        private PoolInstance _poolManagerInstance;
        private bool _inUse = false;
        public bool InUse { get => _inUse; set => _inUse = value; }
        public PoolObjects PoolObjectType;
        public PoolObjects PoolObject { get => PoolObjectType; set => PoolObjectType = value; }
        [SerializeField] double _addExperieceValue = 0.15d;
        [SerializeField] double experienceFractionReduction = 1.23d;
        void OnEnable()
        {
            XPManager.OnLevelUp += ChangeExpOnLevelUp;
        }
        void OnDisable()
        {
            XPManager.OnLevelUp -= ChangeExpOnLevelUp;
        }
        private void ChangeExpOnLevelUp()
        {
            _addExperieceValue /= experienceFractionReduction;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _xpManager = other.gameObject.GetComponent<XPManager>();
                _xpManager.AddExperience(_addExperieceValue);
                SetInUseObject(false);
            }
        }
        public void Action(PoolInstance _poolInstance, Vector3 _position, Quaternion _rotation, Transform _possibleParent = null)
        {
            _poolManagerInstance = _poolInstance;
            SetInUseObject(true);
            transform.SetPositionAndRotation(_position, _rotation);
        }

        public GameObject GetCurrentGameObject()
        {
            return gameObject;
        }

        public void SetInUseObject(bool _isActive)
        {
            _inUse = _isActive;
            gameObject.SetActive(_isActive);
        }

        public void SetObjectPosition(Vector3 _positionToSpawn)
        {
            
        }
}
