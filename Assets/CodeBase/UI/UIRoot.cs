using UnityEngine;

namespace CodeBase.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] GameObject RootWindows;
        [SerializeField] GameObject RootPopups;
        [SerializeField] GameObject RootLoadingCurtain;

        public void Init()
        {
            RootWindows = Instantiate(new GameObject($"[{nameof(RootWindows).ToUpper()}]"),transform);
            RootPopups = Instantiate(new GameObject($"[{nameof(RootPopups).ToUpper()}]"), transform);
            RootLoadingCurtain = Instantiate(new GameObject($"[{nameof(RootLoadingCurtain).ToUpper()}]"), transform);

            DontDestroyOnLoad(this);
        }
        public void AddWindow(Transform transform) => transform.SetParent(RootWindows.transform);
        public void AddPopup(Transform transform) => transform.SetParent(RootPopups.transform);
        public void AddCurtain(Transform transform) => transform.SetParent(RootLoadingCurtain.transform);

        public void Clear()
        {
            for (int i = 0; i < RootWindows.transform.childCount; i++)
                Destroy(RootWindows.transform.GetChild(i).gameObject);

            for (int i = 0; i < RootPopups.transform.childCount; i++)
                Destroy(RootPopups.transform.GetChild(i).gameObject);
        }
    }
}
