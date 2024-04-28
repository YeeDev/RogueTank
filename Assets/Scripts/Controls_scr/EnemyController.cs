using System.Collections;
using UnityEngine;
using Yee.Math;
using RTank.Core;
using RTank.Movement;
using RTank.Combat;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Shooter))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] LayerMask playerLayer;
        [SerializeField] float checkerRadius;

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

            player = GameObject.FindGameObjectWithTag("Player").transform;

            turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();
            turnOrganizer.AddEnemy();
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
            else if (CheckIfPlayerInRange()) { StartCoroutine(CallAction(shooter.Shoot())); }
            else { StartCoroutine(CallAction(mover.MoveAndRotate(moveBehaviour.CalculateMovePoint()))); }
        }

        private IEnumerator CallAction(IEnumerator action)
        {
            yield return action;

            turnOrganizer.EndEnemyTurn();
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