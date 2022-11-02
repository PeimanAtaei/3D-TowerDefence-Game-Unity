using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
	// Start is called before the first frame update
	private BiuldManager buildManager;
    public GameObject[] attackPints;
	public EnemyWaves[] waves;
	//public Transform enemyPrefabe;
	public int enemyCounter = 0;
	public float timeBetwinWaves = 10;
	public float countDown = 20f;
	public int waveIndex = 0;
	private Text waveNumber;
	public Animator anim;
	public static EnemySpawner instance;
	public bool wavesEnded = false;
	public GameObject victoryUi;
	public Text waveCountDownTimer;
	private Slider EnemyWaveSlider;

    private void Start()
    {
		buildManager = GetComponent<BiuldManager>();
		EnemyWaveSlider = GameObject.Find("Canvas/TopPanel/EnemyTroops/HealthSlider").GetComponent<Slider>();
		waveNumber = GameObject.Find("Canvas/TopPanel/EnemyTroops/HealthSlider/Text").GetComponent<Text>();
		attackPints = GameObject.FindGameObjectsWithTag("AttackPoint");
	}

    void Update()
	{
		if (!buildManager.BuildingPhase)
		{
			if (countDown <= 0 && !wavesEnded)
			{
				StartCoroutine("SpawnWaves");
				countDown = timeBetwinWaves;
			}

			countDown -= Time.deltaTime;
			//waveCountDownTimer.text = waveIndex.ToString();
		}

	}

	public void VictoryPage()
	{
		victoryUi.SetActive(true);
	}

	IEnumerator SpawnWaves()
	{
		int wavein = waveIndex + 1;
		waveNumber.text = "Wave "+wavein.ToString();
		EnemyWaveSlider.value = wavein;

		PlayerState.roundsRecord = wavein;
		EnemyWaves wave = waves[waveIndex];

		for (int i = 0; i < wave.TypesOfEnemy; i++)
		{
			for (int j = 0; j < wave.count; j++)
			{
				SpawneEnemy(wave.enemy[i]);
				yield return new WaitForSeconds(0.1f);
			}

		}
		waveIndex++;

		if (waveIndex == waves.Length)
		{
			wavesEnded = true;
		}

	}

	void SpawneEnemy(GameObject enemyPrefabe)
	{
		Transform point = attackPints[Random.Range(0, attackPints.Length - 1)].transform;
		Instantiate(enemyPrefabe, point.position, point.rotation);
		enemyCounter++;

	}

}
