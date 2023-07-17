using Infrastructure.Input;
using UnityEngine;
using UnityEngine.AI;

namespace GamePlay.Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement: MonoBehaviour
    {
        private IInputService _inputService;
        private IMoveAnimator _moveAnimator;
        private Vector3 _temp;
        private NavMeshAgent _nav;
        private float _playerSpeed;
        private float _rotationSpeed;

        public void Init(IInputService inputService, IMoveAnimator moveAnimator, float speedMove, float speedRotate)
        {
            _inputService=inputService;
            _moveAnimator = moveAnimator;
            _nav = GetComponent<NavMeshAgent>();
            _playerSpeed = speedMove;
            _rotationSpeed = speedRotate;
        }
        public void Move()
        {
            float inputHorizontal = _inputService.GetHorizontal;
            float inputVertical = _inputService.GetVertical;
       
            _temp.x = inputHorizontal;
            _temp.z = inputVertical;
           
            _moveAnimator.MoveAnimation(_temp.magnitude);

            _nav.Move(_temp * _playerSpeed * Time.deltaTime);
             
           // _rigidbody.transform.Translate(_temp * Time.deltaTime * _playerSpeed, Space.World);

            Rotation();
        }

        private void Rotation()
        {
            Vector3 tempDirect = transform.position + Vector3.Normalize(_temp);
            Vector3 lookDirection = tempDirect - transform.position;
            if (lookDirection != Vector3.zero)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                                                           Quaternion.LookRotation(lookDirection), _rotationSpeed * Time.deltaTime);
            }
        }

        public void FinishGame()
        {
            _playerSpeed = _rotationSpeed = 0;
        }
    }
}