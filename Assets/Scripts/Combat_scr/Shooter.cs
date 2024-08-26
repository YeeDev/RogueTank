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
        [SerializeField] AudioClip shootClip;
        [SerializeField] AudioClip reloadClip;

        bool hasShell;
        MapData mapData;
        Animator animator;
        AudioSource audioSource;

        public bool HasShell => hasShell;
        public MapData SetMapData { set => mapData = value; }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        public IEnumerator Shoot()
        {
            hasShell = false;
            Shell shell = Instantiate(shellPrefab, muzzle.position, muzzle.rotation);
            shell.SetMapData = mapData;
            OnUsingAmmo?.Invoke(hasShell);
            audioSource.PlayOneShot(shootClip, 5);

            yield return new WaitUntil(() => shell == null);
        }

        public IEnumerator Reload()
        {
            hasShell = true;

            animator.SetTrigger("Reloading");
            audioSource.PlayOneShot(reloadClip, 2);
            OnUsingAmmo?.Invoke(hasShell);

            yield return new WaitForSeconds(0.8f);
        }
    }
}