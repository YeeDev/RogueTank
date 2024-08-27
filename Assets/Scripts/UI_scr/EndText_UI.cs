using UnityEngine;
using TMPro;
using RTank.Core;
using System.Collections;

namespace RTank.UI
{
    public class EndText_UI : MonoBehaviour
    {
        [Range(0, 100)][SerializeField] float waitEndTime = 1;
        [SerializeField] GameObject endPanel;
        [SerializeField] TextMeshProUGUI endText;
        [SerializeField] TextMeshProUGUI buttonText;

        TurnOrganizer turnOrganizer;

        private void Awake() => turnOrganizer = GameObject.FindGameObjectWithTag("TurnOrganizer").GetComponent<TurnOrganizer>();

        private void OnEnable() => turnOrganizer.OnEndMatch += StartWinPanelActivation;
        private void OnDisable() => turnOrganizer.OnEndMatch += StartWinPanelActivation;

        private void StartWinPanelActivation(bool playerWon) => StartCoroutine(ActivateWinPanel(playerWon));

        private IEnumerator ActivateWinPanel(bool playerWon)
        {
            yield return new WaitForSeconds(waitEndTime);

            endText.text = playerWon ? "You won!" : "You lost!";
            buttonText.text = playerWon ? "Next Level" : "Restart";
            endPanel.SetActive(true);
        }
    }
}