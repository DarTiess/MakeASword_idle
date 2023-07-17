using System;
using System.Collections.Generic;
using Infrastructure.Input;
using Infrastructure.Level;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Transform> _itemsPlaces;
        private PlayerAnimator _playerAnimator;
        private PlayerMovement _move;
        private PlayerStack _playerStack;

        private bool _isDead;
        private bool _stacking;
        private List<Item> _itemsList;
        [SerializeField] private float _itemHeight;
        private float _yAxis;
        [SerializeField] private int _maxCountItems;
        [SerializeField] private Item _itemPrefab;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpForce;
        [SerializeField] private ItemType _type;
        private int _indexItem=0;

        public void Init(IInputService input, float speedMove, float speedRotate)
        {
            _playerAnimator = GetComponent<PlayerAnimator>(); 
            _move = GetComponent<PlayerMovement>();
            _move.Init(input,_playerAnimator, speedMove, speedRotate);
          //  _playerStack = new PlayerStack(_itemsPlaces);
          //  _itemsList.Add(_itemsPlaces);
          CreateItemsInList();
        }

        private void CreateItemsInList()
        {
            _itemsList = new List<Item>(_maxCountItems);
            for (int i = 0; i < _maxCountItems; i++)
            {
                Item item = Instantiate(_itemPrefab, 
                                        _itemsPlaces[i].position,
                                        transform.rotation, transform);
                item.transform.localScale = new Vector3(1, 1, 1);
                item.Init(_jumpDuration,_jumpForce, _type);
                item.HideItem();
                _itemsList.Add(item);
            }
          
        }

        private void FixedUpdate()
        {
            if (_isDead) return;
        
            _move.Move();
        }

        public void AddItemInStack()
        { 
            _playerAnimator.HasStack();
            _itemsList[_indexItem].ShowItem();
            _indexItem++;
            _stacking = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent<Spawner>(out Spawner spawner))
            {
                if (!_stacking)
                {
                    _stacking = true;
                    spawner.PushItemToPlayer(this);
                    Debug.Log("Trigger Spawner");
                    
                }
            }
            
        }
    }
}