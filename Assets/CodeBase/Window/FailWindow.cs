using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static CodeBase.UI.Window.FailWindow;

namespace CodeBase.UI.Window
{
    public class FailWindow : WindowBase<FailWindowResult>
    {
        public enum FailWindowResult
        {
            Replay,
            Lobby,
        }
        
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _lobbyButton;

        public override Task<FailWindowResult> InitAndShow()
        {
            _replayButton.onClick.AddListener(ReplayButtonOnClick);
            _lobbyButton.onClick.AddListener(LobbyButtonOnClick);
            return base.InitAndShow();
        }

        private void LobbyButtonOnClick()
        {
            _result = FailWindowResult.Lobby;
            Close();
        }

        private void ReplayButtonOnClick()
        {
            _result = FailWindowResult.Replay;
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