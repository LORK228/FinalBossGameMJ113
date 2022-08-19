using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCameraMove : MonoBehaviour
{
    public Transform firstCamera;
    private void Update()
    {
        transform.rotation = firstCamera.rotation;
        transform.position = firstCamera.position;
    }
}
