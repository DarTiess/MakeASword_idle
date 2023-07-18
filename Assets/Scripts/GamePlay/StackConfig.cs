using UnityEngine;

namespace GamePlay
{
    [CreateAssetMenu(menuName = "Create StackConfig", fileName = "StackConfig", order = 0)]
    public class StackConfig : ScriptableObject
    {
        public int MaxCountItems;
        public Item ItemPrefab;
        public float JumpDuration;
        public float JumpForce;
        public ItemType Type;
    }
}