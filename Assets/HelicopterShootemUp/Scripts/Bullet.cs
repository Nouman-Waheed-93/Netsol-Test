using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;

public class Bullet : PooledObject
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damageAmount;
    [SerializeField]
    private float lifeTime;

    private Rigidbody rigidbody;
    private float remainingLifeTime;
    #region Unity Callbacks

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        remainingLifeTime = lifeTime;
    }

    private void Update()
    {
        SpendLife();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = transform.position + transform.forward * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        IDamageable damageable;
        if(other.transform.parent.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(damageAmount);
        }
        pool.ReturnPooledObject(this);
    }

    #endregion

    #region Private Methods
    private void SpendLife()
    {
        remainingLifeTime -= Time.deltaTime;
        if (remainingLifeTime <= 0)
        {
            pool.ReturnPooledObject(this);
        }
    }
    #endregion
}
