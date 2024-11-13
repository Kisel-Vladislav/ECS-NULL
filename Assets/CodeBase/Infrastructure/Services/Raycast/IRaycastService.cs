using UnityEngine;

namespace CodeBase.Infrastructure.Services.Raycast
{
    public interface IRaycastService
    {
        RaycastHit? GetMouseRaycastHit();
    }
}