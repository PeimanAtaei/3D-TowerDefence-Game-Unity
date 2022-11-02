using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoomCamera : MonoBehaviour
{
    private BiuldManager buildManager;
    public Camera mainCamera;
    public float prespectiveZoomSpeed;
    public float orthoZoomSpeed;
    public float movmentSpeed;

    private void Start()
    {
        buildManager = FindObjectOfType<BiuldManager>();
    }
    private void Update()
    {
        if(Input.touchCount >1 && !buildManager.isBuildingTower)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;

            mainCamera.fieldOfView += deltaMagnitudediff * orthoZoomSpeed;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 10f, 30f);

            Vector3 TouchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(-TouchDeltaPosition.x * movmentSpeed, 0, -TouchDeltaPosition.y * movmentSpeed);
            transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, -1000f, 500f),
                    Mathf.Clamp(transform.position.y, 137f, 137f),
                    Mathf.Clamp(transform.position.z, -100f, 700f)
                    );
        }
    }
}
