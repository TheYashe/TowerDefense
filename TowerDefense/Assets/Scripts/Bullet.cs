using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;

        private GameObject targetObject;
        private Vector3 direction;

        public void Initialize(GameObject target)
        {
            targetObject = target;
            direction = (targetObject.transform.position - transform.position).normalized;
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                Destroy(targetObject);
            }
        }
    }
}
