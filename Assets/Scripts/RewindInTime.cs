using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class RewindInTime : MonoBehaviour
{
    [SerializeField] private float _maxRecordSeconds;
    [SerializeField] private bool isPlayer;
    private bool startRewind = false;
    private List<PointInTime> _pointsInTime;
    Rigidbody rb;
    RigidbodyFirstPersonController controller;
    private void Start()
    {
        _pointsInTime = new List<PointInTime>();
        rb = GetComponent<Rigidbody>();
        if (isPlayer) controller = GetComponent<RigidbodyFirstPersonController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            startRewind = true;
            rb.isKinematic = true;
            if (isPlayer) controller.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        if (startRewind) Rewind();
        else Record();
    }

    private void Rewind()
    {
        Time.timeScale = 3;
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
            rb.isKinematic = false;
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
