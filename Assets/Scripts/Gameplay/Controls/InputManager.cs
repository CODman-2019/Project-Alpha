using System;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


namespace DefaultNamespace
{
	public class InputManager : MonoBehaviour
	{
		[ReadOnlyInspector] public Vector2 moveValue;
		public GameObject joystick;
		public GameObject joystickOutline;
		public Camera cam;
		public float tempValue;
		private bool joystickOutlineNotNull;
		private bool joystickNotNull;


		private void Awake() {
			EnhancedTouchSupport.Enable();
		}

		private void Start() {
			joystickNotNull = joystick != null;
			joystickOutlineNotNull
				= joystickOutline != null;
			TouchReset();
		}

		void Update() {
			if (EnhancedTouch.Touch.activeFingers.Count == 1)
				GetTouchInformation(EnhancedTouch.Touch.activeFingers[0].currentTouch);
		}

		private void GetTouchInformation(EnhancedTouch.Touch currentTouch) {
			switch (currentTouch.phase) {
				case TouchPhase.Began:
					OnTouchStart(currentTouch.startScreenPosition);
					break;
				case TouchPhase.Moved:
					OnDrag(ScreenSpaceConversion(currentTouch.screenPosition),
						ScreenSpaceConversion(currentTouch.startScreenPosition));
					break;
				case TouchPhase.Ended:
					TouchReset();
					break;
			}
		}

		Vector3 ScreenSpaceConversion(Vector2 touchLocation) =>
			cam.ScreenToWorldPoint(new Vector3(touchLocation.x, touchLocation.y, cam.transform.position.z * -1));

		void OnDrag(Vector2 currentPosition, Vector2 startPosition) {
			var m_TouchDelta = currentPosition - startPosition;
			moveValue = Vector2.ClampMagnitude(m_TouchDelta, 1.0f);


			if (joystickNotNull)
				joystick.transform.position = new Vector2(startPosition.x + moveValue.x, startPosition.y + moveValue.y);
			Debug.DrawLine(startPosition, currentPosition, Color.red);
		}

		private void OnTouchStart(Vector2 startPosition) {
			if (joystickNotNull || joystickOutlineNotNull) {
				joystick.transform.position = ScreenSpaceConversion(startPosition);
				joystickOutline.transform.position = ScreenSpaceConversion(startPosition);

				joystick.SetActive(true);
				joystickOutline.SetActive(true);
			}
		}

		private void TouchReset() {
			if (joystickNotNull || joystickOutlineNotNull) {
				joystick.SetActive(false);
				joystickOutline.SetActive(false);
			}

			moveValue = Vector2.zero;
		}
	}
}