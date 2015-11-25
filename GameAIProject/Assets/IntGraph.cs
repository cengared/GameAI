using UnityEngine;
using System.Collections.Generic;

public interface IntGraph {
	bool addNode(int a);                 // true if node added
	bool addEdge(int a, int b, int cost);  // true if edge added
	List<int> nodes();
	List<int> neighbours(int a);
	int cost(int a, int b);    // -1 if no edge
}

class AdjacencyGraph : IntGraph {
	
	protected Dictionary<int, Dictionary<int, int>> adj;
	
	public AdjacencyGraph() {
		adj = new Dictionary<int, Dictionary<int, int>> ();
	}
	
	// Add a new node
	public bool addNode(int a) {
		
		if (a > -1 && !adj.ContainsKey (a)) {
			// Add the node
			adj[a] = new Dictionary<int, int>();
			return true;
		} else {
			// Node already exists or negative index
			return false;
		}
	}
	
	// Add the edge a->b
	public bool addEdge(int a, int b, int cost) {
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
	public int cost(int a, int b) {
		return adj [a] [b];
	}
}