using System.Collections;
using UnityEngine;
using RTank.Movement;
using RTank.Core;
using RTank.Combat;
using RTank.CoreData;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    [RequireComponent(typeof(Shooter))]
    public class Controller : MonoBehaviour, ITransferData
    {
        long previousPosition = 1;
        Mover mover;
        Shooter shooter;
        MapData mapData;
        TurnOrganizer turnOrganizer;

        public void TransferMapData(MapData mapData) => this.mapData = mapData;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            shooter = GetComponent<Shooter>();

            turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();
        }

        private void Start()
        {
            mapData.AddToTile(previousPosition);
            shooter.SetMapData = mapData;
        }

        private void Update()
        {
            if (turnOrganizer.TurnRunning) { return; }

            ReadMoveInput();
            ReadInput(Input.GetMouseButtonDown(0), shooter.Shoot());
            ReadInput(Input.GetMouseButtonDown(1), shooter.Reload());
        }

        private void ReadMoveInput()
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                Vector3 axis = CalculateAxis();
                if (mapData.CanMoveToTile(axis)) { ReadInput(true, mover.Stuck(axis)); }
                else
                {
                    mapData.RemoveFromTile(~previousPosition);
                    ReadInput(true, mover.MoveAndRotate(axis));
                    previousPosition = mapData.GetTile((int)axis.x, (int)axis.z);
                    mapData.AddToTile(previousPosition);
                }
            }
        }

        private void ReadInput(bool condition, IEnumerator action)
        {
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

        private Vector3 CalculateAxis()
        {
            bool pressedHorizontal = Input.GetButtonDown("Horizontal");
            float axis = pressedHorizontal ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Vertical");
            Vector3 newPosition = pressedHorizontal ? Vector3.right : Vector3.forward;
            newPosition *= axis;

            return newPosition + transform.position;
        }
    }
}