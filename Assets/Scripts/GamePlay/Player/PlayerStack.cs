using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerStack
    {
        private Queue<Item> _itemsList;
        private Transform _itemsPlaces;
        private int _indexPlace;

        public PlayerStack(Transform itemsPlaces)
        {
            _itemsList = new Queue<Item>(10);
            _itemsPlaces = itemsPlaces;
            _indexPlace = 0;
        }
        
        public void AddItem(Item item)
        {
            _itemsList.Enqueue(item);
            item.transform.position = _itemsPlaces.position;
            _itemsPlaces.position += new Vector3(0, 1f, 0);
        }
    }
}