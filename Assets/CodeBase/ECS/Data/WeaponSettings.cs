using UnityEngine;

namespace CodeBase.ECS.Data
{
    [CreateAssetMenu]
    public class WeaponSettings : ScriptableObject
    {
        public GameObject WeaponPrefab;
        public GameObject ProjectilePrefab;
        public Transform ProjectileSocket;
        public float ProjectileSpeed;
        public float ProjectileRadius;
        public int WeaponDamage;
        public int CurrentInMagazine;
        public int MaxInMagazine;
        public int TotalAmmo;
        public float Cooldown;
    }
}
