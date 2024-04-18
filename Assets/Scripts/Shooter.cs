using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Transform muzzle;
    [SerializeField] GameObject shellPrefab;

    public IEnumerator Shoot()
    {
        GameObject shell = Instantiate(shellPrefab, muzzle.position, muzzle.rotation);

        yield return new WaitUntil(() => shell == null);
    }
}
