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
        private void Awake()
        {
            _raycastService = new RaycastService();
        }
        private void Update()
        {
            RaycastHit? hitt = _raycastService.GetMouseRaycastHit();
            if (hitt != null)
            {
                var pos = _grid.GetWorldPosition(hitt.Value.point);
                GridIndicator.transform.position = pos;
            }
        }
    }
}
