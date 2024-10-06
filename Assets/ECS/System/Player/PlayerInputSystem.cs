using CodeBase.ECS.Component;
using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<HasWeapon> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var hasWeapon = ref _filter.Get1(i);

                ref var moveInput = ref entity.Get<MoveInput>();
                moveInput.vector = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

                if (Input.GetMouseButton(1))
                    entity.Get<TryAim>();
                else
                    entity.Get<AimFinished>();


                if (Input.GetMouseButtonDown(0))
                {
                    hasWeapon.weapon.Get<Shoot>();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    ref var weapon = ref hasWeapon.weapon.Get<Weapon>();
                    if (weapon.currentInMagazine < weapon.maxInMagazine)
                    {
                        entity.Get<TryReload>();
                    }
                }
            }
        }
    }
}
