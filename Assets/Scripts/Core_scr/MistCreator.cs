using UnityEngine;
using RTank.CoreData;

namespace RTank.Core
{
    public class MistCreator : MonoBehaviour
    {
        [SerializeField] GameObject mistPrefab;

        public void AddMist(MapData mapData)
        {
            for (int x = 0; x < mapData.Columns; x++)
            {
                for (int z = 0; z < mapData.Rows; z++)
                {
                    Instantiate(mistPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
                }
            }
        }
    }
}