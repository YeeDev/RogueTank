using System.Collections;
using UnityEngine;
using RTank.Movement;
using RTank.Core;
using RTank.Combat;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Shooter))]
    public class Controller : MonoBehaviour
    {
        Mover mover;
        Shooter shooter;
        TurnOrganizer turnOrganizer;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            shooter = GetComponent<Shooter>();

            turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();
        }

        private void Update()
        {
            ReadInput(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"), "CallMovement");
            ReadInput(Input.GetMouseButtonDown(0), "CallShoot");
            ReadInput(Input.GetMouseButtonDown(1), "CallReload");
        }

        private void ReadInput(bool condition, string coroutineToRun)
        {
            if (turnOrganizer.TurnRunning) { return; }

            if (condition)
            {
                turnOrganizer.RunTurn();
                StartCoroutine(coroutineToRun);
            }
        }

        private IEnumerator CallMovement()
        {
            Vector3 newPosition = CalculateAxis();

            yield return StartCoroutine(mover.MoveAndRotate(newPosition));

            turnOrganizer.EndPlayerTurn();
        }

        private static Vector3 CalculateAxis()
        {
            bool pressedHorizontal = Input.GetButtonDown("Horizontal");
            float axis = pressedHorizontal ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Vertical");
            Vector3 newPosition = pressedHorizontal ? Vector3.right : Vector3.forward;
            newPosition *= axis;

            return newPosition;
        }

        private IEnumerator CallShoot()
        {
            yield return shooter.Shoot();

            turnOrganizer.EndPlayerTurn();
        }

        private IEnumerator CallReload()
        {
            yield return shooter.Reload();

            turnOrganizer.EndPlayerTurn();
        }
    }
}