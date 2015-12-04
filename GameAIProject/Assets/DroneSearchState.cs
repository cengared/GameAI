using UnityEngine;
using System.Collections;

public class DroneSearchState : State<DroneAI> {

	public void enter(DroneAI drone) {
		Debug.Log ("Drone entering Search State");

	}
	
	public void execute(DroneAI drone, StateMachine<DroneAI> fsm) {
		drone.findGoal ();
		drone.findPath (drone.goal);
		fsm.changeState (new DroneMoveState ());
	}
	
	public void exit(DroneAI drone) {
		Debug.Log ("Drone exiting Search State");
		
	}
}
