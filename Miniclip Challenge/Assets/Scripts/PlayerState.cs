using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour {

	public static PlayerState playerInstance;

	public int money;
	public int startMoney;
	public int crown = 0;
	public static int health;
	public int startHealth;
	public static int roundsRecord;


	void Awake()
	{
		if (playerInstance != null)
		{
			return;
		}
		playerInstance = this;
	}

	void Start () {
		roundsRecord = 0;
		money = startMoney;
		health = startHealth;
	}



}
