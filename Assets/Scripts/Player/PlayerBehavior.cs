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
		private PlayerGraphics									playerGraphics;
		// Movement
		private bool											isRunning;
		private float											speed								= 0;
		private float											maxSpeed							= 3.5f;
		private float											acceleration						= 8f;
		
		// UI Components
		private IControlBehavior								control;
		

		#endregion

		private void Start()
		{
			Initialise();
		}

		public void Initialise()
		{
			playerBehavior = this;
			playerGraphics = PlayerGraphics.instance;
			
			// Link control
			control = Control.CurrentControl;
		}


		private void Update()
		{
			MovementHandler();

		}


		private void MovementHandler()
		{
			if (control != null && control.IsInputActive && control.FormatInput.sqrMagnitude > 0.1f)
			{
				// --- Enable Input
				if (!isRunning)
				{
					isRunning = true;
					playerGraphics.GetPlayerAnimator().SetBool(RUN_HASH, true);
					speed = 0;
				}

				float maxAllowedSpeed = control.FormatInput.magnitude * maxSpeed;
				
				if (speed > maxAllowedSpeed)
				{
					speed -= acceleration * Time.deltaTime;
					if (speed < maxAllowedSpeed)
					{
						speed = maxAllowedSpeed;
					}
				}
				else
				{
					
					speed += acceleration * Time.deltaTime;
					if (speed > maxAllowedSpeed)
					{
						speed = maxAllowedSpeed;
					}
				}
				
				transform.position += control.FormatInput * Time.deltaTime * speed;
				playerGraphics.GetPlayerAnimator().SetFloat(MOVEMENT_MULTIPLIER_HASH, speed / maxSpeed);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(control.FormatInput.normalized), 0.2f); 


			}
			else
			{
				// Disable Input
				if (isRunning)
				{
					isRunning = false;

					playerGraphics.GetPlayerAnimator().SetBool(RUN_HASH, false);
				}
			}
		}
	}
}


