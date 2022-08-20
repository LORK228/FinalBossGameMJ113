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

    void Start()
    {
        TimeWhenStartGame = DateTime.Now;
        player = GameObject.Find("Player");
        navMeshBoss = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(healValue <= 0 && Time.timeScale != 0)
        {
            EndOfTheGame();
        }
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        GameOverTime.GetComponent<TextMeshProUGUI>().text = $"You were able to defeat the boss in: \n{TimeWhenEndGame.Minute - TimeWhenStartGame.Minute} minute & {TimeWhenEndGame.Second - TimeWhenStartGame.Second} seconds";
    }

    private void distantAttack()
    {
        throw new NotImplementedException();
    }
}
