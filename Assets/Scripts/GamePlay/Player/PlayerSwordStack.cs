using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerSwordStack: PlayerBaseStack
    {
        public event Action Empty;
        protected override void SetScale(Item item)
        {
            item.transform.localScale *=2;
        }

        public override void PushItemsToTarget(IAddItems target)
        {
            if (_indexItem>0)
            {
                Stock gObject = target as Stock;
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