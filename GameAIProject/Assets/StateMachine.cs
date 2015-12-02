using UnityEngine;
using System.Collections;
/*
public class StateMachine<A> {

	A agent;
	State<A> current;

	public StateMachine(A a) {
		agent = a;
	}

	public void getAgent(){
		return agent;
	}

	public State<A> getState() {
		return current;
	}

	public void setState(State<A> s) {
		current = s;
	}

	public void changeState(State<A> next){
		current.exit (this);
		current = next;
		current.enter (this);
	}

	public void update(){
		current.execute (agent, this);
	}
}
*/