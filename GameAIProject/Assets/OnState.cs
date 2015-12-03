using UnityEngine;
using System.Collections;

public class OnState : State<SwitchAI> {

	public void enter(SwitchAI s) {
	}

	public void execute(SwitchAI s, StateMachine<SwitchAI> fsm) {
		if (!s.isOn ())
			s.turnOn ();
		else
			fsm.changeState (new OffState());

	}

	public void exit(SwitchAI s) {
	}
}
