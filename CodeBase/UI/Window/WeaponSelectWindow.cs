using CodeBase.Player.Data;
using CodeBase.StaticData;
using CodeBase.UI.WeaponSelectPanel;

namespace CodeBase.UI.Window
{
    public class WeaponSelectWindow : TwoButtonWindow
    {
        public WeaponSelectWindowContentController _contentController;

        private IStaticDataService _staticDataService;
        private PersistentProgress _persistentProgress;
        public void Construct(IStaticDataService staticDataService, PersistentProgress persistentProgress)
        {
            _persistentProgress = persistentProgress;
            _staticDataService = staticDataService;

        }
        private void Start()
        {
            ShowContent();
        }
        private void ShowContent()
        {
            _contentController.Fill(_staticDataService.ForWeaponSelectPanelItems());
        }
        public void Clear()
        {

        }
        protected override void Close()
        {
            _persistentProgress.Player.SelectWeapon = _contentController.SelectItemID;
            base.Close();
        }
    }
}