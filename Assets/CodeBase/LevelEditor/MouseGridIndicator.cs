using CodeBase.Grid;
using UnityEngine;

namespace CodeBase.LevelEditor
{
    public class MouseGridIndicator
    {
        public LevelGrid _grid;
        public GameObject GridIndicator;

        public void UpdatePosition(Vector3 pos)
        {
            var position = _grid.GetWorldPosition(pos);
            GridIndicator.transform.position = position;
        }
    }
}
