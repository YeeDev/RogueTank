using UnityEngine;
using RTank.Combat;

namespace RTank.UI
{
    public class Ammo_UI : MonoBehaviour
    {
        [SerializeField] GameObject bullet;

        Shooter shooter;

        private void Start()
        {
            shooter = GameObject.FindWithTag("Player").GetComponent<Shooter>();
            shooter.OnUsingAmmo += UpdateUI;
        }

        private void OnDisable() => shooter.OnUsingAmmo -= UpdateUI;

        private void UpdateUI(bool hasAmmo) => bullet.SetActive(hasAmmo);
    }
}