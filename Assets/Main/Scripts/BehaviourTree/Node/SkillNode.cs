using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UGG.Combat;
using UGG.Move;
using System.Collections.Generic;

public class SkillNode : Action
{
	[SerializeField, Header("ººƒ‹¥Ó≈‰")] private List<CombatSkillBase> skills = new List<CombatSkillBase>();
    public Animator _animator;
    public CharacterMovementBase _characterMovementBase;
    [SerializeField] private CombatSkillBase currentSkill;
    public SharedFloat DistanceToPlayer;
    public SharedTransform currentTarget;
    public override void OnStart()
	{
        InitialSkill();
    }
    public override void OnAwake()
    {
        base.OnAwake();
      //  InitialSkill();
    }

    public override TaskStatus OnUpdate()
    {
        GetSkill();
        if (DistanceToPlayer!=null&&currentSkill!=null)
        {
            currentSkill.InvokeSkill(DistanceToPlayer.Value,GetDirectionForTarget());
        }
        
        if (currentSkill != null&&currentSkill.GetSkillIsDone()==true )
        {
            currentSkill = null;
            Debug.Log("Success");
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
	}
    //private TaskStatus AICombatAction()
    //{      
        
    //}
    private void InitialSkill()
    {
        if (skills.Count == 0) return;
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].InitSkill(_animator,  _characterMovementBase);

            if (!skills[i].GetSkillIsDone())
            {
                skills[i].ResetSkill();
            }
        }
    }
    private void GetSkill()
    {
        if (currentSkill == null)
        {
            currentSkill = GetAnDoneSkill();
        }
    }
    public CombatSkillBase GetAnDoneSkill()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].GetSkillIsDone()==false) return skills[i];
            else continue;
        }

        return null;
    }

    public CombatSkillBase GetSkillUseName(string name)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].GetSkillName().Equals(name)) return skills[i];
            else continue;
        }
        return null;
    }

    public CombatSkillBase GetSkillUseID(int id)
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].GetSkillID() == id) return skills[i];
            else continue;
        }
        return null;
    }
    public Vector3 GetDirectionForTarget() => (currentTarget.Value.position - transform.root.position).normalized;
}