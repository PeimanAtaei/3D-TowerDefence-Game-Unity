using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
	private TowerHealth towerHealth;

	[Header("Fields")]
	public string towerType;
	public Transform target, partToRotation;
	public float range, turnSpeed = 10f;
	public float fireRate = 1f;
	private float fireCountDown = 0f;

	[Header("Fire Option")]
	public GameObject bulletPrefabe;
	public Transform firePoint1;
	public Transform firePoint2;

	[Header("Setup Option")]
	public string EnemyTag = "Enemy";
	public AudioSource Sound;
	public AudioClip shootSound;


	void Start()
	{
		towerHealth = GetComponent<TowerHealth>();
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}


	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

			if (distanceToEnemy <= shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}

		}

		if (nearestEnemy != null && shortestDistance <= range)
		{
			if (towerType == "tank" && shortestDistance <= 20)
				return;
			else
				target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}
	}

	void Update()
	{
		if (towerHealth.isAlive)
		{
			if (target == null)
			{
				return;
			}
			LuckOnTarget();
			if (fireCountDown <= 0f)
			{
				Shoot(firePoint1);
				if (towerType == "robot")
					Shoot(firePoint2);

				Sound.clip = shootSound;
				Sound.Play();
				fireCountDown = 1f / fireRate;
			}
			fireCountDown -= Time.deltaTime;
		}
	}

	void LuckOnTarget()
	{
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotation.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}


	void Shoot(Transform firePoint)
	{
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefabe, firePoint.position, firePoint.rotation);
		Bullet bulletSc = bulletGO.GetComponent<Bullet>();
		if (bulletSc != null)
		{
			bulletSc.Seek(target);
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}


}
