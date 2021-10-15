namespace AFSInterview
{
    using UnityEngine;

    public class GravityBullet : Bullet
    {
        [SerializeField] private float speed;
        [SerializeField] private int bulletsPerBurst;

        private WaitForSeconds burstInterval = new WaitForSeconds(0.25f);
        private WaitForSeconds towerDelayToNextBurst = new WaitForSeconds(5f);

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
