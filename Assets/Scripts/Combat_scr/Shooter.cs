using System.Collections;
using UnityEngine;
using RTank.CoreData;
using RTank.Movement;

namespace RTank.Combat
{
    public class Shooter : MonoBehaviour
    {
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
            if (!hasShell)
            {
                Debug.Log("No shell, cannot shoot. Lost turn.");
                yield break;
            }

            hasShell = false;
            Shell shell = Instantiate(shellPrefab, muzzle.position, muzzle.rotation);
            shell.SetMapData = mapData;

            yield return new WaitUntil(() => shell == null);
        }

        public IEnumerator Reload()
        {
            hasShell = true;

            animator.SetTrigger("Reloading");

            yield return new WaitForSeconds(0.8f);
        }
    }
}