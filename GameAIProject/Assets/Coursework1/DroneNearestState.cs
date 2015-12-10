using UnityEngine;
using System.Collections;

// DroneAI GoHome state
public class DroneNearestState : State<DroneAI> {

	public void enter(DroneAI drone) {
		Debug.Log ("Drone entering Nearest State");
		// finds the nearest waypoint to the drone's current position
		drone.nearest = drone.findNearestWaypoint (drone.transform.position);
		Debug.Log ("Nearest waypoint to Drone is node " + drone.nearest.name);
	}

	// sets the next waypoint to the nearest waypoint and calls the drone AI move function
	// when it arrives at the waypoint the Search State is started.
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		drone.next = drone.nearest;
		drone.move ();
		if (drone.arrived)
			fsm.changeState (new DroneSearchState ());
	}
	
	public void exit(DroneAI drone) {
		Debug.Log ("Drone exiting Nearest State");
	}
}
