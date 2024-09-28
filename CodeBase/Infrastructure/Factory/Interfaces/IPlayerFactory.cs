using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IPlayerFactory
    {
        GameObject Create(Vector3 position, Quaternion rotation);
    }
}