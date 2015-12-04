using UnityEngine;
using System.Collections;

// the move state for the Agent AI state machine
public class AgentMoveState : State<AgentAI> {
	
	public void enter(AgentAI agent) {
		Debug.Log ("Enter MoveState");
		Debug.Log ("Agent awoken count = " + agent.count);
	}

	// during the execution of the move state, the agent checks to see how far away from the player it is
	// and if it's over the threshold then the agent stops moving and enters the SleepState
	public void execute(AgentAI agent, StateMachine<AgentAI> fsm) {
		agent.move ();
		GameObject g = GameObject.Find ("Player");
		float distance = Vector3.Distance (agent.transform.position, g.transform.position);
		if (distance > 30f)
			fsm.changeState (new AgentSleepState ());
	}
	
	public void exit(AgentAI agent) {
		Debug.Log ("Exit MoveState");
		agent.rotateCW = !agent.rotateCW; // reverse rotation of scan mode
	}

}
