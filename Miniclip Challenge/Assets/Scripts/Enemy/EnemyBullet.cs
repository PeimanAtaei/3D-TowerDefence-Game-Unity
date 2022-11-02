using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	private Transform target;
	public float speed;
	public GameObject BulletImapackEfect;
	public float explosionRadus = 0f;
	private TowerHealth towerHealth;
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
		Debug.Log("Hit Enemy Some Thing");
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

			if (collder.tag == "Tower")
			{
				Damage(collder.transform);
			}

		}
	}


	void Damage(Transform tower)
	{
		towerHealth = tower.GetComponent<TowerHealth>();

		if (towerHealth != null)
		{
			towerHealth.TakeDamage(damageOfBullet);
		}

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadus);
	}
}
