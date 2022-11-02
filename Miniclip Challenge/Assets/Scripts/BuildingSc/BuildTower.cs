using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTower : MonoBehaviour
{

	private BiuldManager biuldManager;
	public GameObject panel;

	private void Start()
    {
		biuldManager = FindObjectOfType<BiuldManager>();
	}
    public void AcceptTower()
	{
		panel.SetActive(false);
		biuldManager.FinilizeTurrent();
	}

	public void RejectTower()
	{
		biuldManager.CancelTurrent();
	}
}
