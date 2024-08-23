using System.Collections;
using UnityEngine;
using RTank.CoreData;
using RTank.Movement;
using System;

namespace RTank.Combat
{
    public class Shooter : MonoBehaviour
    {
        public Action<bool> OnUsingAmmo;

        [SerializeField] Transform muzzle;
        [SerializeField] Shell shellPrefab;

        bool hasShell;
        MapData mapData;
        Animator animator;

        public bool HasShell => hasShell;
        public MapData SetMapData { set => mapData = value; }

        private void Awake() => animator = GetComponent<Animator>();

        public IEnumerator Shoot()
        {
            hasShell = false;
            Shell shell = Instantiate(shellPrefab, muzzle.position, muzzle.rotation);
            shell.SetMapData = mapData;
            OnUsingAmmo?.Invoke(hasShell);

            yield return new WaitUntil(() => shell == null);
        }

        public IEnumerator Reload()
        {
            hasShell = true;

            animator.SetTrigger("Reloading");
            OnUsingAmmo?.Invoke(hasShell);

            yield return new WaitForSeconds(0.8f);
        }
    }
}