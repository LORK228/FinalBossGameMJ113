using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindAnimation : MonoBehaviour
{
    [HideInInspector] public LinkedList<SecondAndNameAnim> secondAndNames;
    RewindInTime player;
    float timer;
    Animator animator;
    bool firstAnimation = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        timer = 0;
        secondAndNames = new LinkedList<SecondAndNameAnim>();
        player = GameObject.Find("Player").GetComponent<RewindInTime>();
    }

    void Update()
    {
        if (player.isdead)
        {
            if (firstAnimation)
            {
                secondAndNames.First.Value.Seconds = secondAndNames.First.Next.Value.Seconds;
                var next = secondAndNames.First.Next;
                for (int i = 1; i < secondAndNames.Count; i++)
                {
                    if(next.Next == null)
                    {
                        next.Value.Seconds = 0;
                        continue;
                    }
                    next.Value.Seconds = next.Next.Value.Seconds;
                    next = next.Next;
                }
            }
            timer += Time.deltaTime;
            Rewind();
        }
    }

    private void Rewind()
    {
        if (secondAndNames.Count > 0)
        {
            SecondAndNameAnim pointInTime = secondAndNames.First.Value;
            float seconds = pointInTime.Seconds;
            if(timer >= seconds / Time.timeScale)
            {
                animator.speed = animator.speed * (-1);
                animator.Play(pointInTime.NameOfAnimation);
                secondAndNames.RemoveFirst();
                timer = 0;
                if(firstAnimation)
                firstAnimation = false;
            }
        }
    }
}
