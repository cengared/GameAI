using UnityEngine;
using System.Collections;

// the Agent AI behaviours
public class AgentAI : MonoBehaviour {

	// holds the state machine
	StateMachine<AgentAI> fsm; 
	// variables for controlling the AI
	public Vector3 direction { get; set; }
	public bool triggered { get; set; }
	public bool scanning { get; set; }
	public bool collided { get; set; }
	public bool chase { get; set; }
	public bool rotateCW { get; set; }
	public int count { get; set; }
	private float moveSpeed;
	private float rotateSpeed;
	private bool goHome;
	private Vector3 home;
	
	public AgentAI() {
		fsm = new StateMachine<AgentAI> (this);
		fsm.setState (new SleepState ()); // starts the agent in the sleepstate
		scanning = true;
		moveSpeed = 6f;
		rotateSpeed = 30f;
		count = 0;
		goHome = false;
		home = new Vector3 (0f, 0.75f, 0f); // set vector location for 'home'
	}

	void Update () {
		fsm.update ();
		// if a wall is hit the SleepState is called which goes straight to it's execute method
		// with the end result of the agent looking 'stunned' until triggered again
		if (collided)
			fsm.setState (new SleepState ());
		// chase mode is activating by triggering the agent more than 5 times
		// during this mode the agent chases the player until a trigger is made
		if (chase) {
			GameObject g = GameObject.Find ("Player");
			transform.LookAt (g.transform.position);
			transform.Translate ((moveSpeed / 2 ) * Vector3.forward * Time.deltaTime);
		}
		// checkes if the return key is held down and if so interrupts the state machine and sets goHome to true
		if (Input.GetKeyDown(KeyCode.Return)) {
			fsm.interrupt ();
			goHome = true;
		}
		// when the return key is released goHome is set to false and the state machine resumes
		if (Input.GetKeyUp (KeyCode.Return)) {
			goHome = false;
			fsm.resume ();
		}

		// this is the goHom behaviour which translates the current agent position to the pre-set home position
		if (goHome) {
			direction = transform.position - home;
			transform.Translate (direction.normalized * moveSpeed * Time.deltaTime);
		}


	}

	// the scan function sets the agent to rotate around it's centre
	public void scan() {
		if (rotateCW)
			transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
		else
			transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
	}

	// the move function translates the agent in the direction as set by the AwakeState
	public void move() {
		transform.Translate (direction * moveSpeed * Time.deltaTime);
	}



	// checks for trigger collisions in the game world
	void OnTriggerEnter(Collider other) {
		if (other.name == "Player")
			triggered = true;
	}

	// checks for trigger exits
	void OnTriggerExit(Collider other) {
		if (other.name == "Player")
			triggered = false;
	}

	// checks for collisions and when detected it sets the hitWall boolean to true, if it was a wall that was hit
	void OnCollisionEnter(Collision collision) {
		GameObject g = collision.gameObject;
		if (g.tag == "Wall")
			collided = true;
	}

	// resets boolean on collsion exit
	void OnCollisionExit(Collision collision) {
		GameObject g = collision.gameObject;
		if (g.tag == "Wall")
			collided = false;
	}
}
