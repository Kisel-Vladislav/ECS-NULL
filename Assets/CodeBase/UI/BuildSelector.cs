using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.StaticData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI
{
    public class BuildPanelContentController : MonoBehaviour
    {
        [SerializeField] private Transform _contentParent;

        private IStaticDataService _staticDataService;
        private BuildItemViewFactory _itemViewFactory;

        private List<BuildPanelItemView> _content = new();
        private BuildPanelItemView _previewedItem;

        public void Construct(BuildItemViewFactory itemViewFactory, IStaticDataService staticDataService)
        {
            _itemViewFactory = itemViewFactory;
            _staticDataService = staticDataService;
        }

        private void OnDestroy()
        {
            foreach (var buildItemView in _content)
                buildItemView.OnClick -= ChangeItem;
        }

        public void Fill(BuildGroupType buildGroupType)
        {
            var contentData = _staticDataService.ForBuilds(buildGroupType);
            foreach (var item in contentData)
            {
                var itemView = CreateAndSubscribeItemView(item);
                _content.Add(itemView);

                if (_previewedItem == null)
                    ChangeItem(itemView);
            }
        }
        private void SubscribeToItemView(BuildPanelItemView buildItemView) => 
            buildItemView.OnClick += ChangeItem;
        private BuildPanelItemView CreateAndSubscribeItemView(BuildPanelItemData item)
        {
            var itemView = _itemViewFactory.Create(item, _contentParent);
            SubscribeToItemView(itemView);
            return itemView;
        }
        private void ChangeItem(BuildPanelItemView view)
        {
            _previewedItem?.UnSelect();

            view.Select();
            _previewedItem = view;
        }

        internal void Construct(BuildItemViewFactory buildItemViewFactory, object staticDataService)
        {
            throw new NotImplementedException();
        }
    }

    public class BuildItemViewFactory
    {
        private readonly IAssetProvider _assetProvider;

        public BuildItemViewFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public BuildPanelItemView Create(BuildPanelItemData item, Transform parent)
        {
            var view = _assetProvider.Instance<BuildPanelItemView>(AssetsPath.BuildPanelItemView, parent);
            view.Initialize(item);
            return view;
        }
    }
}
