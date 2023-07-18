using System.Collections;
using System.Collections.Generic;
using Infrastructure.Input;
using UnityEngine;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAnimator))]
    [RequireComponent(typeof(PlayerMetalStack))]
    [RequireComponent(typeof(PlayerSwordStack))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Transform> _metalsPlaces;
        [SerializeField] private List<Transform> _swordsPlaces;
        private PlayerAnimator _playerAnimator;
        private PlayerMovement _move;
        private PlayerMetalStack _playerMetalStack;
        private PlayerSwordStack _playerSwordStack;

        private bool _isDead;
        public bool _stacking;
        private bool _metalStackFull;
        private bool _swordStackFull;

        private void FixedUpdate()
        {
            if (_isDead) return;
        
            _move.Move();
        }

        public void Init(IInputService input, 
                         float speedMove, 
                         float speedRotate, 
                         StackConfig metalStackConfig, 
                         StackConfig swordStackConfig)
        {
            _playerAnimator = GetComponent<PlayerAnimator>(); 
            _move = GetComponent<PlayerMovement>();
            _move.Init(input,_playerAnimator, speedMove, speedRotate);
            _playerMetalStack = GetComponent<PlayerMetalStack>();
            _playerMetalStack.Init(metalStackConfig, _metalsPlaces);
            _playerMetalStack.Full += () => { _metalStackFull = true; };
            _playerMetalStack.Empty += () => { _metalStackFull = false; };

            _playerSwordStack = GetComponent<PlayerSwordStack>();
            _playerSwordStack.Init(swordStackConfig, _swordsPlaces);
            _playerSwordStack.Full += () => { _swordStackFull = true; };
            _playerSwordStack.Empty += () => { _swordStackFull = false; };

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.TryGetComponent<Spawner>(out Spawner spawner))
            {
                TryTakeMetalItem(spawner);
                return;
            }

            if (other.gameObject.TryGetComponent<Factory>(out Factory factory))
            {
                PushMetalItems(factory);
                TryTakeSwordItem(factory);
                return;
            }

            if (other.gameObject.TryGetComponent(out Stock stock))
            {
                stock.ShowDisplay();
                _playerSwordStack.PushItemsToTarget(stock);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<Stock>(out Stock stoke))
            {
                stoke.HideDisplay();
            }
        }

        private void TryTakeSwordItem(Factory factory)
        {
            if(_swordStackFull) return;

            if (!_stacking)
            {
                StopCoroutine(StopStacking());
                factory.PushItemToPlayer(this);
                _stacking = true;
                StartCoroutine(StopStacking());
            }
        }

        private void PushMetalItems(Factory factory)
        {
            _playerMetalStack.PushItemsToTarget(factory);
            _playerAnimator.StackEmpty();
        }

        private void TryTakeMetalItem(Spawner spawner)
        {
            if(_metalStackFull) return;
            
            if (!_stacking)
            {
                StopCoroutine(StopStacking());
                spawner.PushItemToPlayer(this);
                _playerAnimator.HasStack();
                StartCoroutine(StopStacking());
            }
        }

        private IEnumerator StopStacking()
        {
            _stacking = true;
            yield return new WaitForSeconds(1.5f);
            _stacking = false;
        }

        public void AddItemInStack(ItemType type)
        { 
            
            _stacking = false;
            switch (type)
            {
                case ItemType.Metal:
                    _playerMetalStack.AddItem();
                    break;
                case ItemType.Sword: 
                    _playerSwordStack.AddItem();
                    break;
                default:
                    Debug.Log("I don't need type like this");
                    break;
            }
            _stacking = false;
        }
    }
}