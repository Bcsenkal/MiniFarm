using System;
using UnityEngine;

namespace Utilities
{
    [AddComponentMenu("Rocinante/Range Viewer")]
    public class RangeViewer : MonoBehaviour
    {
        [Range(0f,100f)]
        [SerializeField] private float range;

        [SerializeField] private Color sphereColor = Color.red;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = sphereColor;
            Gizmos.DrawSphere(transform.position,range);
        }
    }
}
