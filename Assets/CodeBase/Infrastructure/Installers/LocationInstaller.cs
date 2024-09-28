using CodeBase.Infrastructure.Services.Level;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class LocationInstaller : MonoInstaller
    {
        public Transform SpawnPoint;
        public override void InstallBindings()
        {
            var levelService = Container.Resolve<ILevelService>();
            levelService.WordObjectCollector = new WordObjectCollector()
            {
                SpawnPoint = SpawnPoint,
            };
        }
    }
}
