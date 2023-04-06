using UnityEngine;

namespace Utilities
{
    public class ColorProvider : MonoBehaviour
    {
        [Header("Color Settings")]
        [SerializeField] private float _hueMin;
        [SerializeField] private float _hueMax = 1;
        [SerializeField] private float _saturationMin = 1;
        [SerializeField] private float _saturationMax = 1;
        [SerializeField] private float _valueMin = 1;
        [SerializeField] private float _valueMax = 1;
        [SerializeField] private float _alphaMin = 0.6f;
        [SerializeField] private float _alphaMax = 0.6f;
    
        public Color GetRandomColor()
        {
            return Random.ColorHSV(_hueMin, _hueMax, _saturationMin, 
                _saturationMax, _valueMin, _valueMax, _alphaMin, _alphaMax);
        }
    }
}