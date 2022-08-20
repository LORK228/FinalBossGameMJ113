using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAIMover : MonoBehaviour
{
    [SerializeField] private float _distantWhenRangedAttacks;
    private NavMeshAgent navMeshBoss;
    private GameObject player;
    
    private float distanceToPlayer;
    public float healValue;

    void Start()
    {
        player = GameObject.Find("Player");
        navMeshBoss = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(healValue <= 0)
        {
            EndOfTheGame();
        }

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer >= _distantWhenRangedAttacks)
        {
            distantAttack();
        }
        navMeshBoss.SetDestination(player.transform.position);
    }

    private void EndOfTheGame()
    {
        throw new NotImplementedException();
    }

    private void distantAttack()
    {
        throw new NotImplementedException();
    }
}
