using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemEnum {UnDef, Cutlass, Blunderbuss, Bottle, Bucket};

public class Item : MonoBehaviour {

	public itemEnum itemName;

	public GameObject lastHit;

	void Start () {
		
	}

	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		//Debug.Log ("hit something");
		lastHit = col.gameObject;
	}

	public void Break() {
		// Instantiate particle effect for glass smashing or whatever
		/// Have a break in case we want to wait for anything
		//yield return null;
		Destroy (gameObject);
	}

	public IEnumerator Consumed() {
		// Whatever fancy effects we want
		yield return null;

		Destroy (gameObject); 
	}
}
