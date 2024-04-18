using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        Mover mover;
        Shooter shooter;
        TurnOrganizer turnOrganizer;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            shooter = GetComponent<Shooter>();

            turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();
            turnOrganizer.AddEnemy();
        }

        private void OnEnable() => turnOrganizer.OnPlayerEnd += TakeTurn;
        private void OnDisable() => turnOrganizer.OnPlayerEnd -= TakeTurn;

        private void TakeTurn()
        {
            if (!shooter.HasShell)
            {
                StartCoroutine(CallReload());
            }
            else if (CheckIfPlayerInRange())
            {
                StartCoroutine(CallShoot());
            }
            else
            { 
                StartCoroutine(CallMove());
            }
        }

        private bool CheckIfPlayerInRange()
        {
            return Physics.Raycast(transform.position + Vector3.forward, transform.forward, 1f, playerLayer);          
        }

        private IEnumerator CallReload()
        {
            yield return shooter.Reload();

            turnOrganizer.EndEnemyTurn();
        }

        private IEnumerator CallMove()
        {
            yield return mover.MoveAndRotate(Vector3.forward);

            turnOrganizer.EndEnemyTurn();
        }

        private IEnumerator CallShoot()
        {
            yield return shooter.Shoot();

            turnOrganizer.EndEnemyTurn();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + Vector3.forward, transform.forward);
        }
    }
}