using UnityEngine;

namespace CodeBase.Infrastructure.Data
{
    public static class Extensions
    {
        #region Vector3

        public static Vector3 AddY(this Vector3 v, float y)
            => new(v.x, v.y + y, v.z);

        #endregion

        #region Color

        public static Color WithAlpha(this Color color, float alpha) =>
            new Color(color.r, color.g, color.b, alpha);

        #endregion

    }
}
