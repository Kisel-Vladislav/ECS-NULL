using CodeBase.ECS.Data;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private PlayerStaticData _player;
        private WeaponSettings _weapon;

        public PlayerStaticData ForPlayer() => _player;
        public WeaponSettings ForWeapon() => _weapon;

        public void LoadPlayer() => _player = Resources.Load<PlayerStaticData>("StaticData/Player/PlayerData");

        public void LoadWeapon() => _weapon = Resources.Load<WeaponSettings>("StaticData/Weapon/Pistol");
    }
}
