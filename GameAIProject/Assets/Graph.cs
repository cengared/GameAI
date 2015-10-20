using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface Graph<N> 
{
	bool addNode(N a);                 // true if node added
	bool addEdge(N a, N b, int cost);  // true if edge added
	List<N> nodes();
	List<N> neighbours(N a);
	int cost(N a, N b);    // -1 if no edge
}

public class Graph 
{



}

