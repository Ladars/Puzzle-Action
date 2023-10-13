using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGG.Combat;

public class ActivateMoveInput : StateMachineBehaviour
{
    private PlayerCombatSystem combatSystem;

    [SerializeField] private float maxAllowMoveTime;
    [SerializeField] private float currentAllowMoveTime;
    [SerializeField] private bool IsAllowInterupt;


   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //��ȡ��ҹ���ϵͳ�ű�
        if (combatSystem == null) combatSystem = animator.GetComponent<PlayerCombatSystem>();

        currentAllowMoveTime = maxAllowMoveTime;
        animator.SetBool("IsAllowInterrupt", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(currentAllowAttackTime);
        //�����ǰ���������빥���ź��ټ�ʱ ��ʱ��ﵽ �������빥���ź�
        if (currentAllowMoveTime > 0)
        {
            currentAllowMoveTime -= Time.deltaTime;
            animator.SetBool("IsAllowInterrupt",false);
            if (currentAllowMoveTime <= 0)
            {
                animator.SetBool("IsAllowInterrupt", true);
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("IsAllowInterrupt", false);
    }
}
