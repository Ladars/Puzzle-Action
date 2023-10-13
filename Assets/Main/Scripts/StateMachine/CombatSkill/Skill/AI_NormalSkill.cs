using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI_NormalAbility", menuName = "Skill/AI_NormalSkill")]
public class AI_NormalSkill : CombatSkillBase
{
    
    
    public override void InvokeSkill()
    {
       
        throw new System.NotImplementedException();
    }

    public override void InvokeSkill(float distance,Vector3 direction)
    {
        Debug.Log(distance);
        if (animator.CheckAnimationTag("Motion") && skillIsDone==false)
        {
            if (distance > skillUseDistance + 0.1f)
            {
                
                movement.CharacterMoveInterface(direction, 1.4f, true);
                animator.SetFloat(verticalID, 1f, 0.25f, Time.deltaTime);
                animator.SetFloat(horizontalID, 0f, 0.25f, Time.deltaTime);
            }
            else if (distance<skillUseDistance+0.1f&&skillIsDone==false)
            {
                UseSkill();
            }            
        }
    }
   // public Vector3 GetDirectionForTarget() => (currentTarget.position - transform.root.position).normalized;
}
