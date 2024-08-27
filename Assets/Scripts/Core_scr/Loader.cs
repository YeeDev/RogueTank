using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTank.Core
{
    public class Loader : MonoBehaviour
    {
        int nextLevelIndex;

        TurnOrganizer turnOrganizer;

        private void Awake()
        {
            GameObject turnOrganizerObject = GameObject.FindGameObjectWithTag("TurnOrganizer");
            if (turnOrganizerObject != null) { turnOrganizer = turnOrganizerObject.GetComponent<TurnOrganizer>(); }

            nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        }

        private void OnEnable() { if(turnOrganizer != null) { turnOrganizer.OnEndMatch += SetLevelIndex; } }
        private void OnDisable() { if (turnOrganizer != null) { turnOrganizer.OnEndMatch -= SetLevelIndex; } }

        private void SetLevelIndex(bool playerWon) => nextLevelIndex = playerWon ? nextLevelIndex : nextLevelIndex - 1;

        public void LoadLevel(bool nextLevel) => SceneManager.LoadScene(nextLevel ? nextLevelIndex % SceneManager.sceneCountInBuildSettings : 0);

        public void LoadFirstLevel() => SceneManager.LoadScene(1);
    }
}