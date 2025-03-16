using System;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Utilities
{
    public class ScaleUp : MonoBehaviour
    {
        
        [SerializeField] private Vector3 scaleUpScale = new Vector3(1f, 1f, 1f);
        [SerializeField] private float tweenDuration;
        private Vector3 _startScale;
        private MotionHandle _scaleUpTweenHandle;
        private void OnEnable()
        {
            _startScale = transform.localScale;
            ScaleUpTween();
        }


        private void ScaleUpTween()
        {
            _scaleUpTweenHandle = LMotion.Create(_startScale, scaleUpScale, tweenDuration)
                .WithEase(Ease.OutBack)
                .BindToLocalScale(transform);
        }
        
        private void OnDisable()
        {
            if (_scaleUpTweenHandle.IsActive())
            {
                _scaleUpTweenHandle.Cancel();
            }

            transform.localScale = _startScale;
        }
        
        

        
    }
}
