using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : Item {

	public float maxVelocity;
	public int ghostsStored;

	private Rigidbody _rigidbody;
	private Player _player;
	private Vector3 prevVel;

	void Awake () {
		_rigidbody = GetComponent<Rigidbody> ();
	}
	
	void Update () {
		prevVel = _rigidbody.velocity;
	}

	void OnCollisionEnter (Collision col) {

		// If the bottle touches a ghost
		if (col.gameObject.tag == "Ghost") {
			// Capture the ghost!
		}
		// If the bottle doesn't touch a ghost, but touches a puddle.
		else if (col.gameObject.tag == "Puddle") {
			// Do nothing?
		}
		// If the bottle doesn't touch a ghost/puddle, but touches a player.
		else if (col.gameObject.tag == "Player") {
			// Have the player pick it
			if (lastHit != col.gameObject && !col.gameObject.GetComponent<Player> ().itemEquipped) {
				col.gameObject.GetComponentInChildren<Pickup> ().PickupObj (gameObject);
			}
		}
		// If the bottle doesn't touch a ghost/puddle/player and is travelling as fast as it can before breaking
		else if (prevVel.magnitude > maxVelocity) {
			Break ();
		}

		lastHit = col.gameObject;
	}
}
