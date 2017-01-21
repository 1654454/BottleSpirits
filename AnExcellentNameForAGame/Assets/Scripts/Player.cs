using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int playerNum;
	//[HideInInspector]
	public bool itemEquipped;
	[HideInInspector]
	public Vector3 lookDir;
	[HideInInspector]
	public GameObject heldObj;
	private CharacterController _controller;
	private Pickup _pickup;

	// Use this for initialization
	void Awake () {
		_controller = GetComponent<CharacterController> ();
		_pickup = GetComponent<Pickup> ();
	}

	// Update is called once per frame
	void Update () {
		lookDir = transform.forward;
		heldObj = GetComponentInChildren<Pickup> ().heldObj;
	}

	public void Pickup (GameObject obj) {
		_pickup.PickupObj (obj);
	}
}
