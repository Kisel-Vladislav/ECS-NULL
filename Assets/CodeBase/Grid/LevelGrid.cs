using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        private const int X = 5;
        private const int Z = 5;

        private class BuildCell
        {
            public GameObject GameObject;
        }

        private int _cellSize = 2;
        private Grid<BuildCell> _grid;
        [SerializeField] Material _gridMaterial;

        private void Awake()
        {
            _grid = new Grid<BuildCell>(X, Z, _cellSize);

            float planeWidth = X * _cellSize;
            float planeLength = Z * _cellSize;

            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.localScale = new Vector3(planeWidth / 10, 1, planeLength / 10);
            plane.transform.position = Vector3.zero;
            plane.GetComponent<Renderer>().material = _gridMaterial;
        }

        public Vector3 GetWorldPosition(Vector3 position) =>
            _grid.GetWorldPosition(position);

        public void PlaceBuild(Vector3 position, Build build)
        {
            int x, z;
            _grid.GetCellIndicesFromPosition(position, out x, out z);

            var cell = _grid.Get(x, z);

            if (cell == null || !IsCellEmpty(cell))
                return;

            var worldPosition = _grid.GetWorldPosition(x, z);

            cell.Value = new BuildCell();
            cell.Value.GameObject = Instantiate(build.prefab, worldPosition, Quaternion.identity, transform);
        }

        private bool IsCellEmpty(Grid<BuildCell>.GridTile cell)
        {
            return cell.Value == null || cell.Value.GameObject == null;

        }
    }
}
