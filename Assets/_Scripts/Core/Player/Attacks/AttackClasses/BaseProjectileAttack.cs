using System.Collections;
using System.Collections.Generic;
using Core.Pool;
using UnityEngine;
using System;
public class BaseProjectileAttack : MonoBehaviour, IGenericPoolObject
{
    public event Action <GameObject, GameObject> OnProjectileCollision;
    private GameObject _caster;
    private float _speed;
    private float _range;
    private Vector3 _travelDirection;
    private float _distanceTraveled;
    private bool _inUse = false;
    public bool InUse { get => _inUse; set {_inUse = value;} }
    [SerializeField] PoolObjects _poolObject = PoolObjects.BASICATTACK;
    public PoolObjects PoolObject { get => _poolObject; set {_poolObject = value;} }

    public void Fire (GameObject caster, Vector3 target, float speed, float range)
    {
        // SetInUseObject(true);
        _caster = caster;
        _speed = speed;
        _range = range;
        _travelDirection = target - transform.position;
        _travelDirection.z = 0f;
        _travelDirection.Normalize();
        _distanceTraveled = 0.0f;
    }

    void Update()
    {
        float distToTravel = _speed * Time.deltaTime;
        transform.Translate(_travelDirection * distToTravel);
        _distanceTraveled += distToTravel;
        if (_distanceTraveled > _range)
            SetInUseObject(false);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") )
        {
            OnProjectileCollision?.Invoke(_caster, other.gameObject);
            InUse = false;
            gameObject.SetActive(false);
        }
    }


    public void SetObjectPosition(Vector3 _positionToSpawn)
    {
        transform.position = _positionToSpawn;
    }

    public void SetInUseObject(bool _isActive)
    {
        InUse = _isActive;
        gameObject.SetActive(_isActive);
    }

    public GameObject GetCurrentGameObject()
    {
        return gameObject;
    }

    public void Action(PoolInstance _poolInstance, Vector3 _position, Quaternion _rotation, Transform _possibleParent = null)
    {
        SetInUseObject(true);
    }
}
