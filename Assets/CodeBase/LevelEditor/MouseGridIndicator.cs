using CodeBase.Grid;
using CodeBase.Infrastructure.Services.Raycast;
using UnityEngine;
using Zenject;

namespace CodeBase.LevelEditor
{
    public class MouseGridIndicator : MonoBehaviour
    {
        public LevelGrid _grid;
        public GameObject GridIndicator;

        private IRaycastService _raycastService;

        [Inject]
        public void Construct(IRaycastService raycastService)
        {
            _raycastService = raycastService;
        }

        private void Update()
        {
            RaycastHit? hit = _raycastService.GetMouseRaycastHit();
            if (hit != null)
            {
                var pos = _grid.GetWorldPosition(hit.Value.point);
                GridIndicator.transform.position = pos;
            }
        }
    }
}
