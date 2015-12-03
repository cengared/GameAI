using UnityEngine;
using System.Collections;

public class SwitchAI : MonoBehaviour {
	
	StateMachine<SwitchAI> fsm;

	public SwitchAI() {
		fsm = new StateMachine<SwitchAI> (this);
		fsm.setState(new OnState());
	}

	void Update () {
		fsm.update ();
	}

	public bool isOn() {
		Light l = transform.GetComponent<Light> ();
		if (l.intensity > 0)
			return true;
		else
			return false;
	}

	public void turnOn() {
		Light l = transform.GetComponent<Light> ();
		l.intensity = 2;
	}

	public void turnOff() {
		Light l = transform.GetComponent<Light> ();
		l.intensity = 0;
	}
	
}
