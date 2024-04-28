using UnityEngine;
using RTank.CoreData;

public interface IMoveBehaviour
{
    public Vector3 CalculateMovePoint(MapData mapData);
}
