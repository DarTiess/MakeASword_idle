using System;
using Infrastructure.Level;
using UnityEngine;

namespace CamFollow {
    public class CamFollower : MonoBehaviour
    {
    
        [Space][Header("Vector")] 
        [SerializeField] private float _speedVector;
        [SerializeField] private float _vectorY = 10;
        [SerializeField] private float _vectorZ = 10;
        [SerializeField] private float _vectorX;
        [SerializeField] private bool _vectorXFrom0;
        [SerializeField] private bool _lookAtTarget;
        
        [Space][Space]  
        [SerializeField] private bool _dropTarget;
        [SerializeField] private ParticleSystem _particleWin;

        private Transform _target;
        private Vector3 _temp;

        private ILevelEvents _levelEvents;

        public void Init(ILevelEvents levelEvents, Transform player)
        {
            _levelEvents = levelEvents;
            _target = player;
            _levelEvents.OnLevelWin += OnLevelWin;
            _levelEvents.OnLevelLost += OnLevelLost;
        } 
    
        private void OnLevelWin()               
        {
            if (_particleWin){_particleWin.Play();}
        }
        private void OnLevelLost()
        {
            SetStop();
        }
        private void FixedUpdate()   
        {                 
            if (!_target) return;
       
           MoveVector(); 
        }

       private void MoveVector()
        {
            _temp = _target.position;
            _temp.y += _vectorY;
            _temp.z -= _vectorZ;
            _temp.x = _vectorXFrom0 ? _vectorX : _temp.x + _vectorX ;   
                                  
            transform.position = Vector3.Lerp(transform.position,_temp,_speedVector * Time.deltaTime);
            if (_lookAtTarget) transform.LookAt(_target.position); 
        }

       public void SetStop()
        {
            if(!_dropTarget) return;  
            _target = null;  
        }

    }
}