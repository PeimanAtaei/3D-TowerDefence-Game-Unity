using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NudeSc : MonoBehaviour {

	public	GameObject	turrent,nude;
	public	Vector3		turrentPositionOfset;

	public	Renderer	rend;
	public	Color		hoverColor,startColor;


	private BiuldManager buildManager;


	void Start () {
		buildManager = BiuldManager.instance;
	}

	private void Update()
	{
		if (buildManager.BuildingPhase && buildManager.isBuildingTower)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hitInfo;
				if (Physics.Raycast(ray, out hitInfo))
				{
					if (hitInfo.collider.gameObject.tag == "Nude")
					{
						nude = hitInfo.collider.gameObject;
						rend = nude.GetComponent<Renderer>();
						NudeSelected();
					}else if(hitInfo.collider.gameObject.tag == "Cliff")
                    {
						return;
					}
				}
			}
		}

	}


	void NudeSelected()
	{

		if (IsPointerOverUIObject())
		{
			return;
		}

		else
			buildManager.BuildTurrentOn();

}
public Vector3 GetBuildPosition()
	{
		return nude.transform.position + turrentPositionOfset;
	}

	private bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
