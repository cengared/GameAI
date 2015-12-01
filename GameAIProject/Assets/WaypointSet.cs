using UnityEngine;
using System;
using System.Collections.Generic;

public class WaypointSet : MonoBehaviour {

	protected List<GameObject> waypointObjects;
	protected Dictionary<int, Vector3> waypointPositions;
	protected IGraph graph;
	protected float cost = 1; // unit cost

	// Use this for initialization
	void Start () {
		// build waypoint set
		waypointObjects = new List<GameObject>();
		waypointPositions = new Dictionary<int, Vector3>();
		foreach (Transform t in transform) {
			//Debug.Log("Found " + t.name);
			waypointObjects.Add (t.gameObject);
		}
		Debug.Log ("Waypoint set built");
		// graph creation
		graph = new AGraph (); 

		// get position data for each waypoint
		foreach(GameObject w in waypointObjects){
			int node = w.GetComponent<Waypoint>().nodeID;
			Vector3 pos = w.GetComponent<Transform>().position;
			waypointPositions.Add(node, pos);
			Debug.Log ("Waypoint " + node + " position found");
			graph.addNode(node);
		}
		Debug.Log ("All waypoint positions mapped");

		// set the edges for each node/waypoint as specified by the adjacents variable in the Waypoint object
		foreach (GameObject waypoint in waypointObjects) {
			int a = waypoint.GetComponent<Waypoint>().nodeID;
			// uses the set adjacents for each waypoint to get the adjacents for the nodes in the graph
			List<int> adjs = waypoint.GetComponent<Waypoint>().adjacents;
			foreach(int b in adjs){
				// sets the cost to be the distance between waypoints
				cost = Vector3.Distance(waypointPositions[a], waypointPositions[b]);
				//Debug.Log ("Distance between waypoints " + a + " and " + b + " is: " + cost);
				graph.addEdge(a, b, cost); // creates an edge of the graph for waypoints a & b with calculated cost
			}

		}
		Debug.Log ("Waypoint graph created");

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

	public int getCount(){
		return waypointObjects.Count;
	}

	public IGraph getGraph(){
		return graph;
	}
}
