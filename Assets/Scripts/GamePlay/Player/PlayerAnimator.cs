﻿using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerAnimator: MonoBehaviour, IMoveAnimator
    {
        private Animator _animator;
        private static readonly int IS_MOVE = Animator.StringToHash("IsMove");
        private static readonly int HAS_STACK = Animator.StringToHash("HasStack");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void MoveAnimation(float speed)
        {
            _animator.SetFloat(IS_MOVE,speed);  
        }

        public void HasStack()
        {
            _animator.SetBool(HAS_STACK, true);
        }
    
        public void StackEmpty()
        {
            _animator.SetBool(HAS_STACK, false);
        }
    }
}