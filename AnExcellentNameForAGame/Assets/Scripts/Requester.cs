using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Requester : MonoBehaviour {

	public float barMax; // The number of seconds finishing using the object will take
	public float tickDownRate; // The a
	private float barVal;
	private float barPercent;
	private bool finished;

	private Player _player;
	private Item _item;

	public itemEnum requiredItem;
	public GameObject itemToSpawn;
	public bool claimTribute;
	public int numToSpawn;
	public Transform spawnTransform;

	void Update () {
		// If they've filled the bar, don't fill it further.
		if (barVal >= barMax) {
			barVal = barMax;
			if (!finished) {
				barVal = barMax;
				SpawnItem (itemToSpawn, numToSpawn);
				finished = true;
			}
		}
		if (barVal < 0) {
			barVal = 0;
		}
		barPercent = barVal / barMax;
		//Debug.Log (barPercent);
		if (!Input.GetButton("Use1") && !Input.GetButton ("Use2")) {
			barVal -= tickDownRate * Time.deltaTime;
		}
	}

	void OnTriggerEnter (Collider obj) {
		// Check if the object entering the trigger zone has the player script
		if (obj.GetComponent<Player>() != null) {
			// If it does, make it the player.
			_player = obj.GetComponent<Player> ();
			// Check if the player has an Item script in their children (they're holding an item)
			if (_player.GetComponentInChildren<Item>() != null) {
				// If they do, make it the item.
				Debug.Log("Item detected");
				_item = _player.GetComponentInChildren<Item> ();
			}
		}
	}

	void OnTriggerStay (Collider obj) {
		// If the thing that is in front of the object is a player...
		if (_player != null) {
			// Then check what player it is and if they're using their use button.
			if ((_player.playerNum == 1 && Input.GetButton ("Use1")) || (_player.playerNum == 2 && Input.GetButton ("Use2"))) {
				// If they need to be holding a specific item...
				if (requiredItem != itemEnum.UnDef) {
					// Then see if they're holding anything and if it's the specific item. If they are, use it.
					if (_player.itemEquipped && _item != null && requiredItem.Equals (_item.itemName)) {
						Use ();
					}
				}
				// Otherwise, if they don't need to be holding a specific item...
				else {
					// See if they're holding nothing. If they are, use it.
					if (_item == null) {
						Use (); 
					}
				}
			}
		}
	}

	void OnTriggerExit () {
		_player = null;
		_item = null;
	}

	void Use () {
		Debug.Log (barPercent);
		barVal += Time.deltaTime;
	}

	void SpawnItem (GameObject obj, int count) {
		Vector3 spawnPos = spawnTransform.position + new Vector3 (Random.Range (-1.5f, 1.5f), 0, 0);
		for (int i = 0; i<count; i++) {
			GameObject newObj = Instantiate (obj, spawnPos, Quaternion.identity) as GameObject;
		}
		if (claimTribute) {
			_player.itemEquipped = false;
			StartCoroutine (_item.Consumed ());
		}
	}
}
