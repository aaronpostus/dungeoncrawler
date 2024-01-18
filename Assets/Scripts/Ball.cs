using UnityEngine;

namespace YaoLu
{
    public class Ball : MonoBehaviour
    {
        public Rigidbody body;

        public float forceStrength = 10f;

        private void OnCollisionEnter(Collision collision)
        {
            // get kicked if collide with foot
            if (collision.gameObject.CompareTag("Foot"))
            {

                Vector3 normal = collision.contacts[0].normal;

                body.AddForce(normal * forceStrength, ForceMode.Impulse);
                collision.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
