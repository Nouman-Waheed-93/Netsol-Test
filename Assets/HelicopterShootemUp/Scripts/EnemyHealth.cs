using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [HideInInspector]
    public EnemyManager manager;

    [SerializeField]
    private float initialHealth;

    private float currHealth;

    #region Unity Callbacks

    private void OnEnable()
    {
        currHealth = initialHealth;
    }

    #endregion

    public void TakeDamage(float damageAmount)
    {
        currHealth -= damageAmount;
        if(currHealth <= 0)
        {
            gameObject.SetActive(false);
            manager.RespawnEnemy(this);
        }
    }
}
