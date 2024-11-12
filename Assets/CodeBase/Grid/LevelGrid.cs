using UnityEngine;

namespace CodeBase.Grid
{
    public class LevelGrid : MonoBehaviour
    {
        private Grid<Build> _grid;

        private void Awake()
        {
            _grid = new Grid<Build>(10, 10, 2);
        }
        public Vector3 GetWorldPosition(Vector3 position) =>
            _grid.GetWorldPosition(position);
        private class Build
        {
            public GameObject GameObject;
        }
    }
}
