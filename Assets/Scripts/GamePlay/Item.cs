using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class Item: MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        private float _jumpDuration;
        private Transform _startPosition;
        private float _jumpForce;

        public void Init(float duration,float jumpForce, ItemType type)
        {
            _jumpDuration = duration;
            _jumpForce = jumpForce;
            _type = type;
            _startPosition = transform;
           
        }

        public void ShowItem()
        {
            gameObject.SetActive(true);
        }
       
        public void HideItem()
        {
            gameObject.SetActive(false);
        }

      

        public void MoveToStackPlace(Vector3 stackPosition)
        {
            transform.DOJump(stackPosition, _jumpForce, 1, _jumpDuration).SetEase(Ease.OutQuad);
        }

        public void MoveToPlayer(Player.Player player)
        {
            transform.DOKill();
            transform.DOJump(player.transform.position, 2f, 1, _jumpDuration)
                     .OnComplete(() =>
                     {
                        HideItem();
                        Debug.Log("Pushed Item");
                        transform.position = _startPosition.position; 
                        player.AddItemInStack();
                     });
        }
    }
}