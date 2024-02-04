using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    Camera myCamera;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();  
    }

    private void OnGUI()
    {
        if(Mathf.Abs(myCamera.orthographicSize + Input.mouseScrollDelta.y - 13.5f) < 13.5f)
        {
            myCamera.orthographicSize += Input.mouseScrollDelta.y;
            transform.position += 0.55f* Input.mouseScrollDelta.y * Vector3.up;
        }
    }

    

}
