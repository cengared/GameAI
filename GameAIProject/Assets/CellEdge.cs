using UnityEngine;

// an abstract class to reprensent the edge of a cell, with specific Wall & Passage versions using passed prefabs
public abstract class CellEdge : MonoBehaviour {
	
	public Cell cell, other;
	public Direction direction;

	// defines a edge as the connection between two adjacent cells and the direction of the edge
	public void initialise (Cell cell, Cell other, Direction direction) {
		this.cell = cell;
		this.other = other;
		this.direction = direction;
		cell.setEdge(direction, this);
		transform.parent = cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.toRotation();
	}
}

