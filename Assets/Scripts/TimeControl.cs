using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    private float slowdown= 0.02f;
    private float Duration= 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = slowdown;
            Time.fixedDeltaTime = Time.timeScale * 0.05f;
        }
        if (Time.timeScale < 1f)
        {
            Time.timeScale += (1f / Duration) * Time.deltaTime;
            print(Time.timeScale);
        }
    }
}