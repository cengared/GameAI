using UnityEngine;
using System.Collections.Generic;

// the Drone AI behaviours
public class DroneAI : MonoBehaviour {

	// holds the state machine
	StateMachine<DroneAI> fsm;
	public Waypoint home;
	private IGraph graph;
	private float moveSpeed, startTime;
	private Queue<int> path;
	private GameObject waypointSet;
	public Waypoint nearest { get; set; }
	public Waypoint next { get ; set; }
	public Waypoint goal { get ; set; }
	public bool arrived { get; set; }
	public int count { get; set; }

	public DroneAI() {
		fsm = new StateMachine<DroneAI> (this);
		fsm.setState (new DroneNearestState ());
		moveSpeed = 10f;
		path = new Queue<int> ();
		count = 0; // keep track of how many times the drone catches the agent
	}

	void Start(){
		waypointSet = GameObject.Find ("WaypointSet");
		nearest = findNearestWaypoint (transform.position);
		graph = waypointSet.GetComponent<WaypointSet> ().getGraph ();
	}
	
	// Update is called once per frame
	void Update () {
		fsm.update ();
	}

	public void move() {
		Vector3 a = transform.position;
		Vector3 b = next.transform.position;
		float aToB = Vector3.Distance (a, b);
		if (!arrived) {
			// implementation of linear interpolation
			float soFar = (Time.deltaTime) * moveSpeed;
			float done = soFar / aToB;
			transform.position = Vector3.Lerp (a, b, done);
			if (transform.position == b){ // this checks if the agent has reach it's current target
				arrived = true; // and if so sets the arrived boolean to true
			}
		}
	}

	public bool reachedTarget(Waypoint t){
		return (transform.position == t.transform.position);
	}

	// returns the nearest waypoint to the vector a
	// this means this can find the nearest waypoint to any object
	public Waypoint findNearestWaypoint(Vector3 a) {
		float smallest = 99999999; // an arbitrary large number to compare against in the beginning 
		Waypoint g = waypointSet.GetComponentInChildren<Waypoint> (); // needed for having a variable to store the nearest in and eventually return
		foreach (Transform t in waypointSet.transform) {
			Waypoint w = t.GetComponent<Waypoint> ();
			Vector3 b = w.transform.position;
			float distance = Vector3.Distance(a, b);
			if (distance > 0 && distance < smallest){
				smallest = distance;
				g = w;
			}
		}
		return g;
	}

	public void findGoal() {
		GameObject a = GameObject.Find ("Agent");
		goal = findNearestWaypoint (a.transform.position);
		Debug.Log ("Nearest waypoint to Agent is " + goal.name);
	}

	public void findPath(Waypoint g) {
		AStarSearch aStar = new AStarSearch ();
		aStar.setGraph (graph);
		List<int> p = aStar.findPath (nearest.nodeID, g.nodeID, true);
		if (p != null) {
			string s = "";
			for(int i = 0; i < p.Count; i++) {
				if (i > 0) s += ", ";
				s += p[i];
			}
			Debug.Log ("Path found: " + s);
		} else {
			Debug.Log ("No path found");
		}
		foreach (int j in p)
			path.Enqueue(j);
		int node = path.Dequeue();
		foreach (Transform t in waypointSet.transform) {
			if (t.GetComponent<Waypoint>().nodeID == node)
				next = t.GetComponent<Waypoint>().getWaypoint();
		}
		//Debug.Log ("The next waypoint is " + next.name);
	}

	public void findNext(){
		if (path.Count > 0) {
			int node = path.Dequeue ();
			foreach (Transform t in waypointSet.transform) {
				if (t.GetComponent<Waypoint>().nodeID == node)
					next = t.GetComponent<Waypoint>().getWaypoint();
			}
			Debug.Log ("Next node is " + next.name);
			arrived = false;
		}
	}
}
