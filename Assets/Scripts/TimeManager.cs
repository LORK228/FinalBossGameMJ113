using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public bool isDo;
    private GameObject[] rewindInTimesObjects;
    private List<RewindInTime> rewindInTimes;

    private void Start()
    {
        rewindInTimes = new List<RewindInTime>();
        rewindInTimesObjects = GameObject.FindGameObjectsWithTag("CanReturnInTime");
        foreach (var item in rewindInTimesObjects) rewindInTimes.Add(item.GetComponent<RewindInTime>());
        
    }

    void Update()
    {
       if (isDo)
       foreach (var item in rewindInTimes)
       item.isdead = true;
    }
}
