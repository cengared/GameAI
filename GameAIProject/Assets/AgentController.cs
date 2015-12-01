using UnityEngine;
using System.Collections.Generic;

public class AgentController : MonoBehaviour {
	// needed variables - public one are for easy changing via the inspector
	public int target;
	public int speed;
	public bool useGraph;
	protected Vector3 a, b;
	protected float startTime, aToB;
	protected IGraph graph;
	protected int currentWaypoint;
	protected Queue<int> path;
	protected GameObject waypointSet;
	protected bool arrived;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		a = transform.position;
		waypointSet = GameObject.Find ("WaypointSet"); // gets gameobject data from WaypointSet
		int numWaypoints = waypointSet.GetComponent<WaypointSet> ().getCount (); // gets the number of waypoints
		int closestWaypoint = -1; // for setting what the closewaypoint is as it'll be the start point for the graph search
		path = new Queue<int> ();

		// this makes it so that this conforms to the requirements of lab3-5
		if (useGraph) {
			float smallest = 99999999; // an arbitrary large number to compare against in the beginning 
			for (int i = 0; i < numWaypoints; i++) {
				// this goes through all the waypoints in waypoint set and gets their game world position
				Vector3 t = waypointSet.GetComponent<WaypointSet> ().getWaypointPosition (i);
				float current = Vector3.Distance (a, t); // gets the distance to the current waypoint being checked
				// checks if the current distance is the smallest and if so sets that waypoint to be the closest
				if (current > 0 && current < smallest) {
					smallest = current;
					closestWaypoint = i;
				}
			}
			// the b vector is then set to the position of the closest waypoint
			b = waypointSet.GetComponent<WaypointSet> ().getWaypointPosition (closestWaypoint);
			aToB = Vector3.Distance (a, b); // sets distance from a to b from vectors a and b
			//Debug.Log("Smallest distance is " + smallest);
			//Debug.Log ("The closest waypoint is " + closestWaypoint);
			// A graph search is needed now to find the best path to the target waypoint
			graph = waypointSet.GetComponent<WaypointSet> ().getGraph ();
			//BFS bfs = new BFS ();
			//bfs.setGraph (graph);
			//List<int> p = bfs.findPath (closestWaypoint, target, true);
			AStarSearch aStar = new AStarSearch ();
			aStar.setGraph (graph);
			List<int> p = aStar.findPath (closestWaypoint, target, true);
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
			currentWaypoint = path.Dequeue();
			//Debug.Log ("Current waypoint is: " + currentWaypoint);
			//Debug.Log("Next waypoint is: " + path.Peek());
		} else {
			// performs as in lab3-4 and moves straight to the target waypoint
			b = waypointSet.GetComponent<WaypointSet> ().getWaypointPosition (target);
			aToB = Vector3.Distance (a, b);
		}
		arrived = false; // allows movement
	}
	
	// Update is called once per frame
	void Update () {
		// moves agent only when it hasn't arrived at it's destination
		if (!arrived) {
			// implementation of linear interpolation
			float soFar = (Time.time - startTime) * speed;
			float done = soFar / aToB;
			transform.position = Vector3.Lerp (a, b, done);
			if (transform.position == b){ // this checks if the agent has reach it's current target
				arrived = true; // and if so sets the arrived boolean to true
				if (currentWaypoint == target)
					Debug.Log("Arrived at target");
			}
		} else {// this runs when the agents reaches it's current target
			if (currentWaypoint != target){ // checks if the main target has been reached and if not the next target is set
				if (path.Count != 0){ // checks if the path queue is empty
					int	next = path.Dequeue(); // sets the next waypoint from the front of the queue
					Debug.Log ("Moving to waypoint " + next);
					startTime = Time.time; // sets a new start time for the lerp
					// gets vectors a & b which correspond to the agent (a) and the next waypoint (b)
					a = transform.position;
					b = waypointSet.GetComponent<WaypointSet>().getWaypointPosition(next);
					aToB = Vector3.Distance (a, b); // derives distance between waypoints
					currentWaypoint = next; // updates the current waypoint with the new soon to be current
					arrived = false; // allows movement again
				}
			} 
		}
	}


	
}
