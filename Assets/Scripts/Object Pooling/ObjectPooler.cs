using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoSingleton<ObjectPooler>
{
    [SerializeField] private List<ObjectPooledItem> itemsToPool;
    [SerializeField] private List<ObjectPooledItem> textsToPool;
    [SerializeField] private GameObject pooledObjectHolder;

    private List<PoolableObjectBase> pooledObjects;

    private void Awake()
    {
        pooledObjects = new List<PoolableObjectBase>();
        foreach (ObjectPooledItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                PoolableObjectBase obj = Instantiate(item.objectToPool).GetComponent<PoolableObjectBase>();
                obj.transform.SetParent(pooledObjectHolder.transform);
                obj.gameObject.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }

    public void DeInit()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].gameObject.SetActive(false);
        }
    }

    public PoolableObjectBase GetPooledObjectWithType(PoolObjectType refType)
    {
        for (int i = pooledObjects.Count - 1; i > -1; i--)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].ObjectType == refType)
            {
                return pooledObjects[i];
            }
        }
        foreach (ObjectPooledItem item in itemsToPool)
        {
            if (item.objectToPool.GetComponent<PoolableObjectBase>().ObjectType == refType)
            {
                if (item.shouldExpand)
                {
                    PoolableObjectBase obj = Instantiate(item.objectToPool).GetComponent<PoolableObjectBase>();
                    obj.gameObject.SetActive(false);
                    pooledObjects.Add(obj);
                    obj.transform.SetParent(pooledObjectHolder.transform);
                    return obj;
                }
            }
        }
        return null;
    }
}