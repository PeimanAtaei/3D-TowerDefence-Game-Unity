using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour {

	void Awake()
	{
		GameObject[] musicObjects = GameObject.FindGameObjectsWithTag ("music");

		if(musicObjects.Length > 1)
		{
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
	}
}
