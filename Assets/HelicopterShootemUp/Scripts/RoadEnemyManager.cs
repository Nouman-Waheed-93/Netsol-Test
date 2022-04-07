using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld.Space;
using Wrld;
using Wrld.Transport;

public class RoadEnemyManager : EnemyManager
{
    public override bool InstantiateEnemyAt(LatLong latLong)
    {
        var spawnedEnemy = Instantiate(enemyPrefab) as GameObject;
        spawnedEnemy.GetComponent<EnemyHealth>().manager = this;
        spawnedEnemy.GetComponent<RoadEnemy>().Spawn(latLong);
        return true;
    }
}
