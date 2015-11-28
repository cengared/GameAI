using UnityEngine;
using System.Collections.Generic;

public interface IGraph {
	bool addNode(int a);                 // true if node added
	bool addEdge(int a, int b, float cost);  // true if edge added
	List<int> nodes();
	List<int> neighbours(int a);
	float cost(int a, int b);    // -1 if no edge
}

class AGraph : IGraph {
	
	protected Dictionary<int, Dictionary<int, float>> adj;
	
	public AGraph() {
		adj = new Dictionary<int, Dictionary<int, float>> ();
	}
	
	// Add a new node
	public bool addNode(int a) {
		
		if (a > -1 && !adj.ContainsKey (a)) {
			// Add the node
			adj[a] = new Dictionary<int, float>();
			return true;
		} else {
			// Node already exists or negative index
			return false;
		}
	}
	
	// Add the edge a->b
	public bool addEdge(int a, int b, float cost) {
		if (adj.ContainsKey (a) && adj.ContainsKey (b)) {
			// Both nodes exist
			if (!adj[a].ContainsKey(b)) {
				// Add new edge
				adj[a][b] = cost;
				return true;
				
			} else {
				// Edge already exists
				return false;
			}
		} else {
			// One of the nodes does not exist
			return false;
		}
	}
	
	
	
	// List all the nodes
	public List<int> nodes() {
		return new List<int>(adj.Keys);
	}
	
	// List the neighbour nodes for a given node
	public List<int> neighbours(int a) {
		return new List<int>(adj[a].Keys);
	}
	
	// The cost for edge a->b
	public float cost(int a, int b) {
		return adj [a] [b];
	}
}
