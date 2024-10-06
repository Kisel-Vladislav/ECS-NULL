using CodeBase.UI;

namespace CodeBase.Infrastructure.Factory
{
    public interface IUIFactory
    {
        void Clear();
        Hud CreateHud();
        void CreateLobby();
        void CreateUIRoot();
    }
}