using UnityEngine;

namespace RTank.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shell : MonoBehaviour
    {
        [SerializeField] float speed;

        Rigidbody rb;

        private void Awake() => rb = GetComponent<Rigidbody>();

        private void Start() => rb.velocity = transform.forward * speed * Time.fixedDeltaTime;
    }
}