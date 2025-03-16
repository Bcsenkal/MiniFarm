using System;
using UnityEngine;

namespace Utilities
{
    public class PlayerFollower : MonoBehaviour
    {
        [SerializeField] private bool smoothFollow;
        private Transform _playerTransform;

        
        [SerializeField] private float smoothTime;
        private Vector3 _velocity;
        private readonly float _maxSpeed=10f;
        
        
        void Start()
        {
            //************Must be under the same parent with the player**********\\
            _playerTransform = transform.parent.GetChild(0);
        }
        
        private void Update()
        {
            if (!smoothFollow) return;
            transform.position = Vector3.SmoothDamp(transform.position, _playerTransform.position, ref _velocity,
                smoothTime, _maxSpeed, Time.deltaTime);
        }

        private void LateUpdate()
        {
            if (smoothFollow) return;
            transform.position = _playerTransform.position;
        }
    }
}
