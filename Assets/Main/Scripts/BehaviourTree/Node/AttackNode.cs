using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UGG.Move;

public class AttackNode : Action
{
	public Animator animator;
	public string skillName;
    public float attackRange;
    public SharedFloat DistanceToPlayer;
    public SharedTransform currentTarget;
    public AIMovement movement;
    [SerializeField] private bool isAttack;
	public override void OnStart()
	{
		
	}


    public override TaskStatus OnUpdate()
    {
        if (animator.CheckAnimationTag("Motion") && DistanceToPlayer.Value > attackRange)
        {
            isAttack = false;
            movement.CharacterMoveInterface(GetDirectionForTarget(), 1.4f, true);
            animator.SetFloat("Vertical", 1f, 0.25f, Time.deltaTime);
            animator.SetFloat("Horizontal", 0f, 0.25f, Time.deltaTime);
            return TaskStatus.Running;
        }
        else if(DistanceToPlayer.Value<attackRange && isAttack == false)
        {
            isAttack = true;
            animator.SetFloat("Vertical", 0f);
            animator.SetFloat("Horizontal", 0f, 0.25f, Time.deltaTime);
            animator.Play(skillName, 0, 0f);
            isAttack = false;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    public Vector3 GetDirectionForTarget() => (currentTarget.Value.position - transform.root.position).normalized;
}