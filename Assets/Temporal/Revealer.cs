using System.Collections.Generic;
using UnityEngine;

public class Revealer : MonoBehaviour
{
    [SerializeField] string mistTag;

    Transform player;
    Dictionary<Collider, Animator> mist = new Dictionary<Collider, Animator>();

    private void Awake()
    {
        player = transform.root;
        transform.parent = null;
    }

    private void LateUpdate() { if (player != null) { transform.position = player.position; } }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(mistTag)) { return; }

        if (!mist.ContainsKey(other)) { mist.Add(other, other.GetComponent<Animator>()); }

        mist[other].SetBool("FadeOut", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(mistTag)) { return; }

        mist[other].SetBool("FadeOut", false);
    }
}
