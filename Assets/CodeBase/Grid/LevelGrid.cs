using Assets;
using CodeBase.Infrastructure.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        private const int X = 50;
        private const int Z = 50;
        private const int GridLevels = 3;
        private const int CellSize = 2;

        [SerializeField] private Material _gridMaterial;

        private class BuildCell
        {
            public GameObject GameObject;
        }

        private List<Grid<BuildCell>> _levels;

        private void Awake()
        {
            InitGridLevels();
            CreateGridPlane();
        }

        public Vector3 GetWorldPosition(Vector3 position)
        {
            float levelYOffset = RoundedToLevelIndex(position) * CellSize;
            return _levels[0].GetWorldPosition(position).AddY(levelYOffset);
        }
        public void PlaceBuild(Vector3 position, Build build)
        {
            var grid = GetGridForBuilding(position);
            if (grid == null)
                return;

            var cell = grid.Get(position);
            if (cell == null || !IsCellEmpty(cell))
                return;

            var worldPosition = GetWorldPosition(position);

            cell.Value = new BuildCell
            {
                GameObject = Instantiate(build.prefab, worldPosition, Quaternion.identity, transform)
            };
        }
        public void DestroyBlock(Vector3 position)
        {
            var grid = GetGridForDestruction(position);
            if (grid == null)
                return;

            var cell = grid.Get(position);
            if (cell == null || IsCellEmpty(cell))
                return;

            Destroy(cell.Value.GameObject);
            cell.Value = null;
        }

        private void InitGridLevels()
        {
            _levels = new List<Grid<BuildCell>>();
            for (int i = 0; i < GridLevels; i++)
            {
                var grid = new Grid<BuildCell>(X, Z, CellSize);
                _levels.Add(grid);
            }
        }
        private void CreateGridPlane()
        {
            float planeWidth = X * CellSize;
            float planeLength = Z * CellSize;

            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            plane.transform.localScale = new Vector3(planeWidth / 10, 1, planeLength / 10);
            plane.transform.position = Vector3.zero;

            plane.GetComponent<Renderer>().material = _gridMaterial;
        }
        private Grid<BuildCell> GetGridForBuilding(Vector3 position)
        {
            int levelIndex = RoundedToLevelIndex(position);
            return IsLevelIndexValid(levelIndex) ? _levels[levelIndex] : null;
        }
        private Grid<BuildCell> GetGridForDestruction(Vector3 position)
        {
            int levelIndex = Mathf.FloorToInt(position.y / CellSize);
            levelIndex = Mathf.Max(levelIndex, 0);

            return IsLevelIndexValid(levelIndex) ? _levels[levelIndex] : null;
        }
        private int RoundedToLevelIndex(Vector3 position) => 
            Mathf.RoundToInt(position.y / CellSize);
        private bool IsLevelIndexValid(int levelIndex) => 
            levelIndex >= 0 && levelIndex < _levels.Count;
        private bool IsCellEmpty(Grid<BuildCell>.GridTile cell) => 
            cell.Value == null || cell.Value.GameObject == null;
    }
}
