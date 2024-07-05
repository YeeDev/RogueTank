using UnityEngine;
using TMPro;
using RTank.Core;

namespace RTank.UI
{
    public class EndText_UI : MonoBehaviour
    {
        [SerializeField] GameObject endPanel;
        [SerializeField] TextMeshProUGUI endText;

        TurnOrganizer turnOrganizer;

        private void Awake() => turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();

        private void OnEnable() => turnOrganizer.OnEndMatch += ActivateWinPanel;
        private void OnDisable() => turnOrganizer.OnEndMatch += ActivateWinPanel;

        private void ActivateWinPanel(bool playerWon)
        {
            endText.text = playerWon ? "You won!" : "You lost!";
            endPanel.SetActive(true);
        }
    }
}