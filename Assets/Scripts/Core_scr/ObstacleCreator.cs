using UnityEngine;
using RTank.CoreData;
using System;
using Random = UnityEngine.Random;

namespace RTank.Core
{
    public class ObstacleCreator : MonoBehaviour
    {
        [SerializeField] int totalObstacles;
        [Range(1, 100)] [SerializeField] int chanceToSpawn;
        [SerializeField] GameObject obstaclePrefab;
        [SerializeField] MapData mapData;

        private void Awake() => CreateObstacles();

        private void CreateObstacles()
        {
            long obstaclesBits = 0;
            int obstaclesCreated = totalObstacles;

            for (int i = 1; i < mapData.TotalTiles; i++)
            {
                if (obstaclesCreated <= 0) { break; }

                if (Random.Range(1, 100) <= chanceToSpawn)
                {
                    obstaclesBits |= (long)Mathf.Pow(2, i);
                    obstaclesCreated--;
                    Instantiate(obstaclePrefab, mapData.GetCoordinate(i, 0), Quaternion.identity);
                }
            }

            mapData.SetObstaclePositions(obstaclesBits);
        }
    }
}