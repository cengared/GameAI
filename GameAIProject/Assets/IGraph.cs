using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IGraph {
	bool addNode(int a, int x, int y);                 // true if node added
	bool addEdge(int a, int b, float cost);  // true if edge added
	List<int> listNodes();
	List<int> neighbours(int a);
	float cost(int a, int b);    // -1 if no edge
}