using UnityEngine;
using RTank.CoreData;

namespace RTank.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shell : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] ParticleSystem dustParticles;

        Rigidbody rb;
        MapData mapData;
        AudioSource audioSource;

        public MapData SetMapData { set => mapData = value; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            audioSource = dustParticles.GetComponent<AudioSource>();
        }

        private void Start() => rb.velocity = transform.forward * speed * Time.fixedDeltaTime;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Destructible"))
            {
                UpdateMap(collision);

                dustParticles.transform.parent = null;
                dustParticles.transform.position = collision.transform.position;
                dustParticles.Play();

                if (collision.transform.root.CompareTag("Player"))
                {
                    collision.transform.root.GetComponentInChildren<AudioListener>().transform.parent = null;
                }

                GameObject toDestroy = collision.transform.root != null ? collision.transform.root.gameObject : collision.gameObject;
                audioSource.Play();
                Destroy(toDestroy);
            }

            Destroy(gameObject);
        }

        private void UpdateMap(Collision collision)
        {
            Vector3 collisionPos = collision.transform.position;
            mapData.RemoveFromTile(~mapData.GetTile((int)collisionPos.x, (int)collisionPos.z));
        }
    }
}