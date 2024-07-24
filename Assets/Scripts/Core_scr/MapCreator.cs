using UnityEngine;
using RTank.CoreData;
using Random = UnityEngine.Random;

namespace RTank.Core
{
    public class MapCreator : MonoBehaviour
    {
        [Header("Basic Terrain")]
        [SerializeField] MapData mapData;
        [SerializeField] GameObject[] terrainPrefab;
        [SerializeField] BoxCollider wallPrefab;
        [SerializeField] ObstacleCreator obstacleCreator;
        [SerializeField] UnitPlacer unitPlacer;
        [SerializeField] MistCreator mistCreator;
        [SerializeField] BuildingsCreator buildingsCreator;

        private void Awake()
        {
            CreateMap();

            obstacleCreator?.CreateObstacles(mapData);
            unitPlacer?.AddUnits(mapData);
            mistCreator?.AddMist(mapData);
            buildingsCreator?.CreateBuildings(mapData);
        }

        private void CreateMap()
        {
            CreateTiles();
            CreateLimits();
        }

        private void CreateTiles()
        {
            for (int x = 0; x < mapData.Columns; x++)
            {
                for (int z = 0; z < mapData.Rows; z++)
                {
                    int randomIndex = Random.Range(0, terrainPrefab.Length - 1);
                    GameObject terrain = Instantiate(terrainPrefab[randomIndex], new Vector3(x, 0, z), Quaternion.identity, transform);
                    terrain.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                }
            }
        }

        private void CreateLimits()
        {
            CreateWall(new Vector3(-1, 0, mapData.MidRow), new Vector3(1, 1, mapData.Rows + 2)); // Left
            CreateWall(new Vector3(mapData.Columns, 0, mapData.MidRow), new Vector3(1, 1, mapData.Rows + 2)); // Right
            CreateWall(new Vector3(mapData.MidColumn, 0, -1), new Vector3(mapData.Columns + 2, 1, 1)); // Botoom
            CreateWall(new Vector3(mapData.MidColumn, 0, mapData.Rows), new Vector3(mapData.Columns + 2, 1, 1)); // Top
        }

        private void CreateWall(Vector3 position, Vector3 size)
        {
            BoxCollider box = Instantiate(wallPrefab, position, Quaternion.identity, transform);
            box.size = size;
        }
    }
}