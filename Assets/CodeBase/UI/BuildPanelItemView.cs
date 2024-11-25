using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.StaticData;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class BuildPanelItemView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _content;

        public BuildTypeId BuildId { get; private set; }

        public event Action<BuildPanelItemView> OnClick;

        public void OnPointerClick(PointerEventData eventData) =>
            OnClick?.Invoke(this);

        public void Initialize(BuildPanelItemData item)
        {
            BuildId = item.BuildTypeId;
            _content.sprite = item.Sprite;
        }
        public void Select()
        {
            _content.color = _content.color.WithAlpha(0.25f);
        }
        public void UnSelect()
        {
            _content.color = _content.color.WithAlpha(1);
        }

    }
}
