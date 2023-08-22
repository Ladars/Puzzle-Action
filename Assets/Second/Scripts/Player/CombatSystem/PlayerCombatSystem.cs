using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGG.Health;

namespace UGG.Combat
{
    public enum WeaponType
    {
        Sword = 1,
        Greatsword = 2,
        Arrow = 3
    }
    public class PlayerCombatSystem : CharacterCombatSystemBase
    {
        [SerializeField] private Transform currentTarget;

        //Speed
        [SerializeField, Header("攻击移动速度倍率"), Range(.1f, 10f)]
        private float attackMoveMult;
        
        //检测
        [SerializeField, Header("检测敌人")] private Transform detectionCenter;
        [SerializeField] private float detectionRang;

        //缓存
        private Collider[] detectionedTarget = new Collider[1];
        //允许攻击输入
        [SerializeField] private bool allowAttackInput;
      
        [SerializeField] bool isGS;

        Quaternion LockQuaternion= new Quaternion(0, 0, 0, 0);
  
     
        //other
        [SerializeField] private float rollCoolTime = 2f;
        private bool isRoll;
        float rollCounter;
        private float targetRotation;
        private Action onUpdateInvincibleTime;
        private PlayerHealthSystem healthSystem;

        protected override void Awake()
        {
            base.Awake();
            healthSystem = GetComponentInParent<PlayerHealthSystem>();
            onUpdateInvincibleTime += healthSystem.updateInvincibleTime;
        }
        private void lockRotation()
        {
            transform.rotation = LockQuaternion;
        }
        private void Update()
        {
            PlayerAttackAction();
            PlayerBowAction();
            DetectionTarget();
            ActionMotion();
            UpdateCurrentTarget();
            SwitchWeapon();
            lockRotation();
         
            UpdateRollAnimation();
        }
        private void LateUpdate()
        {
            OnAttackActionAutoLockON();
        }
  

        private void PlayerAttackAction()
        {
            if (!_animator.CheckAnimationTag("Attack"))
            {
                SetAllowAttackInput(true);
            }
            if (!allowAttackInput)
            {
                if (_animator.CheckCurrentTagAnimationTimeIsExceed("Motion", 0.01f) && !_animator.IsInTransition(0))
                {
                    SetAllowAttackInput(true);
                }
            }
           
            
               
           if (_characterInputSystem.playerLAtk && allowAttackInput)
           {
                _animator.SetTrigger(lAtkID);
                 SetAllowAttackInput(false);
           }
           _animator.SetBool(sWeapon, isGS);  
        }
        private void PlayerBowAction()
        {
            if (_characterInputSystem.playerRAtk)
            {
                _animator.SetBool("IsAim",true);
            }
            else
            {
                _animator.SetBool("IsAim", false);
            }
        }
        public void SwitchWeapon()
        {
            
            if (_characterInputSystem.GSWeapon )
            {
                isGS = true;
            }
            if (_characterInputSystem.Sword)
            {
                isGS = false;
            }
        }
        private void OnAttackActionAutoLockON()
        {
         
            if (CanAttackLockOn())
            {
                if (_animator.CheckAnimationTag("Attack") || _animator.CheckAnimationTag("GSAttack"))
                {
                    transform.root.rotation = transform.LockOnTarget(currentTarget, transform.root.transform, 50f);
                    
                }
            }
        }
    
     


        private void ActionMotion()
        {
            if (_animator.CheckAnimationTag("Attack") || _animator.CheckAnimationTag("GSAttack") ||_animator.CheckAnimationTag("Roll")|| _animator.CheckAnimationTag("BowAttack"))
            {
                _characterMovementBase.CharacterMoveInterface(transform.forward,_animator.GetFloat(animationMoveID) * attackMoveMult,true);
            }
        }

        #region 动作检测
        
        /// <summary>
        /// 攻击状态是否允许自动锁定敌人
        /// </summary>
        /// <returns></returns>
        private bool CanAttackLockOn()
        {
            if (_animator.CheckAnimationTag("Attack") || _animator.CheckAnimationTag("GSAttack"))
            {
                if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.75f)
                {
                    return true;
                }
            }
            return false;
        }


        private void DetectionTarget()
        {
            int targetCount = Physics.OverlapSphereNonAlloc(detectionCenter.position, detectionRang, detectionedTarget,enemyLayer);

            if (targetCount >0)
            {
                SetCurrentTarget(detectionedTarget[0].transform);
            }
        }
        private void SetCurrentTarget(Transform target)
        {
            if (currentTarget ==null &&currentTarget !=target)
            {
                currentTarget = target;
            }
        }
        private void UpdateCurrentTarget()
        {
            if (_animator.CheckAnimationTag("Motion"))
            {
                if (_characterInputSystem.playerMovement.sqrMagnitude >0)
                {
                    currentTarget = null;
                }
            }
        }
        /// <summary>
        /// 获取当前是否允许输入攻击信号
        /// </summary>
        /// <returns></returns>
        public bool GetAllowAttackInput() => allowAttackInput;

        /// <summary>
        /// 设置是否允许攻击信号输入
        /// </summary>
        /// <param name="allow"></param>
        public void SetAllowAttackInput(bool allow) => allowAttackInput = allow;
        #endregion

        private void UpdateRollAnimation()
        {

            if (rollCounter > 0)
            {
                rollCounter -= Time.deltaTime;
            }
            else
            {
                isRoll = true;
            }

            if (_characterInputSystem.playerRoll && _characterInputSystem.playerMovement != Vector2.zero && isRoll)
            {
                onUpdateInvincibleTime();
                rollCounter = rollCoolTime;
                isRoll = false;
                var rollDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;

                if (_characterInputSystem.playerMovement.x > 0 && rollDirection.x < 0)
                {
                    targetRotation -= 180f;
                }
                else if (_characterInputSystem.playerMovement.x < 0 && rollDirection.x > 0)
                {
                    targetRotation += 180f;
                }
                if (_characterInputSystem.playerMovement.y > 0 && rollDirection.z < 0)
                {
                    targetRotation += 180f;
                }
                else if (_characterInputSystem.playerMovement.y < 0 && rollDirection.z > 0)
                {
                    targetRotation -= 180f;
                }
              //  transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, 0);
                _animator.Play("Roll_F");


            }
            else if (_characterInputSystem.playerRoll && _characterInputSystem.playerMovement == Vector2.zero && isRoll)
            {
                onUpdateInvincibleTime();
                rollCounter = rollCoolTime;
                isRoll = false;
                _animator.Play("Roll_B");
            }
        }
    }
}

