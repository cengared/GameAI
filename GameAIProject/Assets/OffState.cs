using UnityEngine;
using System.Collections;

public class OffState : State<SwitchAI> {

	public void enter(SwitchAI s) {
	}

	public void execute(SwitchAI s, StateMachine<SwitchAI> fsm) {
		if (s.isOn())
			s.turnOff ();
		else
			fsm.changeState (new OffState());
	}

	public void exit(SwitchAI s) {
	}
}
