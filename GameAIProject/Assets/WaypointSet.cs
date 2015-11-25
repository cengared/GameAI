using UnityEngine;
using System;
using System.Collections.Generic;

//public enum Node { A, B, C, D, E, F, G, H}

public class WaypointSet : MonoBehaviour {

	protected List<GameObject> waypointObjects;
	protected Dictionary<int, Vector3> waypointPositions;
	protected IntGraph graph;
	protected int cost = 1; // unit cost

	// Use this for initialization
	void Start () {
		// build waypoint set
		waypointObjects = new List<GameObject>();
		waypointPositions = new Dictionary<int, Vector3>();
		foreach (Transform t in transform) {
			Debug.Log("Found " + t.name);
			waypointObjects.Add (t.gameObject);
		}
		Debug.Log ("Waypoint set built");

		// get position data for each waypoint
		foreach(GameObject w in waypointObjects){
			int node = w.GetComponent<Waypoint>().nodeID;
			Vector3 pos = w.GetComponent<Transform>().position;
			waypointPositions.Add(node, pos);
			Debug.Log ("Waypoint " + node + " position found");
		}
		Debug.Log ("All waypoint positions mapped");

		// graph creation
		graph = new AdjacencyGraph (); 

		// create nodes to represent waypoints
		Array nodes = Enum.GetValues (typeof(Node));
		foreach (Node n in nodes)
			graph.addNode ((int) n);

		// set the edges for each node/waypoint as specified by the adjacents variable in the Waypoint object
		foreach (GameObject waypoint in waypointObjects) {
			List<int> adjs = waypoint.GetComponent<Waypoint>().adjacents;
			foreach(int b in adjs){
				int a = waypoint.GetComponent<Waypoint>().nodeID;
				graph.addEdge(a, b, cost);
			}

		}


	}

	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 getWaypointPosition(int n){
		if (waypointPositions.ContainsKey(n))
		    return waypointPositions[n];
		else
		    return new Vector3();
	}
}
