using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public abstract class PullingSystem: MonoBehaviour
    {
        [SerializeField] protected List<Transform> _itemsPlaces;
        [SerializeField] protected StackConfig _stackConfig;
        [SerializeField] protected float _itemDeliveryTime;
        [SerializeField] protected float _itemHeight;
        protected float _yAxis;
        protected List<Item> _itemsList;

        protected int _indexPlace=0;
        protected int _indexItem=0;
        protected bool _canPush;
        protected float _timer=0;

        private void Start()
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

        protected virtual void CreateItemsList()
        {
            _itemsList = new List<Item>(_stackConfig.MaxCountItems);
            for (int i = 0; i <_stackConfig. MaxCountItems; i++)
            {
                Item item = Instantiate(_stackConfig.ItemPrefab, 
                                        transform.position,
                                        transform.rotation, transform);
                item.Init(_stackConfig.JumpDuration,_stackConfig.JumpForce, _stackConfig.Type);
                item.HideItem();
                _itemsList.Add(item);
            }
          
        }

        protected abstract void SpawnItem();
        public virtual void PushItemToPlayer(Player.Player player)
        {
            if (_indexItem>0)
            {
                _itemsList[_indexItem-1].MoveToPlayer(player);
                if (_indexItem>0)
                {
                    _indexItem-=1;

                    ChangeFreePlace();
                }
               
            }
        }

        private void ChangeFreePlace()
        {
            if (_indexPlace > 0)
            {
                _indexPlace -= 1;
            }
            else
            {
                _indexPlace = _itemsPlaces.Count - 1;
                TryReduceRow();
            }
        }

        private void TryReduceRow()
        {
            if (_yAxis > 0)
            {
                _yAxis -= _itemHeight;
            }
        }
       

    }
}