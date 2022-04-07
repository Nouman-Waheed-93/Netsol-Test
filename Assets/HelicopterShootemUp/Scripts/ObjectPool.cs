using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private PooledObject objectPrefab;
    [SerializeField]
    private int initialSize;
    [SerializeField]
    private bool makePoolChild;

    private Stack<PooledObject> pool = new Stack<PooledObject>();

    private void Awake()
    {
        InitializePool();
    }

    public PooledObject GetPooledObject()
    {
        if(pool.Count <= 0)
        {
            return CreatePooledObject();
        }
        return pool.Pop();
    }

    public void ReturnPooledObject(PooledObject objectToReturn)
    {
        objectToReturn.gameObject.SetActive(false);
        pool.Push(objectToReturn);
    }

    private void InitializePool()
    {
        for(int i = 0; i < initialSize; i++)
        {
            PooledObject newObject = CreatePooledObject();
            newObject.gameObject.SetActive(false);
            pool.Push(newObject);
        }
    }

    private PooledObject CreatePooledObject()
    {
        PooledObject newObject = Instantiate(objectPrefab);
        if(makePoolChild)
            newObject.transform.parent = transform;
        newObject.pool = this;
        return newObject;
    }
}
