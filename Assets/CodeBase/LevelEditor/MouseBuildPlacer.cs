using CodeBase.Grid;
using CodeBase.Infrastructure.Service.InputService;
using CodeBase.Infrastructure.Services.Raycast;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.LevelEditor
{
    public class MouseBuildPlacer : MonoBehaviour
    {
        private IRaycastService _raycastService;
        private IInputService _inputService;
        public Build build;
        public LevelGrid _grid;


        [Inject]
        public void Construct(IRaycastService raycastService, IInputService inputService)
        {
            _raycastService = raycastService;
            _inputService = inputService;
        }

        private void Update()
        {
            RaycastHit? hit = _raycastService.GetMouseRaycastHit();
            if (hit != null && _inputService.IsPointerDown())
            {
                _grid.PlaceBuild(hit.Value.point,build);
            }
        }
    }
}
