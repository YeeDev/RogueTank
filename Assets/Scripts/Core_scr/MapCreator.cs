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

        private void Awake() => CreateMap();

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
                    GameObject terrain = Instantiate(terrainPrefab[randomIndex], new Vector3(x, -1, z), Quaternion.identity, transform);
                    terrain.transform.rotation = Quaternion.Euler(0, Random.Range(0, 4) * 90, 0);
                }
            }
        }

        private void CreateLimits()
        {
            CreateWall(new Vector3(-1, 0, mapData.MidRow), new Vector3(1, 1, mapData.Rows + 2));
            CreateWall(new Vector3(mapData.Columns, 0, mapData.MidRow), new Vector3(1, 1, mapData.Rows + 2));
            CreateWall(new Vector3(mapData.MidColumn, 0, -1), new Vector3(mapData.Columns + 2, 1, 1));
            CreateWall(new Vector3(mapData.MidColumn, 0, mapData.Rows), new Vector3(mapData.Columns + 2, 1, 1));
        }

        private void CreateWall(Vector3 position, Vector3 size)
        {
            BoxCollider box = Instantiate(wallPrefab, position, Quaternion.identity, transform);
            box.size = size;
        }
    }
}