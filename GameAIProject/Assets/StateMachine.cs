using UnityEngine;
using System.Collections;

// generic state machine class
public class StateMachine<A> {

	A agent;
	State<A> current;

	public StateMachine(A a) {
		agent = a;
	}

	public A getAgent(){
		return agent;
	}

	public State<A> getState() {
		return current;
	}

	public void setState(State<A> s) {
		current = s;
	}

	public void changeState(State<A> next){
		current.exit (agent);
		current = next;
		current.enter (agent);
	}

	public void update(){
		current.execute (agent, this);
	}
}
