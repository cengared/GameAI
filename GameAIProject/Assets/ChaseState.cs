using UnityEngine;
using System.Collections;

// the chase state for AgentAI
public class ChaseState : State<AgentAI> {

	// upon entering the chase state, the chase mode is active in AgentAI
	public void enter(AgentAI agent) {
		Debug.Log ("Enter ChaseState");
		agent.chase = true;
	}
	
	// this checks for a trigger from the player on the agent at which point the SleepState is activated
	public void execute(AgentAI agent, StateMachine<AgentAI> fsm) {
		if (agent.triggered)
			fsm.changeState (new SleepState ());
	}
	
	// at state exit the chase mode is turned off and the awoken count reset
	public void exit(AgentAI agent) {
		Debug.Log ("Exit ChaseState");
		agent.count = 0;
		agent.chase = false;
	}
}
