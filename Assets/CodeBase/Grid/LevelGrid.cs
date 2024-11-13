using UnityEngine;

namespace CodeBase.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        private const int X = 100;
        private const int Z = 100;
        private int _cellSize = 2;
        private Grid<Build> _grid;
        [SerializeField] Material _gridMaterial;
        private void Awake()
        {
            _grid = new Grid<Build>(X, Z, _cellSize);

            float planeWidth = X * _cellSize;
            float planeLength = Z * _cellSize;

            var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            plane.transform.localScale = new Vector3(planeWidth / 10, 1, planeLength / 10);
            plane.transform.position = Vector3.zero;
            plane.GetComponent<Renderer>().material = _gridMaterial;
        }
        public Vector3 GetWorldPosition(Vector3 position) =>
            _grid.GetWorldPosition(position);
        private class Build
        {
            public GameObject GameObject;
        }
    }
}
