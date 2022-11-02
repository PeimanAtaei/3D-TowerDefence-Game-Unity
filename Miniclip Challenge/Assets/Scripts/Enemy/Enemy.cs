using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
	private BiuldManager biuldManager;
	private PlayerState playerState;
	private UIController uiController;
	private MoveTo moveScript;
	private EnemySpawner wavespawn;


	public float healthEnemy;
	public int worthPrice;
	public GameObject dieEnemyEffect;
	public Enemy[] Enemies;


	void Start()
	{
		biuldManager = BiuldManager.instance;
		uiController = UIController.uiInstance;
		moveScript = GetComponent<MoveTo>();
		playerState = GameObject.FindObjectOfType<PlayerState>();
		wavespawn = GameObject.FindObjectOfType<EnemySpawner>();
	}

	public void TakeDamage(float amount)
	{
		healthEnemy -= amount;
		
		if (healthEnemy <= 0)
		{
			DieEnemy();
		}
	}

	private void DieEnemy()
	{
		playerState.crown +=1;
		wavespawn.enemyCounter--;
		uiController.SetData();

		GameObject effect = (GameObject)Instantiate(dieEnemyEffect, transform.position, Quaternion.identity);
		Destroy(effect, 3f);


		if(wavespawn.enemyCounter==0 && wavespawn.wavesEnded)
		{
			uiController.ShowFinishPage(1);
		}

		moveScript.anim.SetBool("Dead",true);
		moveScript.speed = 0.0f;
		gameObject.tag = "Untagged";

		StartCoroutine("DestroyDeadEnemy");

	}

	IEnumerator DestroyDeadEnemy()
    {
		yield return new WaitForSeconds(3);
		Destroy(gameObject);
    }
}
