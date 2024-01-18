using UnityEngine;

namespace YaoLu
{
    public class Rock : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            // hurt if collide with foot
            if (collision.gameObject.CompareTag("Foot"))
            {
                collision.gameObject.GetComponentInParent<FSMCtrl>().Hurt();
                collision.gameObject.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
