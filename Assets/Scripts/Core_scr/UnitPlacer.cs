using UnityEngine;
using System.Collections.Generic;
using RTank.CoreData;
using Yee.Math;

namespace RTank.Core
{
    public class UnitPlacer : MonoBehaviour
    {
        [SerializeField] int numberOfPatrolEnemies;
        [SerializeField] GameObject patrolEnemyPrefab;
        [SerializeField] int numberOfRoamEnemies;
        [SerializeField] GameObject roamEnemyPrefab;
        [SerializeField] GameObject playerPrefab;

        public int GetTotalEemies => numberOfPatrolEnemies + numberOfRoamEnemies;

        public void AddUnits(MapData mapData)
        {
            ITransferData transfer = Instantiate(playerPrefab, Vector3.zero,Quaternion.identity). GetComponent<ITransferData>();
            transfer.TransferMapData(mapData);

            SpawnEnemies(numberOfPatrolEnemies, patrolEnemyPrefab, mapData);
            SpawnEnemies(numberOfRoamEnemies, roamEnemyPrefab, mapData);
        }

        private void SpawnEnemies(int numberToSpawn, GameObject unitToSpawn, MapData mapData)
        {
            List<int> freeSpaces = mapData.GetFreeTiles(true);

            for (int i = 0; i < numberToSpawn; i++)
            {
                if(freeSpaces.Count <= 0) { break; }

                freeSpaces.Shuffle();
                int index = freeSpaces[0];

                Vector3 placePosition = mapData.GetCoordinate(index, 0);
                ITransferData transfer = Instantiate(unitToSpawn, placePosition, Quaternion.identity).GetComponent<ITransferData>();
                transfer.TransferMapData(mapData);
                mapData.AddToTile(mapData.GetTile((int)placePosition.x, (int)placePosition.z));

                freeSpaces.RemoveAt(0);
            }
        }
    }
}