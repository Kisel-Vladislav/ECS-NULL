using CodeBase.UI;

namespace CodeBase.Infrastructure.Factory
{
    public interface IUIFactory
    {
        UIRoot Root { get; }

        void Clear();
        Hud CreateHud();
        void CreateLobby();
        void CreateUIRoot();
    }
}