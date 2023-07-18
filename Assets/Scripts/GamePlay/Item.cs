using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class Item: MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        private float _jumpDuration;
        private float _jumpForce;

        public void Init(float duration,float jumpForce, ItemType type)
        {
            _jumpDuration = duration;
            _jumpForce = jumpForce;
            _type = type;
        }

        public void ShowItem(Transform startPosition)
        {
            transform.position = startPosition.position;
            gameObject.SetActive(true);
        }
       
        public void HideItem()
        {
            gameObject.SetActive(false);
        }

        public void MoveToStackPlace(Vector3 stackPosition)
        {
            transform.DOJump(stackPosition, _jumpForce, 1, _jumpDuration)
                     .SetEase(Ease.Linear);
        }

        public void MoveToPlayer(Player.Player player)
        {
            transform.DOKill();
            transform.DOJump(player.transform.position,_jumpForce, 1, _jumpDuration)
                     .OnComplete(() =>
                     {
                         HideItem();
                         player.AddItemInStack(_type);
                     });
        }

        public void MoveToTarget(Transform factory, IAddItems itemsStack)
        {
            transform.DOKill();
            transform.DOJump(factory.transform.position, _jumpForce, 1, _jumpDuration)
                     .OnComplete(() =>
                     {
                         HideItem();
                         itemsStack.AddItem();
                     });
        }
    }
}