using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace _Scripts.Extensions
{
	public static class BoundsExtensions
	{
		public struct Edge : IEquatable<Edge>
		{
			public Vector3 start;
			public Vector3 end;

			public Edge(Vector3 start, Vector3 end)
			{
				this.start = start;
				this.end = end;
			}

			public static List<Edge> GetEdges(Bounds bounds)
			{
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;

				// Define the 8 vertices
				Vector3[] vertices = new Vector3[]
				{
					new Vector3(min.x, min.y, min.z),
					new Vector3(max.x, min.y, min.z),
					new Vector3(max.x, min.y, max.z),
					new Vector3(min.x, min.y, max.z),
					new Vector3(min.x, max.y, min.z),
					new Vector3(max.x, max.y, min.z),
					new Vector3(max.x, max.y, max.z),
					new Vector3(min.x, max.y, max.z)
				};

				// Define the 12 edges using the vertex indices
				int[,] edgeIndices = new int[,]
				{
					{ 0, 1 }, { 1, 2 }, { 2, 3 }, { 3, 0 },
					{ 4, 5 }, { 5, 6 }, { 6, 7 }, { 7, 4 },
					{ 0, 4 }, { 1, 5 }, { 2, 6 }, { 3, 7 }
				};

				List<Edge> edges = new List<Edge>();

				for (int i = 0; i < edgeIndices.GetLength(0); i++)
				{
					Vector3 start = vertices[edgeIndices[i, 0]];
					Vector3 end = vertices[edgeIndices[i, 1]];

					edges.Add(new Edge(start, end));
				}

				return edges;
			}

			public static bool IsEdgeSharedWithBound(Edge edge, Bounds bounds)
			{
				List<Edge> edgesInBounds = GetEdges(bounds);
				
				foreach (Edge e in edgesInBounds)
				{
					//todo: problem may be in this place; edge is tested to equality, but it can be just inside and if it is - cannot pass test
					if (edge.Equals(e))
					{
						return true;
					}
				}

				return false;
			}

			public bool Equals(Edge other)
			{
				return (start == other.start && end == other.end) || (start == other.end && end == other.start);
			}

			public override bool Equals(object obj) => obj is Edge other && Equals(other);

		}

				public static Dictionary<int, List<Bounds>> GetOverlappingBounds(Bounds[] boundsArray, int startingIndex, out int reachedIndex)
		{
			var overlappingBoundsDictionary = new Dictionary<int, List<Bounds>>();
			List<Bounds> groupedBounds = new List<Bounds>(boundsArray.Length);

			reachedIndex = startingIndex;
			
			for (int i = 0; i < boundsArray.Length; i++)
			{
				if (groupedBounds.Contains(boundsArray[i])) continue;

				reachedIndex = overlappingBoundsDictionary.Count + startingIndex;
				
				overlappingBoundsDictionary.Add(reachedIndex, new List<Bounds>());
				overlappingBoundsDictionary[reachedIndex].Add(boundsArray[i]);

				groupedBounds.Add(boundsArray[i]);

				List<Bounds> boundsInGroup = overlappingBoundsDictionary[reachedIndex];

				bool updated;

				do
				{
					updated = false;

					for (int k = 0; k < boundsInGroup.Count; k++)
					{
						Bounds b = boundsInGroup[k];

						for (int j = 0; j < boundsArray.Length; j++)
						{
							if (groupedBounds.Contains(boundsArray[j])) continue;

							if (b.Intersects(boundsArray[j]))
							{
								groupedBounds.Add(boundsArray[j]);
								boundsInGroup.Add(boundsArray[j]);

								updated = true;
							}
						}
					}

				} while (updated);
			}

			return overlappingBoundsDictionary;
		}

		public static Dictionary<int, List<Bounds>> GetOverlappingBounds(List<Bounds> boundsList, int startingIndex,
			out int reachedIndex)
		{
			var overlappingBoundsDictionary = new Dictionary<int, List<Bounds>>();
			List<Bounds> groupedBounds = new List<Bounds>(boundsList.Count);

			reachedIndex = startingIndex;

			for (int i = 0; i < boundsList.Count; i++)
			{
				if (groupedBounds.Contains(boundsList[i])) continue;

				reachedIndex = overlappingBoundsDictionary.Count + startingIndex;

				overlappingBoundsDictionary.Add(reachedIndex, new List<Bounds>());
				overlappingBoundsDictionary[reachedIndex].Add(boundsList[i]);

				groupedBounds.Add(boundsList[i]);

				List<Bounds> boundsInGroup = overlappingBoundsDictionary[reachedIndex];

				bool updated;

				do
				{
					updated = false;

					for (int k = 0; k < boundsInGroup.Count; k++)
					{
						Bounds b = boundsInGroup[k];

						for (int j = 0; j < boundsList.Count; j++)
						{
							if (groupedBounds.Contains(boundsList[j])) continue;

							if (b.Intersects(boundsList[j]))
							{
								groupedBounds.Add(boundsList[j]);
								boundsInGroup.Add(boundsList[j]);

								updated = true;
							}
						}
					}

				} while (updated);
			}

			return overlappingBoundsDictionary;
		}
		
	}

}