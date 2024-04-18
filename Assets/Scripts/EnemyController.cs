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
            StartCoroutine(CallMove());
        }

        private IEnumerator CallMove()
        {
            yield return mover.MoveAndRotate(Vector3.forward);

            turnOrganizer.EndEnemyTurn();
        }
    }
}