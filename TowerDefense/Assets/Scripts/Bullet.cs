using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;

        private GameObject targetObject;

        public virtual void Initialize(GameObject target)
        {
            targetObject = target;
        }

        private void Update()
        {
            var direction = (targetObject.transform.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                Destroy(targetObject);
            }
        }
    }
}
