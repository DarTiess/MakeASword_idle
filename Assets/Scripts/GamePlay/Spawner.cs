using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class Spawner: MonoBehaviour
    {

        [SerializeField] private Transform[] _itemPlace;
        [SerializeField] private Item _itemPrefab;
        [SerializeField] private int _maxCountItems;
        
        [SerializeField] private ItemType _type;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _itemDeliveryTime;
        [SerializeField] private float _itemHeight;
        private float _yAxis;
        private int _countItems;
        private List<Item> _itemsList;

        private int _indexPlace = 0;
        private int _indexItem = 0;
        private bool _canPush;
        private float _timer=0;

        void Start()
        {
            CreateItemsList();
        }

        private void Update()
        {
            if (!_canPush)
            {
                return;
            }

            _timer += Time.deltaTime;
            
            if (_timer <= _itemDeliveryTime)
            {
                return;
            }
            _canPush = false;
            SpawnItem();
        }

        private void CreateItemsList()
        {
            _itemsList = new List<Item>(_maxCountItems);
            for (int i = 0; i < _maxCountItems; i++)
            {
                Item item = Instantiate(_itemPrefab, 
                                               transform.position,
                                              transform.rotation, transform);
                item.Init(_jumpDuration,_jumpForce, _type);
                item.HideItem();
                _itemsList.Add(item);
            }
          
            _canPush = true;
        }
        
        private void SpawnItem()
        {
           
            foreach (Item item in _itemsList)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    Vector3 placePosition = new Vector3(_itemPlace[_indexPlace].position.x,
                                                        _itemPlace[_indexPlace].position.y + _yAxis,
                                                        _itemPlace[_indexPlace].position.z);
                    item.ShowItem();
                    item.MoveToStackPlace(placePosition);
                    
                    _canPush = true;
                    _timer = 0;
                    
                    _indexItem+=1;
                    if (_indexPlace < _itemPlace.Length-1)
                    {
                        _indexPlace+=1;
                    }
                    else
                    {
                        _indexPlace = 0;
                        _yAxis += _itemHeight;
                    }
                    return;
                }
            }
           
        }
        
        public void PushItemToPlayer(Player.Player player)
        {
            if (_indexItem>0)
            {
                _itemsList[_indexItem-1].MoveToPlayer(player);
                if (_indexItem>0)
                {
                    _indexItem-=1;
                    _indexPlace -= 1;
                }
                   
            }
        }
    }
}