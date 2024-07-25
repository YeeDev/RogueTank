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

        public MapData SetMapData { set => mapData = value; }

        private void Awake() => rb = GetComponent<Rigidbody>();

        private void Start() => rb.velocity = transform.forward * speed * Time.fixedDeltaTime;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Destructible"))
            {
                UpdateMap(collision);

                dustParticles.transform.parent = null;
                dustParticles.transform.position = collision.transform.position;
                dustParticles.Play();

                GameObject toDestroy = collision.transform.root != null ? collision.transform.root.gameObject : collision.gameObject;
                Destroy(toDestroy);
                //TODO particles and other stuff
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