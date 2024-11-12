using CodeBase.Grid;
using UnityEngine;

namespace CodeBase.LevelEditor
{
    public class MouseGridIndicator  : MonoBehaviour
    {
        public LevelGrid _grid;
        public GameObject GridIndicator;

        private void Update()
        {
            RaycastHit? hitt = Raycast();
            if (hitt != null)
            {
                var pos = _grid.GetWorldPosition(hitt.Value.point);
                GridIndicator.transform.position = pos;
            }
        }
        private RaycastHit? Raycast()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue))
                return hit;

            return null;
        }
    }
}
