using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    public float timer;
    public Transform player; 
    public float chaseRange = 8;
   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer > 5)
            animator.SetBool("isPatrolling",true);
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if(distance < chaseRange)
            animator.SetBool("isChasing", true);
    }
}
