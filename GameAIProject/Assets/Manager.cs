using UnityEngine;
using System.Collections;

// class for handling the start of the maze generation and reset via pressing the space bar
public class Manager : MonoBehaviour {

	public Maze mazePrefab;
	private Maze mazeInstance;

	// Use this for initialization
	void Start () {
		begin ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
			restart ();
	}

	private void begin() {
		// instantiate takes a predefined prefab (attached to the manager object via the inspector) and creates a new instance of it
		mazeInstance = Instantiate (mazePrefab) as Maze;
		// using a coroutine so I can also use WaitForSeconds when generating the maze
		// ref: http://docs.unity3d.com/ScriptReference/Coroutine.html
		StartCoroutine(mazeInstance.generate ());
	}

	// restart stops all coroutines, destroys the current maze instance object and calls begin again
	private void restart() {
		StopAllCoroutines ();
		Destroy (mazeInstance.gameObject);
		begin ();
	}
}
