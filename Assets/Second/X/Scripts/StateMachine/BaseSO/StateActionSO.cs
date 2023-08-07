using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGG.Move;

public abstract class StateActionSO : ScriptableObject
{
    [SerializeField] protected int statePriority;//״̬���ȼ�
    protected Animator _animator;
    protected AICombatSystem _combatSystem;
    protected int verticalID= Animator.StringToHash("Vertical");
    protected int movementID = Animator.StringToHash("Movement");
    protected int runID = Animator.StringToHash("Run");
    protected int horizontalID= Animator.StringToHash("Horizontal");
    protected AIMovement _movement;

    public virtual void Init(StateMachineSystem stateSystem)
    {
        _animator = stateSystem.GetComponentInChildren<Animator>();
        _combatSystem = stateSystem.GetComponentInChildren<AICombatSystem>();
        _movement = stateSystem.GetComponent<AIMovement>();
    }

    public virtual void OnEnter(StateMachineSystem stateMachineSystem) { }

    public abstract void OnUpdate();

    public virtual void OnExit() { }

    /// <summary>
    /// ��ȡ״̬���ȼ�
    /// </summary>
    /// <returns></returns>
    public int GetStatePriority() => statePriority;
}
