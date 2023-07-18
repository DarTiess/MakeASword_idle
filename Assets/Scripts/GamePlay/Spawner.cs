using UnityEngine;

namespace GamePlay
{
    public class Spawner: PullingSystem
    {
        protected override void CreateItemsList()
        {
            base.CreateItemsList();
            _canPush = true;
        }

        protected override void SpawnItem()
        {
            for (int i = 0; i < _itemsList.Count; i++)
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
               
                    ChangeItemIndex();
                    ChangePlaceIndex();
                    break;
                } 
            }
        }

        public override void PushItemToPlayer(Player.Player player)
        {
            base.PushItemToPlayer(player);
            _canPush = true;
            
        }

     
    }
}