using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core.Health
{
    public class LifeHUD : MonoBehaviour
    {
        private SpriteMask _healthBar;
        private float _initialScale;

        void Awake()
        {
            _healthBar = GetComponentInChildren<SpriteMask>();
        }
        void Start()
        {
            _initialScale = _healthBar.transform.localScale.y;
        }

        public void ChangeSpriteFiller(float maxLifeReference, float modifierLifeValue)
        {
            if (_healthBar.transform.localScale.y == 0) return;
            var normalizedValue = NormalizeValue(0, maxLifeReference, 0, _initialScale, modifierLifeValue);
            _healthBar.transform.localScale =  new Vector3(_healthBar.transform.localScale.x, normalizedValue, _healthBar.transform.localScale.z);
        }

        private float NormalizeValue(float minMeasurementRangeValue, float maxMeasurementRangeValue, float minRangeTargetValue, float maxRangeTargetValue, float currentValue)
        {
           return (currentValue - minMeasurementRangeValue) / (maxMeasurementRangeValue - minMeasurementRangeValue) * (maxRangeTargetValue - minRangeTargetValue) + minRangeTargetValue;
        }
    }

}