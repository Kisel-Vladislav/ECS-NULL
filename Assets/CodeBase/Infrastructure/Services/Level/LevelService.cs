using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Services.Level
{
    public class LevelService : ILevelService
    {
        public WordObjectCollector WordObjectCollector { get; set; }
    }
}