using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class RewindInTime : MonoBehaviour
{
    private float _maxRecordSeconds = 16;
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isSecondCamera;
    private bool startRewind = false;
    private List<PointInTime> _pointsInTime;
    Rigidbody rb;
    RigidbodyFirstPersonController controller;
    SecondCameraMove second;

    private void Start()
    {
        if(isSecondCamera) second = GetComponent<SecondCameraMove>();
        if (isPlayer) controller = GetComponent<RigidbodyFirstPersonController>();
        _pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isSecondCamera)
            {
                Destroy(second.firstCamera.gameObject);
                Destroy(second);
                GetComponent<AudioListener>().enabled = true;
                GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f,1);
                GetComponent<AudioSource>().Play();
            }
            startRewind = true;
            if (!isSecondCamera) rb.isKinematic = true;
            if (isPlayer) Destroy(controller);
        }
    }
    private void FixedUpdate()
    {
        if (startRewind) Rewind();
        else Record();
    }

    private void Rewind()
    {
        
        Time.timeScale += 0.002f;
        if(_pointsInTime.Count > 0)
        {
            PointInTime pointInTime = _pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            _pointsInTime.RemoveAt(0);
        }
        else
        {
            startRewind = false;
            if (!isSecondCamera) rb.isKinematic = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Record()
    {
        if(_pointsInTime.Count > Math.Round(_maxRecordSeconds / Time.fixedDeltaTime))
        {
            _pointsInTime.RemoveAt(_pointsInTime.Count - 1);
        }
        _pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }
}
