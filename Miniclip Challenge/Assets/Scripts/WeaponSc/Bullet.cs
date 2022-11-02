using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Transform target;
	public float speed;
	public GameObject BulletImapackEfect;
	public float explosionRadus = 0f;
	private Enemy enemyOj;
	public int damageOfBullet = 10;

	public void Seek(Transform _target)
	{
		target = _target;
	}


	void Update()
	{
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.magnitude <= distanceThisFrame)
		{

			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);
		transform.LookAt(target);

	}

	void HitTarget()
	{

		GameObject impackEfect = (GameObject)Instantiate(BulletImapackEfect, transform.position, transform.rotation);
		Destroy(impackEfect, 5f);


		if (explosionRadus > 0f)
		{
			Explode();
		}
		else
		{
			Damage(target);
		}



		Destroy(gameObject);
	}

	void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadus);
		foreach (Collider collder in colliders)
		{

			if (collder.tag == "Enemy")
			{
				Damage(collder.transform);
			}

		}
	}


	void Damage(Transform enemy)
	{
		enemyOj = enemy.GetComponent<Enemy>();

		if (enemyOj != null)
		{
			enemyOj.TakeDamage(damageOfBullet);
		}

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadus);
	}
}
