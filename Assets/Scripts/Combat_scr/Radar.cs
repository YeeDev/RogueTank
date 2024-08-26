using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RTank.Combat
{
    public class Radar : MonoBehaviour
    {
        public Action OnSearch;

        [SerializeField] float radarTime = 2f;
        [SerializeField] AudioClip radarClip;

        int radarLoads;
        int currentRadarLoads;
        AudioSource audioSource;
        List<ParticleSystem> enemiesRadar = new List<ParticleSystem>();

        public int GetInitialLoads => radarLoads;
        public int GetCurrentLoads => currentRadarLoads;
        public bool HasLoads => currentRadarLoads > 0;

        private void Awake() => audioSource = GetComponent<AudioSource>();

        public void SetInitialRadarLoads(int loads)
        {
            radarLoads = loads;
            currentRadarLoads = radarLoads;
        }

        public IEnumerator Search()
        {
            if (enemiesRadar.Count <= 0) { PopulateList(); }

            if (currentRadarLoads > 0 && enemiesRadar.Count > 0)
            {
                foreach (ParticleSystem particle in enemiesRadar)
                {
                    if (particle != null) { particle.Play(); }
                }

                currentRadarLoads--;
                audioSource.PlayOneShot(radarClip, 4);

                OnSearch?.Invoke();
            }

            yield return new WaitForSeconds(radarTime);
        }

        private void PopulateList()
        {
           GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemiesRadar.Add(enemy.GetComponentInChildren<ParticleSystem>());
            }
        }
    }
}