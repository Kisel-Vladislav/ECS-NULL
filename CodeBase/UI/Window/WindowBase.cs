using PrimeTween;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Window
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class WindowBase : MonoBehaviour
    {
        private const float Duration = 0.5f;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _window;

        protected TaskCompletionSource<bool> _taskCompletionSource;
        protected bool _userAccepted;

        private void OnDestroy() => 
            Tween.StopAll(this);
        public virtual Task<bool> InitAndShow()
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();

            SetVisible(true);

            return _taskCompletionSource.Task;
        }
        protected virtual void Close()
        {
            SetVisible(false);
            _taskCompletionSource.SetResult(_userAccepted);
        }

        // TODO when destroy...
        private async void SetVisible(bool v)
        {
            await Animate(v);

            try
            {
                gameObject.SetActive(v);
            }
            catch (System.Exception)
            {
            }
        }

        private async Task Animate(bool v)
        {
            gameObject.SetActive(true);
            await Tween.Alpha(target: _canvasGroup,
                              endValue: v ? 1f : 0f,
                              startValue: v ? 0.5f : 1f,
                              duration: v ? Duration : 0.1f)
                       .Group(
                Tween.Scale(target: _window,
                            endValue: v ? 1 : 0.5f,
                            startValue: v ? 0.5f : 1,
                            duration: Duration,
                            ease: v ? Ease.OutExpo : Ease.Default));
        }
    }
}