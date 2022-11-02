using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
	private UIController menuController;
	public bool isAlive = true;
	public float health;
    public GameObject smokeEffect,explosionEffect;
	public GameObject healthBar;

    private void Start()
    {
		menuController = UIController.uiInstance;

	}
    public void TakeDamage(float amount)
	{
		healthBar.SetActive(true);
		healthBar.GetComponent<Slider>().value = health;

		if (health >0)
			health -= amount;
		else
			DieTower();
	}

	private void DieTower()
	{
		if(isAlive)
        {
			isAlive = false;
			GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation);
			Instantiate(smokeEffect, transform.position, transform.rotation * Quaternion.Euler(-90f, 0f, 0f));
			Destroy(effect, 3f);

			if (gameObject.name != "Base")
				gameObject.tag = "Untagged";
			else
            {
				menuController.ShowFinishPage(-1);
			}
			healthBar.SetActive(false);
		}
		
		//PlayerState.money += worthPrice;
		//biuldManager.SetMoneyText();
		
		//Destroy(effect, 3f);
		//Enemies = FindObjectsOfType<Enemy>();

		/*if (Enemies.Length <= 1 && wavespawn.wavesEnded)
		{
			Debug.Log("Level Complite");
			wavespawn.VictoryPage();
			PlayerPrefs.SetInt("levelNumber", wavespawn.levelNumber + 1);
		}*/
		//Destroy(gameObject);
	}
}
