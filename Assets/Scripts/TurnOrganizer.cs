using UnityEngine;
using System;

namespace RTank.Core
{
    public class TurnOrganizer : MonoBehaviour
    {
        public Action OnPlayerEnd;

        bool turnRunning;

        public bool TurnRunning => turnRunning;

        public void RunTurn() => turnRunning = !turnRunning;

        public void EndTurn()
        {
            if (OnPlayerEnd != null) { OnPlayerEnd(); }
            else
            {
                //TODO change to actual logic when enemies get implemented
                turnRunning = false;
                Debug.Log("Player wins");
            }
        }
    }
}