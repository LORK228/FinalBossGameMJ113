using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rewindanim : StateMachineBehaviour
{
    RewindAnimation rewind;
    float time;
    public string AnimName;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rewind = animator.GetComponent<RewindAnimation>();
        time = 0;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(rewind!=null);
        if(!rewind.player.isdead)
        rewind.secondAndNames.AddFirst(new SecondAndNameAnim(time, AnimName));   
    }
}
