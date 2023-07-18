using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerMetalStack: PlayerBaseStack
    {
       
        protected override void SetScale(Item item)
        {
           item.transform.localScale = new Vector3(1, 1, 1);
        }

        public override void PushItemsToTarget(IAddItems target)
        {
            if (_indexItem>0)
            {
                Factory gObject = target as Factory;
                MoveItem(target, gObject.transform);
            }
        }
    }
}