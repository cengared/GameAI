using UnityEngine;
using System.Collections;

public class AgentController : MonoBehaviour {
	public int target;
	protected Vector3 a, b;
	protected float startTime, aToB;
	protected int speed = 2;

	// Use this for initialization
	void Start () {
		a = transform.position;
		GameObject waypointSet = GameObject.Find ("WaypointSet");
		b = waypointSet.GetComponent<WaypointSet> ().getWaypointPosition (target);
		startTime = Time.time;
		aToB = Vector3.Distance (a, b);
	}
	
	// Update is called once per frame
	void Update () {

		float soFar = (Time.time - startTime) * speed;
		float done = soFar / aToB;
		transform.position = Vector3.Lerp (a, b, done);
	}
}
