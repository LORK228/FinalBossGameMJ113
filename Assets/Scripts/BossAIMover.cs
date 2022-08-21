using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossAIMover : MonoBehaviour
{
    [SerializeField] private float _distantWhenRangedAttacks;
    private NavMeshAgent navMeshBoss;
    private GameObject player;
    
    private float distanceToPlayer;
    public float healValue;
    public GameObject GameOverText;
    public GameObject GameOverTime;
    private DateTime TimeWhenStartGame;
    private DateTime TimeWhenEndGame;
    private TimeManager killPlayer;
    private Animator AnimAI;
    public int aggresion=0;
    void Start()
    {
        AnimAI = GetComponent<Animator>();
        TimeWhenStartGame = DateTime.Now;
        player = GameObject.Find("Player");
        navMeshBoss = GetComponent<NavMeshAgent>();
        killPlayer = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }
    void Update()
    {
        transform.LookAt(player.transform);
        transform.position = new Vector3(transform.position.x, 1.9275f, transform.position.z);
        if(healValue <= 0 && Time.timeScale != 0 && killPlayer.isDo == false)
        {
            EndOfTheGame();
        }
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        AnimAI.SetFloat("Distance", distanceToPlayer);
    }

    private void EndOfTheGame()
    {
        Time.timeScale = 0;
        TimeWhenEndGame = DateTime.Now;
        GameOverText.SetActive(true);
        GameOverTime.SetActive(true);
        GameOverTime.GetComponent<TextMeshProUGUI>().text = $"You were able to defeat the boss in: \n{(TimeWhenEndGame - TimeWhenStartGame).Minutes} minutes & {(TimeWhenEndGame - TimeWhenStartGame).Seconds} seconds";
    }

    private void distantAttack()
    {
        throw new NotImplementedException();
    }
}
