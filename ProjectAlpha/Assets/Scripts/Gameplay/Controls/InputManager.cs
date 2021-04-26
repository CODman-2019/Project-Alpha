using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

namespace DefaultNamespace
{
	public class InputManager : MonoBehaviour
	{
		private PlayerActions playerActions;
		public Vector2 moveValue;
		public GameObject joystick;
		public Camera cam;

		private void Awake() {
			if (playerActions == null) {
				playerActions = new PlayerActions();
				playerActions.Gameplay.Move.performed += context => moveValue = context.ReadValue<Vector2>();
				playerActions.Gameplay.Move.canceled += context => moveValue = Vector2.zero;

				playerActions.Gameplay.TouchPress.started += context => OnTouchStart();
				playerActions.Gameplay.TouchPress.canceled += context => OnTouchEnd();
			}
		}

		private void Start() {
			joystick.SetActive(false);
		}

		private void OnEnable() {
			playerActions.Enable();
		}

		private void OnDisable() {
			playerActions.Disable();
		}

		private void OnTouchEnd() {
			joystick.SetActive(false);
			Debug.Log("Touch Ended");
		}

		private void OnTouchStart() {
			joystick.SetActive(true);
			joystick.transform.position = playerActions.Gameplay.TouchPosition.ReadValue<Vector2>();
			
			Debug.Log($"Touch Start");
		}


		// Start is called before the first frame update


		// Update is called once per frame
		void Update() {
		}
	}
}