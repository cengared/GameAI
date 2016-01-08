using UnityEngine;

// definitions of directions
public enum Direction {
	North,
	East,
	South,
	West
}

// class for handling the defined directions
public static class Directions {
	
	public const int Count = 4; // total number of available directions

	// returns a random number within the range of directions to give a random direction
	public static Direction randomValue {
		get {
			return (Direction)Random.Range(0, Count);
		}
	}

	// defines directions as vectors
	private static MazeVector[] vectors = {
		new MazeVector(0, 1),
		new MazeVector(1, 0),
		new MazeVector(0, -1),
		new MazeVector(-1, 0)
	};

	// converts a direction to a vector
	public static MazeVector toMazeVector2 (this Direction direction) {
		return vectors[(int)direction];
	}

	// defining the opposite directions (for cells with passages
	private static Direction[] opposites = {
		Direction.South,
		Direction.West,
		Direction.North,
		Direction.East
	};

	// returns the opposite direction as defined above
	public static Direction getOpposite (this Direction direction) {
		return opposites[(int)direction];
	}

	// sets the rotations required for each direction
	// used http://docs.unity3d.com/ScriptReference/Quaternion.Euler.html for reference
	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};

	// returns the required rotation values for the direction
	public static Quaternion toRotation (this Direction direction) {
		return rotations[(int)direction];
	}
}

