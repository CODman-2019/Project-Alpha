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
		// private PlayerActions playerActions;
		public Vector2 moveValue;
		public GameObject joystick;
		public GameObject joystickOutLine;
		public Camera cam;
		[SerializeField] private float m_deadZone;
		public float tempValue;

		private void Awake() {
			EnhancedTouchSupport.Enable();
		}

		// if (playerActions == null) {
		// 	playerActions = new PlayerActions();
		// 	playerActions.Gameplay.Move.performed += context => moveValue = context.ReadValue<Vector2>();
		// 	playerActions.Gameplay.Move.canceled += context => moveValue = Vector2.zero;
		//
		// 	playerActions.Gameplay.TouchPress.started += context => OnTouchStart();
		// 	playerActions.Gameplay.TouchPress.canceled += context => OnTouchEnd();
		// }
		// private void Start() {
		// 	joystick.SetActive(false);
		// }


		void Update() {
			if (EnhancedTouch.Touch.activeFingers.Count == 1)
				GetTouchInformation(EnhancedTouch.Touch.activeFingers[0].currentTouch);
		}

//TODO
//Get Joystick and calculate axis
		private void GetTouchInformation(EnhancedTouch.Touch currentTouch) {
			switch (currentTouch.phase) {
				case TouchPhase.Began:
					joystick.transform.position = ScreenSpaceConvertion(currentTouch.screenPosition);
					joystickOutLine.transform.position = ScreenSpaceConvertion(currentTouch.startScreenPosition);

					break;
				case TouchPhase.Moved:
					OnDrag(ScreenSpaceConvertion(currentTouch.screenPosition), ScreenSpaceConvertion(currentTouch.startScreenPosition));
					break;
				case TouchPhase.Ended:
					TouchReset();
					break;
			}
		}

		Vector3 ScreenSpaceConvertion(Vector2 touchLocition) =>
			cam.ScreenToWorldPoint(new Vector3(touchLocition.x, touchLocition.y, cam.transform.position.z * -1));

		void OnDrag(Vector2 currentPosition, Vector2 startPosition) {
			var m_touchDelta = currentPosition - startPosition;
			moveValue = Vector2.ClampMagnitude(m_touchDelta, 1.0f);

			joystick.transform.position = new Vector2(startPosition.x + moveValue.x, startPosition.y + moveValue.y);
			Debug.DrawLine(startPosition, currentPosition, Color.red);


			// if (m_touchDelta.magnitude > m_deadZone) {
			// 	//joystick axis here
			// }
		}

		private void TouchReset() {
			throw new System.NotImplementedException();
		}

		// private void OnTouchEnd() {
		// 	joystick.SetActive(false);
		// 	Debug.Log("Touch Ended");
		// }
		//
		// private void OnTouchStart() {
		// 	joystick.SetActive(true);
		// 	joystick.transform.position = playerActions.Gameplay.TouchPosition.ReadValue<Vector2>();
		// 	
		// 	Debug.Log($"Touch Start");
		// }


		// Start is called before the first frame update


		// Update is called once per frame
	}
}