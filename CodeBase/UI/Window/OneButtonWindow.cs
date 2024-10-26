using CodeBase.Infrastructure.Services.Audio;
using CodeBase.StaticData;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Window
{
    public class OneButtonWindow : WindowBase
    {
        [SerializeField] private Button okButton;
        [SerializeField] private AudioClip _okButtonPress;

        protected IAudioService _audioService;

        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public override Task<bool> InitAndShow()
        {
            okButton.onClick.AddListener(Accept);

            return base.InitAndShow();
        }

        protected override void Close()
        {
            okButton.onClick.RemoveAllListeners();
            base.Close();
        }

        private void Accept()
        {
            _audioService.Play(_okButtonPress);
            _userAccepted = true;
            Close();
        }
    }
}