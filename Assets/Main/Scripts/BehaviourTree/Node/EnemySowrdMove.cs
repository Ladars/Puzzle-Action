using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UGG.Move;

public class EnemySowrdMove : Action
{
    public Animator _animator;
    public SharedTransform currentTarget;
    public AIMovement _movement;
    public SharedFloat DistanceToPlayer;

    protected int verticalID = Animator.StringToHash("Vertical");
    protected int movementID = Animator.StringToHash("Movement");
    protected int runID = Animator.StringToHash("Run");
    protected int horizontalID = Animator.StringToHash("Horizontal");
    private int randomHorizontal;
    public override TaskStatus OnUpdate()
    {
        NoCombatMove();
        return TaskStatus.Running;
    }
    private void NoCombatMove()
    {
        //如果动画处于Motion状态
        if (_animator.CheckAnimationTag("Motion"))
        {
            if (GetCurrentTargetDistance() < 2f + 0.1f)
            {
                //_movement.CharacterMoveInterface(GetDirectionForTarget(), 1.4f, true);
                _movement.CharacterMoveInterface(-_movement.transform.forward, 1.4f, true);
                _animator.SetFloat(verticalID, -1f, 0.25f, Time.deltaTime);
                _animator.SetFloat(horizontalID, 0f, 0.25f, Time.deltaTime);

                randomHorizontal = GetRandomHorizontal();

                if (GetCurrentTargetDistance() < 1.5 + 0.05f)
                {
                    if (!_animator.CheckAnimationTag("Hit") || !_animator.CheckAnimationTag("Defen"))
                    {
                        randomHorizontal = GetRandomHorizontal();
                    }
                }
              
            }
            else if (GetCurrentTargetDistance() > 2f + 0.1f && GetCurrentTargetDistance() < 6.1+0.5f)
            {
                if (HorizontalDirectionHasObject(randomHorizontal))
                {
                    switch (randomHorizontal)
                    {
                        case 1:
                            randomHorizontal = -1;
                            break;
                        case -1:
                            randomHorizontal = 1;
                            break;
                        default:
                            break;
                    }
                }

                _movement.CharacterMoveInterface(_movement.transform.right * ((randomHorizontal == 0) ? 1 : randomHorizontal), 1.4f, true);
                _animator.SetFloat(verticalID, 0f, 0.25f, Time.deltaTime);
                _animator.SetFloat(horizontalID, ((randomHorizontal == 0) ? 1 : randomHorizontal), 0.25f, Time.deltaTime);
            }
            else if (GetCurrentTargetDistance() > 6.1 + 0.5f)
            {
                _movement.CharacterMoveInterface(_movement.transform.forward, 1.4f, true);

                _animator.SetFloat(verticalID, 1f, 0.25f, Time.deltaTime);
                _animator.SetFloat(horizontalID, 0f, 0.25f, Time.deltaTime);
            }
        }
        else
        {
            _animator.SetFloat(verticalID, 0f);
            _animator.SetFloat(horizontalID, 0f);
            _animator.SetFloat(runID, 0f);
        }
    }
    public float GetCurrentTargetDistance()
    {
        if (currentTarget != null)
        {
            //  Debug.Log(Vector3.Distance(currentTarget.Value.position, transform.root.position));
            DistanceToPlayer = Vector3.Distance(currentTarget.Value.position, transform.root.position);
            return DistanceToPlayer.Value;
        }
        else
        {
            DistanceToPlayer = 9999999;
            return DistanceToPlayer.Value;
        }
    }
    public Vector3 GetDirectionForTarget() => (currentTarget.Value.position - transform.root.position).normalized;
    private bool HorizontalDirectionHasObject(int direction)
    {
        //Debug.Log("isHorizontal");
        return Physics.Raycast(_movement.transform.position, _movement.transform.right * direction, 1.5f, 1 << 8);//layer层的第八个
    }
    private int GetRandomHorizontal() => Random.Range(-1, 2);
}
