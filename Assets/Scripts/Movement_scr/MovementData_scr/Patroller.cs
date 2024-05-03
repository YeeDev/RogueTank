using UnityEngine;
using RTank.CoreData;
using Random = UnityEngine.Random;

namespace RTank.Movement.Data
{
    public class Patroller : MonoBehaviour, IMoveBehaviour
    {
        int axis = 1;
        Vector3 moveDirection;

        private void Awake() => moveDirection = Random.Range(1, 100) < 51 ? Vector3.forward : Vector3.right;

        public Vector3 CalculateMovePoint(MapData mapData)
        {
            Vector3 movePoint = transform.position;

            for (int i = 0; i < 2; i++)
            {
                movePoint = transform.position + moveDirection * axis;

                if (mapData.CanMoveToTile(movePoint))
                {
                    if (i == 1) { return transform.position; }

                    axis *= -1;
                }
            }

            return movePoint;
        }
    }
}