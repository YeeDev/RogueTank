using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTank.Controls
{
    [RequireComponent(typeof(Mover))]
    public class Controller : MonoBehaviour
    {
        bool inAction;
        Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (inAction) { return; }

            Vector3 newPosition = Vector3.zero;
            if (Input.GetButtonDown("Horizontal"))
            {
                newPosition = Vector3.right * Input.GetAxisRaw("Horizontal");
                StartCoroutine(mover.Move(newPosition));
            }
            else if (Input.GetButtonDown("Vertical"))
            {
                newPosition = Vector3.forward * Input.GetAxisRaw("Vertical");
                StartCoroutine(mover.Move(newPosition));
            }
        }
    }
}