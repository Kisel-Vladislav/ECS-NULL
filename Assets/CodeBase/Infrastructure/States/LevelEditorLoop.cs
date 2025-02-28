using CodeBase.Grid;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.InputService;
using CodeBase.Infrastructure.Services.Raycast;
using CodeBase.Infrastructure.StaticData;
using CodeBase.LevelEditor;
using CodeBase.UI;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LevelEditorLoop : IState, ITickable
    {
        private readonly IRaycastService _raycastService;
        private readonly IUIFactory _uIFactory;
        private readonly IInputService _inputService;
        private readonly IStaticDataService _staticDataService;

        private LevelEditorHud LevelEditorHud => _uIFactory.Root.GetComponentInChildren<LevelEditorHud>();

        private MouseGridIndicator mouseGridIndicator;
        private MouseGridInteractor mouseGridInteractor;
        private bool init;

        public LevelEditorLoop(IRaycastService raycastService, IUIFactory uIFactory, IInputService inputService,IStaticDataService staticDataService)
        {
            _raycastService = raycastService;
            _uIFactory = uIFactory;
            _inputService = inputService;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            var levelGrid = Object.FindFirstObjectByType<LevelGrid>();
            LevelEditorHud.BuildPanelContentController.OnChangePreview += (id) => mouseGridInteractor.build = _staticDataService.ForBuild(id);

            mouseGridIndicator = new MouseGridIndicator();
            mouseGridInteractor = new MouseGridInteractor(_inputService);
            mouseGridIndicator._grid = levelGrid;
            mouseGridIndicator.GridIndicator = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mouseGridInteractor._grid = levelGrid;

            init = true;
        }

        public Task Exit() =>
            Task.CompletedTask;

        public void Tick()
        {
            if (!init)
                return;

            RaycastHit? hit = _raycastService.GetMouseRaycastHit();
            if (hit != null)
            {
                var point = hit.Value.point;
                point += hit.Value.normal * 0.1f;

                mouseGridIndicator.UpdatePosition(point);
                mouseGridInteractor.Update(point);
            }
        }
    }
}
