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

        public bool HasShell => hasShell;
        public MapData SetMapData { set => mapData = value; }

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

            yield return new WaitForSeconds(1); //Change to animation and sound time
        }
    }
}