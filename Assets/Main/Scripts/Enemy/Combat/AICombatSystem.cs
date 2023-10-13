using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGG.Combat;

public class AICombatSystem : CharacterCombatSystemBase
{
    [SerializeField,Header("范围检测")] private Transform detectionCenter;
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private LayerMask ObstacleMask;
    [SerializeField] Collider[] collidersTarget = new Collider[1];
    [SerializeField,Header("目标")] private Transform currentTarget;

    private int lockOnID=Animator.StringToHash("LockOn");
    [SerializeField, Header("技能搭配")] private List<CombatSkillBase> skills = new List<CombatSkillBase>();
    private void Start()
    {
        InitialSkill();
    }
    private void Update()
    {
        AIView();
        UpdateAnimationMove();
        LockOnTarget();
    }
    private void AIView()
    {
        int targetCount = Physics.OverlapSphereNonAlloc(detectionCenter.position,detectionRange,collidersTarget,EnemyMask);
        if (targetCount>0)
        {
            Vector3 direcction = (collidersTarget[0].transform.position- transform.root.position).normalized;
            
            if (!Physics.Raycast(transform.root.position+transform.root.up*.5f,direcction,out var hit,detectionRange,ObstacleMask))
            {
                if (Vector3.Dot(direcction, transform.root.forward) > 0.25f)
                {
                    currentTarget = collidersTarget[0].transform;
                }
            }
        }
        else
        {
            currentTarget = null;
        }
    }
    private void LockOnTarget()
    {
        if (_animator.CheckAnimationTag("Motion")&&currentTarget !=null)
        {
            _animator.SetFloat(lockOnID, 1f);
            transform.root.rotation = transform.LockOnTarget(currentTarget,transform.root.transform,50);
        }
        else
        {
            _animator.SetFloat(lockOnID, 0);
        }
    }
    private void UpdateAnimationMove()
    {
        if (_animator.CheckAnimationTag("Roll")|| _animator.CheckAnimationTag("Attack"))
        {
            _characterMovementBase.CharacterMoveInterface(transform.root.forward,_animator.GetFloat("AnimationMove"),true);
        }
    }
    public Transform GetCurrentTarget()
    {
        if (currentTarget == null) return null;

        return currentTarget;
    }
    public float GetCurrentTargetDistance()
    {
        if (currentTarget!=null)
        {
            return Vector3.Distance(currentTarget.position, transform.root.position);
        }
        else
        {
            return 9999999;
        }
    }
   
    #region skill
    private void InitialSkill()
    {
        if (skills.Count == 0) return;
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].InitSkill(_animator,this,_characterMovementBase);
            if (!skills[i].GetSkillIsDone())
            {
                skills[i].ResetSkill();
            }
        }
    }
    public CombatSkillBase GetAnDoneSkill()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i].GetSkillIsDone()) return skills[i];
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
    #endregion
    public Vector3 GetDirectionForTarget() => (currentTarget.position - transform.root.position).normalized;
}
