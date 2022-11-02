using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTower : MonoBehaviour
{
    public string draggingTag;
    public Camera mainCamera;

    public Vector3 dis;
    public float posX;
    public float posZ;

    public bool touched = false;
    public bool dragging = false;
    public Transform toDrag;
    public Rigidbody toDragRigidbody;
    public Vector3 previousPosition;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.touchCount!=1)
        {
            dragging = false;
            touched = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if(touch.phase == TouchPhase.Began)
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(pos);

            if(Physics.Raycast(ray,out hit) && hit.collider.tag == draggingTag)
            {
                toDrag = hit.transform;
                previousPosition = toDrag.position;
                //toDragRigidbody = toDrag.GetComponent<Rigidbody>();

                dis = mainCamera.WorldToScreenPoint(previousPosition);
                posX = Input.GetTouch(0).position.x - dis.x;
                posZ = Input.GetTouch(0).position.y - dis.y;

                //SetDraggingProperties(toDragRigidbody);
                touched = true;
            }
        }

        if (touched && touch.phase == TouchPhase.Moved)
        {
            /*dragging = true;
            float posXNow = Input.GetTouch(0).position.x - posX;
            float posZNow = Input.GetTouch(0).position.y - posZ;

            Vector3 curPos = new Vector3(posXNow,0f,posZNow);
            Vector3 worldPos = mainCamera.ScreenToViewportPoint(curPos)-previousPosition;
            worldPos = new Vector3(worldPos.x,0f,worldPos.z);

            toDragRigidbody.velocity = worldPos / (Time.deltaTime * 100);
            previousPosition = toDrag.position;

            Debug.Log(toDrag.name);*/

            float posXNow = Input.GetTouch(0).position.x - posX;
            float posZNow = Input.GetTouch(0).position.y - posZ;

            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            toDrag.position = touchPosition;

        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
            touched = false;
            previousPosition = new Vector3(0f,0f,0f);
            //SetFreeProperties(toDragRigidbody);
        }
    }

    private void SetDraggingProperties(Rigidbody rb)
    {
        //rb.isKinematic = false;
        //rb.drag = 20;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        //rb.isKinematic = true;
        //rb.drag = 5;
    }
}
