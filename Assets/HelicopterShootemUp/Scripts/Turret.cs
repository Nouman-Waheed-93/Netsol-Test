using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    private ObjectPool bulletPool;
    /// <summary>
    /// the minimum time between firing two bullets
    /// </summary>
    [SerializeField]
    private float firingInterval;
    
    [SerializeField]
    private float targetingAngle;

    private Transform myTransform;
    private Transform target;
    private float lastFireTime;

    #region Unity Callbacks
    private void Start()
    {
        myTransform = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            if(other.GetComponentInParent<IDamageable>() != null)
            {
                if(Vector3.Angle(myTransform.forward, other.transform.position - myTransform.position) < targetingAngle)
                    target = other.transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == target)
            target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && Time.time - lastFireTime > firingInterval)
        {
            if (!target.gameObject.activeInHierarchy)
            {
                target = null;
                return;
            }
            GameObject newBullet = bulletPool.GetPooledObject().gameObject;
            newBullet.SetActive(true);
            newBullet.transform.position = myTransform.position;
            newBullet.transform.LookAt(target.position);
            lastFireTime = Time.time;
        }
    }
    #endregion
}
