using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple class with very simple gravity.
/// </summary>
public class PlayerMovement : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;

	private GameObject[] players;

	public float snapDistance;
	public float speed;
	public float sprintModifier;
	public float rotationSpeed = 1f;
	private float p1Mult;
	private float p2Mult;
	private Vector3 moveVector;
	CharacterController controller1;
	CharacterController controller2;
	private List<CharacterController> controllers = new List<CharacterController>();


	//Assign our controller
	void Awake() {
//		players [0] = player1;
//		players [1] = player2;
		controllers.Add (player1.GetComponent<CharacterController> ());
		controllers.Add (player2.GetComponent<CharacterController> ());

	}
		
	/// <summary>
	/// Update, we set MoveVector to zero to reset it for this fram otherwise it will grow over each frame
	/// </summary>
	void Update () {
		if (Input.GetButton ("Sprint1")) {
			p1Mult = speed * sprintModifier;
		} else {
			p1Mult = speed;
		}
		if (Input.GetButton ("Sprint2")) {
			p2Mult = speed * sprintModifier;
		} else {
			p2Mult = speed;
		}
			
		foreach(CharacterController controller in controllers) {
			if (!controller.isGrounded)
				controller.Move (Physics.gravity * Time.deltaTime);
		}
		if (controllers[0].isGrounded) {
			SafeMove (new Vector3 (Input.GetAxis ("Horizontal1") * p1Mult, 0, Input.GetAxis ("Vertical1") * p1Mult), controllers[0]); 
		}
		if (controllers[1].isGrounded) {
			SafeMove (new Vector3 (Input.GetAxis ("Horizontal2") * p2Mult, 0, Input.GetAxis ("Vertical2") * p2Mult), controllers[1]); 
		}
	}

	/// Walk down slopes safely. Prevents Player from "hopping" down hills.
	/// Apply gravity before running this. Should only be used if Player
	/// was touching ground on the previous frame.
	void SafeMove(Vector3 velocity, CharacterController controller) {
		// X and Z first. We don't want the sloped ground to prevent
		// Player from falling enough to touch the ground.
		Vector3 displacement;
		displacement.x = velocity.x * Time.deltaTime;
		displacement.y = 0;
		displacement.z = velocity.z * Time.deltaTime;
		controller.Move(displacement);
		// Now Y
		displacement.y = velocity.y * Time.deltaTime;
		// Our steepest down slope is 45 degrees. Force Player to fall at least
		// that much so he stays in contact with the ground.
		if (-Mathf.Abs(displacement.x) < displacement.y && displacement.y < 0) {
			displacement.y = -Mathf.Abs(displacement.x) - 0.001f;
		}
		displacement.z = 0;
		displacement.x = 0;
		controller.Move(displacement);
		if (velocity != Vector3.zero) {
			controller.transform.rotation = Quaternion.Slerp (
				controller.transform.rotation,
				Quaternion.LookRotation (velocity),
				Time.deltaTime * rotationSpeed);
		}
	}
}
