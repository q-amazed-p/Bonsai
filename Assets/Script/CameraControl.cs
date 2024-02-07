using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    Camera myCamera;
    [SerializeField] float scrollSpeed;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();  
    }

    private void OnGUI()
    {
        //CAMERA ZOOMING

        if(MathfExt.IsBoundBy(myCamera.orthographicSize - Input.mouseScrollDelta.y, 0, 27))
        {
            myCamera.orthographicSize -= Input.mouseScrollDelta.y;

            if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && MathfExt.Vector3IsPlaneBoundBy(transform.position - 0.03f * Input.mouseScrollDelta.y * (transform.position - myCamera.ScreenToWorldPoint(Input.mousePosition)), -40, 40, -10, 40))
            {
                transform.position -= 0.03f * Input.mouseScrollDelta.y * (transform.position - myCamera.ScreenToWorldPoint(Input.mousePosition));
            }
            
        }

        //CAMERA PANNING

        if(MathfExt.Vector3IsPlaneBoundBy(transform.position + (Input.GetAxis("Vertical") * Vector3.up + Input.GetAxis("Horizontal") * Vector3.right) * myCamera.orthographicSize * Time.deltaTime * scrollSpeed, -40, 40, -10, 40))    //(transform.position.y + Input.GetAxis("Vertical") * myCamera.orthographicSize * Time.deltaTime * scrollSpeed < 40 && transform.position.y + Input.GetAxis("Vertical") * myCamera.orthographicSize * Time.deltaTime * scrollSpeed > -10 && transform.position.x + Input.GetAxis("Horizontal") * myCamera.orthographicSize * Time.deltaTime * scrollSpeed < 40 && transform.position.x + Input.GetAxis("Horizontal") * myCamera.orthographicSize * Time.deltaTime * scrollSpeed > -40)
        {
            transform.position += (Input.GetAxis("Vertical") * Vector3.up + Input.GetAxis("Horizontal") * Vector3.right)* myCamera.orthographicSize * Time.deltaTime * scrollSpeed;
        }
    }

    

}
