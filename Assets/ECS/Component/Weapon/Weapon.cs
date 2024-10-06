using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.WeaponComponent
{
    public struct Weapon
    {
        public EcsEntity owner;
        public GameObject projectilePrefab;
        public Transform projectileSocket;
        public float projectileSpeed;
        public float projectileRadius;
        public int weaponDamage;
        public int currentInMagazine;
        public int maxInMagazine;
        public int totalAmmo;
        public float Cooldown;
    }
}   
