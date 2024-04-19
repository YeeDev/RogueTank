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
            ReadInput(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"), mover.MoveAndRotate(CalculateAxis()));
            ReadInput(Input.GetMouseButtonDown(0), shooter.Shoot());
            ReadInput(Input.GetMouseButtonDown(1), shooter.Reload());
        }

        private void ReadInput(bool condition, IEnumerator action)
        {
            if (turnOrganizer.TurnRunning) { return; }

            if (condition)
            {
                turnOrganizer.RunTurn();
                StartCoroutine(CallAction(action));
            }
        }

        private IEnumerator CallAction(IEnumerator action)
        {
            yield return StartCoroutine(action);

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
    }
}