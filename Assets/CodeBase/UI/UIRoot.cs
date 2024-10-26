using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private GameObject _rootWindows;
        [SerializeField] private GameObject _rootPopups;
        [SerializeField] private GameObject _rootLoadingCurtain;

        [SerializeField] private Curtain _curtain;

        public void Init()
        {
            //_rootWindows = Instantiate(new GameObject($"[{nameof(_rootWindows).ToUpper()}]"),transform);
            //_rootPopups = Instantiate(new GameObject($"[{nameof(_rootPopups).ToUpper()}]"), transform);
            //_rootLoadingCurtain = Instantiate(new GameObject($"[{nameof(_rootLoadingCurtain).ToUpper()}]"), transform);

            DontDestroyOnLoad(this);
        }

        public async Task ShowCurtain() =>  await _curtain.Show();
        public async Task HideCurtain() => await _curtain.Hide();

        public void AddWindow(Transform transform) => transform.SetParent(_rootWindows.transform);
        public void AddPopup(Transform transform) => transform.SetParent(_rootPopups.transform);
        public void AddCurtain(Curtain curtain)
        {
            curtain.transform.SetParent(_rootLoadingCurtain.transform);
            _curtain = curtain;
        }
        public void Clear()
        {
            for (int i = 0; i < _rootWindows.transform.childCount; i++)
                Destroy(_rootWindows.transform.GetChild(i).gameObject);

            for (int i = 0; i < _rootPopups.transform.childCount; i++)
                Destroy(_rootPopups.transform.GetChild(i).gameObject);
        }
    }
}
