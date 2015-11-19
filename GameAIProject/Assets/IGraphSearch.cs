using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IGraphSearch {
	void setGraph(Graph g); 
	List<int> findPath(int start, int end, bool trace); 
}
