using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Pool;
using Core.Player;
public class DropsSpawner : MonoBehaviour
{
    public PoolObjects DropPoolObject = PoolObjects.XP;
    private PoolInstance _poolManager;
    private int _enemiesPoolSize;
    private IGenericPoolObject _enemyType;
    private int _enemiesCounter = 0;
    private GameObject _referenceGameObject;
    private GameObject _playerReference;
    [SerializeField] float spawningInterval;
    Vector3 _randomSpawningPosition = Vector3.zero;
    void Start()
    {
        PlayerController.PlayerToFollow += SetPlayerReference;
        InvokeRepeating(nameof (SpawnDrop), spawningInterval, spawningInterval);
    }
    void OnDisable()
    {
        PlayerController.PlayerToFollow -= SetPlayerReference;
    }

    private void SetPlayerReference(GameObject player)
    {
        _playerReference = player;
        _randomSpawningPosition = player.transform.position;
    }
    [ContextMenu("Spawn enemy")]
    public void SpawnDrop()
    {

        _poolManager = PoolInstance.Instance[DropPoolObject];
        _enemiesPoolSize = _poolManager.size;
        if (_enemiesCounter >= _enemiesPoolSize)
        {
            RequestNewEnemyInstance();
            return;
        }
        _enemyType = _poolManager.PoolList[_enemiesCounter++].GetComponent<IGenericPoolObject>();
        _referenceGameObject = _enemyType.GetCurrentGameObject();
        _enemyType.GetCurrentGameObject().SetActive(true);
        var randomPos = new Vector2 ( _randomSpawningPosition.x + Random.Range(Random.Range(-10, 10), Random.Range(-10, 10)), _randomSpawningPosition.y + Random.Range(Random.Range(-10, 10), Random.Range(-10, 10))) ;
        _enemyType?.Action(_poolManager, randomPos, Quaternion.identity, transform);
    } 

    private void RequestNewEnemyInstance()
    {
        _referenceGameObject = _poolManager.RequestNewInstance(DropPoolObject, _referenceGameObject);
        _enemyType = _referenceGameObject.GetComponent<IGenericPoolObject>();
        // _enemyType.GetCurrentGameObject().GetComponent<DropBehavior>().PlayerGO = _playerReference;
        _enemyType.GetCurrentGameObject().SetActive(true);
        var randomPos = new Vector2 ( _randomSpawningPosition.x + Random.Range(Random.Range(-10, 10), Random.Range(-10, 10)), _randomSpawningPosition.y + Random.Range(Random.Range(-10, 10), Random.Range(-10, 10))) ;
        _enemyType?.Action(_poolManager, randomPos, Quaternion.identity, transform);
    }
}
