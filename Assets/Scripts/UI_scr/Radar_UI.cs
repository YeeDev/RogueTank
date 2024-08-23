using RTank.Combat;
using UnityEngine;
using System.Collections;

namespace RTank.UI
{
    public class Radar_UI : MonoBehaviour
    {
        [SerializeField] RectTransform emptyRadar;
        [SerializeField] RectTransform unusedRadar;

        const float tileSize = 100f;

        Radar radar;

        private void Start()
        {
            radar = GameObject.FindWithTag("Player").GetComponent<Radar>();
            radar.OnSearch += UpdateUI;

            StartCoroutine(WaitToIntializeUI());
        }

        private IEnumerator WaitToIntializeUI()
        {
            yield return new WaitUntil(() => radar.GetInitialLoads > 0);

            ResizeImage(emptyRadar, radar.GetInitialLoads);
            ResizeImage(unusedRadar, radar.GetInitialLoads);
        }

        private void OnDisable() => radar.OnSearch -= UpdateUI;

        private void UpdateUI() => ResizeImage(unusedRadar, radar.GetCurrentLoads);

        private void ResizeImage(RectTransform image, int multiplier)
        {
            Vector2 newSize = image.sizeDelta;
            newSize.x = multiplier * tileSize;
            image.sizeDelta = newSize;
        }
    }
}