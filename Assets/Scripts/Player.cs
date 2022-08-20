using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float healValue;
    private TimeManager killPlayer;
    void Start()
    {
        killPlayer = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }
    private void Update()
    {
        if (healValue <= 0)
        {
            KillPlayer();
        }
    }
    public void KillPlayer()
    {
        killPlayer.isDo = true;
    }
}
