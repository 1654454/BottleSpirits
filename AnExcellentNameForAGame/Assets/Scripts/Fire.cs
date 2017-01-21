using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

	public float rate;
	public float initialSize;
	public float maxStampSize;
	public float maxSize;

	public GameObject colliderObj;
	private Collider _collider;
	private bool tooBig;

	// Use this for initialization
	void Awake () {
		_collider = GetComponentInChildren<BoxCollider> ();
		colliderObj.transform.localScale = Vector3.zero;
		_collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (colliderObj.transform.localScale.x < maxSize) {
			colliderObj.transform.localScale += Vector3.one * rate * Time.deltaTime;
		}

		if (colliderObj.transform.localScale.x > maxStampSize && !tooBig) {
			BigFire ();
		}
	}

	void OnTriggerEnter (Collider col) {
		Debug.Log ("Trigger");
		if (col.tag == "Player") {
			StartCoroutine (Extinguish ());
		}
	}

	void BigFire() {
		_collider.isTrigger = false;
		tooBig = true;
	}

	IEnumerator Extinguish () {
		Debug.Log ("Fire extinguished");
		yield return null;
		Destroy (gameObject);
	}
}
