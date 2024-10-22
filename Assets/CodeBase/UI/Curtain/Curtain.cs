using PrimeTween;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    [SerializeField] private RectTransform maskRect;
    [SerializeField] private RectTransform imageRect;
    [SerializeField] private float _transitionTime;
    [SerializeField] private bool _rotate;

    public RectTransform TransitionCanvas;

    private float screenWidth;
    private float screenHeight;
    private float maxSize;

    private void Start()
    {
        SetupMaxSize();
    }

    public void Show()
    {
        Tween.UISizeDelta(maskRect, Vector3.zero, _transitionTime, Ease.InOutQuad);

        if(_rotate)
            Tween.Rotation(maskRect, new Vector3(0,0,180), _transitionTime, Ease.InOutQuad);
    }
    public void Hide()
    {
        Tween.CompleteAll(maskRect);
        Tween.UISizeDelta(maskRect, new Vector2(maxSize, maxSize), _transitionTime, Ease.InOutQuad);

        if (_rotate)
            Tween.Rotation(maskRect,Vector3.zero, _transitionTime, Ease.InOutQuad);
    }

    public void SetupMaxSize()
    {
        screenWidth = TransitionCanvas.rect.width;
        screenHeight = TransitionCanvas.rect.height;

        maxSize = Mathf.Max(screenWidth, screenHeight);
        maxSize += maxSize / 4;
    }
}
