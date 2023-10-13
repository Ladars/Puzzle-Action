using System.Collections;
using System.Collections.Generic;
using UGG.Combat;
using UnityEngine;

public class EnemyCombatBase : CharacterCombatSystemBase
{
    private void Update()
    {
        UpdateAnimationMove();
    }
    private void UpdateAnimationMove()
    {
        if (_animator.CheckAnimationTag("Roll") || _animator.CheckAnimationTag("Attack"))
        {
            _characterMovementBase.CharacterMoveInterface(transform.root.forward, _animator.GetFloat("AnimationMove"), true);
        }
    }
}
