using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class RewindInTime : MonoBehaviour
{
    
    [SerializeField] private bool isPlayer;
    [SerializeField] private bool isSecondCamera;

    private float _maxRecordSeconds = 16;
    private bool startRewind = false;
    private LinkedList<PointInTime> _pointsInTime;
    private Rigidbody rb;
    private RigidbodyFirstPersonController controller;
    private SecondCameraMove second;

    [HideInInspector] public bool isdead = false;

    private void Start()
    {
        if(isSecondCamera) second = GetComponent<SecondCameraMove>();
        if (isPlayer) controller = GetComponent<RigidbodyFirstPersonController>();
        _pointsInTime = new LinkedList<PointInTime>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isdead)
        {
            if (isSecondCamera)
            {
                Destroy(second.firstCamera.gameObject);
                Destroy(second);
                GetComponent<AudioListener>().enabled = true;
                GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(0.9f,1);
                GetComponent<AudioSource>().Play();
            }
            else rb.isKinematic = true;

            if (isPlayer) Destroy(controller);

            startRewind = true;
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
            PointInTime pointInTime = _pointsInTime.First.Value;
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            _pointsInTime.RemoveFirst();
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
            _pointsInTime.RemoveLast();
        }
        _pointsInTime.AddFirst(new PointInTime(transform.position, transform.rotation));
    }
}
