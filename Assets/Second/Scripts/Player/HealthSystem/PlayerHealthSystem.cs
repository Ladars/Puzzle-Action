using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGG.Health
{
    public class PlayerHealthSystem : CharacterHealthSystemBase
    {
        public override void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
        {
            SetAttacker(attacker);
            _animator.Play(hitAnimationName, 0, 0f);
            GameAssets.Instance.PlaySoundEffect(_audioSource, SoundAssetsType.hit);
            transform.rotation = transform.LockOnTarget(attacker, transform, 50);
        }
    }
}

