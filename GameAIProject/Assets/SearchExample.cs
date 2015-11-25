using UnityEngine;
using System;
using System.Collections.Generic;

// Define some node names (easier to remember than plain integers)
public enum Node { A, B, C, D, E, F, G, H, I }

public class SearchExample : MonoBehaviour {

	public int source = (int) Node.A;
	public int target = (int) Node.I;
	protected IntGraph graph;
	
	// Use this for initialization
	void Start () {
		graph = new AdjacencyGraph ();

		buildExample ();

		BFS bfs = new BFS ();
		bfs.setGraph (graph);
		AStarSearch aStar = new AStarSearch ();
		aStar.setGraph (graph);

		//List<int> path = bfs.findPath ((int) Node.A, (int) Node.G, true);
		List<int> path = aStar.findPath (source, target, true);

		if (path != null) {
			String p = "";
			for(int i = 0; i < path.Count; i++) {
				if (i > 0) p += ", ";
				p += path[i];
			}
			Debug.Log ("Path found: " + p);
		} else {
			Debug.Log ("No path found");
		}
	}

	
	// Update is called once per frame
	void Update () {
		
	}

	// Build the example from the lecture slides
	public void buildExample() {

		// Get all possible values for Node
		Array nodes = Enum.GetValues (typeof(Node));

		// Add these nodes
		foreach (Node n in nodes) {
			graph.addNode ((int) n);
			//Debug.Log("Added node " + ((int) n));
		}

		// Add edges
		addEdge (Node.A, Node.B);
		addEdge (Node.A, Node.C);
		addEdge (Node.A, Node.E);

		addEdge (Node.B, Node.F);
		addEdge (Node.C, Node.D);
		addEdge (Node.C, Node.G);

		addEdge (Node.D, Node.F);
		addEdge (Node.D, Node.G);
		addEdge (Node.E, Node.F);
		addEdge (Node.E, Node.H);

		addEdge (Node.F, Node.I);
		addEdge (Node.G, Node.I);
		addEdge (Node.H, Node.I);
	}

	// Add undirected edge with unit cost
	public void addEdge(Node a, Node b) {
		graph.addEdge ((int) a, (int) b, 1);
		graph.addEdge ((int) b, (int) a, 1);
	}

}



