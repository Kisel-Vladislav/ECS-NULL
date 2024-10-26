using PrimeTween;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Curtain : MonoBehaviour
{
    [SerializeField] private RectTransform maskRect;
    [SerializeField] private RectTransform imageRect;
    [SerializeField] private Image image;
    [SerializeField] private float _transitionTime;
    [SerializeField] private Color _baseColor;
    [SerializeField] private Color _transitionColor;
    [SerializeField] private bool _rotate;

    public RectTransform TransitionCanvas;

    private float screenWidth;
    private float screenHeight;
    private float maxSize;

    private void Start()
    {
        Setup();
    }

    public async Task Show()
    {
        var tween = Tween.UISizeDelta(maskRect, Vector3.zero, _transitionTime, Ease.InOutQuad)
            .Group(Tween.Color(image, _baseColor, _transitionTime));

        if(_rotate)
            tween.Group(Tween.Rotation(maskRect, new Vector3(0,0,180), _transitionTime, Ease.InOutQuad));

        await tween;
    }
    public async Task Hide()
    {
        Tween.CompleteAll(maskRect);
        var tween = Tween.UISizeDelta(maskRect, new Vector2(maxSize, maxSize), _transitionTime, Ease.InOutQuad)
            .Group(Tween.Color(image, _transitionColor, _transitionTime));

        if (_rotate)
            tween.Group(Tween.Rotation(maskRect,Vector3.zero, _transitionTime, Ease.InOutQuad));

        await tween;
    }

    public void Setup()
    {
        screenWidth = TransitionCanvas.rect.width;
        screenHeight = TransitionCanvas.rect.height;

        maxSize = Mathf.Max(screenWidth, screenHeight);
        maxSize += maxSize / 4;
    }
}
