using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int size;
        public bool canExpand;

    }

    public static ObjectPooler Instance;
    public List<ObjectPoolItem> itemsToPool;
    void Awake()
    {
        Instance = this;
    }

    public List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = (GameObject) Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.objectToPool.tag == tag)
            {
                if (item.canExpand)
                {
                    GameObject obj = (GameObject) Instantiate(item.objectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }
        return null;
    }
}
