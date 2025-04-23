using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Watermelon;

namespace FXnRXn.PetDoctor
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IControlBehavior
	{
		public static Joystick instance { get; private set; }

		#region Variables
		
		[Header("Joystick :")] 
		[SerializeField] protected Image			backgroundImage;
		[SerializeField] protected Image			handleImage;
		
		[Space]
		[SerializeField] Color						backgroundActiveColor			= Color.white;
		[SerializeField] Color						backgroundDisableColor			= Color.white;

		[SerializeField] Color						handleActiveColor				= Color.white;
		[SerializeField] Color						handleDisableColor				= Color.white;

		[Space]
		[SerializeField] float						handleRange						= 1;
		[SerializeField] float						deadZone						= 0;
		
		
		[Header("Tutorial")]
		[SerializeField] bool						useTutorial;
		[SerializeField] GameObject					pointerGameObject;


		private RectTransform						baseRectTransform;
		private RectTransform						backgroundRectTransform;
		private RectTransform						handleRectTransform;
		private bool								isActive;
		public bool									IsInputActive => isActive;
		private bool								canDrag;
		private Canvas								canvas;
		private Camera								canvasCamera;
		protected Vector2							input							= Vector2.zero;
		public Vector3								FormatInput => new Vector3(input.x, 0, input.y);
		private Vector2								defaultAnchoredPosition;
		private Animator							joystickAnimator;
		private bool								isTutorialDisplayed;
		private bool								hideVisualsActive;

		// Events
		public event SimpleCallback					OnInputActivated;
		

		#endregion

		
		
		//--------------------------------------------------------------------------------------------------------------

		public void Initialise(Canvas canvas)
		{
			this.canvas = canvas;
			instance = this;
			
			
			baseRectTransform = GetComponent<RectTransform>();
			backgroundRectTransform = backgroundImage.rectTransform;
			handleRectTransform = handleImage.rectTransform;
			
			canvasCamera = canvas.worldCamera;
			
			Vector2 center = new Vector2(0.5f, 0.5f);
			backgroundRectTransform.pivot = center;
			handleRectTransform.anchorMin = center;
			handleRectTransform.anchorMax = center;
			handleRectTransform.pivot = center;
			handleRectTransform.anchoredPosition = Vector2.zero;

			isActive = false;
			
			
			defaultAnchoredPosition = backgroundRectTransform.anchoredPosition;
		}


		#region Pointer Control
		
		public void OnPointerDown(PointerEventData eventData)
		{
			canDrag = !WorldSpaceRaycaster.Raycast(eventData);
			if (!canDrag) return;
			if (!isTutorialDisplayed)
			{
				//TODO = Tutorial Display true code
			}

			backgroundRectTransform.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
			
			// TODO = If Tuto seen then active the color
			
			isActive = true;
			OnInputActivated?.Invoke();
			OnDrag(eventData);
		}
		public void OnDrag(PointerEventData eventData)
		{
			if (!isActive || !canDrag) return;

			Vector2 position = RectTransformUtility.WorldToScreenPoint(canvasCamera, backgroundRectTransform.position);
			Vector2 radius = backgroundRectTransform.sizeDelta / 2;
			input = (eventData.position - position) / (radius * canvas.scaleFactor);
			HandleInput(input.magnitude, input.normalized, radius, canvasCamera);
			handleRectTransform.anchoredPosition = input * radius * handleRange;
		}
		public void OnPointerUp(PointerEventData eventData)
		{
			WorldSpaceRaycaster.OnPointerUp(eventData);
			
			if(!isActive) return;
			isActive = false;
			ResetControl();

		}
		
		#endregion
		
		
		
		protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
		{
			Vector2 localPoint = Vector2.zero;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRectTransform, screenPosition, canvasCamera, out localPoint))
			{
				Vector2 pivotOffset = baseRectTransform.pivot * baseRectTransform.sizeDelta;
				return localPoint - (backgroundRectTransform.anchorMax * baseRectTransform.sizeDelta) + pivotOffset;
			}
			return Vector2.zero;
		}
		
		protected void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
		{
			if (magnitude > deadZone)
			{
				if (magnitude > 1)
					input = normalised;
			}
			else
				input = Vector2.zero;
		}
		
		public void ResetControl()
		{
			isActive = false;
			backgroundRectTransform.anchoredPosition = defaultAnchoredPosition;
			input = Vector2.zero;
			handleRectTransform.anchoredPosition = Vector2.zero;
		}
		
		public void EnableControl()
		{
			gameObject.SetActive(true);
		}

		public void DisableControl()
		{
			gameObject.SetActive(false);
			isActive = false;

			ResetControl();
		}
		
		
	}
}


