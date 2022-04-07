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
    private string poiSetID;
    [SerializeField]
    private POIDownloader poiDownloader;

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
        poiDownloader.GetPOIs(poiSetID, ReceivePOIs);
    }

    private void ReceivePOIs(LatLongDegrees[] latlons)
    {
        Debug.Log("received");
        foreach (LatLongDegrees pos in latlons)
        {
            Debug.Log("adding");
            enemyPositions.Add(LatLong.FromDegrees(pos.lat, pos.lon));
        }
        StartCoroutine(InstantiateEnemies());
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
