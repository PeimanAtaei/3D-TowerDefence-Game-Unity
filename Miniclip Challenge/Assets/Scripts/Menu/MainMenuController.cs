using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    private UnitDataController unitDataController;
    private Animator fadeAnim, unitAnim, infoAnim ,alrmAnim;
    private Text coinTxt, crownTxt, levelTxt, energyTxt, sliderTxt;
    private Slider levelSlider;

    private Text unitTitle, unitType, unitLabel;
    private Slider unitRange, unitHealth, unitPower, unitShot;
    public GameObject labelPb, abilityPb;
    private GameObject unitGridLayout, infoGridLayout,towerObject;

    void Start()
    {
        SetupViews();
        SetData();
        SetUnitPanel();
        ButtonClicked(unitDataController.unitInfo[2]);
    }

    private void SetupViews()
    {
        unitDataController = FindObjectOfType<UnitDataController>();
        Debug.Log(unitDataController.unitInfo.Count + " unit count");

        fadeAnim = GameObject.Find("Canvas/FadingPage").GetComponent<Animator>();
        unitAnim = GameObject.Find("Canvas/UnitPanel").GetComponent<Animator>();
        infoAnim = GameObject.Find("Canvas/UnitInfo").GetComponent<Animator>();
        alrmAnim = GameObject.Find("Canvas/Alarm").GetComponent<Animator>();

        coinTxt = GameObject.Find("Canvas/TopPanel/PlayerCoin&Energy/Coin/Value").GetComponent<Text>();
        energyTxt = GameObject.Find("Canvas/TopPanel/PlayerCoin&Energy/Energy/Value").GetComponent<Text>();
        crownTxt = GameObject.Find("Canvas/TopPanel/PlayerCrown/Background/CrwonNumber").GetComponent<Text>();
        levelTxt = GameObject.Find("Canvas/TopPanel/PlayerLevel/LevelIcon/LevelNumber").GetComponent<Text>();
        sliderTxt = GameObject.Find("Canvas/TopPanel/PlayerLevel/LevelIcon/Slider/PassedSteps").GetComponent<Text>();

        levelSlider = GameObject.Find("Canvas/TopPanel/PlayerLevel/LevelIcon/Slider").GetComponent<Slider>();
        unitGridLayout = GameObject.Find("Canvas/UnitPanel/Background/UnitGrid");
        infoGridLayout = GameObject.Find("Canvas/UnitInfo/Background/InfoGrid");



    }

    public void ClickManager(string id)
    {
        switch (id)
        {
            case "battleButton":
                {
                    StartCoroutine("StartScene", "GameScene");
                    break;
                }
            case "unitPanel":
                {
                    unitAnim.SetBool("ShowPanel",true);
                    break;
                }
            case "closeUnitPanel":
                {
                    unitAnim.SetBool("ShowPanel", false);
                    break;
                }
            case "shopButton":
                {
                    ShowAlarm("Shop Section is not implemented");
                    break;
                }

        }
    }

    IEnumerator StartScene(string sceneName)
    {
        fadeAnim.Play("FadeIn");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }

    private void SetData()
    {
        coinTxt.text = PlayerPrefs.GetInt("Coin") + "";
        crownTxt.text = PlayerPrefs.GetInt("Crown") + "";

        levelTxt.text = PlayerPrefs.GetInt("Crown") / 20 + "";
        levelSlider.value = PlayerPrefs.GetInt("Crown") % 20;
        sliderTxt.text = PlayerPrefs.GetInt("Crown") % 20 + "/20";

    }

    private void SetUnitPanel()
    {
        foreach (var item in unitDataController.unitInfo)
        {
            GameObject unit = Instantiate(item.gridButtonPb) as GameObject;

            unit.GetComponent<Button>().image.sprite = item.icon;
            unit.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(item));

            unit.transform.SetParent(unitGridLayout.transform, false);
        }
    }

    void ButtonClicked(UnitDataController.Unit unit)
    {
        
        GameObject.Find("Canvas/Buttons/UnitTitle/Text").GetComponent<Text>().text = unit.labels[0].value;
        towerObject = GameObject.Find("TowerObject");

        if (towerObject != null)
        {
            Destroy(towerObject);
            infoAnim.SetBool("ShowPanel", true);
        }            

        Instantiate(unit.objectPb).name="TowerObject"; 


        foreach (Transform child in infoGridLayout.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in unit.labels)
        {
            GameObject label = Instantiate(labelPb) as GameObject ;
            Debug.Log(label.transform.childCount + " count");
            label.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = item.title;
            label.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = item.value;
            //label.transform.GetChild(3).GetComponent<Text>().text = item.value;
            label.transform.SetParent(infoGridLayout.transform, false);
        }

        foreach (var item in unit.abilities)
        {
            GameObject label = Instantiate(abilityPb) as GameObject;
            label.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = item.title;
            label.transform.GetChild(1).GetComponent<Slider>().value = item.value;
            label.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = item.value+"";
            label.transform.SetParent(infoGridLayout.transform, false);
        }
    }

    public void ShowAlarm(string newTxt)
    {
        Text alarmText = GameObject.Find("Canvas/Alarm/Text").GetComponent<Text>();
        alarmText.text = newTxt;
        alrmAnim.Play("Alarm");
    }
}
