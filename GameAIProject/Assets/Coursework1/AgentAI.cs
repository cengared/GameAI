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
	private float rotationMargin;
	
	public AgentAI() {
		fsm = new StateMachine<AgentAI> (this);
		fsm.setState (new AgentSleepState ()); // starts the agent in the sleepstate
		scanning = true;
		moveSpeed = 10f;
		rotateSpeed = 30f;
		count = 0;
		goHome = false;
		home = new Vector3 (0f, 0.75f, 0f); // set vector location for 'home'
		rotationMargin = 1.0f;
	}

	void Update () {
		fsm.update ();
		// if a wall is hit the SleepState is called which goes straight to it's execute method
		// with the end result of the agent looking 'stunned' until triggered again
		if (collided) {
			transform.position = home;
			fsm.setState (new AgentSleepState ());
		}
		// chase mode is activating by triggering the agent more than 5 times
		// during this mode the agent chases the player until a trigger is made
		if (chase) {
			GameObject g = GameObject.Find ("Player");
			Vector3 tDirection = g.transform.position - transform.position;
			float step = rotateSpeed * Time.deltaTime;
			Vector3 newDirection = Vector3.RotateTowards(transform.forward, tDirection, step, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDirection);
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
		Debug.Log ("Trigger entered");
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
		if (g.tag == "Obstacle")
			collided = true;
	}

	// resets boolean on collsion exit
	void OnCollisionExit(Collision collision) {
		GameObject g = collision.gameObject;
		if (g.tag == "Obstacle")
			collided = false;
	}

	// RotateAwayFrom code by Asvarduil at http://forum.unity3d.com/threads/rotate-away-from-game-object-method.144651/
	private void RotateAwayFrom (Vector3 position)
	{
		Vector3 facing = transform.position - position;
		if(facing.magnitude < rotationMargin) { return; }
		
		// Rotate the rotation AWAY from the player...
		Quaternion awayRotation = Quaternion.LookRotation(facing);
		Vector3 euler = awayRotation.eulerAngles;
		euler.y -= 180;
		awayRotation = Quaternion.Euler(euler);
		
		// Rotate the game object.
		transform.rotation = Quaternion.Slerp(transform.rotation, awayRotation, rotateSpeed * Time.deltaTime);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
	}

}
