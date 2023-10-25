using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Core.Pool
{

    public class PoolInstance : MonoBehaviour
    {
        public static Dictionary<PoolObjects, PoolInstance> Instance {get; private set;}

        public PoolObjects poolObjectTypeInstance = PoolObjects.ATTACKS;
        public GameObject PoolPrefab;
        private IGenericPoolObject _poolMember;

        public IGenericPoolObject GenericPoolObjectInstance;
        // Use an enumerator to change the dictionary key
        //public InstatiableItem 
        public int size = 40;
        public List<GameObject> PoolList = new List<GameObject>();

        private void Awake()
        {
            Dictionary<PoolObjects, PoolInstance> _instance = Instance ?? new Dictionary<PoolObjects, PoolInstance>();
            
            //poolPrefab.TryGetComponent(out IGenericPoolObject itemInstance);
            PoolPrefab.TryGetComponent( out GenericPoolObjectInstance);
            //poolObjectInstance = itemInstance.PoolObject;
            if (!_instance.ContainsKey(poolObjectTypeInstance))
            {
                _instance.Add(poolObjectTypeInstance, this);
                Instance = _instance;
                InitPool();
            }
            else
                Destroy(gameObject);

        }

        private void InitPool()
        {
            GenericPoolObjectInstance.GetCurrentGameObject().SetActive(true);


            for (int i = 0; i < size; i++)
            {
                var item = GenericPoolObjectInstance;
                var currPrefab = Instantiate( item.GetCurrentGameObject() );
                // var currentPoolItem = Instantiate( item.GetCurrentGameObject());

                item.PoolObject = GenericPoolObjectInstance.PoolObject;
                PoolList.Add(currPrefab.GetComponent<IGenericPoolObject>().GetCurrentGameObject());
                // PoolList.Add( ( IGenericPoolObject) item);
                currPrefab.SetActive(false);
                DontDestroyOnLoad(item.GetCurrentGameObject());
            }
        }

        public PoolInstance GetPoolInstance(PoolObjects poolObjectType)
        {
            PoolInstance requiredPoolInstance;
            return Instance.TryGetValue(poolObjectType, out requiredPoolInstance) ? requiredPoolInstance : null;
        }

        public GameObject RequestNewInstance(PoolObjects objectType, GameObject objectTypeToCreate)
        {
            // Dictionary<PoolObjects, PoolInstance> _instance = Instance ?? new Dictionary<PoolObjects, PoolInstance>();
            
            //poolPrefab.TryGetComponent(out IGenericPoolObject itemInstance);
            PoolPrefab.TryGetComponent( out GenericPoolObjectInstance);
            //poolObjectInstance = itemInstance.PoolObject;
            if (! Instance.ContainsKey(poolObjectTypeInstance))
            {
                return null;
            }
            else
            {
                var possibleInstance = Instance[objectType].PoolList.Find (x  =>  x.GetComponent<IGenericPoolObject>().InUse == false  );
                if ( possibleInstance)
                    return possibleInstance; 
                var newPrefabToAdd = Instantiate(objectTypeToCreate);
                Instance[objectType].PoolList.Add(newPrefabToAdd);
                newPrefabToAdd.SetActive(false);
                DontDestroyOnLoad(newPrefabToAdd);
                return newPrefabToAdd;
            }

        }

        // public void ItemInstance(Vector2 position, Quaternion rotation, Transform possibleParent = null)
        //         => ItemInstance(new Vector3(position.x, position.y, 0), rotation, possibleParent);

        // public GameObject ItemInstance(Vector3 position, Quaternion rotation, Transform possibleParent = null)
        // {
        //     if (poolList.Count == 0)
        //     {
        //         Debug.LogError($"Pool {genericPoolObjectInstance} not big enough");
        //         return null;
        //     }
        //     var item = poolList[0];
        //     poolList.RemoveAt(0);
        //     item.GetComponent<IGenericPoolObject>().Action(this, position, rotation, possibleParent);
        //     return item;
        // }

        // public void Release(GameObject item)
        // {
        //     item.SetActive(false);
        //     poolList.Add(item);
        // }
    }

}

public enum PoolObjects
{
    MINIONENEMY, PATHENEMY,BOSSENEMY, ATTACKS, CHESTS, POWERUPS, XP,
    BASICATTACK, AREAATTACK
}