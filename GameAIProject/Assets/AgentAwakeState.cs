using UnityEngine;
using System.Collections;

// the awake state for the Agent AI state machine
public class AgentAwakeState : State<AgentAI> {

	// upon entering the awake state, a direction to move is worked out
	public void enter(AgentAI agent) {
		Debug.Log ("Enter AwakeState");
		GameObject g = GameObject.Find ("Player");
		agent.direction = agent.transform.position - g.transform.position;
	}

	// this activates the move state
	public void execute(AgentAI agent, StateMachine<AgentAI> fsm) {
		fsm.changeState (new AgentMoveState ());
	}

	// the direction vector is normalised at state exit
	public void exit(AgentAI agent) {
		agent.direction.Normalize ();
		agent.count++; // increases awoken count
		Debug.Log ("Exit AwakeState");
	}
	
}
