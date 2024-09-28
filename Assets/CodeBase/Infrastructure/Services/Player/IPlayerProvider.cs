using UnityEngine;

namespace CodeBase.Infrastructure.Services.Player
{
    public interface IPlayerProvider
    {
        public GameObject Player { get; set; }
        void CleanUp();
    }
}

