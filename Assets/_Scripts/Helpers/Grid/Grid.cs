using UnityEngine;

namespace _Scripts.Helpers.Grid
{
	public class Grid
	{
		public class GridElement
		{
			public bool free;

			public GridElement(bool free)
			{
				this.free = free;
			}
		}
		
		private Vector3 originPosition;
		private int xSize;
		private int zSize;
		private int cellSize;

		private GridElement[] elements;
		
		public Grid(Vector3 startPosition, int xSize, int zSize, int cellSize)
		{
			this.originPosition = startPosition;
			this.xSize = xSize;
			this.zSize = zSize;
			this.cellSize = cellSize;
			
			CreateGrid();
		}

		private void CreateGrid()
		{
			elements = new GridElement[xSize * zSize];
			
			for (int i = 0; i < elements.Length; i++)
			{
				elements[i] = new GridElement(free: true);
			}

			/*for (int x = 0; x < xSize; x++)
			{
				for (int z = 0; z < zSize; z++)
				{
					Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.green, 500f);
					Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.green, 500f);
				}
			}
			
			Debug.DrawLine(GetWorldPosition(0, zSize), GetWorldPosition(xSize, zSize),Color.green, 500f);
			Debug.DrawLine(GetWorldPosition(xSize, 0), GetWorldPosition(xSize, zSize),Color.green, 500f);*/
		}

		private Vector3 GetWorldPosition(int x, int z)
		{
			return new Vector3(x, 0f, z) * cellSize + originPosition;
		}

		public Vector3 GetWorldPosition(Vector3 worldPos)
		{
			int x, z;
			GetXZ(worldPos, out x, out z);
			return GetWorldPosition(x, z);
		}

		public Vector3 GetCenterGrid(Vector3 worldPos)
		{
			return GetWorldPosition(worldPos) + new Vector3(cellSize, 0f, cellSize) * 0.5f;
		}

		private void GetXZ(Vector3 worldPos, out int x, out int z)
		{
			x = Mathf.FloorToInt((worldPos - originPosition).x / cellSize);
			z = Mathf.FloorToInt((worldPos - originPosition).z / cellSize);
		}

		public GridElement GetValue(int x, int z)
		{
			if (x >= 0 && z >= 0 && x < xSize && z < zSize)
			{
				return elements[GetIndex(x, z)];
			}
			else
			{
				return null;
			}
		}

		public GridElement GetValue(Vector3 worldPos)
		{
			int x, z;
			GetXZ(worldPos, out x, out z);
			return GetValue(x, z);
		}

		public void SetValue(int x, int z, GridElement value)
		{
			if (x >= 0 && z >= 0 && x < xSize && z < zSize)
			{
				elements[GetIndex(x, z)] = value;
			}
		}

		public void SetValue(Vector3 worldPos, GridElement value)
		{
			int x, z;
			GetXZ(worldPos, out x, out z);
			SetValue(x,z,value);
		}

		public int GetIndex(int x, int z)
		{
			return z * zSize + x;
		}
	}
}