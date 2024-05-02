using UnityEngine;
using RTank.CoreData;

namespace RTank.Movement.Data
{
    public interface IMoveBehaviour
    {
        public Vector3 CalculateMovePoint(MapData mapData);
    }
}