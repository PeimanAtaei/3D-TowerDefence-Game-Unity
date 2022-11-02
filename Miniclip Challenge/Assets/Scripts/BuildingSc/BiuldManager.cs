using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiuldManager : MonoBehaviour {

	public static BiuldManager instance;
	private UIController uiController;
	private PlayerState playerState;
	private NudeSc nudeInstance;

	[Header("Fields")]
	public bool BuildingPhase = true;
	public bool isBuildingTower = false;
	private GameObject CurrentNude;
	private TurrentBluePrint turrentToBild;
	

	[Header("Prefabes")]
	public GameObject towerObject;
	private GameObject unStableMechine;

	void Awake()
	{
		if(instance !=null)
		{
			return;
		}
		instance = this;
	}

	void Start()
	{

		uiController = UIController.uiInstance;
		playerState = PlayerState.playerInstance;
		nudeInstance = GetComponent<NudeSc>();

		uiController.SetData();
	}

    public bool CanBuild{get { return turrentToBild != null; }}
	public bool HasMoney{get { return playerState.money >= turrentToBild.cost; }}

	public void SetTurrentToBuild(TurrentBluePrint turrent)
	{
		turrentToBild = turrent;
	}

	public void BuildTurrentOn()
	{

		if(playerState.money < turrentToBild.cost)
		{
			ShowAlarm("No Enough Gold");
			uiController.selectedTowerBtn.GetComponent<Animator>().SetBool("IsSelected", false);
			isBuildingTower = false;
			return;
		}

		
			
		if(CurrentNude!=null)
        {
			CurrentNude.GetComponent<Renderer>().material.color = nudeInstance.startColor;
		}
		CurrentNude = nudeInstance.nude;


		if (unStableMechine != null)
			Destroy(unStableMechine);
			

		towerObject = (GameObject) Instantiate(turrentToBild.prefabe , nudeInstance.GetBuildPosition(),Quaternion.identity);
		CurrentNude.GetComponent<Renderer>().material.color = nudeInstance.hoverColor;
		unStableMechine = towerObject;
	}

	public void FinilizeTurrent()
    {
		playerState.money -= turrentToBild.cost;
		uiController.SetData();

		unStableMechine = null;
		towerObject = null;
		isBuildingTower = false;

		uiController.selectedTowerBtn.GetComponent<Animator>().SetBool("IsSelected",false);
		CurrentNude.GetComponent<Renderer>().material.color = nudeInstance.startColor;
		uiController.PlaySounds(1,Random.Range(5,7));
	}

	public void CancelTurrent()
	{
		unStableMechine = null;
		isBuildingTower = false;

		Destroy(towerObject);
		uiController.selectedTowerBtn.GetComponent<Animator>().SetBool("IsSelected", false);
		CurrentNude.GetComponent<Renderer>().material.color = nudeInstance.startColor;
	}

	public void ShowAlarm(string newTxt)
	{
		Text alarmText = GameObject.Find("Canvas/Alarm/Text").GetComponent<Text>();
		alarmText.text = newTxt;
		uiController.uiAnim.Play("ShowAlarm");
	}
}
