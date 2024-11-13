using UnityEngine;

namespace CodeBase.Infrastructure.Services.Raycast
{
    public class RaycastService : IRaycastService
    {
        public RaycastHit? GetMouseRaycastHit()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, float.MaxValue))
                return hit;

            return null;
        }
    }
}
