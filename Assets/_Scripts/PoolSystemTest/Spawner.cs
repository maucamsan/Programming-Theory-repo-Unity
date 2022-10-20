using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    // void Start()
    // {
    //     GameObject enemy = ObjectPooler.Instance.GetPooledObject("Enemy");
    //     if (enemy != null)
    //     {
    //         Debug.Log("bla");
    //         enemy.SetActive(true);
    //     }
    // }
    ObjectPoolerV2 objectPooler;

    public Transform spawnPosition;
    void Start()
    {
        objectPooler = ObjectPoolerV2.Instance;

        objectPooler.SpawnFromPool("Enemy", spawnPosition.position, spawnPosition.rotation);
       
    }
   
}
