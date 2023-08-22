using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGG.Health
{
    public class PlayerHealthSystem : CharacterHealthSystemBase
    {
        [SerializeField, Header("无敌帧")] private float invincibleTime = 1.5f;
        [SerializeField, Header("无敌帧计时器")] private float invincibleTimeCounter;
      
        private void countInvincibleTime()
        {
            if (invincibleTimeCounter > 0)
            {
                invincibleTimeCounter -= Time.deltaTime;
            }
        }
        public void updateInvincibleTime()
        {
            invincibleTimeCounter = invincibleTime;
        }
        public override void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
        {
            if (invincibleTimeCounter<=0)
            {
                SetAttacker(attacker);
                _animator.Play(hitAnimationName, 0, 0f);
                GameAssets.Instance.PlaySoundEffect(_audioSource, SoundAssetsType.hit);
                transform.root.rotation = transform.root.LockOnTarget(attacker, transform, 50);
                GameObjectPoolSystem.Instance.TakeGameObject("Blood", transform.root.position, transform.root.rotation);
            }
           
        }
        protected override void Update()
        {
            base.Update();
            OnHitLockAttacker();
            countInvincibleTime();
        }
     
        private void OnHitLockAttacker()
        {
            if (_animator.CheckAnimationTag("Hit"))
            {
                transform.rotation = transform.LockOnTarget(currentAttacker, transform, 50f);
            }         
        }
    }
}

