using CodeBase.Grid;
using CodeBase.Infrastructure.Service.InputService;
using CodeBase.Infrastructure.Services.Raycast;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.LevelEditor
{
    public class MouseGridInteractor : MonoBehaviour
    {
        private IRaycastService _raycastService;
        private IInputService _inputService;
        public Build build;
        public LevelGrid _grid;
        public bool _isDestroyMode;

        [Inject]
        public void Construct(IRaycastService raycastService, IInputService inputService)
        {
            _raycastService = raycastService;
            _inputService = inputService;
        }

        private void Update()
        {
            if (!_inputService.IsPointerDown())
                return;

            var hit = _raycastService.GetMouseRaycastHit();
            if (hit == null)
                return;

            Interact(hit.Value.point);
        }

        private void Interact(Vector3 point)
        {
            if (_isDestroyMode)
                _grid.DestroyBlock(point);
            else
                _grid.PlaceBuild(point, build);
        }
    }
}
