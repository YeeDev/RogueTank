using UnityEngine;
using TMPro;
using RTank.Core;

namespace RTank.UI
{
    public class Turn_UI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI turnText;

        TurnOrganizer turnOrganizer;

        private void Awake() => turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();

        private void OnEnable()
        {
            turnOrganizer.OnPlayerEnd += SetEnemyTurnText;
            turnOrganizer.OnEnemyEnd += SetPlayerTurnText;
        }

        private void OnDisable()
        {
            turnOrganizer.OnPlayerEnd -= SetEnemyTurnText;
            turnOrganizer.OnEnemyEnd -= SetPlayerTurnText;
        }

        private void SetPlayerTurnText() => turnText.text = "Player";
        private void SetEnemyTurnText() => turnText.text = "Enemy";
    }
}
