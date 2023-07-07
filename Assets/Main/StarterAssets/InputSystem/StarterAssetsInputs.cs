using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool drawWeapon;
		public bool sheath;
		public bool lightAttack;
		public bool heavyAttack;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		//Input action
		public InputActionAsset inputActions;
		public InputAction m_lightAttack;
		public InputAction m_heavyAttack;

		public bool GetLightAttackDown() => m_lightAttack.WasPressedThisFrame();
		public bool GetHeavyAttackDown() => m_heavyAttack.WasPressedThisFrame();


		private void OnEnable()
		{
			inputActions?.Enable();
		}
		private void OnDisable()
		{
			inputActions?.Disable();
		}
		private void InputInitialize()
		{
			m_lightAttack = inputActions["LightAttack"];
			m_heavyAttack = inputActions["HeavyAttack"];
		}
        private void Awake()
        {
			InputInitialize();

		}

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnDrawWeapon(InputValue value)
        {
			DrawWeaponInput(value.isPressed);
        }
		public void OnSheath(InputValue value)
        {
			SheathInput(value.isPressed);
        }
		public void OnLightAttack(InputValue value)
        {
			lightAttackInput(value.isPressed);
        }
		public void OnHeavyAttack(InputValue value)
        {
			HeavyAttackInput(value.isPressed);
        }
       
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		public void DrawWeaponInput(bool newDrawWeaponState)
		{
			drawWeapon = newDrawWeaponState;
		}
		public void SheathInput(bool newSheathState)
        {
			sheath = newSheathState;
        }

		public void lightAttackInput(bool lightAttackState)
        {
			lightAttack = lightAttackState;
        }
		public void HeavyAttackInput(bool heavyAttackState)
		{
			heavyAttack = heavyAttackState;
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
		
	}
	
}