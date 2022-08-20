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

    void Start()
    {
        TimeWhenStartGame = DateTime.Now;
        player = GameObject.Find("Player");
        navMeshBoss = GetComponent<NavMeshAgent>();
        killPlayer = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }
    void Update()
    {
        if(healValue <= 0 && Time.timeScale != 0 && killPlayer.isDo == false)
        {
            EndOfTheGame();
        }
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            GameOverText.SetActive(false);
            GameOverTime.SetActive(false);
            killPlayer.isDo = true;
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
