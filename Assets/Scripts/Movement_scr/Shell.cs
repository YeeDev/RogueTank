using UnityEngine;
using RTank.CoreData;

namespace RTank.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Shell : MonoBehaviour
    {
        [SerializeField] float speed;

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

                GameObject toDestroy = collision.transform.parent != null ? collision.transform.parent.gameObject : collision.gameObject;
                Destroy(toDestroy);
                //TODO particles and other stuff
            }

            Destroy(gameObject);
        }

        private void UpdateMap(Collision collision)
        {
            Vector3 collisionPos = collision.transform.position;
            mapData.UpdateMap(~mapData.GetTile((int)collisionPos.x, (int)collisionPos.z));
        }
    }
}