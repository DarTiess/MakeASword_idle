using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Player
{
    public abstract class PlayerBaseStack: MonoBehaviour, IAddItems
    {
        protected List<Item> _itemsList;
        private List<Transform> _itemsPlaces;
        protected int _indexPlace;
        private int _maxCountItems;
        private Item _itemPrefab;
        private float _jumpDuration;
        private float _jumpForce;
        private ItemType _type;
        protected int _indexItem;

        public event Action Full;

        public void Init(StackConfig stackConfig, List<Transform> itemsPlaces)
        {
            _itemsList =new List<Item>(stackConfig.MaxCountItems);
            _maxCountItems = stackConfig.MaxCountItems;
            _itemPrefab = stackConfig.ItemPrefab;
            _itemsPlaces = itemsPlaces;
            _jumpDuration = stackConfig.JumpDuration;
            _jumpForce = stackConfig.JumpForce;
            _type = stackConfig.Type;
            _indexPlace = 0;
            
            CreateItemsList();
        }

        public void AddItem()
        {
            if (_indexItem < _maxCountItems)
            {
                _itemsList[_indexItem].ShowItem(_itemsPlaces[_indexPlace]);
                _indexItem++;
                _indexPlace++;
                if (_indexItem >= _maxCountItems)
                {
                    Full?.Invoke();
                }
            }
        }

        public abstract void PushItemsToTarget(IAddItems target);

        private void CreateItemsList()
        {
            for (int i = 0; i < _maxCountItems; i++)
            {
                Item item = Instantiate(_itemPrefab, 
                                        _itemsPlaces[i].position,
                                        transform.rotation, transform);
                SetScale(item);
                item.Init(_jumpDuration,_jumpForce, _type);
                item.HideItem();
                _itemsList.Add(item);
            }
          
        }

        protected abstract void SetScale(Item item);
    }
}