using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class GraphSearch : IGraphSearch
{
	Graph graph;
	Queue<int> frontier;
	List<int> visited;
	int current;

	public GraphSearch()
	{
		graph = new Graph();
		frontier = new Queue<int>();
		visited = new List<int>();
	}

	public void setGraph(Graph g)
	{
		graph = g;
	}

	public List<int> findPath(int start, int end, bool trace)
	{

		frontier.Enqueue(start);
		visited.Add(start);
		while (frontier.Count != 0) 
		{
			current = frontier.Dequeue();
			if (current == end)
				break;
			List<int> next = graph.neighbours(current);
			for (int i = 0; i < next.Count; i++)
			{
				if (!(visited.Contains(next[i])))
				{
					frontier.Enqueue(next[i]);
					visited.Add(current);
				}
			}
		}
		return visited;
	}



}
