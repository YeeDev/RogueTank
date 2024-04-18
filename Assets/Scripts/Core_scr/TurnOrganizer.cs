using UnityEngine;
using System;

namespace RTank.Core
{
    public class TurnOrganizer : MonoBehaviour
    {
        public Action OnPlayerEnd;

        int totalEnemies;
        int waitingForEnemies;
        bool turnRunning;

        public bool TurnRunning => turnRunning;

        public void RunTurn() => turnRunning = !turnRunning;
        public void AddEnemy() => totalEnemies++;

        private void Start()
        {
            waitingForEnemies = totalEnemies;
        }

        public void EndPlayerTurn()
        {
            if (OnPlayerEnd != null) { OnPlayerEnd(); }
            else
            {
                //TODO End level
                turnRunning = false;
                Debug.Log("Player wins");
            }
        }

        public void EndEnemyTurn()
        {
            waitingForEnemies--;

            if (waitingForEnemies <= 0)
            {
                turnRunning = false;
                waitingForEnemies = totalEnemies;
            }
        }
    }
}