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

        int radarLoads;
        int currentRadarLoads;
        List<ParticleSystem> enemiesRadar = new List<ParticleSystem>();

        public int GetInitialLoads => radarLoads;
        public int GetCurrentLoads => currentRadarLoads;
        public bool HasLoads => currentRadarLoads > 0;

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