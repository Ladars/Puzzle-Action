using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NormalAbility",menuName ="Skill/NormalSkill")]
public class NormalSkill : CombatSkillBase
{
    public override void InvokeSkill()
    {
        if (animator.CheckAnimationTag("Motion")&&skillIsDone)
        {
            if (combat.GetCurrentTargetDistance()>skillUseDistance+0.1f)
            {

                movement.CharacterMoveInterface(combat.GetDirectionForTarget(), 1.4f, true);
                animator.SetFloat(verticalID, 1f, 0.25f, Time.deltaTime);
                animator.SetFloat(horizontalID, 0f, 0.25f, Time.deltaTime);             
            }
            else
            {
                UseSkill();
            }
        }
    }

    public override void InvokeSkill(float distance,Vector3 direction)
    {
        throw new System.NotImplementedException();
    }
}
