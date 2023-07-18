﻿using System.Collections.Generic;
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
           
            foreach (Item item in _itemsList)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    Vector3 placePosition = new Vector3(_itemsPlaces[_indexPlace].position.x,
                                                        _itemsPlaces[_indexPlace].position.y + _yAxis,
                                                        _itemsPlaces[_indexPlace].position.z);
                    item.ShowItem(transform);
                    item.MoveToStackPlace(placePosition);
                    
                    _canPush = true;
                    _timer = 0;

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

        public override void PushItemToPlayer(Player.Player player)
        {
            base.PushItemToPlayer(player);
            _canPush = true;
        }
    }
}