using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(InputManager), typeof(Rigidbody))]
	public class Player : MonoBehaviour
	{
		private InputManager m_inputManager;
		private Rigidbody m_rb;
		[SerializeField] private float m_speed;
		private Vector3 offset;

		private void Awake() {
			m_rb = GetComponent<Rigidbody>();
			m_inputManager = GetComponent<InputManager>();
		}

		private void Start() {
			offset = m_inputManager.cam.transform.position - transform.position;
		}

		private void FixedUpdate() {
			Movement();
		}

		private void LateUpdate() {
			m_inputManager.cam.transform.position = transform.position + offset;
		}

		void Movement() {
			Vector3 movement = new Vector3(m_inputManager.moveValue.x, 0, m_inputManager.moveValue.y).normalized;

			transform.Translate(movement * (m_speed * Time.deltaTime), Space.World);
			var rot = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 720 * Time.deltaTime);
		}
	}
}