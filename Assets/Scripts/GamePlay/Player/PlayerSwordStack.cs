using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerSwordStack: PlayerBaseStack
    {
        protected override void SetScale(Item item)
        {
            item.transform.localScale *=2;
        }

        public override void PushItemsToTarget(IAddItems target)
        {
            if (_indexItem>0)
            {
                Stock gObject = target as Stock;
                MoveItem(target, gObject.transform);
            }
        }
    }
}