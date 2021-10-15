using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class BurstTower : Tower
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private int bulletsPerBurst;

        private WaitForSeconds burstInterval = new WaitForSeconds(0.25f);
        private WaitForSeconds towerDelayToNextBurst = new WaitForSeconds(5f);
        private Enemy targetEnemy;

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
                    var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
                    bullet.Initialize(targetEnemy.gameObject);
                }

                fireTimer = firingRate;
            }
        }
    }
}
