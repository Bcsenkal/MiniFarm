using System;
using Managers;
using UnityEngine;

namespace Utilities
{
    
    [AddComponentMenu("Rocinante/Rotator")]
    public class Rotator : MonoBehaviour
    {
        private bool _canRotate;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Vector3 rotationVector;


        private void OnEnable()
        {
            _canRotate = true;
        }

        private void OnDisable()
        {
            _canRotate = false;
        }

        void Update()
        {
            if (!_canRotate) return;
            transform.Rotate(rotationVector*(rotationSpeed*Time.deltaTime),Space.Self);
        }
    }
}
