using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Player;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerProvider _playerProvider;
        private readonly IStaticDataService _staticDataService;
        public PlayerFactory(IAssetProvider assetProvider, IPlayerProvider playerProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _playerProvider = playerProvider;
            _staticDataService = staticDataService;
        }

        public GameObject Create(Vector3 at, Quaternion rotation)
        {
            var playerStaticData = _staticDataService.ForPlayer();
            var player = _assetProvider.Instance(playerStaticData.Prefab,at,rotation);
            _playerProvider.Player = player;
            return player;
        }
    }
}

