using UnityEngine;
namespace CodeBase.ECS.WeaponComponent
{
    public struct Projectile
    {
        public int damage;
        public Vector3 direction;
        public float radius;
        public float speed;
        public Vector3 previousPos;
        public GameObject projectileGO;
    }
}   
