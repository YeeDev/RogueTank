using System.Collections;
using UnityEngine;
using RTank.Movement;
using RTank.Core;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    public class Controller : MonoBehaviour
    {
        Mover mover;
        TurnOrganizer turnOrganizer;

        private void Awake()
        {
            mover = GetComponent<Mover>();

            turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();
        }

        private void Update()
        {
            ReadMoveInput();
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
    }
}