using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTank.CoreData;

namespace RTank.Core
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] MapData mapData;
        [SerializeField] GameObject terrainPrefab;
        [SerializeField] BoxCollider wallPrefab;

        private void Awake()
        {
            CreateMap();
        }

        private void CreateMap()
        {
            for (int x = 0; x < mapData.Columns; x++)
            {
                for (int z = 0; z < mapData.Rows; z++)
                {
                    Instantiate(terrainPrefab, new Vector3(x, -1, z), Quaternion.identity, transform);
                }
            }

            BoxCollider box = Instantiate(wallPrefab, new Vector3(-1, 0, mapData.MidRow), Quaternion.identity, transform);
            box.size = new Vector3(1, 1, mapData.Rows + 2);

            box = Instantiate(wallPrefab, new Vector3(mapData.Columns, 0, mapData.MidRow), Quaternion.identity, transform);
            box.size = new Vector3(1, 1, mapData.Rows + 2);

            box = Instantiate(wallPrefab, new Vector3(mapData.MidColumn, 0, -1), Quaternion.identity, transform);
            box.size = new Vector3(mapData.Columns + 2, 1, 1);

            box = Instantiate(wallPrefab, new Vector3(mapData.MidColumn, 0, mapData.Rows), Quaternion.identity, transform);
            box.size = new Vector3(mapData.Columns + 2, 1, 1);
        }
    }
}