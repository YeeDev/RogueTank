using UnityEngine;
using RTank.CoreData;
using Yee.Math;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace RTank.Core
{
    public class ObstacleCreator : MonoBehaviour
    {
        [SerializeField] int totalObstacles;
        [SerializeField] GameObject obstaclePrefab;

        public void CreateObstacles(MapData mapData)
        {
            mapData.ResetTile();

            List<int> freeSpaces = mapData.GetFreeTiles();

            for (int i = 0; i < totalObstacles; i++)
            {
                if (freeSpaces.Count <= 0) { break; }

                freeSpaces.Shuffle();
                int index = freeSpaces[0];

                Quaternion rotation = Quaternion.Euler(0, 90 * Random.Range(1, 4), 0);
                Vector3 placePosition = mapData.GetCoordinate(index, 0);
                Instantiate(obstaclePrefab, placePosition, rotation);
                mapData.AddToTile(mapData.GetTile((int)placePosition.x, (int)placePosition.z));

                freeSpaces.RemoveAt(0);
            }
        }
    }
}