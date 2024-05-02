using System.Collections;
using UnityEngine;
using Yee.Math;
using RTank.CoreData;
using RTank.Core;
using RTank.Movement;
using RTank.Combat;
using RTank.Movement.Data;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Shooter))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] LayerMask playerLayer;
        [SerializeField] float checkerRadius;
        [SerializeField] MapData mapData;

        long previousPoint;
        Mover mover;
        Shooter shooter;
        Transform player;
        TurnOrganizer turnOrganizer;
        IMoveBehaviour moveBehaviour;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            shooter = GetComponent<Shooter>();
            moveBehaviour = GetComponent<IMoveBehaviour>();

            shooter.SetMapData = mapData;

            player = GameObject.FindGameObjectWithTag("Player").transform;

            turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();
            turnOrganizer.AddEnemy();

            previousPoint = mapData.GetTile((int)transform.position.x, (int)transform.position.z);
            mapData.AddToTile(previousPoint);
        }

        private void OnEnable() => turnOrganizer.OnPlayerEnd += TakeTurn;
        private void OnDisable()
        {
            turnOrganizer.RemoveEnemyFromGame();
            turnOrganizer.OnPlayerEnd -= TakeTurn;
        }

        private void TakeTurn()
        {
            if (!shooter.HasShell) { StartCoroutine(CallAction(shooter.Reload())); }
            else if (CheckIfPlayerInRange())
            {
                if (CheckIfFacingPlayer()) { StartCoroutine(CallAction(mover.TryRotation(player.position))); }
                else { StartCoroutine(CallAction(shooter.Shoot())); }
            }
            else { StartCoroutine(Move()); }
        }

        private IEnumerator CallAction(IEnumerator action)
        {
            yield return action;

            turnOrganizer.EndEnemyTurn();
        }

        private IEnumerator Move()
        {
            Vector3 movePoint = moveBehaviour.CalculateMovePoint(mapData);

            long temporalPreviousPoint = previousPoint;
            previousPoint = mapData.GetTile((int)movePoint.x, (int)movePoint.z);
            mapData.AddToTile(previousPoint);

            yield return mover.MoveAndRotate(movePoint);

            mapData.RemoveFromTile(~temporalPreviousPoint);

            turnOrganizer.EndEnemyTurn();
        }

        private bool CheckIfFacingPlayer()
        {
            Vector3 direction = (player.position - transform.position).normalized;

            float angle = MathY.CalculateFlatAngle(transform.forward, direction);

            return angle > 1;
        }

        private bool CheckIfPlayerInRange()
        {
            bool inRange = Physics.CheckSphere(transform.position, checkerRadius, playerLayer);
            bool canShoot = MathY.CheckEqualAxis(transform.position, player.position);

            return inRange && canShoot;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, checkerRadius);
        }
#endif
    }
}