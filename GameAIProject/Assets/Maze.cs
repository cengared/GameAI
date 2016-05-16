using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// the class for handling the maze generation
public class Maze : MonoBehaviour {
	
	public MazeVector size;
	public Cell cellPrefab;
	private Cell[,] cells;
	public float generationStepDelay;
	public Passage passagePrefab;
	public Wall wallPrefab;

	// returns the cell at the specified coordinates
	public Cell getCell (MazeVector coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	// this is part of the coroutine started by Manager so it requires the IEnumerator type and uses
	// WaitForSeconds to slow down the maze generation so it can be shown being built up over time.
	// ref: http://docs.unity3d.com/ScriptReference/WaitForSeconds.html
	public IEnumerator generate() {
		WaitForSeconds delay = new WaitForSeconds (generationStepDelay);
		cells = new Cell[size.x, size.z];
		List<Cell> activeCells = new List<Cell> (); // a list of cells that have been activated
		firstStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			nextStep(activeCells);
		}
	}

	// this is the first step in the maze generation and creates a cell at a random location
	private void firstStep(List<Cell> active){
		active.Add (createCell (randomCoordinates));
	}

	// this is the next step of maze generation 
	private void nextStep(List<Cell> active){
		int currentIndex = active.Count - 1; // index is set to one less than the current active count
		Cell currentCell = active[currentIndex]; // the cell to work on is the one at the currentIndex in the active list
		// checks if the current cell is fully initialised and if so removes it from the active list and ends the function
		if (currentCell.isFullyInitialised) {
			active.RemoveAt(currentIndex);
			return;
		} // otherwise
		// a random unused direction is chosen
		Direction direction = currentCell.randomUninitialisedDirection;
		// the coordinates of the cell in the selected direction are found
		MazeVector coordinates = currentCell.coordinates + direction.toMazeVector2();
		// if those coordinates are within the range of the maze world 
		if (containsCoordinates(coordinates)) { 
			Cell neighbour = getCell(coordinates); // the cell at those coordinates is set as the neighbour cell
			// a neighbour is null when it hasn't been created for anything yet and so in that case is it 
			// initialised and a passage is formed between the two cells
			if (neighbour == null) { 
				neighbour = createCell(coordinates);
				createPassage(currentCell, neighbour, direction);
				active.Add(neighbour);
			}
			// otherwise a wall is created between the two cells
			else {
				createWall(currentCell, neighbour, direction);
			}
		}
		// if the coordinates are outside of the maze world space then a wall is made on that edge of the cell
		else {
			createWall(currentCell, null, direction);
		}
	}

	// this handles cell creation using the passed coordinates
	private Cell createCell(MazeVector coords) {
		// uses the attached prefab to make a cell object and uses the passed coordinates to set it's location in the world
		Cell newCell = Instantiate (cellPrefab) as Cell; 
		cells [coords.x, coords.z] = newCell;
		newCell.coordinates = coords;
		newCell.name = "Maze cell " + coords.x + ", " + coords.z;
		newCell.transform.parent = transform;
		// adjusts the local position of the cell by set factors so that the cells will be all joined correctly
		newCell.transform.localPosition = new Vector3(coords.x - size.x * 0.5f + 0.5f, 0f, coords.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

	// returns a valid set of random coordinates
	public MazeVector randomCoordinates {
		get {
			return new MazeVector(Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	// checks if the coordinates passed to the function are within the range of the maze world
	public bool containsCoordinates (MazeVector coordinate) {
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}

	// this creates a passage on the edge of two cells
	private void createPassage (Cell cell, Cell otherCell, Direction direction) {
		Passage passage = Instantiate(passagePrefab) as Passage; // creates an instance of the passage prefab
		passage.initialise(cell, otherCell, direction); // initialise the passage
		passage = Instantiate(passagePrefab) as Passage; // creates another instance of the passage prefab
		passage.initialise(otherCell, cell, direction.getOpposite()); // creates an instance of the passage on the edge in the opposite direction
	}

	// this creates a wall on an edge 
	private void createWall (Cell cell, Cell otherCell, Direction direction) {
		Wall wall = Instantiate(wallPrefab) as Wall; // creates an instance of a wall from the prefab
		wall.initialise(cell, otherCell, direction); // initialises the wall instance
		// if the other cell isn't null (meaning that it is in the valid world space) then that other cell's edge is made into a wall
		if (otherCell != null) { 
			wall = Instantiate(wallPrefab) as Wall;
			wall.initialise(otherCell, cell, direction.getOpposite());
		}
	}
}
