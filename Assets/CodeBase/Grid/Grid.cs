using UnityEngine;

namespace CodeBase.Grid
{
    public class Grid<T>
    {
        public class GridTile
        {
            public readonly int X;
            public readonly int Z;

            public T Value;

            public GridTile(int x, int z)
            {
                X = x;
                Z = z;
            }
        }

        private GridTile[,] _grid;
        private Vector2 offset;

        public int X;
        public int Z;
        public float _cellSize;

        public Grid(int x, int z, int cellSize)
        {
            _cellSize = cellSize;
            X = x;
            Z = z;

            offset = new Vector2((X - 1) * 0.5f * cellSize, (Z - 1) * 0.5f * cellSize);

            InitializeGrid();
        }

        public Vector3 GetWorldPosition(Vector3 position)
        {
            GetCellIndicesFromPosition(position, out int x, out int z);
            return GetWorldPosition(x, z);
        }

        public GridTile Get(Vector3 position)
        {
            GetCellIndicesFromPosition(position, out int x, out int z);
            return Get(x, z);
        }
        public GridTile Get(int x, int z) =>
            IsWithinBounds(x, z) ? _grid[x, z] : default;

        public void Set(int x, int z, T v)
        {
            if (IsWithinBounds(x, z))
                _grid[x, z].Value = v;
        }
        public void Set(Vector3 position, T v)
        {
            GetCellIndicesFromPosition(position, out int x, out int z);
            Set(x, z, v);
        }

        public void GetCellIndicesFromPosition(Vector3 position, out int x, out int z)
        {
            x = Mathf.FloorToInt((position.x + offset.x) / _cellSize);
            z = Mathf.FloorToInt((position.z + offset.y) / _cellSize);
        }
        public Vector3 GetWorldPosition(int x, int z) =>
             new Vector3(x * _cellSize + _cellSize / 2 - offset.x, 0f, z * _cellSize + _cellSize / 2 - offset.y);

        private void InitializeGrid()
        {
            _grid = new GridTile[X, Z];
            for (int x = 0; x < X; x++)
                    for (int z = 0; z < Z; z++)
                    _grid[x, z] = new GridTile(x,z);
        }
        private bool IsWithinBounds(int x, int z) =>
            x >= 0 && x < X && z >= 0 && z < Z;
    }
}
