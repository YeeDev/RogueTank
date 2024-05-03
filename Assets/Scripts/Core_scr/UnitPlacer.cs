using UnityEngine;
using RTank.CoreData;
using Random = UnityEngine.Random;

namespace RTank.Core
{
    public class UnitPlacer : MonoBehaviour
    {
        [SerializeField] int numberOfPatrolEnemies;
        [SerializeField] GameObject patrolEnemyPrefab;
        [SerializeField] int numberOfRoamEnemies;
        [SerializeField] GameObject roamEnemyPrefab;
        [SerializeField] GameObject playerPrefab;

        public void AddUnits(MapData mapData)
        {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            SpawnEnemies(numberOfPatrolEnemies, patrolEnemyPrefab, mapData);
            SpawnEnemies(numberOfRoamEnemies, roamEnemyPrefab, mapData);
        }

        private void SpawnEnemies(int numberToSpawn, GameObject unitToSpawn, MapData mapData)
        {
            while (numberToSpawn > 0)
            {
                int index = Random.Range(1, mapData.TotalTiles);

                Vector3 point = mapData.GetCoordinate(index, 0);
                if (mapData.TileIsOccupied(index) || point.x <= 2 || point.z <= 2) { continue; }

                Instantiate(unitToSpawn, mapData.GetCoordinate(index, 0), Quaternion.identity);
                numberToSpawn--;
            }
        }
    }
}