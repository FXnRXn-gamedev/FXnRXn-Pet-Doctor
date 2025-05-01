using FXnRXn.Upgrades;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Speed Upgrade", menuName = "Content/Upgrades/Movement Speed Upgrade")]
public class MovementSpeedUpgrade : ScriptableObject
{
	[System.Serializable]
	public class MovementSpeedStage : BaseUpgradeStage
	{
		[SerializeField] float playerMovementSpeed;
		public float PlayerMovementSpeed => playerMovementSpeed;

		[SerializeField] float playerAcceleration;
		public float PlayerAcceleration => playerAcceleration;
	}
}
