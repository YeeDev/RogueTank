using UnityEngine;
using RTank.CoreData;
using Random = UnityEngine.Random;

namespace RTank.Core
{
    public class BuildingsCreator : MonoBehaviour
    {
        [SerializeField] GameObject[] buildings;
        [SerializeField] Vector3[] edgeRotations;
        [SerializeField] Vector3[] topRotations;

        public void CreateBuildings(MapData mapData)
        {
            for (int x = -1; x < mapData.Columns + 1; x++)
            {
                for (int z = 0; z < mapData.Rows + 1; z++)
                {
                    if (x > -1 && x < mapData.Columns && z < mapData.Rows) { continue; }

                    Instantiate(PickRandomBuilding(), new Vector3(x, 0, z), RandomRotation(mapData, x), transform);
                }
            }
        }

        private Quaternion RandomRotation(MapData mapData, int x)
        {
            int random = Random.Range(0, edgeRotations.Length);
            Vector3 eulerRotation = x > -1 && x < mapData.Columns ? topRotations[random] : edgeRotations[random];

            return Quaternion.Euler(eulerRotation);
        }

        private GameObject PickRandomBuilding() => buildings[Random.Range(0, buildings.Length)];
    }
}