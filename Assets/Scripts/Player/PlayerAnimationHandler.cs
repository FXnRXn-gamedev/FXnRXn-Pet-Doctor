
using UnityEngine;

namespace FXnRXn.PetDoctor
{
	public class PlayerAnimationHandler : MonoBehaviour
	{

		private PlayerBehavior									playerBehavior;


		public void Init(PlayerBehavior _playerBehavior)
		{
			playerBehavior = _playerBehavior;
		}
		
		public void LeftStepCallback()
		{
			playerBehavior.LeftFootParticle();
		}

		public void RightStepCallback()
		{
			playerBehavior.RightFootParticle();
		}

	}
}

