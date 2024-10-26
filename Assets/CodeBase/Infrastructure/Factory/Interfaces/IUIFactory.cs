using CodeBase.UI;
using System.Threading.Tasks;
using static CodeBase.UI.Window.FailWindow;
using static CodeBase.UI.Window.WinWindow;

namespace CodeBase.Infrastructure.Factory
{
    public interface IUIFactory
    {
        UIRoot Root { get; }

        void Clear();
        Hud CreateHud();
        void CreateLobby();
        void CreateUIRoot();

        Task<FailWindowResult> ShowFailWindow();
        Task<WinWindowResult> ShowWinWindow();
    }
}