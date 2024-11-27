using CodeBase.Grid;
using CodeBase.Infrastructure.Service.InputService;
using CodeBase.Infrastructure.Services.Raycast;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.LevelEditor
{
    public class MouseGridInteractor
    {
        private IRaycastService _raycastService;
        private IInputService _inputService;
        public Build build;
        public LevelGrid _grid;
        public bool _isDestroyMode;

        public MouseGridInteractor(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Update(Vector3 position)
        {
            if (!_inputService.IsPointerDown())
                return;

            Interact(position);
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
