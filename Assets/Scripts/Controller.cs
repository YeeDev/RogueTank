using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTank.Movement;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    public class Controller : MonoBehaviour
    {
        Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            ReadMoveInput();
        }

        private void ReadMoveInput()
        {
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
            {
                bool pressedHorizontal = Input.GetButtonDown("Horizontal");
                float axis = pressedHorizontal ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("Vertical");
                Vector3 newPosition = pressedHorizontal ? Vector3.right : Vector3.forward;
                newPosition *= axis;
                StartCoroutine(mover.MoveAndRotate(newPosition));
            }
        }
    }
}