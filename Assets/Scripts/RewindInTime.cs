using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindInTime : MonoBehaviour
{
    [SerializeField] private int _maxRecord;
    private bool startRewind = false;
    private List<Vector3> positions;
    private void Start()
    {
        positions = new List<Vector3>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            print(1);
            startRewind = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            startRewind = false;
        }
    }
    private void FixedUpdate()
    {
        if (startRewind) Rewind();
        else Record();
        
    }

    private void Rewind()
    {
        if(positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            startRewind = false;
        }
    }

    private void Record()
    {
        if(positions.Count != _maxRecord)
        {
            positions.Insert(0, transform.position);
        }
        else 
        {
            positions.RemoveAt(positions.Count-1);
        }
        print(positions.Count);
    }
}
