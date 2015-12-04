using UnityEngine;
using System.Collections;

// the sleep state for the Agent AI state machine
public class SleepState : State<AgentAI> {

	// sets scanning mode to true when entering state
	public void enter(AgentAI agent) {
		Debug.Log ("Entered SleepState");
		agent.scanning = true;
	}

	// during execution of state scanning of area is performed until something intersects with collider
	// at which point the awake state is started
	public void execute(AgentAI agent, StateMachine<AgentAI> fsm) {
		if (agent.count > 5)
			fsm.changeState (new ChaseState ());
		if (agent.scanning)
			agent.scan();
		if (agent.triggered)
			fsm.changeState (new AwakeState ());
	}

	// when exiting the state scanning mode is stopped by changing the agent's scanning boolean
	public void exit(AgentAI agent) {
		Debug.Log ("Exited SleepState");
		agent.scanning = false;
	}
	
}
