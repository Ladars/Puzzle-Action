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
        //获取玩家攻击系统脚本
        if (combatSystem == null) combatSystem = animator.GetComponent<PlayerCombatSystem>();

        currentAllowMoveTime = maxAllowMoveTime;
        animator.SetBool("IsAllowInterrupt", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(currentAllowAttackTime);
        //如果当前不允许输入攻击信号再计时 当时间达到 允许输入攻击信号
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
