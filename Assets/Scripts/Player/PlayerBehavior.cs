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

		[Header("Settings :")] 
		[SerializeField] private float							rotationSpeed					= 10f;
		
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
		private float											speed;
		private float											maxSpeed							= 11f;
		private float											acceleration						= 40f;
		
		// UI Components
		private IControlBehavior								control;
		// Steps particle
		private Transform										leftFootTransform;
		private Transform										rightFootTransform;
		

		#endregion

		private void Awake()
		{
			playerBehavior = this;
		}

		private void Start()
		{
			playerGraphics = PlayerGraphics.instance;
			playerGraphics.Init(this);
			Initialise();
		}

		public void Initialise()
		{
			
			
			if (agent == null) agent = GetComponent<NavMeshAgent>();
			
			
			
			playerAnimator = playerGraphics.GetPlayerAnimator();
			
			// Link control
			control = Control.CurrentControl;
			
			
			// Reset particle parent
			stepParticleSystem.transform.SetParent(null);
			
			leftFootTransform = playerAnimator.GetBoneTransform(HumanBodyBones.LeftFoot).GetChild(0);
			rightFootTransform = playerAnimator.GetBoneTransform(HumanBodyBones.RightFoot).GetChild(0);
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

				Vector3 currentLookDirection = control.FormatInput;
				Vector3 direction = new Vector3(currentLookDirection.x, 0f, currentLookDirection.z);
				direction.Normalize();
				Quaternion targetRot = Quaternion.LookRotation(direction);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
				
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






		public void LeftFootParticle()
		{
			if(!isRunning) return;

			stepParticleSystem.transform.position = leftFootTransform.position - transform.forward * 0.4f;
			stepParticleSystem.Play();
		}

		public void RightFootParticle()
		{
			if(!isRunning) return;
			stepParticleSystem.transform.position = rightFootTransform.position - transform.forward * 0.4f;
			stepParticleSystem.Play();
		}
		
		
		
		
		
	}
}


