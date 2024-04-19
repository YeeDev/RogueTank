using System.Collections;
using UnityEngine;
using RTank.CoreData;
using RTank.Movement;

namespace RTank.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] Transform muzzle;
        [SerializeField] GameObject shellPrefab;
        [SerializeField] MapData data;

        bool hasShell;

        public bool HasShell => hasShell;

        public IEnumerator Shoot()
        {
            if (!hasShell)
            {
                Debug.Log("No shell, cannot shoot. Lost turn.");
                yield break;
            }

            hasShell = false;
            GameObject shell = Instantiate(shellPrefab, muzzle.position, muzzle.rotation);
            shell.GetComponent<Shell>().SetMapData = data;

            yield return new WaitUntil(() => shell == null);
        }

        public IEnumerator Reload()
        {
            hasShell = true;

            yield return new WaitForSeconds(1); //Change to animation and sound time
        }
    }
}