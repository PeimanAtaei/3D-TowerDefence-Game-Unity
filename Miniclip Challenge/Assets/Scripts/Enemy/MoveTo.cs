using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    private Enemy enemy;
    public GameObject[] targets;
    private Transform targetEnemy;
    public NavMeshAgent agent;
    public Animator anim;
    private AnimatorClipInfo[] animatorinfo;
    public float speed;
    private float fireCountDown = 0f;
    public float fireRate = 1f;

    [Header("Fire Option")]
	public GameObject bulletPrefabe;
	public Transform firePoint;

    [Header("Setup Option")]
    public AudioSource Sound;


    void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = agent.speed;

        InvokeRepeating("DetectTargets", 0f, 0.5f);

    }

    private void DetectTargets()
    {
        targets = GameObject.FindGameObjectsWithTag("Tower");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in targets)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
                targetEnemy = enemy.transform;
            }
        }

        if(enemy.healthEnemy>0)
            agent.destination = targetEnemy.position;
    }

    private void Update()
    {
        anim.SetFloat("Speed",agent.speed);
        if(targetEnemy!=null)
        {
            float distance = Vector3.Distance(targetEnemy.position, transform.position);
            if (distance < agent.stoppingDistance)
            {
                FaceTarget();
                anim.SetFloat("Speed", 0);
                agent.speed = 0.0f;


                if (fireCountDown <= 0f)
                {
                    Shoot();
                    fireCountDown = 1f / fireRate;
                }
                fireCountDown -= Time.deltaTime;

            }
            else
            {
                anim.SetFloat("Speed", agent.speed);
                agent.speed = speed;
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (targetEnemy.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = lookRotation;
    }

    void Shoot()
    {
        animatorinfo = anim.GetCurrentAnimatorClipInfo(0);
        if(animatorinfo[0].clip.name.StartsWith ("Anim_Shoot"))
        {
            Sound.Play();
            GameObject bulletGO = (GameObject)Instantiate(bulletPrefabe, firePoint.position, firePoint.rotation);
            EnemyBullet bulletSc = bulletGO.GetComponent<EnemyBullet>();

            if (bulletSc != null)
            {
                bulletSc.Seek(targetEnemy);
            }
        }
    }
}
