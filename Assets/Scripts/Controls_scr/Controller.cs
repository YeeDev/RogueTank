using System.Collections;
using UnityEngine;
using RTank.Movement;
using RTank.Core;

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
            ReadMoveInput();
            ReadShootInput();
        }

        private void ReadMoveInput()
        {
            if (turnOrganizer.TurnRunning) { return; }

            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                turnOrganizer.RunTurn();
                StartCoroutine(CallMovement());
            }
        }

        private IEnumerator CallMovement()
        {
            Vector3 newPosition = CalculateAxis();

            yield return StartCoroutine(mover.MoveAndRotate(newPosition));

            turnOrganizer.EndTurn();
        }

        private static Vector3 CalculateAxis()
        {
            bool pressedHorizontal = Input.GetButtonDown("Horizontal");
            float axis = pressedHorizontal ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Vertical");
            Vector3 newPosition = pressedHorizontal ? Vector3.right : Vector3.forward;
            newPosition *= axis;

            return newPosition;
        }

        private void ReadShootInput()
        {
            if (turnOrganizer.TurnRunning) { return; }

            if (Input.GetMouseButtonDown(0))
            {
                turnOrganizer.RunTurn();
                StartCoroutine(CallShoot());
            }
        }

        private IEnumerator CallShoot()
        {
            yield return shooter.Shoot();

            turnOrganizer.EndTurn();
        }
    }
}