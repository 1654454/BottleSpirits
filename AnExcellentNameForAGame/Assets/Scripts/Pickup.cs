using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	public float throwSpeed;
	public float throwUpSpeed;
	public float throwMult;
	public Transform holdPoint;
	public GameObject cutlass;
	public GameObject blunderbuss;
	public GameObject bottle;
	public GameObject bucket;
	[HideInInspector]
	public GameObject heldObj;
	private Player _player;
	private bool canThrow;

	void Awake () {
		_player = GetComponentInParent<Player> ();
		cutlass.SetActive (false);
		bottle.SetActive (false);
	}

	void Update () {
		if (_player.itemEquipped && canThrow) {
			if ((_player.playerNum == 1 && Input.GetButtonDown ("Pickup1")) ||
				(_player.playerNum == 2 && Input.GetButtonDown ("Pickup2"))) {
				ThrowObj (); 
			}
		}
	}

	void OnTriggerEnter (Collider obj) {
		//Debug.Log ("Object detected");
		if (obj.gameObject.tag == "Pickup") {
			//Debug.Log ("Pickup detected");
		}
	}

	void OnTriggerStay (Collider obj) {
		//Debug.Log ("0");
		if (obj.tag == "Pickup") {
			if ((_player.playerNum == 1 && Input.GetButtonDown ("Pickup1")) ||
				(_player.playerNum == 2 && Input.GetButtonDown ("Pickup2"))) {
				if (!_player.itemEquipped) {
					if (obj.GetComponent<Item>().lastHit != transform.parent.gameObject) {
						StartCoroutine(PickupObj (obj.gameObject)); 
					}
				}
			}
		}
	}

	public IEnumerator PickupObj (GameObject obj) {
		yield return new WaitForEndOfFrame ();
		_player.itemEquipped = true;
		Debug.Log ("Picked up");
		heldObj = obj;
		/*obj.transform.SetParent(transform); 
		obj.transform.position = holdPoint.position;
		obj.transform.localEulerAngles = Vector3.zero;
		obj.GetComponent<Rigidbody>().isKinematic = true;*/
		if (obj.GetComponent<Bottle> () != null) {
			bottle.SetActive (true);
			bottle.GetComponent<Bottle> ().ghostsStored = obj.GetComponent<Bottle> ().ghostsStored;
		} else if (obj.GetComponent<Item> () != null) {
			Item _item = obj.GetComponent<Item> ();
			switch (_item.itemName) {
			case itemEnum.Blunderbuss:
				blunderbuss.SetActive (true);
				break;
			case itemEnum.Bucket:
				bucket.SetActive (true);
				break;
			case itemEnum.Cutlass:
				cutlass.SetActive (true);
				break;
			}
		}
		obj.SetActive (false);
		canThrow = true;
	}

	void ThrowObj () {
		canThrow = false;
		Debug.Log ("Thrown"); 
		/*heldObj = null;
		obj.transform.SetParent (null);
		obj.GetComponent<Rigidbody>().isKinematic = false;*/
		_player.itemEquipped = false;
		heldObj.SetActive (true);
		if (heldObj.GetComponent<Item> () != null) {
			Item _item = heldObj.GetComponent<Item> ();
			switch (_item.itemName) {
			case itemEnum.Blunderbuss:
				blunderbuss.SetActive (false);
				break;
			case itemEnum.Bottle:
				bottle.SetActive (false);
				break;
			case itemEnum.Bucket:
				bucket.SetActive (false);
				break;
			case itemEnum.Cutlass:
				cutlass.SetActive (false);
				break;
			}
		}
		heldObj.transform.position = holdPoint.position;
		heldObj.transform.localEulerAngles = holdPoint.rotation.eulerAngles;
		heldObj.GetComponent<Rigidbody> ().velocity = ((_player.lookDir * throwSpeed + new Vector3 (0, throwUpSpeed, 0)) * throwMult);
	}
}
