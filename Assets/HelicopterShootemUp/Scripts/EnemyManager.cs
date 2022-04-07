using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld.Space;
using Wrld;

public abstract class EnemyManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject enemyPrefab;
    [SerializeField]
    private float enemySpawnDelay;
    [SerializeField]
    private float enemyRespawnDelay;
    [SerializeField]
    private List<LatLongDegrees> enemyPositionsInDegrees;

    private List<LatLong> enemyPositions;

    private WaitForSeconds respawnDelay;
    private WaitForSeconds spawnDelay;

    #region Unity Callbacks

    private void Awake()
    {
        respawnDelay = new WaitForSeconds(enemyRespawnDelay);
        spawnDelay = new WaitForSeconds(enemySpawnDelay);
        InitializeEnemyPositions();
    }

    private void Start()
    {
        StartCoroutine(InstantiateEnemies());
    }

    #endregion

    #region Public Methods

    public void RespawnEnemy(EnemyHealth enemy)
    {
        StartCoroutine(CoroutineRespawnEnemy(enemy));
    }

    #endregion

    #region Private Methods

    private void InitializeEnemyPositions()
    {
        enemyPositions = new List<LatLong>();
        foreach(LatLongDegrees pos in enemyPositionsInDegrees)
        {
            enemyPositions.Add(LatLong.FromDegrees(pos.lat, pos.lon));
        }
    }

    private IEnumerator CoroutineRespawnEnemy(EnemyHealth enemy)
    {
        yield return respawnDelay;
        enemy.gameObject.SetActive(true);
    }

    private IEnumerator InstantiateEnemies()
    {
        for(int i = 0; i < enemyPositions.Count; i++)
        {
            while (!InstantiateEnemyAt(enemyPositions[i]))
            {
                yield return spawnDelay;
            }
        }
    }

    public abstract bool InstantiateEnemyAt(LatLong latLong);

    #endregion
}
