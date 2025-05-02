using System;
using UnityEngine;

namespace FXnRXn.PetDoctor
{
	public class PlayerGraphics : MonoBehaviour
	{
		public static PlayerGraphics instance { get; private set; }
		#region Variables

		
		
		
		
		
		
		
		
		
		
		private PlayerAnimationHandler						playerAnimationHandler;
		private Animator									animator;
		
		

		#endregion

		private void Awake()
		{
			instance = this;
			
		}

		public void Init(PlayerBehavior _playerBehavior)
		{
			if (animator == null) animator = GetComponent<Animator>();
			if (playerAnimationHandler == null) playerAnimationHandler = GetComponent<PlayerAnimationHandler>();
			playerAnimationHandler.Init(_playerBehavior);
			
		}
		
		
		
		
		
		
		
		
		
		
		
		public Animator GetPlayerAnimator() => animator;
		
    
	}
}

