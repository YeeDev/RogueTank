using UnityEngine;
using RTank.CoreData;
using Yee.Math;

namespace RTank.Movement.Data
{
    public class Scout : MonoBehaviour, IMoveBehaviour
    {
        Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };

        public Vector3 CalculateMovePoint(MapData mapData)
        {
            directions.Shuffle();

            foreach (Vector3 direction in directions)
            {
                Vector3 movePoint = direction + transform.position;

                if (!mapData.CanMoveToTile(movePoint)) { return movePoint; }
            }

            return transform.position;
        }
    }
}