namespace AFSInterview
{
    using UnityEngine;

    public class NormalBullet : Bullet
    {
        [SerializeField] private float speed;

        private void Update()
        {
            var direction = (targetObject.transform.position - transform.position).normalized;

            transform.position += direction * speed * Time.deltaTime;

            if ((transform.position - targetObject.transform.position).magnitude <= 0.2f)
            {
                Destroy(gameObject);
                Destroy(targetObject);
            }
        }
    }
}