using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindAnimation : MonoBehaviour
{
    [HideInInspector] public LinkedList<SecondAndNameAnim> secondAndNames;
    [HideInInspector] public RewindInTime player;
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
                secondAndNames.Last.Value.Seconds = secondAndNames.Last.Previous.Value.Seconds;
                var previous = secondAndNames.Last.Previous;
                for (int i = secondAndNames.Count-1; ; i--)
                {
                    if(previous.Previous == null)
                    {
                        previous.Value.Seconds = 0;
                        break;
                    }
                    previous.Value.Seconds = previous.Previous.Value.Seconds;
                    previous = previous.Previous;
                }
            }
            timer += Time.unscaledDeltaTime;
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
                animator.SetFloat("speed", -1);
                animator.Play(pointInTime.NameOfAnimation);
                print(pointInTime.NameOfAnimation+" "+pointInTime.Seconds);
                
                secondAndNames.RemoveFirst();
                timer = 0;
                if(firstAnimation)
                firstAnimation = false;
            }
        }
    }
}
