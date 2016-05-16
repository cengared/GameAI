[System.Serializable] // saves the custom struct

// a vector class to handle maze coordinates using just integers
public struct MazeVector {

	public int x, z;

	public MazeVector(int x, int z) {
		this.x = x;
		this.z = z;
	}

	// overloaded function for the + operator to make it easy to add vectors
	public static MazeVector operator + (MazeVector a, MazeVector b) {
		a.x += b.x;
		a.z += b.z;
		return a;
	}

}
