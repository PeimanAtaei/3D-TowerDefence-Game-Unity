using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    private Enemy enemySc;
    public GameObject explosionEffect;
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemySc = other.gameObject.GetComponent<Enemy>();
            enemySc.TakeDamage(100);
            audio.Play();
            GameObject effect = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(effect, 3f);
            Destroy(gameObject);

        }
    }
}
