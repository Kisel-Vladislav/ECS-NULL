using Leopotam.Ecs;
using UnityEngine;
namespace CodeBase.ECS.Component
{
    public struct Player
    {
        public Animator playerAnimator;
        public Transform playerTransform;
        public CharacterController CharacterController;
        public float playerSpeed;
    }
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
    }
    public struct HasWeapon
    {
        public EcsEntity weapon;
    }
}
