using UnityEngine;
using System;

namespace RTank.Core
{
    public class TurnOrganizer : MonoBehaviour
    {
        public event Action OnPlayerEnd;
        public event Action OnEnemyEnd;
        public event Action<bool> OnEndMatch;

        int totalEnemies;
        int waitingForEnemies;
        bool turnRunning;

        public bool TurnRunning => turnRunning;
        public bool MatchEnded { get; private set; }

        public void RunTurn() => turnRunning = !turnRunning;
        public void AddEnemy() => totalEnemies++;

        private void Start()
        {
            waitingForEnemies = totalEnemies;
        }

        public void EndPlayerTurn()
        {
            if (OnPlayerEnd != null && totalEnemies > 0) { OnPlayerEnd(); }
            else
            {
                //TODO End level
                turnRunning = false;
                MatchEnded = true;
                OnEndMatch?.Invoke(true);
            }
        }

        public void EndEnemyTurn()
        {
            waitingForEnemies--;

            if (waitingForEnemies <= 0)
            {
                turnRunning = false;
                waitingForEnemies = totalEnemies;

                if (GameObject.FindGameObjectWithTag("Player") == null) { OnEndMatch?.Invoke(false); }
                else { OnEnemyEnd?.Invoke(); }
            }
        }

        public void RemoveEnemyFromGame()
        {
            totalEnemies--;
            waitingForEnemies--;
        }
    }
}
