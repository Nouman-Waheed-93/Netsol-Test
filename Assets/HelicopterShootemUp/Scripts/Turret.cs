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

    private Transform target;
    private float lastFireTime;

    private void OnTriggerStay(Collider other)
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            if(other.GetComponentInParent<IDamageable>() != null)
            {
                if(Vector3.Angle(transform.forward, other.transform.position - transform.position) < targetingAngle)
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
            newBullet.transform.position = transform.position;
            newBullet.transform.LookAt(target.position);
            lastFireTime = Time.time;
        }
    }
}
