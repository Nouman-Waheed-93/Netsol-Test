using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wrld;
using Wrld.Space;

public class RoofTopEnemyManager : EnemyManager
{
    public override bool InstantiateEnemyAt(LatLong latLong)
    {
        var ray = Api.Instance.SpacesApi.LatLongToVerticallyDownRay(latLong);
        LatLongAltitude buildingIntersectionPoint;
        bool didIntersectBuilding = Api.Instance.BuildingsApi.TryFindIntersectionWithBuilding(ray, out buildingIntersectionPoint);
        if (didIntersectBuilding)
        {
            var spawnedEnemy = Instantiate(enemyPrefab) as GameObject;
            spawnedEnemy.GetComponent<EnemyHealth>().manager = this;
            spawnedEnemy.GetComponent<GeographicTransform>().SetPosition(buildingIntersectionPoint.GetLatLong());
            Vector3 position = spawnedEnemy.transform.position;
            position.y = (float)buildingIntersectionPoint.GetAltitude();

            spawnedEnemy.transform.position = position;
        }
        return didIntersectBuilding;
    }
}
