using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class Graph : IGraph
{
	public class Node //Node class simplifies management of the Nodes in the Graph
	{
		private int x, y; //X and Y coordinates are used for the graph to allow Heuristics to be calculated
		private Dictionary<int, float> neighbours; //Every other node that can be reached from the current node and how hard that journey is
		public Node(int a, int b) 
		{
			x = a;
			y = b;
			neighbours = new Dictionary<int, float>();
		}
		public int getX() { return x; } //functions control access to nodes class
		public int getY() { return y; }
		public List<int> getNeighbours() { return new List<int>(neighbours.Keys); }
		public void addNeighbour(int a, float b) { neighbours[a] = b; }
		public float getCost(int b) { return neighbours[b]; }
	}
	
	Dictionary<int, Node> nodes; //Each node has an ID number, a location, and a list of nodes it is connected to with a weight    
	
	public Graph()
	{
		nodes = new Dictionary<int, Node>();
	}
	
	public bool addNode(int a, int x, int y) //Adds a node to the list, returns true if successfully added
	{
		if (!nodes.ContainsKey(a))
		{
			nodes.Add(a, new Node(x,y)); //sets the new node with it's x and y coordinates
			return true;
		}
		return false;
	}
	
	public Node getNode(int a) //retrieves the Node information
	{
		if (!nodes.ContainsKey(a))
			return nodes[a];
		else 
			return new Node(-1, -1);
	}
	
	public bool addEdge(int a, int b, float cost) //Adds an edge between two nodes
	{
		if (!nodes.ContainsKey(a)||!nodes.ContainsKey(b))
			return false; //returns false if unsuccessfully added
		nodes[a].addNeighbour(b, cost); //Adds a ONE WAY edge from A to B, in case the graph is directional
		return true;
	}
	
	public List<int> listNodes() //lists out all the nodes in the graph
	{
		return new List<int>(nodes.Keys);
	}
	
	public List<int> neighbours(int a) //lists all the neighbours of a particular node
	{
		if (nodes.ContainsKey(a))
			return nodes[a].getNeighbours();
		else
			return new List<int>();
	}
	
	public float cost(int a, int b) //Returns the cost of travelling between two nodes
	{
		if (nodes.ContainsKey(a))
			if (nodes[a].getNeighbours().Contains(b))
				return nodes[a].getCost(b);
		return -1;
	}
}
