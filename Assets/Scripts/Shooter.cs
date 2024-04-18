using System.Collections;
using UnityEngine;

namespace RTank.Combat
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] Transform muzzle;
        [SerializeField] GameObject shellPrefab;

        bool hasShell;

        public IEnumerator Shoot()
        {
            if (!hasShell)
            {
                Debug.Log("No shell, cannot shoot. Lost turn.");
                yield break;
            }

            hasShell = false;
            GameObject shell = Instantiate(shellPrefab, muzzle.position, muzzle.rotation);

            yield return new WaitUntil(() => shell == null);
        }

        public IEnumerator Reload()
        {
            hasShell = true;

            yield return new WaitForSeconds(1); //Change to animation and sound time
        }
    }
}