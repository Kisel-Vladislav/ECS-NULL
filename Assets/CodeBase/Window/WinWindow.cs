using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static CodeBase.UI.Window.WinWindow;

namespace CodeBase.UI.Window
{
    public class WinWindow : WindowBase<WinWindowResult>
    {
        public enum WinWindowResult
        {
            Replay,
            Lobby,
        }

        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _lobbyButton;

        public override Task<WinWindowResult> InitAndShow()
        {
            _replayButton.onClick.AddListener(ReplayButtonOnClick);
            _lobbyButton.onClick.AddListener(LobbyButtonOnClick);
            return base.InitAndShow();
        }

        private void LobbyButtonOnClick()
        {
            _result = WinWindowResult.Lobby;
            Close();
        }

        private void ReplayButtonOnClick()
        {
            _result = WinWindowResult.Replay;
            Close();
        }
        protected override void Close()
        {
            _replayButton.onClick.RemoveAllListeners();
            _lobbyButton.onClick.RemoveAllListeners();

            base.Close();
        }
    }
}