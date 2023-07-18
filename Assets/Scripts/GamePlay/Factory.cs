using UnityEngine;

namespace GamePlay
{
    public class Factory: PullingSystem, IAddItems
    {
        private int _resource;
        
        public void AddItem()
        {
            _resource++;
            _canPush = true;
            _timer = 0;
            return;
        }
        protected override void SpawnItem()
        {
            if (_resource <= 0)
            {
                return;
            }
           
            for(int i=0;i<_resource;i++)
            {
                if (!_itemsList[_indexItem].gameObject.activeInHierarchy)
                {
                    Vector3 placePosition = new Vector3(_itemsPlaces[_indexPlace].position.x,
                                                        _itemsPlaces[_indexPlace].position.y + _yAxis,
                                                        _itemsPlaces[_indexPlace].position.z);
                    _itemsList[_indexItem].ShowItem(transform);
                    _itemsList[_indexItem].MoveToStackPlace(placePosition);
                    
                    _canPush = true;
                    _timer = 0;
                    _resource -= 1;
                    if (_indexItem < _itemsList.Count - 1)
                    {
                        _indexItem+=1;
                    }
                    if (_indexPlace < _itemsPlaces.Count-1)
                    {
                        _indexPlace+=1;
                    }
                    else
                    {
                        _indexPlace = 0;
                        _yAxis += _itemHeight;
                    }
                    break;
                }
            }
        }
       
        
    }
}