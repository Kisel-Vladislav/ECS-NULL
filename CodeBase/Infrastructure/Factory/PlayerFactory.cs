using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.Player;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerProvider _playerProvider;

        public PlayerFactory(IAssetProvider assetProvider, IPlayerProvider playerProvider)
        {
            _assetProvider = assetProvider;
            _playerProvider = playerProvider;
        }

        public GameObject Create(Vector3 at, Quaternion rotation)
        {
            var player = _assetProvider.Instance<GameObject>(AssetsPath.Player,at,rotation);
            _playerProvider.Player = player;
            return player;
        }
    }
}

