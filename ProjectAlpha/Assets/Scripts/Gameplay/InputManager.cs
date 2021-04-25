using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class InputManager : MonoBehaviour
    {
        private PlayerActions playerActions;
        public Vector2 moveValue;

        private void Awake() {
            if (playerActions == null) {
                playerActions = new PlayerActions();
                playerActions.Gameplay.Move.performed += context => moveValue = context.ReadValue<Vector2>();
                playerActions.Gameplay.Move.canceled += context => moveValue = Vector2.zero;
            }
        }

        private void OnEnable() {
            playerActions.Enable();
        }

        private void OnDisable() {
            playerActions.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
