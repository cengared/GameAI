using UnityEngine;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public int nodeID;
	public List<int> adjacents;
	private Vector3 position;
	private Dictionary<int, Vector3> adjPositions;

	// For each waypoint get the positions of set adjacent waypoints and store them in a dictionary
	void Start () {
		position = transform.position;
		adjPositions = new Dictionary<int, Vector3> ();
		GameObject[] waypoints = GameObject.FindGameObjectsWithTag ("Node");
		foreach (GameObject w in waypoints) {
			int node = w.GetComponent<Waypoint>().nodeID;
			if (adjacents.Contains(node))
				adjPositions.Add (node, w.transform.position);
		}
	}
	
	// Draw a ray line from the current node to all of it's adjacents so the graph can be seen visually
	void Update () {
		foreach (int n in adjacents) 
			Debug.DrawRay(position, (adjPositions[n] - position), Color.green);
	}

	public Waypoint getWaypoint() {
			return this;
	}

}
