using System;
using UnityEngine;

namespace FXnRXn.PetDoctor
{
	public class UIGame : MonoBehaviour
	{
		[SerializeField] private Joystick							joystick;

		private void Awake()
		{
			if (joystick != null)
			{
				joystick.Initialise(GetComponent<Canvas>());
			}
			
		}
	}
}


