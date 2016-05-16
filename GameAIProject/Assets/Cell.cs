using UnityEngine;
using System.Collections;

// class for holding cell data and related behaviours
public class Cell : MonoBehaviour {

	public MazeVector coordinates;
	private int initialisedEdgeCount;
	private CellEdge[] edges = new CellEdge[Directions.Count]; // creates an edge for each direction

	// gets the edge of the cell in the passed direction
	public CellEdge getEdge (Direction direction) {
		return edges[(int)direction];
	}

	// returns true if all the edges of the cell have been initialised
	public bool isFullyInitialised {
		get {
			return initialisedEdgeCount == Directions.Count;
		}
	}

	// sets the edge with the passed direction and cell edge and increases the initalised edge count
	public void setEdge (Direction direction, CellEdge edge) {
		edges[(int)direction] = edge;
		initialisedEdgeCount += 1;
	}

	// picks a random edge that hasn't already been initialised
	public Direction randomUninitialisedDirection {
		get {
			int skips = Random.Range(0, Directions.Count - initialisedEdgeCount);
			for (int i = 0; i < Directions.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0) {
						return (Direction)i;
					}
					skips -= 1;
				}
			}
			// this exception protects from running out of edges to call
			throw new System.InvalidOperationException("Cell has no uninitialised directions left.");
		}
	}
}
