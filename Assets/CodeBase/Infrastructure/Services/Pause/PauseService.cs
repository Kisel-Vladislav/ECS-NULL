using CodeBase.Infrastructure.Services.Pause;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services
{
    public class PauseService
    {
        private readonly List<IPauseHandler> _handlers = new();

        public void Register(IPauseHandler handler) =>
            _handlers.Add(handler);
        public void UnRegister(IPauseHandler handler) =>
            _handlers.Remove(handler);
        public void CleanUp() =>
            _handlers.Clear();
        public void SetPaused(bool isPaused)
        {
            foreach (var handler in _handlers)
            {
                handler.SetPaused(isPaused);
            }
        }

    }
}
