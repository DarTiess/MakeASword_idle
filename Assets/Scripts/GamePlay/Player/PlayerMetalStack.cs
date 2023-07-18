using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerMetalStack: PlayerBaseStack
    {
        public event Action Empty;
        protected override void SetScale(Item item)
        {
           item.transform.localScale = new Vector3(1, 1, 1);
        }

        public override void PushItemsToTarget(IAddItems target)
        {
            if (_indexItem>0)
            {
                Factory gObject = target as Factory;
                _itemsList[_indexItem-1].MoveToTarget(gObject.transform, target);
                if (_indexItem>0)
                {
                    _indexItem-=1;
                    _indexPlace -= 1;
                    if (_indexItem<=0)
                    {
                        Empty?.Invoke();
                    }
                }
            }
        }
    }
}