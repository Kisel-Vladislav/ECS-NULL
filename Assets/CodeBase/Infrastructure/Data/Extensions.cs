using UnityEngine;

namespace Assets
{
    public static class Extensions
    {
        public static Vector3 AddY(this Vector3 v, float y)
            => new(v.x, v.y + y, v.z);
    }
}
