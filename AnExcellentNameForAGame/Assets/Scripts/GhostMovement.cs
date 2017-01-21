using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour {

	public Transform target;
	public Transform[] targets;
	Vector3 destination;
	private NavMeshAgent _agent;

	void Awake () {
		_agent = GetComponent<NavMeshAgent> ();
		destination = target.position;
	}

	void Start () {
		_agent.destination = destination;
	}
	
	void Update () {
		if (Vector3.Distance (destination, transform.position) < 6.0f) {
			target = targets [Random.Range (0, targets.Length)];
			destination = target.position;
			_agent.destination = destination;
		}
	}
}
