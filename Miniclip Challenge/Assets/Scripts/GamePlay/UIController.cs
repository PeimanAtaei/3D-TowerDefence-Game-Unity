using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public static UIController uiInstance;
	PlayerState playerState;
	BiuldManager buildManager;

	[Header("Prefabes")]
	public TurrentBluePrint turrent;
	public TurrentBluePrint robot;
	public TurrentBluePrint tank;
	public TurrentBluePrint mine;

	[Header("Fields")]
	private bool fastSpeed = false;
	public Button selectedTowerBtn;
	public Animator uiAnim;
	public AudioClip[] musics;
	public AudioSource[] audioSources;

	[Header("UI")]
	public Text moneyText;
	public Text crownText;


	void Awake()
	{
		if (uiInstance != null)
		{
			return;
		}
		uiInstance = this;
	}

	void Start()
	{
		buildManager = BiuldManager.instance;
		playerState = PlayerState.playerInstance;
		uiAnim = GameObject.Find("Canvas").GetComponent<Animator>();
	}


	// Buttons --------------------------------------------------------------------------------------------------------------------------------------------------
	public void SelectTower(Button sender)
    {
		buildManager.isBuildingTower = true;

		if(selectedTowerBtn!=null)
			selectedTowerBtn.GetComponent<Animator>().SetBool("IsSelected", false);

		selectedTowerBtn = sender;
		selectedTowerBtn.GetComponent<Animator>().SetBool("IsSelected", true);
		switch (selectedTowerBtn.name)
        {
			case "TurrentSelectBtn":
                {
					buildManager.SetTurrentToBuild(turrent);
					break;
                }
			case "RobotSelectBtn":
				{
					buildManager.SetTurrentToBuild(robot);
					break;
				}
			case "TankSelectBtn":
				{
					buildManager.SetTurrentToBuild(tank);
					break;
				}
			case "MineSelectBtn":
				{
					buildManager.SetTurrentToBuild(mine);
					break;
				}
		}
    }

	public void StartBattlePhase()
    {
		if (buildManager.isBuildingTower)
			buildManager.ShowAlarm("Finish Building Your Tower");
		else
        {
			buildManager.BuildingPhase = false;
			PlaySounds(0,0);
		}
		
    }

	public void SpeedUpGame()
    {
		fastSpeed = !fastSpeed;
		GameObject speedBtn = GameObject.Find("Canvas/TopPanel/Options/SpeedButton/Text");
		if (fastSpeed)
        {
			Time.timeScale = 2f;
			speedBtn.GetComponent<Text>().text = "2";
		}
        else
        {
			Time.timeScale = 1f;
			speedBtn.GetComponent<Text>().text = "1";
		}	
	}




	// Finish Page -----------------------------------------------------------------------------------------------------------------------------------------------

	public void ShowFinishPage(int status)
    {
		audioSources[0].loop = false;
		Time.timeScale = 1f;
		uiAnim.SetInteger("finishGame",status);
		CalculateRewards();

		if(status == 1)
        {
			PlaySounds(0, 1);
			PlaySounds(1, 2);
		}
        else
        {
			PlaySounds(0, 3);
			PlaySounds(1, 4);
		}
	}

	public void FinishButtons(string sceneName)
    {
		StartCoroutine("StartScene",sceneName);
    }




	//-------------------------------------------------------------------------------------------------------------------------------
	
	IEnumerator StartScene(string sceneName)
    {
		uiAnim.SetBool("FadeIn",true);
		yield return new WaitForSeconds(1);
		SceneManager.LoadScene(sceneName);
	}

	private void CalculateRewards()
    {
		PlayerPrefs.SetInt("Crown",PlayerPrefs.GetInt("Crown")+ playerState.crown);
		PlayerPrefs.SetInt("Coin",PlayerPrefs.GetInt("Coin")+ playerState.money);
    }

	public void PlaySounds(int audio,int clip)
    {
		audioSources[audio].clip = musics[clip];
		audioSources[audio].Play();
	}


	public void SetData()
    {
		playerState = PlayerState.playerInstance;
		moneyText.text = playerState.money + "";
		crownText.text = playerState.crown + "";
	}

	
	
}
