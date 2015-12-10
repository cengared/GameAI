using UnityEngine;
using System.Collections;

// DroneAI move state
public class DroneMoveState : State<DroneAI> {

	public void enter(DroneAI drone) {
		Debug.Log ("Drone entering Move State");
		drone.nearest = drone.findNearestWaypoint (drone.transform.position);
	}
	
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		// only moves the drone while set to not having arrived
		if (!drone.arrived) {
			drone.move ();
		} else if (!drone.reachedTarget (drone.goal)) { // check if the goal has been reached
			drone.findNext ();
		}
		// if the drone has stopped moving and the goal has been reached then the state is change to Go Home State
		if (drone.reachedTarget(drone.goal) && drone.arrived)
			fsm.changeState(new DroneGoHomeState());
	}
	
	public void exit(DroneAI drone) {
		Debug.Log ("Drone exiting Move State");
		drone.count++;

	}
}
