using System;
using UnityEngine;
using UnityEngine.AI;
using Watermelon;

namespace FXnRXn.PetDoctor
{
	public class PlayerBehavior : MonoBehaviour
	{
		#region Variables

		public static readonly int			RUN_HASH					= Animator.StringToHash("Run");
		public static readonly int			MOVEMENT_MULTIPLIER_HASH	= Animator.StringToHash("Movement Multiplier");
		
		private static PlayerBehavior		playerBehavior;
		public static Vector3 Position { get => playerBehavior.transform.position; set => playerBehavior.transform.position = value; }
		public Transform Transform { get => playerBehavior.transform; }
		
		[SerializeField] private NavMeshAgent					agent;
		
		[Header("Camera :")]
		[SerializeField] private float							cameraOffset;
		[SerializeField] private float							cameraOffsetSpeed;
		[SerializeField] private float							cameraOffsetResetSpeed;
		[SerializeField] private float							cameraOffsetResetDelay;
		
		[Header("Custom Rig")]
		[SerializeField] float									aimSpeed;

		[Header("Particles")]
		[SerializeField] ParticleSystem							moneyPickUpParticleSystem;
		[SerializeField] ParticleSystem							stepParticleSystem;
		
		
		
		private Animator										playerAnimator;
		// Movement
		private bool											isRunning;
		private float											speed								= 0;
		private float											maxSpeed;
		private float											acceleration;
		
		// UI Components
		private IControlBehavior								control;
		

		#endregion

		private void Awake()
		{
			Initialise();
		}

		public void Initialise()
		{
			playerBehavior = this;
			
			// Link control
			control = Control.CurrentControl;
		}


		private void Update()
		{
			MovementHandler();

		}


		private void MovementHandler()
		{
			if (control.IsInputActive && control.FormatInput.sqrMagnitude > 0.1f)
			{
				Debug.Log("Enable Input");
			}
			else
			{
				Debug.Log("Disable Input");
			}
		}
	}
}


