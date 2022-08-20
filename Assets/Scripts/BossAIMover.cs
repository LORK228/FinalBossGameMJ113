using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAIMover : MonoBehaviour
{
    NavMeshAgent navMeshBoss;
    public Transform f;
    void Start()
    {
        navMeshBoss = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshBoss.SetDestination(f.position);
    }
}
