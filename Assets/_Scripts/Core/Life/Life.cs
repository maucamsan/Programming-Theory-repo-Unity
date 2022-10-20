using UnityEngine;
using System;
using Core.Damage;
using Core.Scriptables;

namespace Core.Life
{
    public class Life : MonoBehaviour
    {
        public event Action OnDeath;
        // [SerializeField]  FloatVariable _maxBaseLife;
        FloatReference _statsValues;
        DamageBehavior _damageBehavior;
        float _maxBaseLife;
        float _currentBaseLife = 50;
        void Awake()
        {
            _damageBehavior = GetComponent<DamageBehavior>();
            _statsValues = GetComponent<FloatReference>();
            _maxBaseLife = _statsValues.floatVariableDict[(int) Variables.HEALTH].Value;
            _currentBaseLife = _maxBaseLife;
        }
        void OnEnable()
        {
            _damageBehavior.OnDamageTaken += ManageLife;
        }

        void OnDisable()
        {
            _damageBehavior.OnDamageTaken -= ManageLife;
        }
        
        
        private void ManageLife(float lifeAmount)
        {
            _currentBaseLife += lifeAmount;
            if (_currentBaseLife >= _maxBaseLife)
                _currentBaseLife = _maxBaseLife;
            if (_currentBaseLife <= 0 && gameObject.CompareTag("Player"))
            {
                _currentBaseLife = 0;
                OnDeath?.Invoke();
                // gameover
                // Display gameover UI and stats of the run
                // if first death, display rewarded ad to revive
                // Stop game -> notiy to gameover manager or class
                // Save data
            }
            else if (_currentBaseLife <= 0)
            {
                // Kill Enemy
                Debug.Log("Dead");
                _currentBaseLife = 0;
            }
        }
    }
}

