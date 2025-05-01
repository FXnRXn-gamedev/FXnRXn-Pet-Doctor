using System;
using UnityEngine;

namespace FXnRXn.PetDoctor
{
	public class PlayerGraphics : MonoBehaviour
	{
		public static PlayerGraphics instance { get; private set; }
		#region Variables

		
		
		
		
		
		
		
		
		
		
		
		private Animator									animator;
		
		

		#endregion

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			instance = this;
			if (animator == null) animator = GetComponent<Animator>();

		}
		
		
		
		
		
		
		
		
		
		
		
		public Animator GetPlayerAnimator() => animator;
		
    
	}
}

