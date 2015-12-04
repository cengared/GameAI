using UnityEngine;
using System.Collections;

public class DroneGoHomeState : State<DroneAI>  {

	public void enter(DroneAI drone) {
		Debug.Log ("Drone entering GoHome State");
		drone.nearest = drone.findNearestWaypoint (drone.transform.position);
		drone.findPath (drone.home);
	}
	
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		// only moves the drone while set to not having arrived
		if (!drone.arrived) {
			drone.move ();
		} else if (!drone.reachedTarget (drone.home)) { // check if home has been reached
			drone.findNext ();
		}
		// if the drone has stopped moving and the goal has been reached then the state is change to Go Home State
		if (drone.reachedTarget(drone.home) && drone.arrived)
			fsm.changeState(new DroneSearchState());
	}
	
	public void exit(DroneAI drone) {
		Debug.Log ("Drone exiting GoHome State");
		Debug.Log ("Drone caught agent " + drone.count + " times");
		
	}
}
