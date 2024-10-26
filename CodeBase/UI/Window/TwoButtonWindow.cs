using CodeBase.UI.Window;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Window
{
    public class TwoButtonWindow : OneButtonWindow
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private AudioClip _cancelButtonPress;

        public override Task<bool> InitAndShow()
        {
            cancelButton.onClick.AddListener(Deny);

            return base.InitAndShow();
        }

        protected override void Close()
        {
            cancelButton.onClick.RemoveAllListeners();
            base.Close();
        }

        private void Deny()
        {
            _audioService.Play(_cancelButtonPress);
            _userAccepted = false;
            Close();
        }
    }
}