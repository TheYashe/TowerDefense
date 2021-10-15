namespace AFSInterview
{
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleTower : Tower
    {
        private void Update()
        {
            targetEnemy = FindClosestEnemy();
            if (targetEnemy != null)
            {
                var lookRotation = Quaternion.LookRotation(targetEnemy.transform.position - transform.position);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                if (targetEnemy != null)
                {
                    Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
                    bullet.Initialize(targetEnemy.transform.position);
                }

                fireTimer = firingRate;
            }
        }
    }
}
