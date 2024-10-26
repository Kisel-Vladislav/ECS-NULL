using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Window
{
    public class TwoButtonWindow : OneButtonWindow
    {
        [SerializeField] private Button cancelButton;

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
            _result = false;
            Close();
        }
    }
}