using UnityEngine;

namespace FXnRXn.PetDoctor
{
	public interface IControlBehavior
	{
		public Vector3 FormatInput { get; }
		public bool IsInputActive { get; }

		public void EnableControl();
		public void DisableControl();
		public void ResetControl();

		public event SimpleCallback OnInputActivated;
	}
}

