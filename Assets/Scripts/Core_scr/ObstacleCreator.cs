using UnityEngine;
using RTank.CoreData;
using System;
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
            
            while (totalObstacles > 0)
            {
                int index = Random.Range(1, mapData.TotalTiles);

                if (mapData.TileIsOccupied(index)) { continue; }

                Quaternion rotation = Quaternion.Euler(0, 90 * Random.Range(1, 4), 0);
                Instantiate(obstaclePrefab, mapData.GetCoordinate(index, 0), rotation);
                mapData.AddToTile((long)Mathf.Pow(2, index));
                totalObstacles--;
            }
        }
    }
}