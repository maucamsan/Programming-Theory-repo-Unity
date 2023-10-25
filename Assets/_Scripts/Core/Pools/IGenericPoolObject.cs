using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Pool
{
    public interface IGenericPoolObject 
    {
        public void SetObjectPosition(Vector3 _positionToSpawn); 
        public void SetInUseObject(bool _isActive);
        public GameObject GetCurrentGameObject();
        public bool InUse {get; set;}
        public PoolObjects PoolObject {get; set;}
        public abstract void Action(PoolInstance _poolInstance, Vector3 _position, Quaternion _rotation, Transform _possibleParent = null);
    }

}