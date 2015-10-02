using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		speed = 1.0;

	
	}
	
	// Update is called once per frame
	void Update () {
		float moveH, moveV;
		if (Input.GetKey (KeyCode.W)
		    moveV = 1.0;
		if (Input.GetKey (KeyCode.D)
		    moveH = 1.0;
		
		
		Vector3 movement = new Vector3 (moveH, 0.0f, moveV);
		
		rb.AddForce (movement * speed);
	}
}
