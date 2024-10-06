using CodeBase.ECS.PlayerComponent;
using CodeBase.ECS.WeaponComponent;
using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.PlayerSystem
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputData, HasWeapon> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var hasWeapon = ref _filter.Get2(i);


                input.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
                input.IsAimButtonPressed = Input.GetMouseButton(1);
                //input.ShootInput = Input.GetMouseButton(0);
                if(Input.GetMouseButtonDown(0))
                {
                    ref var entity = ref _filter.GetEntity(i);
                    hasWeapon.weapon.Get<Shoot>();
                }
                if(Input.GetKeyDown(KeyCode.R))
                {
                    ref var weapon = ref hasWeapon.weapon.Get<Weapon>();
                    if(weapon.currentInMagazine < weapon.maxInMagazine)
                    {
                        ref var entity = ref _filter.GetEntity(i);
                        entity.Get<TryReload>();    
                    }
                }
            }
        }    }
}
