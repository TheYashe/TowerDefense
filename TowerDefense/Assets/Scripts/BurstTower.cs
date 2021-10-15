using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AFSInterview
{
    public class BurstTower : Tower
    {
        [SerializeField] private GameObject predictionPoint;
        [SerializeField] private bool usePredictedPosition;
        [SerializeField] private int bulletsPerBurst;

        private WaitForSeconds burstInterval = new WaitForSeconds(0.25f);

        private void Update()
        {
            targetEnemy = FindClosestEnemy();
            SetPredictedPoint();
            if (targetEnemy != null)
            {
                var lookRotation = Quaternion.LookRotation(predictionPoint.transform.position - transform.position);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            }

            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                if (targetEnemy != null)
                {
                    StartCoroutine(ShotDelayBullet());
                    fireTimer = firingRate;
                }
            }
        }

        private IEnumerator ShotDelayBullet()
        {
            for (int i = 0; i < bulletsPerBurst; i++)
            {
                yield return burstInterval;

                if (targetEnemy != null)
                {
                    Vector3 enemyPosition = usePredictedPosition ? PredictedEnemyPosition() : targetEnemy.transform.position;
                    Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity).GetComponent<Bullet>();
                    bullet.Initialize(enemyPosition);
                }
            }
        }

        private void SetPredictedPoint()
        {
            if (targetEnemy != null)
            {
                predictionPoint.SetActive(true);
                predictionPoint.transform.position = PredictedEnemyPosition();
            }
            else
            {
                predictionPoint.SetActive(false);
            }
        }

        private Vector3 PredictedEnemyPosition()
        {
            return targetEnemy.transform.position + (targetEnemy.Direction.normalized * targetEnemy.Speed * Time.deltaTime);
        }
    }
}
